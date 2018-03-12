using System;
using System.Collections.Generic;
using NitroxClient.Unity.Helper;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class EntityTransformUpdates : Packet
    {
        public List<EntityTransformUpdate> Updates { get; }

        public EntityTransformUpdates()
        {
            Updates = new List<EntityTransformUpdate>();
        }

        public EntityTransformUpdates(List<EntityTransformUpdate> updates)
        {
            Updates = updates;
        }

        public void AddUpdate(string guid, Vector3 position, Quaternion rotation)
        {
            Updates.Add(new EntityTransformUpdate(guid, position, rotation));
        }

        public override string ToString()
        {
            return "[EntityTransformUpdates - Updates: " + Updates.Join(" ") + "]";
        }

        [Serializable]
        public class EntityTransformUpdate
        {
            public string Guid { get; }
            public Vector3 Position { get; }
            public Quaternion Rotation { get; }

            public EntityTransformUpdate(string guid, Vector3 position, Quaternion rotation)
            {
                Guid = guid;
                Position = position;
                Rotation = rotation;
            }

            public override string ToString()
            {
                return "(" + Guid + " " + Position + " " + Rotation + ")";
            }
        }
    }
}
