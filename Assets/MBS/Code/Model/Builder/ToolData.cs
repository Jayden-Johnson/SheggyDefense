#if UNITY_EDITOR

using System;
using MBS.Model.AssetSystem;
using UnityEngine;

namespace MBS.Model.Builder
{
    [Serializable]
    internal class ToolData
    {
        //Assets
        [SerializeField] internal string PackGUID;
        [SerializeField] internal string GroupGuid;
        [SerializeField] internal string CategoryGUID;

        //Mics params
        [SerializeField] internal float GridCellSize;
        [SerializeField] internal bool GridCellSize_LinkToggle;
        [SerializeField] internal float GridLevelHeight;

        [SerializeField] internal int GridLevelNumber;

        [SerializeField] internal Vector2 SelectionGrid_ScrollPos;

        [NonSerialized] internal ModularGroupCategory Category;
        [NonSerialized] internal ModularGroup Group;

        [NonSerialized] internal ModularPack Pack;


        internal ToolData( )
        {
            PackGUID = null;
            GroupGuid = null;
            CategoryGUID = null;

            GridCellSize = 1;
            GridCellSize_LinkToggle = true;

            GridLevelHeight = 1;

            GridLevelNumber = 0;

            SelectionGrid_ScrollPos = Vector2.zero;
        }
    }
}

#endif