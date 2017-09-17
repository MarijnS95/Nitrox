using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EquipmentRemoveItem : PlayerActionPacket
    {
        public Guid OwnerGuid { get; private set; }
        public String Slot { get; private set; }
        public Guid ItemGuid { get; private set; }

        public EquipmentRemoveItem(String playerId, Guid ownerGuid, String slot, Guid itemGuid, Vector3 ownerPosition) : base(playerId, ownerPosition)
        {
            this.OwnerGuid = ownerGuid;
            this.Slot = slot;
            this.ItemGuid = itemGuid;
        }

        public override string ToString()
        {
            return "[EquipmentRemoveItem - playerId: " + PlayerId + " OwnerGuid: " + OwnerGuid + " Slot: " + Slot + " ItemGuid: " + ItemGuid + "]";
        }
    }
}
