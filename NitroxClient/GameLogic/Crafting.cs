using NitroxClient.Communication;
using NitroxClient.GameLogic.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using System;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class Crafting
    {
        private PacketSender packetSender;

        public Crafting(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void FabricatorCrafingStarted(GameObject crafter, TechType techType, float duration)
        {
            Guid crafterGuid = GuidHelper.GetGuid(crafter);
            FabricatorBeginCrafting fabricatorBeginCrafting = new FabricatorBeginCrafting(packetSender.PlayerId, crafterGuid, techType, duration);
            packetSender.Send(fabricatorBeginCrafting);
        }

        public void FabricatorItemPickedUp(GameObject gameObject, TechType techType)
        {
            Guid crafterGuid = GuidHelper.GetGuid(gameObject);
            FabricatorItemPickup fabricatorItemPickup = new FabricatorItemPickup(packetSender.PlayerId, crafterGuid, techType);
            packetSender.Send(fabricatorItemPickup);
            Log.Debug(fabricatorItemPickup);
        }
    }
}
