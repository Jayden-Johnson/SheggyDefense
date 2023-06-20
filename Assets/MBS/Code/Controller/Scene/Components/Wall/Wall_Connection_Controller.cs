#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using MBS.Controller.Scene.Mono;
using MBS.Utilities.Extensions;
using UnityEngine;

namespace MBS.Controller.Scene
{
    public static class WallConnectionController
    {
        internal static void RecalculateConnectionNodes( MBSWallModule mbsWallModule, bool doUpdateMesh = true )
        {
            List<MBSWallModule> frontSideConnectionNode = new List<MBSWallModule>( );
            List<MBSWallModule> rearSideConnectionNode = new List<MBSWallModule>( );

            var frontList = mbsWallModule.frontConnections;
            var rearList = mbsWallModule.rearConnections;

            if ( mbsWallModule.connectedToFront != null )
            {
                frontSideConnectionNode.Add( mbsWallModule );
                frontSideConnectionNode.AddRange( frontList );
                RecalculateConnectionsNode( mbsWallModule.FrontEndPointWorldSpace,
                                            mbsWallModule.frontEndPointConstructorSpace,
                                            frontSideConnectionNode,
                                            mbsWallModule.connectedToFront, doUpdateMesh );
            }
            else
            {
                var frontLocked = CheckLockedConnections( frontList, mbsWallModule );

                if ( frontLocked.first != null && frontLocked.second != null )
                {
                    RecalculateConnectionNodes( frontLocked.first );
                }
                else
                {
                    frontSideConnectionNode.Add( mbsWallModule );
                    frontSideConnectionNode.AddRange( frontList );
                    RecalculateConnectionsNode( mbsWallModule.FrontEndPointWorldSpace,
                                                mbsWallModule.frontEndPointConstructorSpace,
                                                frontSideConnectionNode,
                                                mbsWallModule.connectedToFront, doUpdateMesh );
                }
            }


            if ( mbsWallModule.connectedToRear != null )
            {
                rearSideConnectionNode.Add( mbsWallModule );
                rearSideConnectionNode.AddRange( rearList );
                RecalculateConnectionsNode( mbsWallModule.RearEndPointWorldSpace,
                                            mbsWallModule.rearEndPointConstructorSpace,
                                            rearSideConnectionNode,
                                            mbsWallModule.connectedToRear, doUpdateMesh );
            }
            else
            {
                var rearLocked = CheckLockedConnections( rearList, mbsWallModule );

                if ( rearLocked.first != null && rearLocked.second != null )
                {
                    RecalculateConnectionNodes( rearLocked.first );
                }
                else
                {
                    rearSideConnectionNode.Add( mbsWallModule );
                    rearSideConnectionNode.AddRange( rearList );
                    RecalculateConnectionsNode( mbsWallModule.RearEndPointWorldSpace,
                                                mbsWallModule.rearEndPointConstructorSpace,
                                                rearSideConnectionNode,
                                                mbsWallModule.connectedToRear, doUpdateMesh );
                }
            }
        }

        private static (MBSWallModule first, MBSWallModule second) CheckLockedConnections(
            List<MBSWallModule> list, MBSWallModule mbsWallModule )
        {
            //bool doHaveLockedConnection = false;
            //int lockedConnections = 0;

            for ( var i = 0; i < list.Count; i++ )
            {
                var item = list[ i ];
                
                if ( item.frontConnections.Contains( mbsWallModule ) )
                {
                    if ( item.connectedToFront != null )
                        if ( item.connectedToFront.connectedToFront == item ||
                             item.connectedToFront.connectedToRear == item )
                            return ( item, item.connectedToFront );
                }
                else if ( item.rearConnections.Contains( mbsWallModule ) )
                {
                    if ( item.connectedToRear != null )
                        if ( item.connectedToRear.connectedToFront == item ||
                             item.connectedToRear.connectedToRear == item )
                            return ( item, item.connectedToFront );
                }
            }

            return ( null, null );
        }

