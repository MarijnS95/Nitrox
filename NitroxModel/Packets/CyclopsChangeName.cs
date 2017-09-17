using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CyclopsChangeName : AuthenticatedPacket
    {
        public Guid Guid { get; }
        public String Name { get; }

        public CyclopsChangeName(String playerId, Guid guid, string name) : base(playerId)
        {
            this.Guid = guid;
            this.Name = name;
        }

        public override string ToString()
        {
            return "[CyclopsChangeName PlayerId: " + PlayerId + " Guid: " + Guid + " Name: " + Name + "]";
        }
    }
}
