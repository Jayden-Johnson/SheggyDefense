#if UNITY_EDITOR

namespace MBS.Model.Configuration
{
    public static class Texts
    {
        private const string IF_YOU_SEE_CONTACT_DEVELOPER = "If you see this, please inform the developer about this problem: roman.indiedev@gmail.com";

        private const string CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT =
            "If you didn't try to change MBS internal code, but the problem happens \n, " +
            "please contact the developer: roman.indiedev@gmail.com";

        internal static class MenuItems
        {
            internal const string BUILDER = "Tools/MBS/Builder";
            internal const string MODULAR_PACK_MANAGER = "Tools/MBS/Modular Pack Manager";
        }

        internal static class Builder
        {
            internal static class Window
            {
                internal const string WINDOW_NAME = "MBS Builder";

                internal static class Errors
                {
                    internal static readonly string GRID_SIZE_FIELD_MISSING =
                        $"{WINDOW_NAME}.  Grid Size float field is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string GRID_SIZE_PICKET_TOGGLE_MISSING =
                        $"{WINDOW_NAME}.  Grid Size picker toggle button is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string GRID_SIZE_LINK_TOGGLE_MISSING =
                        $"{WINDOW_NAME}.  Grid Size Link Toggle button is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string LEVEL_HEIGHT_FIELD_MISSING =
                        $"{WINDOW_NAME}.  Level height float field is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string LEVEL_HEIGHT_PICKER_TOGGLE =
                        $"{WINDOW_NAME}.  Level height picker toggle button is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string LEVEL_HEIGHT_POINTER_TOGGLE =
                        $"{WINDOW_NAME}.  Level height pointer toggle button is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string LEVEL_NUMBER_INT_FIELD =
                        $"{WINDOW_NAME}.  Level number int field is missing, cannot set the value." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal static readonly string SELECTION_GRID_NO_GROUPS = "There are no groups to show";

                    internal static readonly string SELECTION_GRID_NO_MODULES = "There are no modules to show";
                }
            }

            internal static class Data
            {
                private const string NAME = "MBS. Builder Data Controller. ";

                internal const string CANNOT_LOAD_PACK =
                    NAME + "Cannot load modular pack. \n";

                internal const string CANNOT_LOAD_GROUP =
                    NAME + "Cannot load modular group, \n" +
                    "selected modular pack has no modular groups. \n";

                internal const string CANNOT_LOAD_MODULE =
                    NAME + "Cannot load module at index 0. \n" +
                    "You are probably forgot to add modules to selected modular group. \n" +
                    "Please add at least one module. \n";
                
                internal const string CANNOT_SELECT_MODULAR_PACK_AT =
                    NAME + "Cannot select modular pack at index {0}";
            }
        }

        internal static class Building
        {
            internal const string CANNOT_START_DRAWING_MODULE =
                "MBS. Cannot start drawing. Module is missing. \n" +
                "Please add at least one module to the group or select another group. \n";

            internal const string CANNOT_START_DRAWING_PREFAB =
                "MBS. Cannot start drawing. The default prefab of the selected module is missing. \n" +
                "Please add the default prefab to the module. \n";

            internal static class WallMeshModifier
            {
                private const string NAME = "MBS. Wall Mesh Modifier. ";

                internal const string ORIGINAL_MESH_MISSING =
                    NAME +
                    "Cannot load original mesh of MBSWallModule. Cannot continue. \n " +
                    "It may be caused by the removal/reimporting of the original mesh(.fbx) asset. \n"
                  + CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                internal const string ORIGINAL_MESH_VERTICES_EMPTY =
                    NAME + "Original mesh is corrupted and has no vertices. \n"
                         + CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;
            }

            internal static class Wall
            {
                internal const string DRAWING_UNDO_NAME = "MBS. Wall Drawing";
            }

            internal static class LoopFinder
            {
                internal const string INFINITE_LOOP =
                    "MBS. Mesh Creator. Unexpected behavior. Infinite loop. " + IF_YOU_SEE_CONTACT_DEVELOPER;
            }

            internal static class Geometry
            {
                private const string NAME = "MBS. Geometry. ";

                internal const string AREA_HAS_NO_PATH_POINTS =
                    NAME + "Area is corrupted and has no path points. Cannot continue. \n" +
                    IF_YOU_SEE_CONTACT_DEVELOPER;

