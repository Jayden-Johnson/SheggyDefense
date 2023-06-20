#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using MBS.Code.Builder.Scene;
using MBS.Model.AssetSystem;
using MBS.Model.Configuration;
using MBS.Utilities.Extensions;
using MBS.Utilities.Helpers;
using UnityEditor;
using UnityEngine;

namespace MBS.Controller.Scene.Mono
{
    [Serializable]
    public class WallSideModification
    {
        public float angle;
        public float positiveSide;
        public float negativeSide;
        public Vector3 abVector;

        public WallSideModification( float angle, float positiveSide, float negativeSide, Vector3 abVector )
        {
            this.angle = angle;
            this.positiveSide = positiveSide;
            this.negativeSide = negativeSide;
            this.abVector = abVector;
        }
    }


    [ExecuteInEditMode]
    public class MBSWallModule : EditorBehaviour
    {
        [SerializeField] internal AssetData data;

        [NonSerialized] private MBSConstruction _mbsConstruction;
        [NonSerialized] private ModularPack _origPack;
        [NonSerialized] private WallGroup _origGroup;
        [NonSerialized] private WallModule _origModule;

        [SerializeField] internal List<MBSWallModuleModifier> meshModifiers;

        [SerializeField] internal float additionalScale; // scale to replace 45 degree prefab if needed
        [SerializeField] internal float fitScale; // scale to fit between end points

        [SerializeField] internal Vector3 frontEndPointConstructorSpace;
        [SerializeField] internal Vector3 rearEndPointConstructorSpace;
        [SerializeField] internal Vector3 frontEndPointLocalSpace;
        [SerializeField] internal Vector3 rearEndPointLocalSpace;


        [SerializeField] internal List<MBSWallModule> frontConnections;
        [SerializeField] internal List<MBSWallModule> rearConnections;

        [SerializeField] internal WallSideModification frontModification;
        [SerializeField] internal WallSideModification rearModification;

        [SerializeField] internal MBSWallModule connectedToFront;
        [SerializeField] internal MBSWallModule connectedToRear;


        internal WallGroup OriginalGroup
        {
            get
            {
                if ( _origGroup != null ) return _origGroup;

                LoadAssetsByGUIDs( );
                return _origGroup;
            }
        }

        internal WallModule OriginalModule
        {
            get
            {
                if ( _origModule != null ) return _origModule;

                LoadAssetsByGUIDs( );
                return _origModule;
            }
        }


        internal Vector3 FrontEndPointWorldSpace => transform.TransformPoint( frontEndPointLocalSpace );
        internal Vector3 RearEndPointWorldSpace => transform.TransformPoint( rearEndPointLocalSpace );


        internal MBSConstruction MbsConstruction
        {
            get
            {
                if ( _mbsConstruction == null )
                    _mbsConstruction = transform.GetComponentInParent<MBSConstruction>( );

                return _mbsConstruction;
            }
        }


        private void OnEnable( )
        {
            if ( DragAndDrop.objectReferences.Length > 0 )
                return;

            if ( data == null )
                return;

            // check local variable because Construction property is used in other way
            if ( MbsConstruction != null && MbsConstruction.IsWallAlreadyAdded( this ) == false )
            {
                frontConnections = new List<MBSWallModule>( 8 );
                rearConnections = new List<MBSWallModule>( 8 );
                
                MbsConstruction.AddWallAndConnect( this );

                WallConnectionController.RecalculateConnectionNodes( this );

                MbsConstruction.AddAreasIfFound( this );
                MbsConstruction.UpdateEndPoints( );
                MbsConstruction.UpdateAreas( );
            }
        }

        private void OnDisable( )
        {
            if ( DragAndDrop.objectReferences.Length > 0 )
                return;

            if ( data == null )
                return;

            if ( MbsConstruction != null )
            {
                _mbsConstruction.RemoveWallAndDisconnect( this );
                _mbsConstruction.RemoveAreasWithWall( this );

                var wallToUpdate = new List<MBSWallModule>( );

                if ( frontConnections != null && frontConnections.Count > 0 )
                    wallToUpdate.Add( frontConnections.ElementAtOrDefault( 0 ) );

                if ( rearConnections != null && rearConnections.Count > 0 )
                    wallToUpdate.Add( rearConnections.ElementAtOrDefault( 0 ) );

                for ( var i = 0; i < wallToUpdate.Count; i++ )
                    if ( wallToUpdate[ i ] != null )
                        WallConnectionController.RecalculateConnectionNodes( wallToUpdate[ i ] );

                ClearConnections( );
            }
        }

