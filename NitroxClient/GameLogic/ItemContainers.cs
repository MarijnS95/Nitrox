﻿using NitroxClient.Communication;
using NitroxClient.GameLogic.Helper;
using NitroxModel.Packets;
using System;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class ItemContainers
    {
        private PacketSender packetSender;

        public ItemContainers(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void AddItem(Pickupable pickupable, GameObject owner)
        {
            Guid ownerGuid = GuidHelper.GetGuid(owner);
            Vector3 ownerPos = owner.transform.position;
            byte[] bytes = SerializationHelper.GetBytes(pickupable.gameObject);

            ItemContainerAdd add = new ItemContainerAdd(packetSender.PlayerId, ownerGuid, bytes, ownerPos);
            packetSender.Send(add);
        }

        public void RemoveItem(Pickupable pickupable, GameObject owner)
        {
            Guid itemGuid = GuidHelper.GetGuid(pickupable.gameObject);
            Guid ownerGuid = GuidHelper.GetGuid(owner);
            Vector3 ownerPos = owner.transform.position;

            ItemContainerRemove remove = new ItemContainerRemove(packetSender.PlayerId, ownerGuid, itemGuid, ownerPos);
            packetSender.Send(remove);
        }
    }
}
