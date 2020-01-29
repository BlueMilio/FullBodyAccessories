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

        private string VisibleTag => $"{GetTagPrefix()}Visible";
        private string ItemTag => $"{GetTagPrefix()}Item";
        private string SocialTag => $"{GetTagPrefix()}Social";
        private string DyeTag => $"{GetTagPrefix()}Dye";

        public Category Category { get; }
        public CustomItemSlot Slot { get; }
        public CustomItemSlot SocialSlot { get; }
        public CustomItemSlot DyeSlot { get; }
        public PanelSide Side { get; }

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
            FBAMod mod = ModContent.GetInstance<FBAMod>();
            CroppedTexture2D emptyTexture = new CroppedTexture2D(mod.GetTexture($"Textures/{sideText}{category.Name}"), 
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

        private string GetTagPrefix()
        {
            string side = Side == PanelSide.None ? "" : Side.ToString();

            return $"{side}{Category.Name}";
        }
    }
}
