using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class FootCategory : Category
    {
        public FootCategory() : base("Foot", "Feet")
        {
            Register(AnkletoftheWind);
            Register(Flipper);
            Register(FlowerBoots, FlurryBoots, FrostsparkBoots, HermesBoots, IceSkates, LavaWaders, LightningBoots, 
                     ObsidianWaterWalkingBoots, RocketBoots, SailfishBoots, SpectreBoots, WaterWalkingBoots);
            Register(FlyingCarpet);
            Register(FrogLeg);
            Register(LuckyHorseshoe, ObsidianHorseshoe);
            Register(ShoeSpikes);
            Register(Tabi);
        }
    }
}