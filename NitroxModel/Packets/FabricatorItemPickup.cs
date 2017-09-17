using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class FabricatorItemPickup : AuthenticatedPacket
    {
        public Guid FabricatorGuid { get; }
        public TechType TechType { get; }

        public FabricatorItemPickup(String playerId, Guid fabricatorGuid, TechType techType) : base(playerId)
        {
            this.FabricatorGuid = fabricatorGuid;
            this.TechType = techType;
        }

        public override string ToString()
        {
            return "[FabricatorItemPickup - FabricatorGuid: " + FabricatorGuid + " TechType: " + TechType + "]";
        }
    }
}
