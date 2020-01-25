using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class RingsCategory : Category
    {
        public RingsCategory() : base("Rings")
        {
            Register(CoinRing, DiamondRing, GoldRing, GreedyRing);
            Register(FleshKnuckles);
        }
    }
}