                internal const string AREA_HAS_NO_PATH_TRIANGLES =
                    NAME + "Area is corrupted and has no triangles. Cannot continue. \n" +
                    IF_YOU_SEE_CONTACT_DEVELOPER;

                internal const string AREA_HAS_NO_EXTREME_POINTS =
                    NAME + "Area is corrupted and has no extreme points. Cannot continue. \n" +
                    IF_YOU_SEE_CONTACT_DEVELOPER;

                internal const string TRIANGLES_HAS_LESS_3_POINTS =
                    NAME + "Triangle has less than 3 points. Cannot continue. \n" + IF_YOU_SEE_CONTACT_DEVELOPER;

                internal const string AREA_WALL_LESS_THAN_3 =
                    NAME + "Area walls number is less than 3, wrong input data." + IF_YOU_SEE_CONTACT_DEVELOPER;
            }

            internal static class Gizmos
            {
                internal const string WALL_BUILDING_GIZMO_MISSING =
                    "MBS. Unexpected behaviour. Cannot get wall gizmo mesh. \n" +
                    "The file deleted or file name is changed. \n " +
                    "If you didn't touch inner MBS files then please contact the developer.";
            }
        }


        internal static class Component
        {
            internal static class Editor
            {
                internal const string UNDO_WALL_MODULE_CHANGED = "MBS. Wall module(s) changed";
                internal const string UNDO_FLOOR_MODULE_CHANGED = "MBS. Floor module(s) changed";
                internal const string UNDO_FLOOR_TYPE_CHANGED = "MBS. Floor Tile Type Changed";
                internal const string UNDO_FLOOR_TILE_SPLIT = "MBS. Floor Tile Split";
                internal const string UNDO_DECOR_MODULE_CHANGED = "MBS. Decor module(s) changed";

                internal const string SINGLE_GROUP_MISSING =
                    "Error. Original modular group is missing. It may be caused by modular pack or group removal.";

                internal const string SOME_GROUP_MISSING =
                    "Error. Some original modular groups are missing. It may be caused by modular pack/group removal.";

                internal const string DIFFERENT_GROUPS =
                    "Selected modules are from different modular groups.";

                internal const string GROUP_EMPTY =
                    "Error. Original modular group is empty. It may be caused by modules removal.";

                internal const string MODULE_MISSING =
                    "Error. Original module is missing. It may be caused by module/group/pack removal.";

                internal const string CANNOT_GET_PARENT_MODULE_COMPONENT =
                    "Error. Parent Module component is missing. " +
                    "It may be caused by component removal or changing the parent for this gameobject.";

                internal const string PARENT_CONSTRUCTION_MISSING =
                    "Error. Parent Construction component is missing. " +
                    "It may be caused by parent gameobject/component removal or by changing the parent for this gameobject.";
            }

            internal static class Wall
            {
                private const string NAME = "MBS. MBSWallModule. ";

                internal const string CANNOT_LOAD_PACK =
                    NAME +
                    "Cannot initialize asset data, modular pack with given GUID not found. \n" +
                    "It may be caused by the removal of the original modular pack." +
                    CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                internal const string CANNOT_LOAD_GROUP =
                    NAME +
                    "Cannot initialize asset data, modular group with given GUID not found. \n" +
                    "It may be caused by the removal of the original group." +
                    CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                internal const string CANNOT_LOAD_MODULE =
                    NAME +
                    ".Cannot initialize asset data, module with given GUID not found \n" +
                    "It may be caused by the removal of the original module." +
                    CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                internal const string WRONG_CONNECTION_POINT =
                    NAME +
                    "Connection point is not equal nor front or rear end point of the wall. \n " +
                    "Connection Point: {0}. Front Point: {1}. Rear Point: {2}." +
                    CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                internal const string CONSTRUCTION_MISSING =
                    NAME + "Cannot change the module, parent construction object is missing. \n" +
                    "It may be caused by removal of the Construction parent gameobject or component \n" +
                    "or by changing the parent of this wall module.";

                internal const string MODULE_CHANGED_UNDO_NAME = "MBS. Wall module changed";

                internal const string PREFAB_CONTROLLER_PREV_PREFAB_NOT_MATCH =
                    "MBS. Wall Prefab Controller. Unexpected behavior. \n" +
                    "Previous prefab is not equal to any of prefabs defined in previous module. Cannot continue." +
                    CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;
            }
        }

