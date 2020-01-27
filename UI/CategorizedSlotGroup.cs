using CustomSlot;
using FullBodyAccessories.Categories;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class CategorizedSlotGroup
    {
        public enum Side
        {
            None,
            Left,
            Right
        }

        public Category Category { get; }
        public CustomItemSlot Slot { get; }
        public CustomItemSlot SocialSlot { get; }
        public CustomItemSlot DyeSlot { get; }

        public CategorizedSlotGroup(int x, int y, Category category, Side side)
        {
            Category = category;

            bool IsValidItem(Item item)
            {
                return Category.Has(item) && (item.modItem == null 
                                              || item.modItem is IFBAAccessory acc && acc.IsValidItem(this));
            };

            string sideText = side == Side.None ? "" : side.ToString();
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
    }
}
