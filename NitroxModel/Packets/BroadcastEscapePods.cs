using System;
using NitroxClient.Unity.Helper;
using NitroxModel.DataStructures.GameLogic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class BroadcastEscapePods : Packet
    {
        public EscapePodModel[] EscapePods { get; }

        public BroadcastEscapePods(EscapePodModel[] escapePods)
        {
            EscapePods = escapePods;
        }

        public override string ToString()
        {
            return "[BroadcastEscapePods " + EscapePods.Join(" ") + "]";
        }
    }
}
