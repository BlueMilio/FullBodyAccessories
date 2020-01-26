using FullBodyAccessories.Categories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class FBAUIState : UIState
    {
        private const int SlotMargin = 6;
        private const int TextMargin = 15;

        private int _lastPage = -1;
        private int _currentPage = 0;

        public DraggableUIPanel Panel { get; private set; }
        public UIText[] TextButtons { get; private set; }

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
            string equip = Language.GetTextValue("Equip");
            string social = Language.GetTextValue("Social");
            Vector2 equipSize = Main.fontMouseText.MeasureString(equip);

            void onMouseOver(UIMouseEvent evt, UIElement element)
            {
                if (element is UIText text && text.TextColor != Color.White)
                {
                    text.TextColor = Color.LightGray;
                }
            }

            void onMouseOut(UIMouseEvent evt, UIElement element)
            {
                if (element is UIText text && text.TextColor != Color.White)
                {
                    text.TextColor = Color.Gray;
                }
            }

            Panel = new DraggableUIPanel();

            y = (int)equipSize.Y;

            // create the buttons to change equipment type
            TextButtons = new UIText[3];

            TextButtons[0] = new UIText(equip);
            TextButtons[0].OnClick += (evt, element) => _currentPage = 0;
            TextButtons[0].OnMouseOver += onMouseOver;
            TextButtons[0].OnMouseOut += onMouseOut;
            Panel.Append(TextButtons[0]);

            TextButtons[1] = new UIText(social)
            {
                TextColor = Color.Gray
            };
            TextButtons[1].Left.Set(equipSize.X + TextMargin, 0);
            TextButtons[1].OnClick += (evt, element) => _currentPage = 1;
            TextButtons[1].OnMouseOver += onMouseOver;
            TextButtons[1].OnMouseOut += onMouseOut;
            Panel.Append(TextButtons[1]);

            TextButtons[2] = new UIText(Language.GetTextValue("Dye"))
            {
                TextColor = Color.Gray
            };
            TextButtons[2].Left.Set(TextButtons[1].Left.Pixels + Main.fontMouseText.MeasureString(social).X + TextMargin, 0);
            TextButtons[2].OnClick += (evt, element) => _currentPage = 2;
            TextButtons[2].OnMouseOver += onMouseOver;
            TextButtons[2].OnMouseOut += onMouseOut;
            Panel.Append(TextButtons[2]);

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
            Panel.Height.Set(slotOffset * 4 + equipSize.Y + Panel.PaddingTop + Panel.PaddingBottom, 0);

            Append(Panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_currentPage != _lastPage)
            {
                AppendToPanel(BackGroup, HeadGroup, LeftArmGroup, NeckGroup, RightArmGroup, LeftRingGroup, WaistGroup,
                       RightRingGroup, LeftFootGroup, RightFootGroup);

                if (_lastPage > -1)
                    TextButtons[_lastPage].TextColor = Color.Gray;

                TextButtons[_currentPage].TextColor = Color.White;
                _lastPage = _currentPage;
            }
        }

        private void AppendToPanel(params CategorizedSlotGroup[] groups)
        {
            foreach (CategorizedSlotGroup group in groups)
            {
                if (_currentPage == 0)
                {
                    Panel.Append(group.Slot);
                    Panel.RemoveChild(group.SocialSlot);
                    Panel.RemoveChild(group.DyeSlot);
                }
                else if (_currentPage == 1)
                {
                    Panel.RemoveChild(group.Slot);
                    Panel.Append(group.SocialSlot);
                    Panel.RemoveChild(group.DyeSlot);
                }
                else if (_currentPage == 2)
                {
                    Panel.RemoveChild(group.Slot);
                    Panel.RemoveChild(group.SocialSlot);
                    Panel.Append(group.DyeSlot);
                }
            }
        }
    }
}
