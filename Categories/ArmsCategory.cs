using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class ArmsCategory : Category
    {
        public ArmsCategory() : base("Arms")
        {
            Register(AnkhShield, CobaltShield, EoCShield, HuntressBuckler, ObsidianShield, PaladinsShield, SquireShield);
            Register( BalloonHorseshoeFart, BalloonHorseshoeHoney, BalloonHorseshoeSharkron, BalloonPufferfish, BlizzardinaBalloon, 
                BlueHorseshoeBalloon, BundleofBalloons, CloudinaBalloon, FartInABalloon, HoneyBalloon, PartyBundleOfBalloonsAccessory, SandstorminaBalloon, 
                SharkronBalloon, ShinyRedBalloon, WhiteHorseshoeBalloon, YellowHorseshoeBalloon);
            Register(BandofRegeneration, BandofStarpower);
            Register(CelestialCuffs, MagicCuffs);
            Register(CharmofMyths);
            Register(ClimbingClaws);
            Register(FeralClaws, FireGauntlet, HandWarmer, MechanicalGlove, PowerGlove, TitanGlove, YoYoGlove);
            Register(LavaCharm);
            Register(ManaFlower);
            Register(MoonStone, SunStone);
            Register(PartyBalloonAnimal);
            Register(Shackle);
        }
    }
}