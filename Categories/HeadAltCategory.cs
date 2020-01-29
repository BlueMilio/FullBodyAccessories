using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class HeadAltCategory : Category
    {
        public HeadAltCategory() : base("Head-Alt")
        {
            Register(AnglerEarring);
            Register(NaturesGift, ObsidianRose);

            // Vanity
            Register(AngelHalo);
        }
    }
}