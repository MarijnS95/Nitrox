using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructionAmountChanged : PlayerActionPacket
    {
        public Guid Guid { get; private set; }
        public float ConstructionAmount { get; }

        public ConstructionAmountChanged(String playerId, Vector3 itemPosition, Guid guid, float constructionAmount) : base(playerId, itemPosition)
        {
            this.Guid = guid;
            this.ConstructionAmount = constructionAmount;
        }

        public override string ToString()
        {
            return "[ConstructionAmountChanged( - playerId: " + PlayerId + " Guid:" + Guid + " ConstructionAmount: " + ConstructionAmount + "]";
        }
    }
}
