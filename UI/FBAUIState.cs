using System.Linq;
using FullBodyAccessories.Categories;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class FBAUIState : UIState
    {
        private const int SlotMargin = 6;

        public const int DefaultX = 200;
        public const int DefaultY = 300;

        public DraggableUIPanel Panel { get; private set; }
        public UITabGroup Tabs { get; private set; }
        public CategorizedSlotGroup[] SlotGroups { get; private set; }

        public bool Visible => Main.playerInventory;

        public override void OnInitialize()
        {
            int x = 0;
            int slotSize = Main.inventoryBackTexture.Width;
            int slotOffset = slotSize + SlotMargin;

            Panel = new DraggableUIPanel();

            // create the buttons to change equipment type
            Tabs = new UITabGroup(new[]
            {
                Language.GetTextValue("Equip"), Language.GetTextValue("Social"), Language.GetTextValue("Dye")
            });
            Tabs.OnSelectedIndexChanged += (evt, lastTab, currentTab) =>
            {
                AppendToPanel(currentTab);
            };
            Panel.Append(Tabs);

            int y = (int)Tabs.Height.Pixels;

            // create the equipment slot groups
            SlotGroups = new[]
            {
                // back
                new CategorizedSlotGroup(x, y, FBAMod.BackCategory, CategorizedSlotGroup.Side.None), 
                // head
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.HeadCategory, CategorizedSlotGroup.Side.None), 
                // left arm
                new CategorizedSlotGroup(x -= slotOffset, y += slotOffset, FBAMod.ArmCategory, 
                                         CategorizedSlotGroup.Side.Left),
                // neck
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.NeckCategory, CategorizedSlotGroup.Side.None), 
                // right arm
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.ArmCategory, CategorizedSlotGroup.Side.Right), 
                // left ring
                new CategorizedSlotGroup(x -= slotOffset * 2, y += slotOffset, FBAMod.RingCategory, 
                                         CategorizedSlotGroup.Side.Left),
                // waist
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.WaistCategory, CategorizedSlotGroup.Side.None), 
                // right ring
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.RingCategory, CategorizedSlotGroup.Side.Right), 
                // left foot
                new CategorizedSlotGroup(x -= slotOffset * 2 - (slotSize / 2), y += slotOffset, FBAMod.FootCategory,
                                         CategorizedSlotGroup.Side.Left), 
                // right foot
                new CategorizedSlotGroup(x += slotOffset, y, FBAMod.FootCategory, CategorizedSlotGroup.Side.Right)
            };

            Panel.Left.Set(DefaultX, 0);
            Panel.Top.Set(DefaultY, 0);
            Panel.Width.Set(slotOffset * 3 + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(slotOffset * 4 + Tabs.Height.Pixels + Panel.PaddingTop + Panel.PaddingBottom, 0);

            AppendToPanel(0);
            Append(Panel);
        }

        private void AppendToPanel(int tab)
        {
            foreach (CategorizedSlotGroup group in SlotGroups)
            {
                if (tab == 0)
                {
                    Panel.Append(group.Slot);
                    Panel.RemoveChild(group.SocialSlot);
                    Panel.RemoveChild(group.DyeSlot);
                }
                else if (tab == 1)
                {
                    Panel.RemoveChild(group.Slot);
                    Panel.Append(group.SocialSlot);
                    Panel.RemoveChild(group.DyeSlot);
                }
                else if (tab == 2)
                {
                    Panel.RemoveChild(group.Slot);
                    Panel.RemoveChild(group.SocialSlot);
                    Panel.Append(group.DyeSlot);
                }
            }
        }
    }
}
