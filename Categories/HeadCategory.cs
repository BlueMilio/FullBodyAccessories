using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class HeadCategory : Category
    {
        public HeadCategory() : base("Head")
        {
            Register(ArcticDivingGear, DivingGear, JellyfishDivingGear);
            Register(Blindfold);
            Register(ObsidianSkull);

            // Vanity
            Register(GingerBeard);
            Register(Yoraiz0rDarkness);
        }
    }
}