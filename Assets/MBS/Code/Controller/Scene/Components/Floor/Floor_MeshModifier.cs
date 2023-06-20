#if UNITY_EDITOR

using UnityEngine;

namespace MBS.Controller.Scene
{
    public enum FloorCorner
    {
        TopLeft,
        TopRight,
        BotRight,
        BotLeft,
        All
    }

    public static class Floor_MeshModifier
    {
        public static void ModifyFloorCorner( GameObject floorPrefab, FloorCorner corner )
        {
            var localScale = floorPrefab.transform.localScale;
            switch ( corner )
            {
                case FloorCorner.TopRight:
                    return;
                case FloorCorner.BotRight:
                    localScale = new Vector3(
                        localScale.x,
                        localScale.y,
                        localScale.z * -1 );
                    floorPrefab.transform.localScale = localScale;
                    return;
                case FloorCorner.BotLeft:
                    localScale = new Vector3(
                        localScale.x * -1,
                        localScale.y,
                        localScale.z * -1 );
                    floorPrefab.transform.localScale = localScale;
                    return;
                case FloorCorner.TopLeft:
                    localScale = new Vector3(
                        localScale.x * -1,
                        localScale.y,
                        localScale.z );
                    floorPrefab.transform.localScale = localScale;
                    return;
            }


            //ModifyObject( floorPrefab, corner );
            // floorPrefab.transform.DoRecursive( t => { ModifyObject( t.gameObject, corner ); } );
        }

        // private static void ModifyObject( GameObject gameObject, FloorCorner corner )
        // {
        //     if ( gameObject.TryGetComponent( out MeshFilter filter ) )
        //     {
        //         var mesh = Object.Instantiate( filter.sharedMesh );
        //
        //         var scaleVector = Vector3.one;
        //         var vertices = mesh.vertices;
        //         var normals = mesh.normals;
        //         var triangles = mesh.triangles;
        //
        //         var localScale = gameObject.transform.localScale;
        //
        //         switch ( corner )
        //         {
        //             case FloorCorner.TopRight:
        //                 //scaleVector = Vector3.one;
        //                 //it is initially top right, no changes needed
        //                 return;
        //                 break;
        //             case FloorCorner.BotRight:
        //                 // scaleVector = new Vector3( 1, 1, -1 );
        //
        //                 localScale = new Vector3(
        //                     localScale.x,
        //                     localScale.y,
        //                     localScale.z * -1 );
        //                 gameObject.transform.localScale = localScale;
        //
        //                 return;
        //             // triangles = ReorderTriangles( triangles );
        //             // break;
        //             case FloorCorner.BotLeft:
        //                 scaleVector = new Vector3( -1, 1, -1 );
        //
        //                 localScale = new Vector3(
        //                     localScale.x * -1,
        //                     localScale.y,
        //                     localScale.z * -1 );
        //                 gameObject.transform.localScale = localScale;
        //                 return;
        //                 // verticies = ModifyVertecies(verticies, scaleVector);
        //                 break;
        //             case FloorCorner.TopLeft:
        //                 scaleVector = new Vector3( -1, 1, 1 );
        //                 localScale = new Vector3(
        //                     localScale.x * -1,
        //                     localScale.y,
        //                     localScale.z );
        //                 gameObject.transform.localScale = localScale;
        //                 return;
        //                 triangles = ReorderTriangles( triangles );
        //                 break;
        //         }
        //
        //         vertices = ModifyVertices( vertices, scaleVector );
        //         normals = ModifyVertices( normals, scaleVector );
        //
        //         mesh = Object.Instantiate( filter.sharedMesh );
        //         mesh.name = "ModifiedFloorMesh";
        //
        //         mesh.SetNormals( normals );
        //         mesh.SetVertices( vertices );
        //         mesh.SetTriangles( triangles, 0 );
        //
        //         gameObject.GetComponent<MeshFilter>( ).mesh = mesh;
        //     }
        // }
        //
        // private static Vector3[ ] ModifyVertices( Vector3[ ] vertices, Vector3 scaleVector )
        // {
        //     for ( var i = 0; i < vertices.Length; i++ )
        //         vertices[ i ] = new Vector3( vertices[ i ].x * scaleVector.x,
        //                                      vertices[ i ].y * scaleVector.y,
        //                                      vertices[ i ].z * scaleVector.z );
        //     return vertices;
        // }
        //
        // public static Vector3[ ] ModifyNormals( Vector3[ ] normals, Vector3 scaleVector )
        // {
        //     for ( var i = 0; i < normals.Length; i++ ) normals[ i ] *= scaleVector.x * scaleVector.z;
        //     return normals;
        // }
        //
        // private static int[ ] ReorderTriangles( int[ ] trinagles )
        // {
        //     for ( var i = 0; i < trinagles.Length; i += 3 )
        //     {
        //         var a = trinagles[ i ];
        //         var b = trinagles[ i + 1 ];
        //         var c = trinagles[ i + 2 ];
        //
        //         trinagles[ i ] = a;
        //         trinagles[ i + 1 ] = c;
        //         trinagles[ i + 2 ] = b;
        //     }
        //
        //     return trinagles;
        // }
    }
}
#endif