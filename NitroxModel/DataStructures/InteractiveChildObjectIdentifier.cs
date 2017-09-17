using System;

namespace NitroxModel.DataStructures
{
    [Serializable]
    public class InteractiveChildObjectIdentifier
    {
        public Guid Guid { get; private set; }
        public String GameObjectNamePath { get; private set; }

        public InteractiveChildObjectIdentifier(Guid guid, String gameObjectNamePath)
        {
            this.Guid = guid;
            this.GameObjectNamePath = gameObjectNamePath;
        }

        public override string ToString()
        {
            return "[InteractiveChildObjectIdentifier - Guid: " + Guid + " GameObjectNamePath: " + GameObjectNamePath + "]";
        }
    }
}
