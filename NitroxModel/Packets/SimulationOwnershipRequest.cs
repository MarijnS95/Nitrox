using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SimulationOwnershipRequest : AuthenticatedPacket
    {
        public Guid Guid { get; }

        public SimulationOwnershipRequest(String playerId, Guid guid) : base(playerId)
        {
            this.Guid = guid;
        }

        public override string ToString()
        {
            return "[SimulationOwnershipRequest - Guid: " + Guid + " PlayerId: " + PlayerId + "]";
        }
    }
}
