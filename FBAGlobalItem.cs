using System.Collections.Generic;
using FullBodyAccessories.Categories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FullBodyAccessories
{
    public sealed class FBAGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            CategoryLoader categoryLoader = CategoryLoader.Instance;

            if (!categoryLoader.HasCategory(item))
                return;

            Category category = categoryLoader.ItemCategory(item);

            tooltips.Add(new TooltipLine(mod, "FBA_CATEGORY", '[' + category.DisplayName + ']')
            {
                // TODO Change color.
                overrideColor = Color.LightSkyBlue
            });
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            var categoryLoader = CategoryLoader.Instance;

            if (!categoryLoader.HasCategory(item))
                return true;

            return slot == 0;
        }
    }
}