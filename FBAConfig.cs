using System.ComponentModel;
using FullBodyAccessories.UI;
using Terraria.ModLoader.Config;

namespace FullBodyAccessories
{
    public class FBAConfig : ModConfig
    {
        public static FBAConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [Label("Hide draggable panel")]
        public bool HidePanel;

        public override void OnChanged()
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            if (ui == null) return;

            ((FBAMod)mod).SlotUI.Panel.Visible = !HidePanel;
        }
    }
}
