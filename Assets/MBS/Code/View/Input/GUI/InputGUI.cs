#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MBS.View.Input.GUI
{
    internal static class InputGUI
    {
        private static SceneView _prevScene;
        private static SceneHelpbarUIElement _helpbar;


        internal static void Start( )
        {
            if ( _helpbar == null )
                _helpbar = new SceneHelpbarUIElement( );
            else
                _helpbar.Clear( );
        }

        internal static void Clear( )
        {
            _helpbar.Clear( );
        }

        internal static void Stop( )
        {
            if ( _prevScene != null )
                DetachHelpbarFromScene( _prevScene );
            else
                DetachHelpbarFromScene( SceneView.lastActiveSceneView );

            _prevScene = null;
            _helpbar = null;
        }


        
        internal static void AttachHelpbarToScene( SceneView sceneView )
        {
            if ( _helpbar == null )
                _helpbar = new SceneHelpbarUIElement( );

            if ( sceneView != _prevScene )
            {
                DetachHelpbarFromScene( _prevScene );
                _helpbar.RegisterCallback<AttachToPanelEvent>( p =>
                {
                    if ( sceneView != null )
                        sceneView.rootVisualElement.RegisterCallback<GeometryChangedEvent>( PanelGeometryEvent );
                } );

                _helpbar.RegisterCallback<DetachFromPanelEvent>( p =>
                {
                    if ( sceneView != null )
                        sceneView.rootVisualElement.UnregisterCallback<GeometryChangedEvent>( PanelGeometryEvent );
                } );
                sceneView.rootVisualElement.Add( _helpbar );

                _prevScene = sceneView;
            }
        }

        private static void DetachHelpbarFromScene( SceneView sceneView )
        {
            if ( sceneView == null || sceneView.rootVisualElement == null )
                return;

            var sceneRoot = sceneView.rootVisualElement;
            var findedHelpbars = sceneRoot.Query<SceneHelpbarUIElement>( ).ToList( );

            for ( var i = 0; i < findedHelpbars.Count; i++ ) sceneRoot.Remove( findedHelpbars[ i ] );
        }


        private static void PanelGeometryEvent( GeometryChangedEvent e )
        {
            if ( _helpbar == null )
            {
                return;
            }

            _helpbar.style.top = e.newRect.height - _helpbar.resolvedStyle.height;
            _helpbar.style.left = 0;
        }
        
        
        internal static void AddMouseElement( UIElement uiElements )
        {
            var keyName = "<b>LMB</b>";
            uiElements.Label = keyName + " - " + uiElements.Label;
            _helpbar.AddMultiElement( uiElements );
        }


        internal static void AddKeyElement( params UIElement[ ] uiElements )
        {
            for ( var i = 0; i < uiElements.Length; i++ )
            {
                var curElement = uiElements[ i ];
                if ( curElement.Key != default && string.IsNullOrEmpty( curElement.Label ) == false)
                {
                    var keyName = GetReadableKeyName( curElement.Key );
                    curElement.Label = keyName + " - " + curElement.Label;
                }
            }

            _helpbar.AddMultiElement( uiElements );
        }

        private static string GetReadableKeyName( KeyCode key )
        {
            var retval = key.ToString( );

            switch ( key )
            {
                case KeyCode.Escape:
                    retval = "Esc";
                    break;
            }

            return "<b>" + retval + "</b>";
        }


        internal static (Vector2 pos, Vector2 size) GetHelpbarSize( )
        {
            return ( new Vector2( _helpbar.resolvedStyle.left, _helpbar.resolvedStyle.top ),
                     new Vector2( _helpbar.resolvedStyle.width, _helpbar.resolvedStyle.height ) );
        }
    }
}

#endif