        private void OnDrawGizmosSelected( )
        {
            // Handles.color = Color.red;
            // Handles.ArrowHandleCap( 0, transform.position, Quaternion.LookRotation( transform.right ),
            //                         data.actualSize.x / 2, EventType.Repaint );
            // Gizmos.DrawSphere( FrontEndPointWorldSpace, 0.1f );
            // Gizmos.DrawSphere( RearEndPointWorldSpace, 0.1f );
        }

        protected override void OnOriginalDestroy( )
        {
            if ( MbsConstruction != null )
            {
                _mbsConstruction.RemoveWallAndDisconnect( this );
                _mbsConstruction.RemoveAreasWithWall( this );
                _mbsConstruction.UpdateEndPoints( );
                ClearConnections( );
            }
        }

        protected override void OnExternalDestroy( )
        {
        }


        internal void WhenPlaced( MBSConstruction mbsConstruction, float additionalScale, float fitScale,
                                  ModularPack pack, WallGroup group, WallModule module, GameObject originalPrefab )
        {
            tag = PredefinedTags.EDITING_WALL;
            _mbsConstruction = mbsConstruction;

            _origPack = pack;
            _origGroup = group;
            _origModule = module;

            data = new AssetData( );
            data.packGuid = pack.Guid;
            data.groupGuid = group.Guid;
            data.moduleGuid = module.Guid;
            data.originalPrefab = originalPrefab;
            data.originalSize = GameObjectHelper.GetSize( originalPrefab );
            data.actualSize = data.originalSize;

            frontModification = new WallSideModification( 0, 0, 0, default );
            rearModification = new WallSideModification( 0, 0, 0, default );

            frontConnections = new List<MBSWallModule>( 8 );
            rearConnections = new List<MBSWallModule>( 8 );

            WhenPlaced_Children( );

            this.additionalScale = additionalScale;
            this.fitScale = fitScale;

            if ( additionalScale != 1 || fitScale != 1 )
            {
                data.actualSize.x = data.originalSize.x * additionalScale * fitScale;
                data.actualSize.y = data.originalSize.y;
                data.actualSize.z = data.originalSize.z;
            }

            UpdateEndPoints( );
        }


        private void WhenPlaced_Children( )
        {
            meshModifiers = new List<MBSWallModuleModifier>( );

            if ( transform.TryGetComponent( out MeshFilter meshFilter ) )
                if ( !transform.TryGetComponent( out MBSWallModuleModifier meshModifier ) )
                {
                    meshModifier = transform.gameObject.AddComponent<MBSWallModuleModifier>( );
                    meshModifier.WhenPlaced( this, meshFilter );

                    meshModifiers.Add( meshModifier );
                }

            transform.DoRecursive( t =>
            {
                if ( t.TryGetComponent( out MeshFilter meshFilter ) )
                    if ( !t.TryGetComponent( out MBSWallModuleModifier meshModifier ) )
                    {
                        meshModifier = t.gameObject.AddComponent<MBSWallModuleModifier>( );
                        meshModifier.WhenPlaced( this, meshFilter );

                        meshModifiers.Add( meshModifier );
                    }
            } );
        }


        private void LoadAssetsByGUIDs( )
        {
            _origPack = ModularPack_Manager.Singleton.ModularPacks
                                           .FirstOrDefault( i => i.Guid == data.packGuid );

            if ( _origPack == null )
            {
                Debug.LogError( Texts.Component.Wall.CANNOT_LOAD_PACK );
                return;
            }

            _origGroup = _origPack.WallGroups.FirstOrDefault( i => i.Guid == data.groupGuid );

            if ( _origGroup == null )
            {
                Debug.LogError( Texts.Component.Wall.CANNOT_LOAD_GROUP );
                return;
            }

            _origModule = _origGroup.Modules.FirstOrDefault( i => i.Guid == data.moduleGuid );

            if ( _origModule == null )
            {
                Debug.LogError( Texts.Component.Wall.CANNOT_LOAD_MODULE );
            }
        }


        private void UpdateEndPoints( )
        {
            transform.localScale = Vector3.one;

            var frontWorld = transform.position + data.actualSize.x / 2 * transform.right;
            frontEndPointLocalSpace = transform.InverseTransformPoint( frontWorld );
            frontEndPointConstructorSpace = MbsConstruction.transform.InverseTransformPoint( frontWorld );

            var rearWorld = transform.position - data.actualSize.x / 2 * transform.right;
            rearEndPointLocalSpace = transform.InverseTransformPoint( rearWorld );
            rearEndPointConstructorSpace = MbsConstruction.transform.InverseTransformPoint( rearWorld );
        }


        internal void ResetSideModifications( )
        {
            ResetSideModification( frontEndPointConstructorSpace );
            ResetSideModification( rearEndPointConstructorSpace );
        }

