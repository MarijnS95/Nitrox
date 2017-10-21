using System.Collections.Generic;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic;
using NitroxClient.GameLogic.Helper;
using NitroxClient.MonoBehaviours;
using NitroxModel.GameLogic;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    class SpawnEntitiesProcessor : ClientPacketProcessor<SpawnEntities>
    {
        private readonly PacketSender packetSender;
        private readonly SimulationOwnership simulationOwnership;
        private readonly HashSet<string> alreadySpawnedGuids = new HashSet<string>();

        public SpawnEntitiesProcessor(PacketSender packetSender, SimulationOwnership simulationOwnership)
        {
            this.packetSender = packetSender;
            this.simulationOwnership = simulationOwnership;
        }

        public override void Process(SpawnEntities packet)
        {
            foreach (SpawnedEntity entity in packet.Entities)
            {
                if (!alreadySpawnedGuids.Contains(entity.Guid))
                {
                    if (entity.TechType != TechType.None)
                    {
                        GameObject gameObject = CraftData.InstantiateFromPrefab(entity.TechType);
                        gameObject.transform.position = entity.Position;
                        GuidHelper.SetNewGuid(gameObject, entity.Guid);
                        gameObject.SetActive(true);

                        Log.Debug("Received spawned entity: " + entity.Guid + " at " + entity.Position + " of type " + entity.TechType);

                        if (simulationOwnership.HasOwnership(entity.Guid))
                        {
                            Log.Debug("Simulating positioning of: " + entity.Guid);
                            EntityPositionBroadcaster.WatchEntity(entity.Guid, gameObject);
                        }

                        alreadySpawnedGuids.Add(entity.Guid);
                    }
                    alreadySpawnedGuids.Add(entity.Guid);
                }
            }
        }
    }
}
