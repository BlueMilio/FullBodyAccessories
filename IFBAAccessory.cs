using FullBodyAccessories.UI;
using Terraria.ModLoader;

namespace FullBodyAccessories
{
    public interface IFBAAccessory
    {
        bool IsValidItem(CategorizedSlotGroup slotGroup);
    }
}