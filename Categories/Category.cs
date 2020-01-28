using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace FullBodyAccessories.Categories
{
    public class Category
    {
        private readonly List<int> _allowedItems = new List<int>();


        public Category(string name, string displayName = default)
        {
            Name = name;
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? name : displayName;
        }


        public void Register<T>() where T : ModItem => Register(ModContent.ItemType<T>());
        public void Register(ModItem modItem) => Register(modItem.item);
        public void Register(Item item) => Register(item.type);

        public void Register(params int[] itemType)
        {
            _allowedItems.AddRange(itemType);
        }


        public void Unregister<T>() where T : ModItem => Unregister(ModContent.ItemType<T>());
        public void Unregister(ModItem modItem) => Unregister(modItem.item);
        public void Unregister(Item item) => Unregister(item.type);

        public void Unregister(params int[] itemType)
        {
            var itemTypeList = itemType.ToList();
            _allowedItems.RemoveAll(t => itemTypeList.Contains(t));
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