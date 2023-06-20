#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace MBS.View.Input.Physical
{
    public static class Mouse
    {
        private static Vector3 _prevFreeConstrPos;
        private static Vector3 _prevSnappedConstrPos;
        private static Vector3 _prevFreeWorldPos;
        private static Vector3 _prevSnappedWorldPos;
        
        public static Vector3 FreeDirection_Constr { get; private set; }
        public static Vector3 SnappedDirection_Constr { get; private set; }
        public static Vector3 FreeDirection_World { get; private set; }
        public static Vector3 SnappedDirection_World { get; private set; }
        
        public static bool IsSnappedToEnd;
        public static bool IsMouseInScene { get; set; }

        public static Ray WorldRay { get; private set; }
        public static Vector3 FreeWorldPos { get; private set; }
        public static Vector3 SnappedWorldPos { get; private set; }
        public static Vector3 FreeConstrPos { get; private set; }
        public static Vector3 SnappedConstrPos { get; private set; }

        public static Func<Vector3> CustomSnappingFunction { get; set; }
        public static bool IsPointerInSceneView { get; set; }


        /// <summary>
        ///     Calculating <seealso cref="FreeWorldPos" /> and <seealso cref="FreeConstrPos" />
        /// </summary>
        /// <param name="rawMousePosition">Current mouse position. Use <seealso cref="Event" />.mousePosition</param>
        /// <param name="grid">Grid to calculate local mouse position relative to it</param>
        /// <returns></returns>
        public static void CalcFreePosition( Vector3 rawMousePosition )
        {
            if ( !IsMouseInScene ) return;
            FreeWorldPos = GetPointOnPlane( rawMousePosition, MbsGrid.Transform.up, MbsGrid.Position_World );
            FreeConstrPos = MbsGrid.Transform.InverseTransformPoint( FreeWorldPos );

            FreeDirection_World = FreeWorldPos - _prevFreeWorldPos;
            FreeDirection_Constr = FreeConstrPos - _prevFreeConstrPos;

            _prevFreeWorldPos = FreeWorldPos;
            _prevFreeConstrPos = FreeConstrPos;
        }

        /// <summary>
        ///     <para>Calculating <seealso cref="SnappedConstrPos" /> and <seealso cref="SnappedWorldPos" />.</para>
        ///     <para>
        ///         <seealso cref="SnappedConstrPos" /> is calculating based on <paramref name="grid" /> and
        ///         <paramref name="customSnappingFunction" />.
        ///     </para>
        ///     <para><seealso cref="SnappedWorldPos" /> is transformed <seealso cref="SnappedConstrPos" /> (local -> world) </para>
        ///     <remarks>Use it before using <seealso cref="SnappedConstrPos" /> and <seealso cref="SnappedWorldPos" /></remarks>
        /// </summary>
        /// <param name="grid"> Currently using grid</param>
        /// <param name="customSnappingFunction"> Custom function which should return snapped position</param>
        public static void CalcSnappedPosition( )
        {
            if ( !IsMouseInScene )
                return;

            if ( CustomSnappingFunction == null )
            {
                SnappedConstrPos = FreeConstrPos;
                SnappedWorldPos = FreeWorldPos;
              
            }
            else
            {
                SnappedConstrPos = CustomSnappingFunction.Invoke( );
                SnappedWorldPos = MbsGrid.Transform.TransformPoint( SnappedConstrPos );

                CustomSnappingFunction = null;
            }

            SnappedDirection_World = SnappedWorldPos - _prevSnappedWorldPos;
            SnappedDirection_Constr = SnappedConstrPos - _prevSnappedConstrPos;

            _prevSnappedWorldPos = SnappedWorldPos;
            _prevSnappedConstrPos = SnappedConstrPos;
        }

        /// <summary>
        ///     Turning <paramref name="rawMousePosition" /> into Ray and Raycasting the <seealso cref="Plane" /> with position
        ///     <paramref name="planePosition" />, and normal <paramref name="planeNormal" />
        /// </summary>
        /// <param name="rawMousePosition"> Current mouse position. Use <seealso cref="Event" />.mousePosition </param>
        /// <returns> Point on a plane if raycast succeed, otherwise <seealso cref="Vector3.zero" /></returns>
        public static Vector3 GetPointOnPlane( Vector3 rawMousePosition, Vector3 planeNormal, Vector3 planePosition )
        {
            var mousePosition = rawMousePosition;
            WorldRay = HandleUtility.GUIPointToWorldRay( mousePosition );
            var plane = new Plane( planeNormal, planePosition );

            if ( plane.Raycast( WorldRay, out var distance ) )
                mousePosition = WorldRay.GetPoint( distance );
            else
                mousePosition = Vector3.zero;

            return mousePosition;
        }
    }
}

#endif