        internal static class AssetSystem
        {
            internal const string WINDOW_NAME = "Modular Pack Manager";
            internal const string CONTEXT_MENU_REFRESH = "Refresh";
            internal const string CONTEXT_MENU_SHOW_HIDDEN = "Show hidden packs";

            internal static readonly string UNSAFE_ACTION_CURRENT_DATA_MISSING =
                $"MBS. {WINDOW_NAME}. Button action is unsafe: current pack data is missing. " +
                IF_YOU_SEE_CONTACT_DEVELOPER;

            internal static readonly string UNSAFE_ACTION_IMPORTER_DATA_COUNT =
                $"MBS. {WINDOW_NAME}. Button action is unsafe: importer data number is zero." +
                IF_YOU_SEE_CONTACT_DEVELOPER;

            internal static readonly string UNSAFE_ACTION_PACK_NAMES_COUNT =
                $"MBS. {WINDOW_NAME}. Button action is unsafe: modular packs names number is zero." +
                IF_YOU_SEE_CONTACT_DEVELOPER;

            internal static readonly string UNSAFE_ACTION_PACK_SELECTOR_INDEX_LESS_ZERO =
                $"MBS. {WINDOW_NAME}. Button action is unsafe: modular pack selector index is less than zero." +
                IF_YOU_SEE_CONTACT_DEVELOPER;


            internal static class Creation
            {
                internal const string NEW_PACK = "New Modular Pack";

                internal const string NEW_WALL_GROUP = "New Wall Group";
                internal const string NEW_FLOOR_GROUP = "New Floor Group";
                internal const string NEW_DECOR_GROUP = "New Decor Group";

                internal const string NEW_WALL_MODULE = "New Wall Module";
                internal const string NEW_FLOOR_MODULE = "New Floor Module";
                internal const string NEW_DECOR_MODULE = "New Decor Module";
            }

            internal static class ModularPack
            {
                // 0 - window name
                // 1 - index
                internal static readonly string PACK_INDEX_LESS_ZERO =
                    "MBS. {0}. Incorrect modular pack index: {1}." + IF_YOU_SEE_CONTACT_DEVELOPER;


                internal static class Manager
                {
                    private const string NAME = "MBS. Modular Pack Manager. ";

                    internal const string FILE_SAVE_PROBLEM =
                        NAME +
                        "A problem occurred while saving the file." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal const string FILE_REMOVING_PROBLEM =
                        NAME +
                        "A problem occurred while removing the modular pack." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal const string MODULAR_PACK_NOT_FOUND =
                        NAME +
                        "A problem occurred while removing the modular pack." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal const string CANNOT_HIDE_PACK =
                        NAME +
                        "Cannot hide (or unhide) pack. Pack with given GUID not found." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;
                }

                internal static class Importer
                {
                    private const string NAME = "MBS. Modular Pack Importer. ";

                    internal const string PARSE_FAILED_JSON_EMPTY =
                        NAME +
                        "TextAsset at \"{0}\" is empty. \n" +
                        CONTACT_DEVELOPER_IF_YOU_ARE_DOING_RIGHT;

                    internal const string GUID_IS_EQUAL =
                        NAME +
                        "Manual action required. Following modular packs have equal GUID: \n" +
                        "\"{0}\" and \n " +
                        "\"{1}\", \n " +
                        "Please change the GUID of one of the packs above via editing \"" +
                        PredefinedNames.DESCRIPTOR_FILE_NAME + "\" file.";

                    internal const string NAME_IS_EQUAL =
                        NAME +
                        "Manual action required. Following modular packs have equal names: \n" +
                        "\"{0}\" and  \"{1}\", \n " +
                        "Please rename one of those modular packs.";
                }

                internal static class Saver
                {
                    private const string NAME = "MBS. Modular Pack Saver";

                    internal const string CANNOT_CREATE_FOLDER =
                        NAME +
                        "Cannot save the modular pack, failed to create folder at \n " +
                        "\"{0}\"." +
                        IF_YOU_SEE_CONTACT_DEVELOPER;

                    internal const string FOLDER_ALREADY_EXIST =
                        NAME +
                        "Cannot save modular pack descriptor file in folder at \"{0}\", the folder already exist.";


                    // 0 - initial name
                    // 1 - changed name
                    internal static string packNameHasChanged =
                        NAME +
                        "The name of the saved modular pack ( {0} ) has changed to ( {1} ), \n" +
                        "because modular pack or folder with the name ( {0} ) already exist, ";
                }

