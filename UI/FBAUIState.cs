using FullBodyAccessories.Categories;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class FBAUIState : UIState
    {
        private const int SlotMargin = 6;

        public DraggableUIPanel Panel { get; private set; }
        public UITabGroup Tabs { get; private set; }

        public CategorizedSlotGroup BackGroup { get; private set; }
        public CategorizedSlotGroup HeadGroup { get; private set; }
        public CategorizedSlotGroup LeftArmGroup { get; private set; }
        public CategorizedSlotGroup NeckGroup { get; private set; }
        public CategorizedSlotGroup RightArmGroup { get; private set; }
        public CategorizedSlotGroup LeftRingGroup { get; private set; }
        public CategorizedSlotGroup WaistGroup { get; private set; }
        public CategorizedSlotGroup RightRingGroup { get; private set; }
        public CategorizedSlotGroup LeftFootGroup { get; private set; }
        public CategorizedSlotGroup RightFootGroup { get; private set; }

        public bool Visible => Main.playerInventory;

        public override void OnInitialize()
        {
            int x = 0;
            int y = 0;
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
                AppendAllCategories(currentTab);
            };
            Panel.Append(Tabs);

            y = (int)Tabs.Height.Pixels;

            // create the equipment slot groups
            // TODO: replace new Category() calls with more accessible variables
            BackGroup = new CategorizedSlotGroup(
                x, y, new BackCategory(), CategorizedSlotGroup.Side.None);
            HeadGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new HeadCategory(), CategorizedSlotGroup.Side.None);
            LeftArmGroup = new CategorizedSlotGroup(
                x -= slotOffset, y += slotOffset, new ArmCategory(), CategorizedSlotGroup.Side.Left);
            NeckGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new NeckCategory(), CategorizedSlotGroup.Side.None);
            RightArmGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new ArmCategory(), CategorizedSlotGroup.Side.Right);
            LeftRingGroup = new CategorizedSlotGroup(
                x -= slotOffset * 2, y += slotOffset, new RingCategory(), CategorizedSlotGroup.Side.Left);
            WaistGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new WaistCategory(), CategorizedSlotGroup.Side.None);
            RightRingGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new RingCategory(), CategorizedSlotGroup.Side.Right);
            LeftFootGroup = new CategorizedSlotGroup(
                x -= slotOffset * 2 - (slotSize / 2), y += slotOffset, new FootCategory(), CategorizedSlotGroup.Side.Left);
            RightFootGroup = new CategorizedSlotGroup(
                x += slotOffset, y, new FootCategory(), CategorizedSlotGroup.Side.Right);

            Panel.Left.Set(500, 0);
            Panel.Top.Set(300, 0);
            Panel.Width.Set(slotOffset * 3 + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(slotOffset * 4 + Tabs.Height.Pixels /*equipSize.Y*/ + Panel.PaddingTop + Panel.PaddingBottom, 0);

            Append(Panel);
            AppendAllCategories(0);
        }

        private void AppendAllCategories(int tab)
        {
            AppendToPanel(tab, BackGroup, HeadGroup, LeftArmGroup, NeckGroup, RightArmGroup, LeftRingGroup,
                          WaistGroup, RightRingGroup, LeftFootGroup, RightFootGroup);
        }

        private void AppendToPanel(int tab, params CategorizedSlotGroup[] groups)
        {
            foreach (CategorizedSlotGroup group in groups)
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
