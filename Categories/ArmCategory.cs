using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class ArmCategory : Category
    {
        public ArmCategory() : base("Arm", "Arms")
        {
            Register(AnkhShield, CobaltShield, EoCShield, HuntressBuckler, ObsidianShield, PaladinsShield, SquireShield);
            Register(BalloonHorseshoeFart, BalloonHorseshoeHoney, BalloonHorseshoeSharkron, BalloonPufferfish,
                     BlizzardinaBalloon, BlueHorseshoeBalloon, BundleofBalloons, CloudinaBalloon, FartInABalloon,
                     HoneyBalloon, PartyBundleOfBalloonsAccessory, SandstorminaBalloon, SharkronBalloon,
                     ShinyRedBalloon, WhiteHorseshoeBalloon, YellowHorseshoeBalloon);
            // Register(BandofRegeneration, BandofStarpower, ManaRegenerationBand);
            Register(CelestialCuffs, MagicCuffs);
            // Register(CharmofMyths);
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