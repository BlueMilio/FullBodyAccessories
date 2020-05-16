using Terraria.ID;
using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class RingCategory : Category
    {
        public RingCategory() : base("Ring", "Rings")
        {
            Register(CoinRing, DiamondRing, GoldRing, GreedyRing);
            Register(FleshKnuckles);

            Register(BandofRegeneration, BandofStarpower);
            Register(CharmofMyths);
            Register(ManaRegenerationBand);
        }
    }
}