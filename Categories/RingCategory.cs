using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class RingCategory : Category
    {
        public RingCategory() : base("Ring")
        {
            Register(CoinRing, DiamondRing, GoldRing, GreedyRing);
            Register(FleshKnuckles);
        }
    }
}