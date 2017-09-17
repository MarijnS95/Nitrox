using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CyclopsActivateShield : AuthenticatedPacket
    {
        public Guid Guid { get; }

        public CyclopsActivateShield(String playerId, Guid guid) : base(playerId)
        {
            this.Guid = guid;
        }

        public override string ToString()
        {
            return "[CyclopsActivateShield PlayerId: " + PlayerId + " Guid: " + Guid + "]";
        }
    }
}
