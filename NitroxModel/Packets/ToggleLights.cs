using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ToggleLights : AuthenticatedPacket
    {
        public Guid Guid { get; private set; }
        public bool IsOn { get; private set; }

        public ToggleLights(String playerId, Guid guid, bool isOn) : base(playerId)
        {
            this.Guid = guid;
            this.IsOn = isOn;
        }

        public override string ToString()
        {
            return "[ToggleLightsPacket PlayerId: " + PlayerId + " Guid: " + Guid + " IsOn: " + IsOn + "]";
        }
    }
}