        internal void ResetSideModification( Vector3 connectionPointConstr )
        {
            if ( MbsConstruction == null )
                return;

            if ( connectionPointConstr.ApxEquals( frontEndPointConstructorSpace ) )
            {
                frontModification = new WallSideModification( 0, 0, 0, default );
            }
            else if ( connectionPointConstr.ApxEquals( rearEndPointConstructorSpace ) )
            {
                rearModification = new WallSideModification( 0, 0, 0, default );
            }
            else
            {
                Debug.LogErrorFormat( Texts.Component.Wall.WRONG_CONNECTION_POINT,
                                      connectionPointConstr, frontEndPointConstructorSpace,
                                      rearEndPointConstructorSpace );
            }
        }

        internal WallSideModification GetModificationAt( Vector3 connectionPointConstr )
        {
            if ( MbsConstruction == null )
                return null;

            if ( connectionPointConstr.ApxEquals( frontEndPointConstructorSpace ) ) return frontModification;

            if ( connectionPointConstr.ApxEquals( rearEndPointConstructorSpace ) )
            {
                return rearModification;
            }

            Debug.LogErrorFormat( Texts.Component.Wall.WRONG_CONNECTION_POINT,
                                  connectionPointConstr, frontEndPointConstructorSpace,
                                  rearEndPointConstructorSpace );
            return null;
        }

        internal void AddFrontConnection( MBSWallModule mbsWallModule )
        {
            if ( mbsWallModule == null || mbsWallModule == this ) return;

            frontConnections.Add( mbsWallModule );
        }

        internal void AddRearConnection( MBSWallModule mbsWallModule )
        {
            if ( mbsWallModule == null || mbsWallModule == this ) return;

            rearConnections.Add( mbsWallModule );
        }


        internal void RemoveConnection( MBSWallModule mbsWallModule )
        {
            frontConnections.Remove( mbsWallModule );
            rearConnections.Remove( mbsWallModule );
        }

        internal void ClearConnections( )
        {
            frontConnections = new List<MBSWallModule>( 8 );
            rearConnections = new List<MBSWallModule>( 8 );
        }


        internal MBSWallModule ChangeModule( WallModule newModule, bool multipleSelection )
        {
            if ( MbsConstruction == null )
            {
                Debug.LogError( Texts.Component.Wall.CONSTRUCTION_MISSING );
                return null;
            }

            var chosen = WallPrefabContoller.ChangeModule_ChosePrefab(
                OriginalModule,
                newModule,
                data.originalPrefab,
                additionalScale );

            var chosenPrefab = chosen.chosenPrefab;
            
            //name
            var newName = chosenPrefab.name;
            if ( transform.parent != null )
            {
                var existedNames = transform.parent.GetComponentsInChildren<Transform>( )
                                            .Select( i => i.gameObject.name );
                existedNames = existedNames.Where( i => i != gameObject.name );
                newName = ObjectNames.GetUniqueName( existedNames.ToArray( ), chosenPrefab.name );
            }


            var changedPrefab = (GameObject)PrefabUtility.InstantiatePrefab( chosenPrefab );
            changedPrefab.transform.SetParent( transform.parent );
            changedPrefab.transform.localPosition = transform.localPosition;
            changedPrefab.transform.localRotation = transform.localRotation;

            var totalScale = chosen.chosenPrefab.transform.localScale;
            totalScale = totalScale.MultiplyByVector3_XXYYZZ( chosen.additionalScale );
            totalScale = totalScale.MultiplyByVector3_XXYYZZ( new Vector3( fitScale, 1, 1 ) );
            changedPrefab.transform.localScale = totalScale;

            Undo.IncrementCurrentGroup( );
            Undo.SetCurrentGroupName( Texts.Component.Wall.MODULE_CHANGED_UNDO_NAME );
            changedPrefab.RecordCreatedUndo( );

            var changedWall = changedPrefab.AddComponent<MBSWallModule>( );
            changedWall.tag = tag;
            changedWall.name = newName;

            changedWall.WhenPlaced( MbsConstruction, chosen.additionalScale.x, fitScale, _origPack, _origGroup, newModule,
                                    chosenPrefab );

            if ( multipleSelection )
            {
                MbsConstruction.AddWallAndConnect( changedWall );
                MbsConstruction.AddAreasIfFound( changedWall );
                changedPrefab.AddToSelection( );
                gameObject.DestroyImmediateUndo( );
            }
            else
            {
                _mbsConstruction.RemoveWallAndDisconnect( this );
                _mbsConstruction.RemoveAreasWithWall( this );
                _mbsConstruction.UpdateEndPoints( );

                MbsConstruction.AddWallAndConnect( changedWall );

                MbsConstruction.AddAreasIfFound( changedWall );
                MbsConstruction.UpdateAreas( );

                changedPrefab.AddToSelection( );

                gameObject.DestroyImmediateUndo( );
            }

            return changedWall;
        }


