using System.Collections.Generic;
using System.Linq;
using CustomSlot;
using FullBodyAccessories.Categories;
using FullBodyAccessories.Network;
using FullBodyAccessories.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;

namespace FullBodyAccessories.Players
{
    public class FBAPlayer : ModPlayer
    {
        private const string
            X_POSITION_TAG = "XPosition",
            Y_POSITION_TAG = "YPosition";


        private CategorizedSlotGroup[] _slotGroups;
        private CustomItemSlot[] _itemSlots;


        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            FBAUIState ui = FBAMod.Instance.SlotUI;

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
            foreach (CategorizedSlotGroup group in SlotGroups)
            {
                if (group.Slot.Item.stack > 0)
                {
                    player.VanillaUpdateAccessory(player.whoAmI, group.Slot.Item, !group.Slot.ItemVisible, ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
                    player.VanillaUpdateEquip(group.Slot.Item);
                }

                if (group.SocialSlot.Item.stack > 0)
                {
                    player.VanillaUpdateVanityAccessory(group.SocialSlot.Item);
                }
            }
        }

        private static bool HasDye(CategorizedSlotGroup group)
        {
            return group.DyeSlot.Item.stack > 0
                   && (group.Slot.Item.stack > 0 || group.SocialSlot.Item.stack > 0);
        }


        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (player.difficulty == 0)
                return;

            // TODO Check if we need everyone to spawn the items.
            if (!player.IsLocalPlayer())
                return;


            CustomItemSlot[] slots = Slots;

            for (int i = 0; i < slots.Length; i++)
            {
                player.QuickSpawnClonedItem(slots[i].Item);
                slots[i].Item = new Item();
            }
        }


        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) => new PlayerSynchronizationPacket(this).Send(fromWho, toWho);


        public override TagCompound Save()
        {
            FBAUIState ui = FBAMod.Instance.SlotUI;

            TagCompound tags = new TagCompound {
                { X_POSITION_TAG, ui.Panel.Left.Pixels },
                { Y_POSITION_TAG, ui.Panel.Top.Pixels }
            };

            foreach (CategorizedSlotGroup group in SlotGroups)
            {
                group.GetTags().ToList().ForEach(t => tags.Add(t.Key, t.Value));
            }

            return tags;
        }

        public override void Load(TagCompound tag)
        {
            FBAUIState ui = FBAMod.Instance.SlotUI;

            ui.Panel.Left.Set(tag.GetFloat(X_POSITION_TAG), 0);
            ui.Panel.Top.Set(tag.GetFloat(Y_POSITION_TAG), 0);

            ui.SlotGroups.ToList().ForEach(g => g.Load(tag));
        }


        public CategorizedSlotGroup[] SlotGroups
        {
            get
            {
                if (this.IsLocalPlayer())
                    return FBAMod.Instance.SlotUI.SlotGroups;

                return _slotGroups ?? (_slotGroups = new CategorizedSlotGroup[FBAMod.Instance.SlotUI.SlotGroups.Length]);
            }
        }

        public CustomItemSlot[] Slots
        {
            get
            {
                if (_itemSlots != null)
                    return _itemSlots;


                List<CustomItemSlot> slots = new List<CustomItemSlot>(SlotGroups.Length * 3);


                foreach (var slotGroup in SlotGroups)
                    foreach (var itemSlot in slotGroup.Slots)
                        slots.Add(itemSlot);


                return (_itemSlots = slots.ToArray());
            }
        }
    }
}
