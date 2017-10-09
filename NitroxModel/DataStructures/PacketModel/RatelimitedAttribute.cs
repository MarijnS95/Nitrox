using System;

namespace NitroxModel.PacketModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RatelimitedAttribute : Attribute
    {
        public float SecondsPerPacket { get; set; }

        public RatelimitedAttribute(float secondsPerPacket)
        {
            SecondsPerPacket = secondsPerPacket;
        }
    }
}
