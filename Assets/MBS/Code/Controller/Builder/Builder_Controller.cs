#if UNITY_EDITOR

using System.Linq;
using MBS.Builder.Scene;
using MBS.Controller.Scene;
using MBS.Model.AssetSystem;
using MBS.Model.Configuration;
using MBS.Model.Scene;
using MBS.Utilities.Extensions;
using MBS.Utilities.Helpers;
using MBS.View.Builder;
using MBS.View.Input;
using MBS.View.Input.Physical;
using UnityEditor;
using UnityEngine;

namespace MBS.Controller.Builder
{
    internal static class Builder_Controller
    {
        private static Object[ ] _objectsToSelect;

        internal static void Launch( MBSConstruction mbsConstruction = null )
        {
            // construction object initialization
            mbsConstruction ??= CreateNewConstruction( );
            MBSConstruction.Current = mbsConstruction;
            mbsConstruction.gameObject.SelectObject( );

            // input and data controllers initialization
            PathsManager.Start( );
            InputManager.Start( );
            BuilderDataController.Initialize( );

            // grid initialization
            MbsGrid.Transform = MBSConstruction.Current.transform;
            MbsGrid.LevelHeight = BuilderDataController.ToolData.GridLevelHeight;
            MbsGrid.LevelNumber = BuilderDataController.ToolData.GridLevelNumber;
            MbsGrid.CellSize = BuilderDataController.ToolData.GridCellSize;
            MbsGrid.Size = 50;
            MbsGrid.Start( );


            // scene data initialization
            SceneData.DoSnapToGrid = true;
            SceneData.DoSnapToEnds = true;

            // launch and initialize builder window
            var window = EditorWindow.GetWindow<Builder_Window>( );
            window.Launch( BuilderDataController.Tool,
                           BuilderDataController.AllModularPacks.Select( i => i.Name ).ToList( ) );
        }

        internal static void Stop( )
        {
            InputManager.Stop( );
            BuilderDataController.SaveBuilderData( );

            SceneData.Clear( );
            BuildingProcedures_Walls.Clear( );
            BuildingProcedures_Floors.Clear( );
            BuildingProcedures_Decorator.Clear( );

            MbsGrid.Clear( );
            //BuilderData_Controller.ClearData( );
            //BuilderData_Controller.ClearCallbacks( );

            if ( EditorWindow.HasOpenInstances<Builder_Window>( ) )
                EditorWindow.GetWindow<Builder_Window>( ).Stop( );

            MBSConstruction.Current = null;
        }


        internal static void DuringSceneGUI( SceneView sceneView )
        {
            if ( MBSConstruction.Current == null )
            {
                CurrentConstructionMissing( );
                return;
            }

            InputManager.DuringSceneGUI( sceneView );
            Builder_Scene.SceneGUI( sceneView );
            InputManager.ExecuteIMGUI( Event.current );
        }

        internal static void CurrentConstructionMissing( )
        {
        }

        internal static void ChangeTool( ToolbarTool tool )
        {
            Picker_Mode.Clear( );
            Pointer_Mode.Clear( );
            Builder_Window.Instance.SetGridPickerToggleValue( false );
            Builder_Window.Instance.SetLevelHeightPickerToggleValue( false );
            Builder_Window.Instance.SetLevelHeightPointerToggleValue( false );


            BuildingProcedures_Walls.Clear( );
            BuildingProcedures_Floors.Clear( );
            BuildingProcedures_Decorator.Clear( );

            BuilderDataController.ClearCallbacks( );
            InputManager.ClearInputData( );

            // Set new
            switch ( tool )
            {
                case ToolbarTool.WallTool:
                    InputWalls_Tool.Setup_IdleInputs( );
                    BuilderDataController.AddModuleChangeCallback( ( m, s ) =>
                    {
                        Gizmos_Controller.SetupMeshDrawFunction( );
                        Gizmos_Controller.SetWallData( s.y );
                    } );

                    break;
                case ToolbarTool.FloorTool:
                    Input_Floors_Tool.Setup_IdleInputs( );
                    BuilderDataController.AddModuleChangeCallback( ( m, s ) =>
                    {
                        var floorModule = m as FloorModule;

                        Gizmos_Controller.SetupMeshDrawFunction( );
                        Gizmos_Controller.SetupMesh( floorModule.GetPrefabCombinedMeshAt( 0 ) );

                        AssetsData.Floor.SquarePrefab = floorModule.SquarePrefab;
                        AssetsData.Floor.TriangularPrefab = floorModule.TriangularPrefab;
                        AssetsData.Floor.IsTriangularExist = floorModule.TriangularPrefab != null;
                        AssetsData.Floor.SquareMesh = floorModule.GetPrefabCombinedMeshAt( 0 );
                        AssetsData.Floor.TriangularMesh = floorModule.GetPrefabCombinedMeshAt( 1 );
                    } );
                    break;
                case ToolbarTool.PlacerTool:
                    BuilderDataController.AddPackChangeCallback( ( p ) => { BuildingProcedures_Decorator.Clear( ); } );

                    BuilderDataController.AddGroupChangeCallback( ( g, s ) => { BuildingProcedures_Decorator.Clear( ); } );

                    BuilderDataController.AddModuleChangeCallback( ( m, s ) =>
                    {
                        BuildingProcedures_Decorator.InitializeAsset( );
                    } );

                    BuildingProcedures_Decorator.Start( );
                    break;
            }

            BuilderDataController.SelectTool( tool );

            var toolData = BuilderDataController.ToolData;
            MbsGrid.CellSize = toolData.GridCellSize;
            MbsGrid.Size = 50;

            EditorWindow.GetWindow<Builder_Window>( ).SetupToolData( tool, toolData );
        }

