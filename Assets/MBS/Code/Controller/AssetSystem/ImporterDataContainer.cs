#if UNITY_EDITOR

using MBS.Model.AssetSystem;

namespace MBS.Controller.AssetSystem
{
    internal class ImporterDataContainer
    {
        internal bool IsHidden;
        internal bool IsUnsaved;
        internal ModularPack Pack;

        internal string GetName( )
        {
            if ( IsUnsaved )
                return "[Unsaved] " + Pack.Name;

            if ( IsHidden )
                return "[Hidden] " + Pack.Name;

            return Pack.Name;
        }
    }
}

#endif