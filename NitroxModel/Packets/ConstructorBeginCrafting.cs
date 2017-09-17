using NitroxModel.DataStructures;
using System;
using System.Collections.Generic;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ConstructorBeginCrafting : AuthenticatedPacket
    {
        public Guid ConstructorGuid { get; protected set; }
        public Guid ConstructedItemGuid { get; protected set; }
        public TechType TechType { get; }
        public float Duration { get; protected set; }
        public List<InteractiveChildObjectIdentifier> InteractiveChildIdentifiers { get; }

        public ConstructorBeginCrafting(String playerId, Guid constructorGuid, Guid constructedItemGuid, TechType techType, float duration, List<InteractiveChildObjectIdentifier> interactiveChildIdentifiers) : base(playerId)
        {
            this.ConstructorGuid = constructorGuid;
            this.ConstructedItemGuid = constructedItemGuid;
            this.TechType = techType;
            this.Duration = duration;
            this.InteractiveChildIdentifiers = interactiveChildIdentifiers;
        }

        public override string ToString()
        {
            String s = "[ConstructorBeginCrafting - ConstructorGuid: " + ConstructorGuid + " ConstructedItemGuid: " + ConstructedItemGuid + " TechType: " + TechType + " Duration: " + Duration + " InteractiveChildIdentifiers: (";

            foreach (InteractiveChildObjectIdentifier childIdentifier in InteractiveChildIdentifiers)
            {
                s += childIdentifier + " ";
            }

            return s + ")";
        }
    }
}
