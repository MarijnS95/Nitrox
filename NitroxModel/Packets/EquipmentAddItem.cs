using System;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EquipmentAddItem : Packet
    {
        public EquippedItemData EquippedItemData { get; }

        public EquipmentAddItem(EquippedItemData equippedItemData)
        {
            EquippedItemData = equippedItemData;
        }

        public override string ToString()
        {
            return "[EquipmentAddItem EquippedItemData: " + EquippedItemData + "]";
        }
    }
}
