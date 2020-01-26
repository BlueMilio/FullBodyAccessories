using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace FullBodyAccessories.Categories
{
    public class Category
    {
        private readonly List<int> _allowedItems = new List<int>();


        public Category(string name, string displayName = "")
        {
            Name = name;
            DisplayName = string.IsNullOrEmpty(displayName) ? name : displayName;
        }


        public void Register<T>() where T : ModItem => Register(ModContent.ItemType<T>());

        public void Register(params int[] itemType)
        {
            foreach(int type in itemType)
                _allowedItems.Add(type);
        }


        public bool Has(int itemType) => _allowedItems.Contains(itemType);
        public bool Has(Item item) => Has(item.type);
        public bool Has(ModItem modItem) => Has(modItem.item);
        public bool Has<T>() where T : ModItem => Has(ModContent.ItemType<T>());


        public string Name { get; }
        public string DisplayName { get; }

        public int[] AllowedItems => _allowedItems.ToArray();


        public bool this[Item item] => this[item.type];
        public bool this[ModItem modItem] => this[modItem.item];
        public bool this[int itemId] => _allowedItems.Contains(itemId);
    }
}