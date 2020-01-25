using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class FeetCategory : Category
    {
        public FeetCategory() : base("Feet")
        {
            Register(AnkletoftheWind);
            Register(Flipper);
            Register(FlowerBoots, FlurryBoots, FrostsparkBoots, HermesBoots, IceSkates, LavaWaders, LightningBoots, ObsidianWaterWalkingBoots, 
                RocketBoots, SailfishBoots, SpectreBoots, WaterWalkingBoots);
            Register(FlyingCarpet);
            Register(FrogLeg);
            Register(LuckyHorseshoe, ObsidianHorseshoe);
            Register(ShoeSpikes);
            Register(Tabi);
        }
    }
}