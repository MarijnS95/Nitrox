using System;
using System.Collections.Generic;
using NitroxClient.Unity.Helper;
using NitroxModel.DataStructures;

namespace NitroxModel.Packets
{
    [Serializable]
    public class SimulationOwnershipChange : Packet
    {
        public List<OwnedGuid> OwnedGuids { get; }

        public SimulationOwnershipChange(string guid, string owningPlayerId)
        {
            OwnedGuids = new List<OwnedGuid>
            {
                new OwnedGuid(guid, owningPlayerId, false)
            };
        }

        public SimulationOwnershipChange(List<OwnedGuid> ownedGuids)
        {
            OwnedGuids = ownedGuids;
        }

        public override string ToString()
        {
            return "[SimulationOwnershipChange: {" + OwnedGuids.Join(" ") + "}]";
        }
    }
}
