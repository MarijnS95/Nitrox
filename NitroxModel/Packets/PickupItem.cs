using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PickupItem : PlayerActionPacket
    {
        public Guid Guid { get; }
        public Vector3 ItemPosition { get; }
        public String TechType { get; }

        public PickupItem(String playerId, Vector3 itemPosition, Guid guid, String techType) : base(playerId, itemPosition)
        {
            this.ItemPosition = itemPosition;
            this.Guid = guid;
            this.TechType = techType;
        }

        public override string ToString()
        {
            return "[Pickup Item - ItemPosition: " + ItemPosition + " Guid: " + Guid + " TechType: " + TechType + "]";
        }
    }
}
