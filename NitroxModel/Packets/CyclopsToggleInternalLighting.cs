using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class CyclopsToggleInternalLighting : AuthenticatedPacket
    {
        public Guid Guid { get; }
        public bool IsOn { get; }

        public CyclopsToggleInternalLighting(String playerId, Guid guid, bool isOn) : base(playerId)
        {
            this.Guid = guid;
            this.IsOn = isOn;
        }

        public override string ToString()
        {
            return "[CyclopsToggleInternalLighting PlayerId: " + PlayerId + " Guid: " + Guid + " IsOn: " + IsOn + "]";
        }
    }
}
