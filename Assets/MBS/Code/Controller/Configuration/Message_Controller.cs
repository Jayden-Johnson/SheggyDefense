#if UNITY_EDITOR

using MBS.Model.Configuration;

namespace MBS.Controller.Configuration
{
    internal static class MessageController
    {
        private const string NEXT_LINE_ELEMENT = "\n";


        internal static string TextValidation_LengthLess2( string textObjectName )
        {
            return string.Format( Texts.AssetSystem.Text.VALIDATION_LENGTH_LESS_THAN2, textObjectName ) +
                   NEXT_LINE_ELEMENT;
        }

        internal static string TextValidation_InapproppriateCharacters( string textObjectName )
        {
            return string.Format( Texts.AssetSystem.Text.VALIDATION_WRONG_CHARACTERS, textObjectName ) +
                   NEXT_LINE_ELEMENT;
        }


        // internal static string PATHSMAN_CantFindFolder( string folderName )
        // {
        //     return string.Format( Texts.Configuration.PathsManager.CANT_FIND_FOLDER, folderName );
        // }
        //
        // internal static string PATHSMAN_CantCreateFolder( string folderName )
        // {
        //     return string.Format( Texts.Configuration.PathsManager.CANT_CREATE_FOLDER, folderName );
        // }
        //
        //
        // internal static string PACK_ONSAVE_NameCahged( string initialName, string changedName )
        // {
        //     return string.Format( Texts.AssetSystem.ModularPack.Saver.packNameHasChanged, initialName,
        //                           changedName );
        // }
        //
        // internal static string PACK_ONCHANGE_IndexLessZero( string windowName, int index )
        // {
        //     return string.Format( Texts.AssetSystem.ModularPack.PACK_INDEX_LESS_ZERO, windowName, index );
        // }


        internal static string GROUP_ModulesListEmpty( string windowName, string assetName )
        {
            return string.Format( Texts.AssetSystem.Group.MODULES_LIST_EMPTY, windowName, assetName ) +
                   NEXT_LINE_ELEMENT;
        }


        internal static string MODULE_Wall_Info( )
        {
            return Texts.AssetSystem.Module.WALL_INFO;
        }

        internal static string MODULE_Floor_Info( )
        {
            return Texts.AssetSystem.Module.FLOOR_INFO;
        }

        internal static string MODULE_ObjectNotPrefab( string windowName, string assetName, string fieldName )
        {
            return string.Format( Texts.AssetSystem.Module.OBJECT_NOT_PREFAB, windowName, assetName, fieldName ) +
                   NEXT_LINE_ELEMENT;
        }

        internal static string MODULE_ONSAVE_FieldValueIsNull( string windowName, string assetName, string fieldName )
        {
            return string.Format( Texts.AssetSystem.Module.FIELD_VALUE_NULL, windowName, assetName, fieldName ) +
                   NEXT_LINE_ELEMENT;
        }
    }
}
#endif