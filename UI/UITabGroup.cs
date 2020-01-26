using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class UITabGroup : UIElement
    {
        public delegate void SelectedTabChangedEvent(UIEvent evt, UIText lastTab, UIText currentTab);

        public delegate void SelectedIndexChangedEvent(UIEvent evt, int lastTab, int currentTab);

        public event SelectedIndexChangedEvent OnSelectedIndexChanged;
        public event SelectedTabChangedEvent OnSelectedTabChanged;

        private UIText _selectedTab = null;

        public Color SelectedColor { get; set; }
        public Color DeselectedColor { get; set; }
        public Color SelectedHoverColor { get; set; }
        public Color DeselectedHoverColor { get; set; }
        public UIText[] Tabs { get; }
        public int TabMargin { get; set; }

        public UIText SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (Array.Find(Tabs, tab => tab == value) != null)
                {
                    ChangeSelectedTab(value);
                }
            }
        }

        public int SelectedIndex
        {
            get => Array.IndexOf(Tabs, SelectedTab);
            set
            {
                if (value > -1 && value < Tabs.Length)
                {
                    ChangeSelectedTab(Tabs[value]);
                }
            }
        }

        public UITabGroup(string[] tabNames) : this(tabNames.ToList()) { }

        public UITabGroup(List<string> tabNames)
        {
            Tabs = new UIText[tabNames.Count];
            SelectedColor = Color.White;
            DeselectedColor = Color.Gray;
            SelectedHoverColor = Color.LightGray;
            DeselectedHoverColor = SelectedHoverColor;
            TabMargin = 10;

            InitializeTabs(tabNames, out float width, out float height);

            SelectedIndex = 0;
            Width.Set(width, 0);
            Height.Set(height, 0);
        }

        private void InitializeTabs(List<string> tabNames, out float width, out float height)
        {
            Vector2 firstTabSize = Main.fontMouseText.MeasureString(tabNames[0]);
            float tabLeft = firstTabSize.X;

            for (int i = 0; i < Tabs.Length; i++)
            {
                UIText tab = new UIText(tabNames[i])
                {
                    TextColor = DeselectedColor
                };

                tab.OnClick += (evt, element) =>
                {
                    SelectedTab = tab;
                    Main.PlaySound(SoundID.MenuTick);
                };

                tab.OnMouseOver += (evt, element) =>
                {
                    tab.TextColor = SelectedTab == tab ? SelectedHoverColor : DeselectedHoverColor;
                };

                tab.OnMouseOut += (evt, element) =>
                {
                    tab.TextColor = SelectedTab == tab ? SelectedColor : DeselectedColor;
                };

                if (i > 0)
                {
                    tab.Left.Set(tabLeft + TabMargin, 0);
                    tabLeft = tab.Left.Pixels + Main.fontMouseText.MeasureString(tab.Text).X;
                }

                Append(tab);
                Tabs[i] = tab;
            }

            width = tabLeft;
            height = firstTabSize.Y;
        }

        public virtual void SelectedTabChanged(UIEvent evt, UIText lastTab, UIText currentTab)
        {
            OnSelectedTabChanged?.Invoke(evt, lastTab, currentTab);
        }

        public virtual void SelectedIndexChanged(UIEvent evt, int lastTab, int currentTab)
        {
            OnSelectedIndexChanged?.Invoke(evt, lastTab, currentTab);
        }

        private void ChangeSelectedTab(UIText newTab)
        {
            int tabIndex = SelectedIndex;
            UIText tab = SelectedTab;

            if (_selectedTab != null)
                _selectedTab.TextColor = DeselectedColor;

            _selectedTab = newTab;
            _selectedTab.TextColor = SelectedColor;

            UIEvent evt = new UIEvent(this);

            SelectedTabChanged(evt, tab, SelectedTab);
            SelectedIndexChanged(evt, tabIndex, SelectedIndex);
        }
    }
}
