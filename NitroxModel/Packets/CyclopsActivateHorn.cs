using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CyclopsActivateHorn : AuthenticatedPacket
    {
        public Guid Guid { get; }

        public CyclopsActivateHorn(String playerId, Guid guid) : base(playerId)
        {
            this.Guid = guid;
        }

        public override string ToString()
        {
            return "[CyclopsActivateHorn PlayerId: " + PlayerId + " Guid: " + Guid + "]";
        }
    }
}
