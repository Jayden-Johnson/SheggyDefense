#if UNITY_EDITOR

using System.IO;
using System.Linq;
using MBS.Model.Configuration;
using UnityEditor;
using UnityEngine;

namespace MBS.Model.AssetSystem
{
    internal class ModularPack_Saver
    {
        public static bool SaveCreatedModularPack( ModularPack createdPack )
        {
            // Check if folder with same name as @createdPack exist

            var folderPath = PathsManager.Singleton.ModularPacksPath + "/" + createdPack.Name;


            if ( AssetDatabase.IsValidFolder( folderPath ) == false )
            {
                // If folder not exist
                // GOOD, then create folder and module inside
                var createdFolderPath = CreateModularPackFolder( createdPack.Name );

                if ( string.IsNullOrEmpty( createdFolderPath ) )
                {
                    Debug.LogFormat(
                        Texts.AssetSystem.ModularPack.Saver.CANNOT_CREATE_FOLDER,
                        folderPath );
                    return false;
                }

                var assetSavePath = createdFolderPath + PredefinedPaths.DESCRIPTOR_SUFFIX_PATH;

                CreateNewAssetAndSave( createdPack, assetSavePath );
            }
            else
            {
                // Folder already exist
                // BAD, throw error and return false

                var prevName = createdPack.Name;

                var subfolders = AssetDatabase.GetSubFolders( PathsManager.Singleton.ModularPacksPath );
                subfolders = subfolders.Select( i => Path.GetFileName( i ) ).ToArray( );

                var uniquePackName = ObjectNames.GetUniqueName( subfolders, createdPack.Name );

                createdPack.Name = uniquePackName;

                var result = SaveCreatedModularPack( createdPack );

                if ( result )
                    Debug.LogWarningFormat( Texts.AssetSystem.ModularPack.Saver.packNameHasChanged, prevName,
                                            uniquePackName );

                return result;
            }

            return true;
        }

        public static bool SaveEditedModularPack( ModularPack editedPack, DescriptorData descriptorData )
        {
            // Try to find a folder with @oldPack name
            if ( File.Exists( descriptorData.AssetPath ) )
            {
                // If module exist
                // GOOD
                // Then delete it

                AssetDatabase.DeleteAsset( descriptorData.AssetPath );

                // Create new TextAsset file
                // p.s: because TextAsset file is not editable

                CreateNewAssetAndSave( editedPack, descriptorData.AssetPath );

                if ( descriptorData.FolderName != editedPack.Name )
                {
                    var renameResult = AssetDatabase.RenameAsset( descriptorData.FolderPath, editedPack.Name );

                    if ( !string.IsNullOrEmpty( renameResult ) ) Debug.LogError( renameResult );
                }
            }
            else
            {
                // Somehow @oldFolder doesn't even exist
                // Bad, but let's create it and module then

                var newFolderPath = CreateModularPackFolder( editedPack.Name );
                var newAssetPath = newFolderPath + PredefinedPaths.DESCRIPTOR_SUFFIX_PATH;
                CreateNewAssetAndSave( editedPack, newAssetPath );
            }

            return true;
        }

        public static void CreateNewAssetAndSave( ModularPack modularPack, string assetPath )
        {
            var newAsset = ConvertToJsonAsset( modularPack );
            AssetDatabase.CreateAsset( newAsset, assetPath );
        }

        public static TextAsset ConvertToJsonAsset( ModularPack modularPack )
        {
            var contentToString = EditorJsonUtility.ToJson( modularPack, true );
            var textAsset = new TextAsset( contentToString );
            return textAsset;
        }

        public static string CreateModularPackFolder( string folderName )
        {
            var createdFolderGuid = AssetDatabase.CreateFolder( PathsManager.Singleton.ModularPacksPath, folderName );
            var createdFolderPath = AssetDatabase.GUIDToAssetPath( createdFolderGuid );
            return createdFolderPath;
        }
    }
}

#endif