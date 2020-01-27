using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Loaders;

namespace FullBodyAccessories.Categories
{
    public sealed class CategoryLoader : SingletonLoader<CategoryLoader, Category>
    {
        private readonly Dictionary<string, Category> _categoryNames = new Dictionary<string, Category>(new CategoryNameEqualityComparer());
        private readonly Dictionary<int, Category> _itemCategories = new Dictionary<int, Category>();


        protected override void PostAdd(Mod mod, Category category, Type type)
        {
            _categoryNames.Add(category.Name, category);

            int[] itemTypes = category.AllowedItems;

            for (int i = 0; i < itemTypes.Length; i++)
                _itemCategories.Add(itemTypes[i], category);
        }


        public void RegisterItem<T>(Category category) where T : ModItem => category.Register<T>();
        public void RegisterItem(Category category, ModItem modItem) => category.Register(modItem);
        public void RegisterItem(Category category, Item item) => category.Register(item);
        public void RegisterItem(Category category, int itemType) => category.Register(itemType);

        public bool HasCategory(int itemType) => _itemCategories.ContainsKey(itemType);
        public bool HasCategory(Item item) => HasCategory(item.type);
        public bool HasCategory(ModItem modItem) => HasCategory(modItem.item);
        public bool HasCategory<T>() where T : ModItem => HasCategory(ModContent.ItemType<T>());

        public Category ItemCategory(int itemType) => _itemCategories[itemType];
        public Category ItemCategory(Item item) => ItemCategory(item.type);
        public Category ItemCategory(ModItem modItem) => ItemCategory(modItem.item);
        public Category ItemCategory<T>() where T : ModItem => ItemCategory(ModContent.ItemType<T>());
        public Category ItemCategory(string categoryName) => _categoryNames[categoryName];


        public bool HasCategory(string categoryName) => _categoryNames.ContainsKey(categoryName);


        private class CategoryNameEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Equals(y, StringComparison.CurrentCultureIgnoreCase);

            public int GetHashCode(string obj) => obj.GetHashCode();
        }
    }
}