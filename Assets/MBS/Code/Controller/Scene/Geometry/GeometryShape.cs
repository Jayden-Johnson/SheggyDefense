#if UNITY_EDITOR

using System;
using MBS.Code.Utilities.Helpers;
using UnityEngine;

namespace MBS.Controller.Scene
{
    internal enum WindingOrder
    {
        Clockwise,
        CounterClockise,
        Invalid
    }

    [Serializable]
    internal class GeometryShape
    {
        [SerializeField] internal float area;
        [SerializeField] internal Vector3[ ] essentialPoints;
        [SerializeField] internal WindingOrder pointsWindingOrder;

        internal float RecalculateArea( )
        {
            if ( essentialPoints == null || essentialPoints.Length == 0 )
                return 0;

            var tempArea = CalculateArea( essentialPoints );

            area = Mathf.Abs( tempArea );

            pointsWindingOrder = GetWindingOrderByArea( tempArea );

            return area;
        }

        /// <returns>
        ///     Area of given points.
        ///     The sign of area value represents the points winding order.
        ///     A positive sign means clockwise order
        ///     and a negative sign means counter-clockwise order.
        /// </returns>
        internal static float CalculateArea( Vector3[ ] areaEssentialPoints )
        {
            var area = 0f;

            for ( var i = 0; i < areaEssentialPoints.Length; i++ )
            {
                var va = areaEssentialPoints[ i ];
                var vb = areaEssentialPoints[ Collections_Helper.GetNextLoopIndex( i, areaEssentialPoints.Length ) ];

                var width = vb.x - va.x;
                var height = ( vb.z + va.z ) / 2;

                area += width * height;
            }

            return area;
        }

        /// <returns>
        ///     Clockwise if area > 0.
        ///     Counter-clockwise if area
        ///     < 0;
        ///         Invalid if area is 0;
        /// </returns>
        internal static WindingOrder GetWindingOrderByArea( float area )
        {
            WindingOrder order;

            if ( area > 0 )
                order = WindingOrder.Clockwise;
            else if ( area < 0 )
                order = WindingOrder.CounterClockise;
            else
                order = WindingOrder.Invalid;

            return order;
        }
    }
}

#endif