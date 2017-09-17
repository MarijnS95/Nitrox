using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CyclopsBeginSilentRunning : AuthenticatedPacket
    {
        public Guid Guid { get; }

        public CyclopsBeginSilentRunning(String playerId, Guid guid) : base(playerId)
        {
            this.Guid = guid;
        }

        public override string ToString()
        {
            return "[CyclopsBeginSilentRunning PlayerId: " + PlayerId + " Guid: " + Guid + "]";
        }
    }
}
