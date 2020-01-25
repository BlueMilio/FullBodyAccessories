using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class WaistCategory : Category
    {
        public WaistCategory() : base("Waist")
        {
            Register(BlackBelt, MonkBelt, Toolbelt);
            Register(CloudinaBottle, BlizzardinaBottle, SandstorminaBottle);
            Register(CopperWatch, TinWatch, SilverWatch, TungstenWatch, GoldWatch, PlatinumWatch);
            Register(YoyoBag);
        }
    }
}