using System;
using System.Collections.Generic;
using CustomSlot;
using FullBodyAccessories.Categories;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class CategorizedSlotGroup
    {
        public enum PanelSide
        {
            None,
            Left,
            Right
        }

        public enum SlotId
        {
            Equip,
            Social,
            Dye
        }


        public CategorizedSlotGroup(int x, int y, Category category, PanelSide side)
        {
            Category = category;
            Side = side;

            bool IsValidItem(Item item)
            {
                Dictionary<int, Predicate<Item>> weakItemConditions = FBAMod.Instance.WeakItemConditions;

                return Category.Has(item) && (!weakItemConditions.ContainsKey(item.type) || weakItemConditions[item.type](item)) && 
                       (item.modItem == null || item.modItem is IFBAAccessory acc && acc.IsValidItem(this));
            };

            string sideText = Side == PanelSide.None ? "" : Side.ToString();
            CroppedTexture2D emptyTexture = new CroppedTexture2D(FBAMod.Instance.GetTexture($"Textures/{sideText}{category.Name}"), 
                                                                 CustomItemSlot.DefaultColors.EmptyTexture);

            Slot = new CustomItemSlot(ItemSlot.Context.EquipAccessory)
            {
                EmptyTexture = emptyTexture,
                HoverText = Language.GetTextValue($"Mods.FullBodyAccessories.{sideText}{Category.Name}"),
                IsValidItem = IsValidItem
            };

            SocialSlot = new CustomItemSlot(ItemSlot.Context.EquipAccessoryVanity)
            {
                EmptyTexture = emptyTexture,
                HoverText = Language.GetTextValue($"Mods.FullBodyAccessories.Social{sideText}{Category.Name}"),
                IsValidItem = IsValidItem
            };

            DyeSlot = new CustomItemSlot(ItemSlot.Context.EquipDye)
            {
                IsValidItem = item => item.dye > 0
            };


            Slots = new CustomItemSlot[3];
            Slots[(int) SlotId.Equip] = Slot;
            Slots[(int) SlotId.Social] = SocialSlot;
            Slots[(int) SlotId.Dye] = DyeSlot;

            Slot.Left.Set(x, 0);
            SocialSlot.Left.Set(x, 0);
            DyeSlot.Left.Set(x, 0);

            Slot.Top.Set(y, 0);
            SocialSlot.Top.Set(y, 0);
            DyeSlot.Top.Set(y, 0);
        }

        public TagCompound GetTags()
        {
            return new TagCompound
            {
                { VisibleTag, Slot.ItemVisible },
                { ItemTag, ItemIO.Save(Slot.Item) },
                { SocialTag, ItemIO.Save(SocialSlot.Item) },
                { DyeTag, ItemIO.Save(DyeSlot.Item) }
            };
        }

        public void Load(TagCompound tag)
        {
            Slot.Item = ItemIO.Load(tag.GetCompound(ItemTag));
            SocialSlot.Item = ItemIO.Load(tag.GetCompound(SocialTag));
            DyeSlot.Item = ItemIO.Load(tag.GetCompound(DyeTag));

            Slot.ItemVisible = tag.GetBool(VisibleTag);
        }

        private string TagPrefix => $"{(Side == PanelSide.None ? "" : Side.ToString())}{Category.Name}";


        private string VisibleTag => $"{TagPrefix}Visible";
        private string ItemTag => $"{TagPrefix}Item";
        private string SocialTag => $"{TagPrefix}Social";
        private string DyeTag => $"{TagPrefix}Dye";

        public Category Category { get; }
        public CustomItemSlot Slot { get; }
        public CustomItemSlot SocialSlot { get; }
        public CustomItemSlot DyeSlot { get; }
        public CustomItemSlot[] Slots { get; }

        public PanelSide Side { get; }
    }
}
