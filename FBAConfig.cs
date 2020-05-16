using System.ComponentModel;
using FullBodyAccessories.UI;
using Terraria.ModLoader.Config;

namespace FullBodyAccessories
{
    public class FBAConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [Label("Hide draggable panel")]
        public bool HidePanel;

        public override void OnChanged()
        {
            var ui = FBAMod.Instance.SlotUI;
            if (ui == default) return;

            ui.Panel.Visible = !HidePanel;
        }
    }
}
