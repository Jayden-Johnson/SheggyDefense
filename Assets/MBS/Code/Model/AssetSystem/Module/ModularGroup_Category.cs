#if UNITY_EDITOR

using System;
using UnityEngine;

namespace MBS.Model.AssetSystem
{
    [Serializable]
    internal class ModularGroupCategory : UniqueObject
    {
        [SerializeField] private string _name;

        internal ModularGroupCategory( string name )
        {
            _name = name;
        }

        internal string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}

#endif