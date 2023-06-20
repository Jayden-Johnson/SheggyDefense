#if UNITY_EDITOR

using MBS.Model.Configuration;

namespace MBS.Controller.Configuration
{
    internal static class PathController
    {
        #region Data

        internal static string GetPATH_BuilderDataAsset( )
        {
            return PathsManager.Singleton.TempDataPath + PredefinedPaths.BUILDER_DATA_PATH;
        }

        #endregion

        #region Asset Importer UI

        internal static string GetPATH_ModularPackManagerWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.MANAGER_WINDOW_PATH;
        }


        internal static string GetPATH_WallGroupEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.WALL_GROUP_EDITOR_WINDOW_PATH;
        }

        internal static string GetPATH_WallModuleEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.WALL_MODULE_EDITOR_WINDOW_PATH;
        }


        internal static string GetPATH_FloorGroupEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.FLOOR_GROUP_EDITOR_WINDOW_PATH;
        }

        internal static string GetPATH_FloorModuleEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.FLOOR_MODULE_EDITOR_WINDOW_PATH;
        }


        internal static string GetPATH_PlacerGroupEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.PLACER_GROUP_EDITOR_WINDOW_PATH;
        }

        internal static string GetPATH_PlacerModuleEditorWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.PLACER_MODULE_EDITOR_WINDOW_PATH;
        }


        internal static string GetPATH_AssetCategoriesWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.ASSET_CATEGORIES_EDITOR_WINDOW_PATH;
        }


        internal static string GetPATH_ListItemWithIcon( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.LIST_ITEM_ICON_AND_LABEL;
        }

        #endregion

        #region OtherUI

        internal static string GetPATH_SceneHelpbarStylesheet( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.HELPBAR_STYLE_SHEET_PATH;
        }

        internal static string GetPATH_AssetEmptyIconPreview( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.ICON_EMPTY_PATH;
        }

        internal static string GetPATH_BuilderWindow( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.BUILDER_WINDOW_PATH;
        }

        internal static string GetPATH_BuilderEmpty( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.BUILDER_WINDOW_EMPTY_PATH;
        }

        #endregion

        #region Editors

        internal static string GetPATH_ConstructionInspector( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.CONSTRUCTION_INSPECTOR_PATH;
        }
        
        internal static string GetPATH_ConstructionInspectorListViewItem( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.CONSTRUCTION_INSPECTOR_LIST_ITEM_PATH;
        }

        #endregion

        #region Config Paths

        internal static string GetPATH_InputConfigAsset( )
        {
            return PathsManager.Singleton.TempDataPath + PredefinedPaths.INPUT_CONFIG_ASSET_PATH;
        }

        internal static string GetPATH_CheckboardMaterial( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.CHECKBOARD_MATERIAL_PATH;
        }

        internal static string GetPATH_WallGizmoMesh( )
        {
            return PathsManager.Singleton.InternalDataPath + PredefinedPaths.ARROW_GIZMO_MESH_PATH;
        }

        #endregion
    }
}

#endif