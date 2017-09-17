using NitroxClient.Communication;
using NitroxModel.Packets;
using System;
using System.Collections.Generic;

namespace NitroxClient.GameLogic
{
    public class SimulationOwnership
    {
        private PacketSender packetSender;
        private Dictionary<Guid, String> ownedGuidsToPlayer;
        private HashSet<Guid> requestedGuids;

        public SimulationOwnership(PacketSender packetSender)
        {
            this.packetSender = packetSender;
            this.ownedGuidsToPlayer = new Dictionary<Guid, String>();
            this.requestedGuids = new HashSet<Guid>();
        }

        public bool HasOwnership(Guid guid)
        {
            String owningPlayerId;

            if (ownedGuidsToPlayer.TryGetValue(guid, out owningPlayerId))
            {
                return owningPlayerId == packetSender.PlayerId;
            }

            return false;
        }

        public void TryToRequestOwnership(Guid guid)
        {
            if (!ownedGuidsToPlayer.ContainsKey(guid) && !requestedGuids.Contains(guid))
            {
                SimulationOwnershipRequest ownershipRequest = new SimulationOwnershipRequest(packetSender.PlayerId, guid);
                packetSender.Send(ownershipRequest);
                requestedGuids.Add(guid);
            }
        }

        public void AddOwnedGuid(Guid guid, String playerId)
        {
            ownedGuidsToPlayer[guid] = playerId;
        }
    }
}