                internal static class Remover
                {
                    private const string NAME = "MBS. Modular Pack Remover. ";

                    internal const string CANNOT_REMOVE_FOLDER =
                        NAME + "Unity internal error. Folder at path: \"{0}\" cannot be removed.";
                }

                internal static class Visibility
                {
                    private const string NAME = "MBS. Modular Pack Visibility Controller. ";

                    internal const string CANNOT_FIND_FOLDER =
                        NAME + "Cannot find folder at path \"{0}\"";

                    internal const string CANNOT_MOVE_FOLDER =
                        NAME + "Cannot move folder from \n " +
                        "\"{0}\" to \n" +
                        "\"{1}\"";
                }
            }


            internal static class Group
            {
                internal const string WALL_GROUP_EDITOR_WINDOW_NAME = "Wall Group Editor";
                internal const string FLOOR_GROUP_EDITOR_WINDOW_NAME = "Floor Group Editor";
                internal const string DECOR_GROUP_EDITOR_WINDOW_NAME = "Decor Group Editor";

                // 0 - window name (ex. Wall Module Window)
                // 1 - group name
                internal const string MODULES_LIST_EMPTY =
                    "MBS. {0}. On Save Validation ( {1} ). The modules list is empty. \n" +
                    "Please add modules to the group or remove the group if you \n" + "don't intend to use it.";
            }


            internal static class Module
            {
                internal const string WALL_MODULE_EDITOR_WINDOW_NAME = "Wall Module Editor";
                internal const string FLOOR_MODULE_EDITOR_WINDOW_NAME = "Floor Module Editor";
                internal const string DECOR_MODULE_EDITOR_WINDOW_NAME = "Decor Module Editor";


                internal const string WALL_INFO =
                    "MBS can stretch and shrink wall prefabs to fill gaps between walls in the scene.\n\n" +
                    "In order to make the stretch and shrink effect less noticeable to the eye (in cases where walls use textures), MBS uses two prefabs for one wall module.\n\n" +
                    "MBS uses the <b>Default Prefab</b> in cases where the wall does not need to be stretched, and also in cases where the wall needs to be shrunk (when the space to be filled is less than the length of the <b>Default Prefab</b>).\n\n" +
                    "<b>Expanded Prefab</b> is used when the wall is placed diagonally on the grid cell, i.e. at 45/135 degrees (in a snap to grid mode), and also when shrinking the <b>Expanded Prefab</b> will be less noticeable than stretching the <b>Default Prefab</b> too much.";

                internal const string FLOOR_INFO =
                    "To create floors from tiles, MBS uses two types of prefabs: square and triangular.\n\n" +
                    "<b>Square Prefab</b> is the default, but if the MBS detects an obstacle (eg a wall that is at a 45 degree angle), " +
                    "then the MBS uses a <b>Triangular Prefab</b> so that the floor tiles do not go beyond the boundaries of the obstacle.\n\n" +
                    "If the <b>Triangular Prefab</b> is not selected, then MBS will only use the <b>Square Prefab</b>.";

                internal const string DECORATOR_INFO = "";

                // 0 - window name
                // 1 - module name
                // 2 - filed name (label)
                internal const string OBJECT_NOT_PREFAB = "MBS: {0}. Value Change Validation ( {1} ). \n" +
                                                          "The selected object of the {2} field is not a prefab asset. \n" +
                                                          "Please select a prefab asset. Make sure you are selecting a prefab asset,\n" +
                                                          "not an instance of prefab placed in Scene.";

                // 0 - window name
                // 1 - module name
                // 2 - field name
                internal const string FIELD_VALUE_NULL = "MBS. {0}. On Save Validation ( {1} ). \n" +
                                                         "The value of the {2} field is null. MBS will not be able to use this module. \n" +
                                                         "Please set the not-null value or remove the module if you don't intend to use it.";
            }


            internal static class Text
            {
                // 0 - textfield name (label)
                internal const string VALIDATION_LENGTH_LESS_THAN2 =
                    "MBS. Text validation error: The {0} \n" +
                    "cannot be less than 2 characters. Please use 2 or more characters.";

                // 0 - textfield name (label)
                internal const string VALIDATION_WRONG_CHARACTERS =
                    "MBS. Text validation error: The {0} \n" +
                    "can only contain letters, numbers, whitespaces, underscore and dot sign. \n" +
                    "Please remove inappropriate characters.";
            }
        }

