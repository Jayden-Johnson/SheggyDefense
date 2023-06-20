#if UNITY_EDITOR

using System;
using System.Linq;
using MBS.Model.AssetSystem;
using MBS.View.Builder;
using UnityEditor;
using UnityEngine;

namespace MBS.Model.Builder
{
    [Serializable]
    internal class BuilderData : ScriptableObject
    {
        [SerializeField] private ToolbarTool _selectedTool;
        [SerializeField] private ToolData[ ] _toolsSavedData;


        internal ToolbarTool SelectedTool
        {
            get => _selectedTool;
            set
            {
                _selectedTool = value;
                MarkDirty( );
            }
        }

        internal ToolData SelectedToolData
        {
            get => _toolsSavedData[ (int)SelectedTool ];
            set
            {
                _toolsSavedData[ (int)SelectedTool ] = value;
                MarkDirty( );
            }
        }

        internal void ColdInitialization( )
        {
            // Create @ToolData for each toolbar tool
            var toolsNumber = Enum.GetNames( typeof( ToolbarTool ) ).Length;
            _toolsSavedData = new ToolData[ toolsNumber ];

            for ( var i = 0; i < _toolsSavedData.Length; i++ )
                _toolsSavedData[ i ] = new ToolData( );

            EditorUtility.SetDirty( this );
        }

        internal void WarmInitialization( )
        {
        }

        private void MarkDirty( )
        {
            EditorUtility.SetDirty( this );
        }

        public void InitializeAssetsData( ModularPack firstPack )
        {
            var fWallGroup = firstPack.WallGroups.FirstOrDefault( );
            var fFloorGroup = firstPack.FloorGroups.FirstOrDefault( );
            var fPlacerGroup = firstPack.DecoratorGroups.FirstOrDefault( );

            _toolsSavedData[ (int)ToolbarTool.WallTool ].Pack = firstPack;
            _toolsSavedData[ (int)ToolbarTool.WallTool ].Group = fWallGroup;

            _toolsSavedData[ (int)ToolbarTool.FloorTool ].Pack = firstPack;
            _toolsSavedData[ (int)ToolbarTool.FloorTool ].Group = fFloorGroup;

            _toolsSavedData[ (int)ToolbarTool.PlacerTool ].Pack = firstPack;
            _toolsSavedData[ (int)ToolbarTool.PlacerTool ].Group = fPlacerGroup;
        }
    }
}

#endif