        internal static void Refresh( )
        {
            if ( EditorWindow.HasOpenInstances<Builder_Window>( ) && MBSConstruction.Current != null )
                Launch( MBSConstruction.Current );
        }


        internal static void SelectPackAt( int index )
        {
            BuilderDataController.SelectModularPackAt( index );
        }


        internal static void SetGridCellSize( float newValue )
        {
            BuilderDataController.ToolData.GridCellSize = newValue;
            MbsGrid.CellSize = newValue;
        }

        internal static void ChangeGridCellSize( float newValue )
        {
            Builder_Window.Instance.SetGridSizeLinkToggleValue( false );
            Builder_Window.Instance.SetGridSizeValue( newValue );

            SetGridCellSize( newValue );
        }

        internal static float SetLockSizeToggle( bool newValue )
        {
            if ( BuilderDataController.SelectedModule == null )
            {
                Builder_Window.Instance.SetGridSizeLinkToggleValue( false );
                return 0;
            }

            BuilderDataController.ToolData.GridCellSize_LinkToggle = newValue;

            if ( newValue )
            {
                var moduleSize = BuilderDataController.SelectedModule.GetSize( ).x;
                BuilderDataController.ToolData.GridCellSize = moduleSize;
                MbsGrid.CellSize = moduleSize;
                return moduleSize;
            }

            return 0;
        }


        internal static void ChangeLevelHeight( float newValue )
        {
            Builder_Window.Instance.SetLevelHeightValue( newValue );
            SetGridLevelHeight( newValue );
        }

        internal static void SetGridLevelHeight( float newValue )
        {
            BuilderDataController.ToolData.GridLevelHeight = newValue;
            MbsGrid.LevelHeight = newValue;
        }


        internal static void SetGridLevelNumber( int newValue )
        {
            BuilderDataController.ToolData.GridLevelNumber = newValue;
            MbsGrid.LevelNumber = newValue;
        }

        internal static float ShowSelectionGrid( int width, int heigh )
        {
            float newGroupSize = -1;

            SelectionGrid.GroupsSelectionGrid( width, heigh,
                                               BuilderDataController.SelectedGroupIndex,
                                               BuilderDataController.SelectedModularPackGroups,
                                               BuilderDataController.ToolData.SelectionGrid_ScrollPos, index =>
                                               {
                                                   _ = BuilderDataController.SelectAssetGroupAt( index );

                                                   if ( BuilderDataController.ToolData.GridCellSize_LinkToggle )
                                                       if ( BuilderDataController.SelectedGroup != null )
                                                           newGroupSize = BuilderDataController.SelectedGroup.GetSize( )
                                                               .x;
                                               },
                                               newScrollPos =>
                                               {
                                                   BuilderDataController.ToolData.SelectionGrid_ScrollPos =
                                                       newScrollPos;
                                               } );

            return newGroupSize;
        }

        internal static void ChangeSceneMode( SceneMode sceneMode, ChangingGridParam gridParam = default )
        {
            Picker_Mode.Clear( );
            Pointer_Mode.Clear( );

            Builder_Window.Instance.SetGridPickerToggleValue( false );
            Builder_Window.Instance.SetLevelHeightPickerToggleValue( false );
            Builder_Window.Instance.SetLevelHeightPointerToggleValue( false );

            switch ( sceneMode )
            {
                case SceneMode.Builder:
                    ChangeTool( BuilderDataController.Tool );
                    break;

                case SceneMode.Picker:
                    Gizmos_Controller.SetupPickerDrawFunction( );

                    switch ( gridParam )
                    {
                        case ChangingGridParam.GridCellSize:
                            Builder_Window.Instance.SetGridPickerToggleValue( true );
                            Picker_Mode.SetParameter( LeadAxis.XAxis );
                            Picker_Mode.Setup_Inputs( ChangeGridCellSize );
                            break;
                        case ChangingGridParam.LevelHeight:
                            Builder_Window.Instance.SetLevelHeightPickerToggleValue( true );
                            Picker_Mode.SetParameter( LeadAxis.YAxis );
                            Picker_Mode.Setup_Inputs( ChangeLevelHeight );
                            break;
                    }

                    break;

                case SceneMode.Pointer:
                    Builder_Window.Instance.SetLevelHeightPointerToggleValue( true );
                    Gizmos_Controller.SetupPointerDrawFunction( );
                    Pointer_Mode.Setup_Inputs( );

                    break;
            }

            SceneData.SceneMode = sceneMode;
            SceneData.BuilderMode = BuilderMode.Idle;
        }


        private static MBSConstruction CreateNewConstruction( )
        {
            GameObject gameobject = new GameObject( GameObjectHelper.GetUniqueName( "MBS Construction" ) );
            gameobject.transform.position = Vector3.zero;

            var construction = gameobject.AddComponent<MBSConstruction>( );
            construction.Initialize( );
            return construction;
        }
    }
}

#endif