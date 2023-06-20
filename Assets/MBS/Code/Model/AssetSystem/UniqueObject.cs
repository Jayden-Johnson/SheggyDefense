#if UNITY_EDITOR

using System;
using UnityEngine;

namespace MBS.Model.AssetSystem
{
    [Serializable]
    internal class UniqueObject
    {
        [SerializeField] protected string guid;


        internal UniqueObject( )
        {
            guid = System.Guid.NewGuid( ).ToString( "N" );
        }

        internal string Guid
        {
            get => guid.ToString();
            set => guid = value;
        }

        internal void GenerateNewGuid( )
        {
            guid = System.Guid.NewGuid( ).ToString( "N" );
        }
    }

    [Serializable]
    internal class UniqueNamedObject : UniqueObject
    {
        [SerializeField] internal string name;

        internal string Name
        {
            get => name;
            set => name = value;
        }
    }
}

#endif