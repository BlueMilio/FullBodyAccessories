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

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            foreach (CategorizedSlotGroup group in ui.SlotGroups)
            {
                switch (group.Category)
                {
                    case ArmCategory _:

                        break;
                    case BackCategory _:

                        break;
                    case FootCategory _:

                        break;
                    case HeadCategory _:

                    case HeadAltCategory _:

                        break;
                    case NeckCategory _:

                        break;
                    case RingCategory _:

                        break;
                    case WaistCategory _:
                        if (HasDye(group))
                        {
                            drawInfo.waistShader = group.DyeSlot.Item.dye;
                        }
                        break;
                }
            }
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            FBAUIState ui = ((FBAMod)mod).SlotUI;

            foreach (CategorizedSlotGroup group in ui.SlotGroups)
            {
                if (group.Slot.Item.stack > 0)
                {
                    player.VanillaUpdateAccessory(player.whoAmI, group.Slot.Item, !group.Slot.ItemVisible,
                                                  ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
                    player.VanillaUpdateEquip(group.Slot.Item);
                }

                if (group.SocialSlot.Item.stack > 0)
                {
                    player.VanillaUpdateVanityAccessory(group.SocialSlot.Item);
                }
            }
        }

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

        private static bool HasDye(CategorizedSlotGroup group)
        {
            return group.DyeSlot.Item.stack > 0
                   && (group.Slot.Item.stack > 0 || group.SocialSlot.Item.stack > 0);
        }
    }
}
