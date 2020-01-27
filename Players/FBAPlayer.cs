using FullBodyAccessories.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FullBodyAccessories.Players
{
    public class FBAPlayer : ModPlayer
    {
        private const string 
            X_POSITION_TAG = "xPos",
            Y_POSITION_TAG = "yPos";


        public override TagCompound Save()
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            return new TagCompound
            {
                { X_POSITION_TAG, ui.Panel.Left.Pixels },
                { Y_POSITION_TAG, ui.Panel.Top.Pixels }
            };
        }

        public override void Load(TagCompound tag)
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            ui.Panel.Left.Set(tag.GetFloat(X_POSITION_TAG), 0);
            ui.Panel.Top.Set(tag.GetFloat(Y_POSITION_TAG), 0);
        }
    }
}
