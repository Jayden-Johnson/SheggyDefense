#if UNITY_EDITOR

using System;
using System.Linq;
using MBS.Controller.AssetSystem;
using MBS.Model.Configuration;
using MBS.Utilities.Helpers;
using UnityEngine;

namespace MBS.Model.AssetSystem
{
    internal class ModularPack_Manager : ScriptableObject, ISingleton
    {
        private static ModularPack_Manager _singleton;

        public static ModularPack_Manager Singleton => _singleton =
                                                           SingletonHelper.GetSingleton(
                                                               _singleton, nameof( ModularPack_Manager ) );


        [SerializeField] private ModularPack[ ] _modularPacks;
        [NonSerialized] private DescriptorData[ ] _descriptorData;

        public ModularPack[ ] ModularPacks
        {
            get => _modularPacks;
            set => _modularPacks = value;
        }

        public DescriptorData[ ] DescriptorsData
        {
            get => _descriptorData;
            set => _descriptorData = value;
        }


        public void ColdInitialization( )
        {
            // Initial loading
            ImportAssets( );
        }

        public void WarmInitialization( )
        {
            // If there was changes - Refresh
            if ( ModularPackPostprocessor.IsAssetsChanged )
                ImportAssets( );
        }


        internal void ImportAssets( )
        {
            DescriptorsData = ModularPack_Importer.ImportAll( ).ToArray( );
            _modularPacks = DescriptorsData.Select( i => i.Pack ).ToArray( );
        }

        public bool SavePack( ModularPack modularPack )
        {
            // Check if there's modular pack in manager that has the same GUID
            var descriptorData = DescriptorsData.Where( i => i.Pack.Guid == modularPack.Guid )?.FirstOrDefault( );

            if ( descriptorData != null )
            {
                // That means this is already saved pack
                // So, save it with this method
                if ( ModularPack_Saver.SaveEditedModularPack( modularPack, descriptorData ) )
                    return true;
            }
            else
            {
                // If there is no pack with the same guid
                // That means it is newly created pack
                if ( ModularPack_Saver.SaveCreatedModularPack( modularPack ) )
                    return true;
            }

            Debug.LogError( Texts.AssetSystem.ModularPack.Manager.FILE_SAVE_PROBLEM );
            return false;
        }

        public void RemovePack( ModularPack modularPack )
        {
            var descriptorData = DescriptorsData.FirstOrDefault( i => i.Pack.Guid == modularPack.Guid );

            DescriptorsData = DescriptorsData.Where( i => i.Pack.Guid != modularPack.Guid ).ToArray( );
            ModularPacks = ModularPacks.Where( i => i.Guid != modularPack.Guid ).ToArray( );

            if ( descriptorData == null )
            {
                Debug.LogError( Texts.AssetSystem.ModularPack.Manager.FILE_REMOVING_PROBLEM );
                return;
            }


            ModularPack_Remover.RemoveModularPack( descriptorData );
        }

        public void HidePack( ModularPack modularPack )
        {
            var descriptorData = DescriptorsData.Where( i => i.Pack.Guid == modularPack.Guid )?.FirstOrDefault( );

            if ( descriptorData == null || descriptorData.Pack == null )
            {
                Debug.LogError( Texts.AssetSystem.ModularPack.Manager.CANNOT_HIDE_PACK );
                return;
            }

            ModularPack_VisibilityController.HideModularPack( descriptorData );
        }

        public void UnhidePack( ModularPack modularPack )
        {
            var descriptorData = DescriptorsData.Where( i => i.Pack.Guid == modularPack.Guid )?.FirstOrDefault( );

            if ( descriptorData == null || descriptorData.Pack == null )
            {
                Debug.LogError( Texts.AssetSystem.ModularPack.Manager.CANNOT_HIDE_PACK  );
                return;
            }

            ModularPack_VisibilityController.UnhideModularPack( descriptorData );
        }
    }
}

#endif