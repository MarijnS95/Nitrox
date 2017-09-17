using NitroxModel.DataStructures.Util;
using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructionCompleted : PlayerActionPacket
    {
        public Guid Guid { get; }
        public Optional<Guid> NewBaseCreatedGuid { get; }

        public ConstructionCompleted(String playerId, Vector3 itemPosition, Guid guid, Optional<Guid> newBaseCreatedGuid) : base(playerId, itemPosition)
        {
            this.Guid = guid;
            this.NewBaseCreatedGuid = newBaseCreatedGuid;
        }

        public override string ToString()
        {
            return "[ConstructionCompleted - playerId: " + PlayerId + " Guid: " + Guid + " NewBaseCreatedGuid: " + NewBaseCreatedGuid + "]";
        }
    }
}
