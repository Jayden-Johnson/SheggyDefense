#if UNITY_EDITOR

using UnityEngine.UIElements;

// Not working if put under namespace

public class MBSImageVisualElement : Image
{
    public new class UxmlFactory : UxmlFactory<MBSImageVisualElement, UxmlTraits>
    {
    }
}
#endif