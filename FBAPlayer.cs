using FullBodyAccessories.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FullBodyAccessories
{
    public class FBAPlayer : ModPlayer
    {
        private const string XPositionTag = "xPos";
        private const string YPositionTag = "yPos";

        public override TagCompound Save()
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            return new TagCompound
            {
                { XPositionTag, ui.Panel.Left.Pixels },
                { YPositionTag, ui.Panel.Top.Pixels }
            };
        }

        public override void Load(TagCompound tag)
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            ui.Panel.Left.Set(tag.GetFloat(XPositionTag), 0);
            ui.Panel.Top.Set(tag.GetFloat(YPositionTag), 0);
        }
    }
}
