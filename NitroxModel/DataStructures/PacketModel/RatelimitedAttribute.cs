using System;

namespace NitroxModel.PacketModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RatelimitedAttribute : Attribute
    {
        public float SecondsPerPacket { get; }
        public bool HasMultipleTargets;

        public RatelimitedAttribute(float secondsPerPacket)
        {
            SecondsPerPacket = secondsPerPacket;
        }
    }
}