        internal void LockConnectionWith( MBSWallModule mbsWallModule )
        {
            if ( frontConnections.Contains( mbsWallModule ) )
            {
                if ( connectedToFront != null )
                {
                    connectedToFront.UnlockConnectionWith( this );
                    connectedToFront = mbsWallModule;
                }
                else
                {
                    connectedToFront = mbsWallModule;
                }
            }
            else if ( rearConnections.Contains( mbsWallModule ) )
            {
                if ( connectedToRear != null )
                {
                    connectedToRear.UnlockConnectionWith( this );
                    connectedToRear = mbsWallModule;
                }
                else
                {
                    connectedToRear = mbsWallModule;
                }
            }
        }

        internal void UnlockConnectionWith( MBSWallModule mbsWallModule )
        {
            if ( connectedToFront == mbsWallModule )
                connectedToFront = null;
            else if ( connectedToRear == mbsWallModule )
                connectedToRear = null;
        }

        internal void ResetLockedConnections( )
        {
            ResetFrontLockedConnection( );
            ResetRearLockedConnection( );
            WallConnectionController.RecalculateConnectionNodes( this );
        }

        internal void ResetFrontLockedConnection( )
        {
            if ( connectedToFront != null )
            {
                if ( connectedToFront.connectedToFront == this )
                {
                    connectedToFront.connectedToFront = null;
                    connectedToFront = null;
                }
                else if ( connectedToFront.connectedToRear == this )
                {
                    connectedToFront.connectedToRear = null;
                    connectedToFront = null;
                }

                WallConnectionController.RecalculateConnectionNodes( this );
            }
        }

        internal void ResetRearLockedConnection( )
        {
            if ( connectedToRear != null )
            {
                if ( connectedToRear.connectedToFront == this )
                {
                    connectedToRear.connectedToFront = null;
                    connectedToRear = null;
                }
                else if ( connectedToRear.connectedToRear == this )
                {
                    connectedToRear.connectedToRear = null;
                    connectedToRear = null;
                }

                WallConnectionController.RecalculateConnectionNodes( this );
            }
        }


        internal void FlipFace( )
        {
            MbsConstruction.RemoveWallAndDisconnect( this );
            ClearConnections( );

            transform.Rotate( 0, 180, 0 );

            UpdateEndPoints( );
            MbsConstruction.AddWallAndConnect( this );

            ( connectedToFront, connectedToRear ) = ( connectedToRear, connectedToFront );

            WallConnectionController.RecalculateConnectionNodes( this );
        }

        internal void FaceWallInsideArea( RoomArea newArea )
        {
            var checkDistance = 0.2f;
            var ofFacePos = transform.position - transform.forward * checkDistance;
            var area2 = newArea.IsPointInsideArea( MbsConstruction.transform.InverseTransformPoint( ofFacePos ) );

            if ( area2 )
            {
                //this.transform.RecordObjectUndo( );
                transform.Rotate( 0, 180, 0 );
                UpdateEndPoints( );
            }
        }

        internal bool IsFacesInsideArea( RoomArea area )
        {
            var checkDistance = 0.2f;
            var ofFacePos = transform.position - transform.forward * checkDistance;
            var isRearInsideArea = area.IsPointInsideArea( MbsConstruction.transform.InverseTransformPoint( ofFacePos ) );
            return !isRearInsideArea;
        }

        internal bool IsThereLockedConnectionAt( Vector3 connectionPointConstr )
        {
            if ( connectionPointConstr.ApxEquals( frontEndPointConstructorSpace ) )
                return connectedToFront != null;

            if ( connectionPointConstr.ApxEquals( rearEndPointConstructorSpace ) )
                return connectedToRear != null;

            var frontDist = ( connectionPointConstr - frontEndPointConstructorSpace ).sqrMagnitude;
            var rearDist = ( connectionPointConstr - rearEndPointConstructorSpace ).sqrMagnitude;

            if ( frontDist < rearDist )
                return connectedToFront != null;
            if ( rearDist < frontDist )
                return connectedToRear != null;

            return false;
        }

        [Serializable]
        internal class AssetData
        {
            [SerializeField] internal string packGuid;
            [SerializeField] internal string groupGuid;
            [SerializeField] internal string moduleGuid;
            [SerializeField] internal GameObject originalPrefab;
            [SerializeField] internal Vector3 originalSize;
            [SerializeField] internal Vector3 actualSize;
        }
    }
}
#endif