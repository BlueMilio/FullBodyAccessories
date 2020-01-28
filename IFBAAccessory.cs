using FullBodyAccessories.UI;

namespace FullBodyAccessories
{
    public interface IFBAAccessory
    {
        bool IsValidItem(CategorizedSlotGroup slotGroup);
    }
}