        private static void RecalculateConnectionsNode( Vector3 connectionPointWorld, Vector3 connectionPointConstr,
                                                        List<MBSWallModule> connectedWalls,
                                                        MBSWallModule lockedMbsWallModule, bool doUpdateMesh )
        {
            // start side
            // 0 - center
            // -1 - left hand side
            // +1 - rigth hand side
            float startSide = 0;

            // do the rest walls belond to the one side 
            var restWallsBelongToOneSide = false;


            var firstWallIndex = -1;
            MBSWallModule firstMbsWallModule = null;

            var secondWallIndex = -1;
            MBSWallModule secondMbsWallModule = null;

            var maxAngle = float.MinValue;

            // if there is a lockedWall
            // set it as a second item
            // and the initial wall as first item
            // skip finding the first wall
            if ( lockedMbsWallModule != null )
            {
                firstWallIndex = 0;
                firstMbsWallModule = connectedWalls.ElementAt( 0 );

                secondWallIndex = connectedWalls.IndexOf( lockedMbsWallModule );
                secondMbsWallModule = lockedMbsWallModule;
                restWallsBelongToOneSide = true;
            }
            else
                // Find the wall item
                // For which all conection will be on one side
            {
                for ( var i = 0; i < connectedWalls.Count; i++ )
                {
                    var nextIndex = ( i + 1 ) % connectedWalls.Count;

                    if ( nextIndex == i || connectedWalls.Count < nextIndex )
                        continue;

                    var currentWall = connectedWalls[ i ];
                    var nextWall = connectedWalls[ nextIndex ];

                    var crossProduct = GetCrossProductRegardToLine( connectionPointWorld,
                                                                    currentWall.transform.position,
                                                                    nextWall.transform.position ).RoundDecimals( );
                    startSide = crossProduct;

                    // checking all connections to see if they are on one side or not
                    restWallsBelongToOneSide = true;
                    for ( var j = 0; j < connectedWalls.Count; j++ )
                    {
                        if ( i == j || i == nextIndex )
                            continue;

                        crossProduct = GetCrossProductRegardToLine( connectionPointWorld,
                                                                    currentWall.transform.position,
                                                                    connectedWalls[ j ].transform.position )
                            .RoundDecimals( );

                        restWallsBelongToOneSide &= ( crossProduct >= 0 && startSide >= 0 ) ||
                                                    ( crossProduct <= 0 && startSide <= 0 );
                    }

                    // if they are on one side then start item have found
                    if ( restWallsBelongToOneSide )
                    {
                        firstWallIndex = i;
                        firstMbsWallModule = connectedWalls[ firstWallIndex ];
                        break;
                    }
                }
            }

            // All walls in one side
            if ( restWallsBelongToOneSide )
            {
                // if there is no locked wall
                // find a second wall
                if ( secondMbsWallModule == null )
                    // Here we will find the second wall
                    for ( var i = 0; i < connectedWalls.Count; i++ )
                    {
                        if ( firstWallIndex == i || connectedWalls[ i ] == firstMbsWallModule )
                            continue;

                        var sideA = connectedWalls[ firstWallIndex ].transform.position - connectionPointWorld;
                        var sideB = connectedWalls[ i ].transform.position - connectionPointWorld;
                        var currentAngle = Vector3.Angle( sideA, sideB ).RoundDecimals( );

                        //if ( currentAngle.ApxEquals( 180 ) )
                        //    currentAngle = 0;
                        if ( currentAngle > maxAngle )
                        {
                            maxAngle = currentAngle;
                            secondMbsWallModule = connectedWalls[ i ];
                            secondWallIndex = i;
                        }
                    }

                // recalculating side
                // updating meshes
                if ( secondMbsWallModule != null )
                {
                    if ( maxAngle != 180 && maxAngle != 0 )
                    {
                        for ( var i = 0; i < connectedWalls.Count; i++ )
                            if ( i != secondWallIndex && i != firstWallIndex )
                            {
                                if ( connectedWalls[ i ].IsThereLockedConnectionAt( connectionPointConstr ) )
                                    continue;

                                connectedWalls[ i ].ResetSideModification( connectionPointConstr );

                                if ( doUpdateMesh )
                                    UpdateMesh( connectedWalls[ i ] );
                            }

                        UpdateSides( firstMbsWallModule, secondMbsWallModule, connectionPointConstr, maxAngle );
                        UpdateSides( secondMbsWallModule, firstMbsWallModule, connectionPointConstr, maxAngle );

                        if ( doUpdateMesh )
                        {
                            UpdateMesh( firstMbsWallModule );
                            UpdateMesh( secondMbsWallModule );
                        }
                    }
                    else
                    {
                        /// Wrong angle
                        for ( var i = 0; i < connectedWalls.Count; i++ )
                        {
                            connectedWalls[ i ].ResetSideModification( connectionPointConstr );

                            if ( doUpdateMesh )
                                UpdateMesh( connectedWalls[ i ] );
                        }
                    }
                }
            }
            else
            {
                ///Not all walls in one side, reset
                for ( var i = 0; i < connectedWalls.Count; i++ )
                {
                    connectedWalls[ i ].ResetSideModification( connectionPointConstr );

                    if ( doUpdateMesh )
                        UpdateMesh( connectedWalls[ i ] );
                }
            }
        }


