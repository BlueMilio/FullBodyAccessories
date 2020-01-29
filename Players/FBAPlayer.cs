using System.Linq;
using FullBodyAccessories.Categories;
using FullBodyAccessories.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FullBodyAccessories.Players
{
    public class FBAPlayer : ModPlayer
    {
        private const string
            XPositionTag = "XPosition",
            YPositionTag = "YPosition";

        public override TagCompound Save()
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            TagCompound tags = new TagCompound {
                { XPositionTag, ui.Panel.Left.Pixels },
                { YPositionTag, ui.Panel.Top.Pixels }
            };

            foreach (CategorizedSlotGroup group in ui.SlotGroups)
            {
                group.GetTags().ToList().ForEach(t => tags.Add(t.Key, t.Value));
            }

            return tags;
        }

        public override void Load(TagCompound tag)
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            ui.Panel.Left.Set(tag.GetFloat(XPositionTag), 0);
            ui.Panel.Top.Set(tag.GetFloat(YPositionTag), 0);

            ui.SlotGroups.ToList().ForEach(g => g.Load(tag));
        }
    }
}
