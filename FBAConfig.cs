using System.ComponentModel;
using FullBodyAccessories.UI;
using Terraria.ModLoader.Config;

namespace FullBodyAccessories
{
    public class FBAConfig : ModConfig
    {
        private bool firstLoad = true;

        public static FBAConfig Instance;
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        [Tooltip("Resets all mod settings, including panel location\n" +
                 "Click \"Save Config\" to see changes")]
        [Label("Reset mod settings")]
        public bool ResetSettings;
        
        public override void OnChanged()
        {
            if (firstLoad)
            {
                firstLoad = false;
                return;
            }

            if (!ResetSettings) return;

            FBAMod fbaMod = (FBAMod)mod;
            fbaMod.SlotUI.Panel.Left.Pixels = FBAUIState.DefaultX;
            fbaMod.SlotUI.Panel.Top.Pixels = FBAUIState.DefaultY;

            ResetSettings = false;
        }
    }
}
