#if UNITY_EDITOR

namespace MBS.Model.Configuration
{
    public static class PredefinedNames
    {
        // Folders
        public const string MBS_FOLDER = "MBS";
        public const string MODULAR_PACKS_FOLDER = "ModularPacks";
        public const string HIDDEN_PACKS_FOLDER = "__HiddenPacks__";
        public const string CODE_FOLDER = "Code";
        public const string TEMP_DATA_FOLDER = "TempData";
        public const string INTERNAL_DATA_FOLDER = "InternalData";

        public const string COMBINER_MODULAR_PACK_GUID = "CombinedModularPackGUID";

        public const string DESCRIPTOR_NAME = "mbs-pack-descriptor";
        public const string DESCRIPTOR_FILE_NAME = "mbs-pack-descriptor.asset";
    }

    public static class PredefinedPaths
    {
        // ----------- Folders
        public static readonly string MODULAR_PACKS_PATH = "/" + PredefinedNames.MODULAR_PACKS_FOLDER;
        public static readonly string HIDDEN_PACKS_PATH = "/" + PredefinedNames.HIDDEN_PACKS_FOLDER;
        public static readonly string CODE_PATH = "/" + PredefinedNames.CODE_FOLDER;
        public static readonly string TEMP_DATA_PATH = "/" + PredefinedNames.TEMP_DATA_FOLDER;
        public static readonly string INTERNAL_DATA_PATH = "/" + PredefinedNames.INTERNAL_DATA_FOLDER;

        // ----------- private shortens
        private const string UIToolkit = "/UIToolkit";

        // ----------- Modular Pack Manager
        public const string MANAGER_WINDOW_PATH = UIToolkit + "/PackManager/PackManager_Window.uxml";

        public const string WALL_GROUP_EDITOR_WINDOW_PATH = UIToolkit + "/PackManager/WallGroupEditor_Window.uxml";
        public const string WALL_MODULE_EDITOR_WINDOW_PATH = UIToolkit + "/PackManager/WallModuleEditor_Window.uxml";

        public const string FLOOR_GROUP_EDITOR_WINDOW_PATH = UIToolkit + "/PackManager/FloorGroupEditor_Window.uxml";
        public const string FLOOR_MODULE_EDITOR_WINDOW_PATH = UIToolkit + "/PackManager/FloorModuleEditor_Window.uxml";

        public const string PLACER_GROUP_EDITOR_WINDOW_PATH = UIToolkit + "/PackManager/PlacerGroupEditor_Window.uxml";

        public const string PLACER_MODULE_EDITOR_WINDOW_PATH =
            UIToolkit + "/PackManager/PlacerModuleEditor_Window.uxml";

        public const string ASSET_CATEGORIES_EDITOR_WINDOW_PATH =
            UIToolkit + "/PackManager/AssetCategoriesEditor_Window.uxml";

        public const string LIST_ITEM_ICON_AND_LABEL = UIToolkit + "/PackManager/ListItem_IconAndLabel.uxml";

        public const string DESCRIPTOR_SUFFIX_PATH = "/" + PredefinedNames.DESCRIPTOR_FILE_NAME;


        // ----------- Builder
        public const string BUILDER_WINDOW_PATH = UIToolkit + "/Builder/Builder_Window.uxml";
        public const string BUILDER_WINDOW_EMPTY_PATH = UIToolkit + "/Builder/Builder_Window_Empty.uxml";


        // ----------- Construction
        public const string CONSTRUCTION_INSPECTOR_PATH = UIToolkit + "/Construction/Construction.uxml";

        public const string CONSTRUCTION_INSPECTOR_LIST_ITEM_PATH =
            UIToolkit + "/Construction/Construction_ListView_Item.uxml";

        // ----------- Helpbar
        public const string HELPBAR_STYLE_SHEET_PATH = UIToolkit + "/Helpbar/Uss/Helpbar_Styles.uss";


        // ----------- Temp Data
        public const string BUILDER_DATA_PATH = "/BuilderData.asset";
        public const string INPUT_CONFIG_ASSET_PATH = "/InputConfig.asset";


        // ----------- Gizmos, Materials
        public const string ARROW_GIZMO_MESH_PATH = "/Meshes/MBSGizmo.fbx";
        public const string CHECKBOARD_MATERIAL_PATH = "/Materials/MBSGeneretedMeshCheckboard.mat";

        // ----------- Icons
        public const string ICON_EMPTY_PATH = "/Icons/icon_empty.png";
    }
}
#endif