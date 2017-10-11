using NitroxModel.Packets;
using System;

namespace NitroxModel.DataStructures.PacketModel
{
    public interface IMostRecentPacketSender
    {
        void UpdateAndSend(Packet newPacket, Action<Packet> sender);
        void Send(Action<Packet> sender);
    }
}
