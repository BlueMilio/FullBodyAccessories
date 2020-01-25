using System.Collections.Generic;
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
	}
}