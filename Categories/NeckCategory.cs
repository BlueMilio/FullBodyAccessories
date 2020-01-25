using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class NeckCategory : Category
    {
        public NeckCategory() : base("Neck")
        {
            Register(CrossNecklace, JellyfishNecklace, PanicNecklace, PygmyNecklace, SharkToothNecklace, SweetheartNecklace);
            Register(ApprenticeScarf, WormScarf);
            Register(StarVeil);
        }
    }
}