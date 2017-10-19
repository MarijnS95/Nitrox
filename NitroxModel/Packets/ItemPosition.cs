using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ItemPosition : PlayerActionPacket
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Guid Guid { get; }

        public ItemPosition(String playerId, Guid guid, Vector3 position, Quaternion rotation) : base(playerId, position)
        {
            this.Guid = guid;
            this.Position = position;
            this.Rotation = rotation;
            this.PlayerMustBeInRangeToReceive = false;
        }

        public override string ToString()
        {
            return "[ItemPosition - playerId: " + PlayerId + " position: " + Position + " Rotation: " + Rotation + " guid: " + Guid + "]";
        }
    }
}
