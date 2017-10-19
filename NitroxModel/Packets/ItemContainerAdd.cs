﻿using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ItemContainerAdd : PlayerActionPacket
    {
        public Guid OwnerGuid { get; }
        public byte[] ItemData { get; }

        public ItemContainerAdd(String playerId, Guid ownerGuid, byte[] itemData, Vector3 ownerPositon) : base(playerId, ownerPositon)
        {
            this.OwnerGuid = ownerGuid;
            this.ItemData = itemData;
        }

        public override string ToString()
        {
            return "[ItemContainerAdd - playerId: " + PlayerId + " OwnerGuid: " + OwnerGuid + " Total Bytes: " + ItemData.Length + "]";
        }
    }
}