        private static void UpdateSides( MBSWallModule currentMbsWallModule, MBSWallModule connectedMbsWallModule,
                                         Vector3 connectionPointConstr,
                                         float angle )
        {
            //to doc: all children object should be with 0 rotation
            //or each vertecies should be calculated relative the parent object, not mesh origin
            var modification = currentMbsWallModule.GetModificationAt( connectionPointConstr );

            if ( modification == null )
                return;

            var position = currentMbsWallModule.transform.position;
            var direction = connectedMbsWallModule.transform.position - position;
            direction /= 2;


            var midPoint = position + direction;


            var dist = ( position - currentMbsWallModule.transform.forward / 2 - midPoint ).sqrMagnitude;
            var dist1 = ( position + currentMbsWallModule.transform.forward / 2 - midPoint ).sqrMagnitude;

            if ( dist1 < dist )
            {
                modification.positiveSide = -1;
                modification.negativeSide = +1;
            }
            else if ( dist1 > dist )
            {
                modification.positiveSide = +1;
                modification.negativeSide = -1;
            }

            modification.angle = angle;


            var pointO = currentMbsWallModule.MbsConstruction.transform.TransformPoint( connectionPointConstr );
            var vectorOa = currentMbsWallModule.transform.position - pointO;
            var vectorOb = connectedMbsWallModule.transform.position - pointO;
            var bPointWorldEqualized = pointO + vectorOb.normalized * vectorOa.magnitude;
            var aPointWorld = pointO + vectorOa;
            var vectorBa = bPointWorldEqualized - aPointWorld;

            modification.abVector = vectorBa.normalized;
        }

        public static void UpdateMesh( MBSWallModule mbsWallModule )
        {
            for ( var i = 0; i < mbsWallModule.meshModifiers.Count; i++ )
            {
                var modifier = mbsWallModule.meshModifiers[ i ];

                if ( !modifier.doModify )
                {
                    modifier.SetupMesh( );
                }
                else
                {
                    var modifiedMesh = Wall_MeshModifier.ModifyMesh( mbsWallModule, modifier );
                    modifier.SetupMesh( modifiedMesh );
                }

                mbsWallModule.transform.localScale = Vector3.one;
            }
        }

        private static float GetCrossProductRegardToLine( Vector3 startLine, Vector3 endLine, Vector3 point )
        {
            var retval = Vector3.Cross( startLine - point, endLine - point ).y;
            return retval;
        }
    }
}
#endif