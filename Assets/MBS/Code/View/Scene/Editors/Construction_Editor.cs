#if UNITY_EDITOR

using System.Linq;
using MBS.Controller.Configuration;
using MBS.Controller.Scene;
using UnityEditor;
using UnityEngine.UIElements;

namespace MBS.View.Scene
{
    [CustomEditor( typeof( MBSConstruction ) )]
    public class ConstructionEditor : Editor
    {
    private MBSConstruction _mBsMbsConstruction;
    
    public override VisualElement CreateInspectorGUI( )
    {
        var windowPath = PathController.GetPATH_ConstructionInspector( );
        var listItemPath = PathController.GetPATH_ConstructionInspectorListViewItem( );
    
        var windowLoaded = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>( windowPath );
        var listItemLoaded = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>( listItemPath );
    
        var window = windowLoaded.CloneTree( );
    
        var editButton = window.Q<Button>( "edit-button" );
        editButton.clicked += ( ) =>
        {
            ( target as MBSConstruction )?.StartEditObject( );
        };
        
        var areasListView = window.Q<ListView>( "areas-listview" );
        areasListView.itemsSource = ( (MBSConstruction)target ).Areas;
        areasListView.makeItem = ( ) => listItemLoaded.Instantiate( );
        areasListView.bindItem = ( visualElement, asd ) =>
        {
            visualElement.RegisterCallback<PointerEnterEvent>( ( f ) =>
            { 
                var area = ( target as MBSConstruction )?.Areas[ asd ];
                var points = area.essentialPoints;
                ( (MBSConstruction)target ).additionalPointsToDraw = points.ToArray( );
                SceneView.RepaintAll( );
            } );
    
            visualElement.RegisterCallback<PointerLeaveEvent>( ( f ) =>
            {
                ( target as MBSConstruction ).additionalPointsToDraw = null;
                SceneView.RepaintAll( );
            } );
    
            visualElement.Q<Label>( ).text = $"Area ({asd.ToString( )})";
    
            visualElement.Q<Button>( ).clicked += ( ) =>
            {
                var area = ( (MBSConstruction)target ).Areas[ asd ];
                var areaFloor = MeshCreator.CreatePlaneMesh( area.essentialPoints, area.pointsWindingOrder );
                areaFloor.name = $"Generated Mesh. Area ({asd.ToString( )}).";
                areaFloor.transform.SetParent( ( target as MBSConstruction ).transform );
            };
        };
    
        return window;
    }
    
    private void OnEnable( )
    {
        _mBsMbsConstruction = target as MBSConstruction;
    }
    
    private void OnDisable( )
    {
        _mBsMbsConstruction.OnEditorDisable( );
    }
    }
}
#endif