        internal static class Configuration
        {
            internal const string CANNOT_ADD_TAGS =
                "MBS. Unexpected error. Cannot add required tags, the TagManager.asset file not found.";

            internal static class PathsManager
            {
                // 0 - folder name
                internal const string CANT_FIND_FOLDER =
                    "MBS. Fatal error. Path Manager: Unable to find folder {0}, cannot continue.";

                // 0 - folder name
                internal const string CANT_CREATE_FOLDER =
                    "MBS. Fatal error. Path Manager: Unable to find and create folder {0}, cannot continue.";
            }
        }

        internal static class Inputs
        {
            internal const string SNAP_TO_GRID = "Snap to grid";
            internal const string SNAP_TO_ENDS = "Snap to ends";
            internal const string MOVE_GRID_ORIGIN = "Move grid origin";

            internal const string CANCEL_ACTION = "Cancel";

            internal static class Wall
            {
                internal const string IDLE_MOUSE_ACTION_LABEL = "Start Drawing";
                internal const string DRAWING_MOUSE_ACTION_LABEL = "End Drawing";
                internal const string SNAP_ACCURACY_LABEL = "Angle Snap Accuracy";
                internal const string INCREASE_LEVEL_NUMBER = "Increase";
                internal const string DECREASE_LEVEL_NUMBER = "Decrease";
                internal const string LEVEL_NUMBER = "Levels number";
            }

            internal static class Floor
            {
                internal const string IDLE_MOUSE_ACTION_LABEL = "Start Drawing";
                internal const string DRAWING_MOUSE_ACTION_LABEL = "End Drawing";
                internal const string AUTOFILL_LABEL = "Autofill area";
            }

            internal static class Decorator
            {
                // internal const string 
                internal static class PlacementMode
                {
                    internal const string MOUSE_ACTION_LABEL = "Place object";
                    internal const string CANCEL_ACTION_LABEL = "Cancel";

                    internal const string PLACEMENT_TYPE = "Placement Type";
                    internal const string PLACEMENT_FACING = "Placement Facing";
                    internal const string COLLISION = "Collision";

                    internal const string RANDOM_MODULE = "Randomize Next Module";
                    internal const string NEXT_MODULE = "Next module";

                    internal const string ADJUST_HEIGHT = "Adjust height";
                    internal const string ADJUST_HEIGHT_SPACE = "Space";
                    internal const string ADJUST_HEIGHT_HEIGHT = "Height";

                    internal const string ROTATION_MODE = "Rotation Mode";
                    internal const string SCALING_MODE = "Scaling Mode";
                    internal const string SNAP_TO_GRID = "Snap To Grid";
                }

                internal static class RotationMode
                {
                    internal const string MOUSE_ACTION_LABEL = "Accept";
                    internal const string CANCEL_ACTION_LABEL = "Cancel";
                    internal const string RESET_ROTATION_LABEL = "Reset";
                    internal const string X_AXIS_LABEL = "X Axis";
                    internal const string Y_AXIS_LABEL = "Y Axis";
                    internal const string Z_AXIS_LABEL = "Z Axis";
                    internal const string ROTATE_AROUND = "Rotate around";
                    internal const string ROTATION_SPACE_LABEL = "Space";
                    internal const string SNAP_ANGLE_LABEL = "Snap angle";
                    internal const string SNAP_ACCURACY_LABEL = "Snap accuracy";
                }

                internal static class ScalingMode
                {
                    internal const string MOUSE_ACTION_LABEL = "Accept";
                    internal const string CANCEL_ACTION_LABEL = "Cancel";
                    internal const string RESET_SCALE_LABEL = "Reset";
                    internal const string SCALE_ALONG_LABEL = "Scale along";
                    internal const string ALL_AXIS_LABEL = "All";

                    internal const string X_AXIS_LABEL = "X Axis";
                    internal const string Y_AXIS_LABEL = "Y Axis";
                    internal const string Z_AXIS_LABEL = "Z Axis";
                    internal const string SNAP_ACCURACY_LABEL = "Snap accuracy";
                }
            }

            internal static class Picker
            {
                internal const string MOUSE_ACTION_LABEL = "Set value";
                internal const string BOUNDS_TYPE_LABEL = "Bounds type";
                internal const string AXIS_PARAMETER_LABEL = "Get value of";
            }

            internal static class Pointer
            {
                public const string MOUSE_ACTION_LABEL = "Set value";
            }
        }
    }
}

#endif