using System;
using FullBodyAccessories.Categories;
using Terraria;
using Terraria.UI;

namespace FullBodyAccessories.Slots
{
    public class CategorizedItemSlot : CustomItemSlot
    {
        private Predicate<Item> _validItemCheck;


        public CategorizedItemSlot(Category category, int context = ItemSlot.Context.InventoryItem, float scale = 1f,
            ArmorType defaultArmorIcon = ArmorType.Head) : base(context, scale, defaultArmorIcon)
        {
            Category = category;
        }


        protected virtual bool CompoundIsValidItem(Item item) => _validItemCheck(item) && Category.Has(item);


        public override Predicate<Item> IsValidItem
        {
            get => _validItemCheck == default ? (Predicate<Item>) Category.Has : CompoundIsValidItem;
            set => _validItemCheck = value;
        }

        public Category Category { get; }
    }
}