using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class DeconstructionBegin : PlayerActionPacket
    {
        public Guid Guid { get; }

        public DeconstructionBegin(String playerId, Vector3 itemPosition, Guid guid) : base(playerId, itemPosition)
        {
            this.Guid = guid;
        }

        public override string ToString()
        {
            return "[DeconstructionBegin( - playerId: " + PlayerId + " Guid: " + Guid + "]";
        }
    }
}
