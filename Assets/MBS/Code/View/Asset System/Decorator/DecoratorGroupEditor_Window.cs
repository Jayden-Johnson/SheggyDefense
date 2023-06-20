#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using MBS.Controller.Configuration;
using MBS.Model.AssetSystem;
using MBS.Model.Configuration;
using MBS.Utilities.Helpers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace MBS.View.AssetSystem
{
    internal class DecoratorGroupEditorWindow : ImporterWindow
    {
        [FormerlySerializedAs( "placerGroup" )] public DecoratorGroup DecoratorGroup;
        public List<DecoratorGroup> tempPlacerGroups;
        public ModularPack modularPack;

        public List<DecoratorModule> modulesList;
        private Button _cancelButton;
        private DropdownField _categoriesDropdown;

        private TextField _groupNameTextField;
        private VisualTreeAsset _listItemVta;
        private Button _manageCategoriesButton;
        private ListView _modulesListView;
        private Button _saveButton;

        private string _selectedCategoryGuid;

        public bool IsOpenningModuleWindow { get; private set; }

        private void OnDisable( )
        {
            if ( HasOpenInstances<PlacerModuleEditorWindow>( ) ) GetWindow<PlacerModuleEditorWindow>( ).Close( );
        }

        private void OnDestroy( )
        {
            if ( HasOpenInstances<PlacerModuleEditorWindow>( ) ) GetWindow<PlacerModuleEditorWindow>( ).Close( );
        }


        //Initialization
        private void CreateGUI( )
        {
            if ( !HasOpenInstances<ModularPackManager_Window>( ) )
            {
                Close( );
            }
            else
            {
                var mpw = GetWindow<ModularPackManager_Window>( );
                if ( !mpw.IsOpenningGroupWindow )
                    Close( );
            }

            SetTitle( Texts.AssetSystem.Group.DECOR_GROUP_EDITOR_WINDOW_NAME );
            SetMinSize( 480, 220 );

            ImportVTAs( );
            QueryVisualElements( );

            ConfigureCategoriesDropdown( );
            ConfigureManagerCategoriesButton( );
            ConfigureAssetsListView( );
            ConfigureSaveButton( );
            ConfigureCancelButton( );
        }

        private void ImportVTAs( )
        {
            var windowVta =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>( PathController.GetPATH_PlacerGroupEditorWindow( ) );
            _listItemVta =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>( PathController.GetPATH_ListItemWithIcon( ) );
            rootVisualElement.Add( windowVta.CloneTree( ) );
        }

        private void QueryVisualElements( )
        {
            _groupNameTextField = rootVisualElement.Q<TextField>( "modular-group-name-textfield" );
            _categoriesDropdown = rootVisualElement.Q<DropdownField>( "categories-dropdown" );
            _manageCategoriesButton = rootVisualElement.Q<Button>( "manage-categories-button" );
            _modulesListView = rootVisualElement.Q<ListView>( "modules-listview" );
            _saveButton = rootVisualElement.Q<Button>( "save-button" );
            _cancelButton = rootVisualElement.Q<Button>( "cancel-button" );
        }

        private void ConfigureCategoriesDropdown( )
        {
            //Callbacks
            _categoriesDropdown.RegisterValueChangedCallback( e =>
            {
                var index = _categoriesDropdown.index;

                if ( index < 0 )
                    index = 0;

                var category = modularPack.DecoratorCategories.ElementAtOrDefault( index );
                _selectedCategoryGuid = category.Guid;
            } );
        }

        private void ConfigureManagerCategoriesButton( )
        {
            _manageCategoriesButton.clicked += ( ) =>
            {
                var window = GetWindow<AssetCategoriesEditorWindow>( );
                window.SetupData( DecoratorGroup, modularPack );
            };
        }

        private void ConfigureAssetsListView( )
        {
            _modulesListView.makeItem += ( ) => _listItemVta.Instantiate( );
            _modulesListView.itemsAdded += e => { modulesList[ e.First( ) ] = new DecoratorModule( ); };

            _modulesListView.bindItem += ( visualElement, index ) =>
            {
                var label = visualElement.Q<Label>( );
                if ( string.IsNullOrEmpty( modulesList[ index ].Name ) )
                {
                    var newName = Texts.AssetSystem.Creation.NEW_DECOR_MODULE;
                    var uniqueName = ObjectNames.GetUniqueName( 
                        modulesList.Select( i => i.Name ).ToArray( ), newName );
                    modulesList[ index ].Name = uniqueName;
                }

                label.text = modulesList[ index ].Name;

                Texture texture = Module.GetPreviewOrEmptyIcon( modulesList[ index ] );
                var image = visualElement.Q<MBSImageVisualElement>( );
                image.image = texture;
            };

            _modulesListView.onItemsChosen += e =>
            {
                IsOpenningModuleWindow = true;
                var window = GetWindow<PlacerModuleEditorWindow>( );
                window.SetupData( modulesList[ _modulesListView.selectedIndex ], modulesList );
                IsOpenningModuleWindow = false;
            };
        }

        private void ConfigureSaveButton( )
        {
            _saveButton.clicked += ( ) =>
            {
                // Validation
                var trimmedName = Text_Helper.TrimAndRemoveDoubleSpaces( _groupNameTextField.value );
                var name = ObjectNames.GetUniqueName(
                    tempPlacerGroups.Select( i => i.Name ).ToArray( ),
                    trimmedName );

                if ( !Text_Helper.IsTextLengthOk( name ) )
                {
                    Debug.LogError( MessageController.TextValidation_LengthLess2( _groupNameTextField.label ) );
                    return;
                }

                if ( !Text_Helper.IsTextContainsOnlyLettersAndDigits( name ) )
                {
                    Debug.LogError(
                        MessageController.TextValidation_InapproppriateCharacters( _groupNameTextField.label ) );
                    return;
                }

                DecoratorGroup.Name = name;
                DecoratorGroup.Modules = modulesList.ToArray( );
                DecoratorGroup.CategoryGuid = _selectedCategoryGuid;

                if ( modulesList.Count == 0 )
                    Debug.LogWarning( MessageController.GROUP_ModulesListEmpty( titleContent.text, name ) );

                if ( HasOpenInstances<ModularPackManager_Window>( ) )
                {
                    var window = GetWindow<ModularPackManager_Window>( );
                    window.RefreshWindow( );
                }

                Close( );
            };
        }

        private void ConfigureCancelButton( )
        {
            _cancelButton.clicked += ( ) => { Close( ); };
        }


        public void UpdateElements( )
        {
            SetupCategories( );
            _modulesListView.Rebuild( );
        }

        public void SetupData( DecoratorGroup decoratorGroup, List<DecoratorGroup> placerGroups, ModularPack modularPack )
        {
            this.modularPack = modularPack;

            this.DecoratorGroup = decoratorGroup;
            tempPlacerGroups = new List<DecoratorGroup>( placerGroups );
            tempPlacerGroups.Remove( this.DecoratorGroup );

            _groupNameTextField.value = decoratorGroup.Name;

            modulesList = this.DecoratorGroup.Modules.ToList( );
            _modulesListView.itemsSource = modulesList;

            SetupCategories( );
        }

        private void SetupCategories( )
        {
            //Setup choices
            var choices = modularPack.DecoratorCategories.Select( i => i.Name ).ToList( );
            _categoriesDropdown.choices = choices;

            //Setup item index
            if ( !string.IsNullOrEmpty( DecoratorGroup.CategoryGuid ) )
            {
                var catsList = modularPack.DecoratorCategories.ToList( );
                var findedCollection = catsList.Where( i => i.Guid == DecoratorGroup.CategoryGuid ).ToList( );

                if ( findedCollection.Count == 0 )
                {
                    _categoriesDropdown.index = 0;
                    //Debug.LogError("I didnt find category with wuch guid");
                    return;
                }

                var index = catsList.IndexOf( findedCollection.First( ) );
                _categoriesDropdown.index = index;
            }
            else
            {
                _categoriesDropdown.index = 0;
            }
        }
    }
}

#endif