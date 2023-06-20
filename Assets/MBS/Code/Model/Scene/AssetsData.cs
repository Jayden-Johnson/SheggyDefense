#if UNITY_EDITOR

using UnityEngine;

namespace MBS.Model.Scene
{
    internal static class AssetsData
    {
        internal static class Floor
        {
            internal static GameObject SquarePrefab;
            internal static GameObject TriangularPrefab;

            internal static Mesh SquareMesh;
            internal static Mesh TriangularMesh;

            internal static bool IsTriangularExist;

            public static void Clear( )
            {
                SquarePrefab = null;
                TriangularPrefab = null;
                IsTriangularExist = false;
            }
        }
    }
}

#endif