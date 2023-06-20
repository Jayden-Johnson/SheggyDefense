#if UNITY_EDITOR

using MBS.View.Input.GUI;
using MBS.View.Input.Physical;
using UnityEditor;
using UnityEngine;

namespace MBS.View.Input
{
    public static class InputManager
    {
        internal static void Start( )
        {
            InputPhysical.Start( );
            InputGUI.Start( );
        }

        internal static void DuringSceneGUI( SceneView newSceneView )
        {
            InputGUI.AttachHelpbarToScene( newSceneView );
        }

        internal static void Stop( )
        {
            InputPhysical.Stop( );
            InputGUI.Stop( );
        }


        public static void ExecuteIMGUI( Event evt )
        {
            InputPhysical.ExecuteIMGUI( evt );
        }

        internal static void ClearInputData( )
        {
            InputPhysical.Clear( );
            InputGUI.Clear( );
        }

        
        internal static void AddMouseElement( UIElement uiElement )
        {
            InputPhysical.AddPointerUp( uiElement.KeyAction );
            InputGUI.AddMouseElement( uiElement );
        }
        
        internal static void AddKeyElement( params UIElement[ ] uiElements )
        {
            for ( var i = 0; i < uiElements.Length; i++ )
            {
                var curElement = uiElements[ i ];
                if ( curElement.Key != default )
                {
                    InputPhysical.AddKeyUp( curElement.Key, curElement.KeyAction );
                }
            }
            InputGUI.AddKeyElement( uiElements );
        }
    }
}

#endif