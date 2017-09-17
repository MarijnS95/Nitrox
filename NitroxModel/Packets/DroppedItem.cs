using NitroxModel.DataStructures.Util;
using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class DroppedItem : PlayerActionPacket
    {
        public Guid Guid { get; }
        public Optional<Guid> WaterParkGuid { get; }
        public TechType TechType { get; }
        public Vector3 ItemPosition { get; }
        public byte[] Bytes { get; }

        public DroppedItem(String playerId, Guid guid, Optional<Guid> waterParkGuid, TechType techType, Vector3 itemPosition, byte[] bytes) : base(playerId, itemPosition)
        {
            this.Guid = guid;
            this.WaterParkGuid = waterParkGuid;
            this.ItemPosition = itemPosition;
            this.TechType = techType;
            this.Bytes = bytes;
        }

        public override string ToString()
        {
            return "[DroppedItem - playerId: " + PlayerId + " guid: " + Guid + " WaterParkGuid: " + WaterParkGuid + " techType: " + TechType + " itemPosition: " + ItemPosition + "]";
        }
    }
}
