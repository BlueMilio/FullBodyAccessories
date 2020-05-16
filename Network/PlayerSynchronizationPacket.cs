using System.IO;
using CustomSlot;
using FullBodyAccessories.Players;
using FullBodyAccessories.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Networking.Packets;

namespace FullBodyAccessories.Network
{
    public class PlayerSynchronizationPacket : ModPlayerNetworkPacket<FBAPlayer>
    {
        public PlayerSynchronizationPacket()
        {
        }

        public PlayerSynchronizationPacket(FBAPlayer fbaPlayer) : base(fbaPlayer)
        {
        }


        protected override bool PreSend(ModPacket modPacket, int? fromWho = null, int? toWho = null)
        {
            CustomItemSlot[] itemSlots = ModPlayer.Slots;

            for (int i = 0; i < itemSlots.Length; i++)
                modPacket.WriteItem(itemSlots[i].Item);

            return true;
        }

        protected override bool MidReceive(BinaryReader reader, int fromWho)
        {
            CustomItemSlot[] itemSlots = ModPlayer.Slots;

            for (int i = 0; i < itemSlots.Length; i++)
                itemSlots[i].Item= reader.ReadItem();

            return true;
        }
    }
}