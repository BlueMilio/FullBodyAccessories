using System;
using System.Collections.Generic;
using FullBodyAccessories.Categories;
using FullBodyAccessories.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace FullBodyAccessories
{
    public class FBAMod : Mod
    {
        private UserInterface _slotInterface;
        public FBAUIState SlotUI;

        // TODO: webmilio fix to use CategoryLoader
        public static ArmCategory ArmCategory { get; } = new ArmCategory();
        public static BackCategory BackCategory { get; } = new BackCategory();
        public static FootCategory FootCategory { get; } = new FootCategory();
        public static HeadAltCategory HeadAltCategory { get; } = new HeadAltCategory();
        public static HeadCategory HeadCategory { get; } = new HeadCategory();
        public static NeckCategory NeckCategory { get; } = new NeckCategory();
        public static RingCategory RingCategory { get; } = new RingCategory();
        public static WaistCategory WaistCategory { get; } = new WaistCategory();

        public FBAMod()
        {
            Instance = this;
        }


        public override void Load()
        {
            if (!Main.dedServ)
            {
                _slotInterface = new UserInterface();
                SlotUI = new FBAUIState();

                SlotUI.Activate();
                _slotInterface.SetState(SlotUI);
            }
        }

        public override void Unload()
        {
            Instance = null;
        }


        public override object Call(params object[] args)
        {
            if (!(args[0] is string cmdName))
                throw new Exception("Invalid Call: first parameter must be a string.");


            cmdName = cmdName.ToLower();


            if (cmdName.Equals("additem"))
            {
                if (args.Length < 3)
                    throw new ArgumentException("Format is \"additem\" \"category name\" itemType [option: Predicate<Item>, item => ... condition here]");


                if (args.Length < 2 || !(args[1] is string categoryName))
                    throw new ArgumentException("Second argument must be the name of the category.");

                if (!CategoryLoader.Instance.HasCategory(categoryName))
                    throw new ArgumentException($"Category \"{categoryName}\" not found.");


                Category category = CategoryLoader.Instance.ItemCategory(categoryName);


                if (args.Length < 3 || !(args[2] is int itemType))
                    throw new ArgumentException("Third argument must be a item type (integer).");

                if (ModContent.GetModItem(itemType) == default)
                    throw new ArgumentException($"No item for type {itemType}.");


                category.Register(itemType);

                if (args.Length >= 4)
                {
                    if (!(args[3] is Predicate<Item> predicate))
                        throw new ArgumentException("Fourth argument must be a predicate.");

                    WeakItemConditions.Add(itemType, predicate);
                }
            }

            return default;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (SlotUI.Visible)
            {
                _slotInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

            if (inventoryLayer != -1)
            {
                layers.Insert(
                    inventoryLayer,
                    new LegacyGameInterfaceLayer("FullBodyAccessories: Slot UI", () =>
                    {
                        if (SlotUI.Visible)
                        {
                            _slotInterface.Draw(Main.spriteBatch, new GameTime());
                        }

                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }


        public static FBAMod Instance { get; private set; }

        internal Dictionary<int, Predicate<Item>> WeakItemConditions { get; } = new Dictionary<int, Predicate<Item>>();
    }
}