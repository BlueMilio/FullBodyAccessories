using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class HeadAltCategory : Category
    {
        public HeadAltCategory() : base("Head-Alt")
        {
            Register(AnglerEarring);
            Register(AngelHalo);
            Register(NaturesGift, ObsidianRose);
        }
    }
}