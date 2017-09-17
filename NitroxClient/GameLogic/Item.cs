using NitroxClient.Communication;
using NitroxClient.MonoBehaviours;
using NitroxModel.DataStructures.Util;
using NitroxClient.GameLogic.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using System;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class Item
    {
        private PacketSender packetSender;

        public Item(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void UpdatePosition(Guid guid, Vector3 location, Quaternion rotation)
        {
            ItemPosition itemPosition = new ItemPosition(packetSender.PlayerId, guid, location, rotation);
            packetSender.Send(itemPosition);
        }

        public void PickedUp(GameObject gameObject, String techType)
        {
            Guid guid = GuidHelper.GetGuid(gameObject);
            Vector3 itemPosition = gameObject.transform.position;

            PickedUp(itemPosition, guid, techType);
        }

        public void PickedUp(Vector3 itemPosition, Guid guid, String techType)
        {
            PickupItem pickupItem = new PickupItem(packetSender.PlayerId, itemPosition, guid, techType);
            packetSender.Send(pickupItem);
        }

        public void Dropped(GameObject gameObject, TechType techType, Vector3 dropPosition)
        {
            Optional<Guid> waterpark = GetCurrentWaterParkGuid();
            Guid guid = GuidHelper.GetGuid(gameObject);
            byte[] bytes = SerializationHelper.GetBytes(gameObject);

            SyncedMultiplayerObject.ApplyTo(gameObject);

            Log.Debug("Dropping item with guid: " + guid);

            DroppedItem droppedItem = new DroppedItem(packetSender.PlayerId, guid, waterpark, techType, dropPosition, bytes);
            packetSender.Send(droppedItem);
        }

        private Optional<Guid> GetCurrentWaterParkGuid()
        {
            Player player = Utils.GetLocalPlayer().GetComponent<Player>();

            if (player != null)
            {
                WaterPark currentWaterPark = player.currentWaterPark;

                if (currentWaterPark != null)
                {
                    Guid waterParkGuid = GuidHelper.GetGuid(currentWaterPark.gameObject);
                    return Optional<Guid>.Of(waterParkGuid);
                }
            }

            return Optional<Guid>.Empty();
        }
    }
}
