using System;
using System.Collections.Generic;
using UnityEngine;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    public class EscapePodModel
    {
        // TODO: Technically these are not filled with real Guid's...
        public Guid Guid { get; }
        public Vector3 Location { get; }
        public Guid FabricatorGuid { get; }
        public Guid MedicalFabricatorGuid { get; }
        public Guid StorageContainerGuid { get; }
        public Guid RadioGuid { get; }

        public List<String> AssignedPlayers { get; }

        public EscapePodModel(Guid guid, Vector3 location, Guid fabricatorGuid, Guid medicalFabricatorGuid, Guid storageContainerGuid, Guid radioGuid) : base()
        {
            this.Guid = guid;
            this.Location = location;
            this.FabricatorGuid = fabricatorGuid;
            this.MedicalFabricatorGuid = medicalFabricatorGuid;
            this.StorageContainerGuid = storageContainerGuid;
            this.RadioGuid = radioGuid;
            this.AssignedPlayers = new List<String>();
        }

        public override string ToString()
        {
            String toString = "[EscapePodModel - Guid: " + Guid + " Location:" + Location + " FabricatorGuid: " + FabricatorGuid + " MedicalFabricatorGuid: " + MedicalFabricatorGuid + " StorageContainerGuid: " + StorageContainerGuid + " RadioGuid: " + RadioGuid + " AssignedPlayers: {";

            foreach (String playerId in AssignedPlayers)
            {
                toString += playerId + " ";
            }

            return toString + "}]";
        }
    }
}
