using static Terraria.ID.ItemID;

namespace FullBodyAccessories.Categories
{
    public sealed class BackCategory : Category
    {
        public BackCategory() : base("Back")
        {
            Register(AnglerTackleBag);
            Register(BeeCloak, CrimsonCloak, StarCloak);
            Register(HiveBackpack);
            Register(MagicQuiver);
            Register(MysteriousCape, RedCape, WinterCape);
        }
    }
}