using System;
using System.Collections.Generic;
using NitroxClient.Unity.Helper;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CellEntities : Packet
    {
        public List<Entity> Entities { get; }

        public CellEntities(List<Entity> entities)
        {
            Entities = entities;
        }

        public override string ToString()
        {
            return "[CellEntities " + Entities.Join(" ") + "]";
        }
    }
}
