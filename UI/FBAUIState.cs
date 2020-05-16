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

        public CustomUIPanel Panel { get; private set; }
        public UITabGroup Tabs { get; private set; }
        public CategorizedSlotGroup[] SlotGroups { get; private set; }

        public bool Visible => Main.playerInventory;

        public override void OnInitialize()
        {
            int x = 0;
            int slotSize = Main.inventoryBackTexture.Width;
            int slotOffset = slotSize + SlotMargin;

            Panel = new CustomUIPanel();

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
                new CategorizedSlotGroup(x, y, CategoryLoader.Instance.GetGeneric<BackCategory>(), CategorizedSlotGroup.PanelSide.None), 
                // head
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<HeadCategory>(), CategorizedSlotGroup.PanelSide.None), 
                // left arm
                new CategorizedSlotGroup(x -= slotOffset, y += slotOffset, CategoryLoader.Instance.GetGeneric<ArmCategory>(), CategorizedSlotGroup.PanelSide.Left),
                // neck
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<NeckCategory>(), CategorizedSlotGroup.PanelSide.None), 
                // right arm
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<ArmCategory>(), CategorizedSlotGroup.PanelSide.Right), 
                // left ring
                new CategorizedSlotGroup(x -= slotOffset * 2, y += slotOffset, CategoryLoader.Instance.GetGeneric<RingCategory>(), CategorizedSlotGroup.PanelSide.Left),
                // waist
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<WaistCategory>(), CategorizedSlotGroup.PanelSide.None), 
                // right ring
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<RingCategory>(), CategorizedSlotGroup.PanelSide.Right), 
                // left foot
                new CategorizedSlotGroup(x -= slotOffset * 2 - (slotSize / 2), y += slotOffset, CategoryLoader.Instance.GetGeneric<FootCategory>(), CategorizedSlotGroup.PanelSide.Left), 
                // right foot
                new CategorizedSlotGroup(x += slotOffset, y, CategoryLoader.Instance.GetGeneric<FootCategory>(), CategorizedSlotGroup.PanelSide.Right)
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
