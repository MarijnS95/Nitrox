using System;
using System.Collections.Generic;
using NitroxClient.Unity.Helper;
using UnityEngine;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    public class EscapePodModel
    {
        public string Guid { get; }
        public Vector3 Location { get; }
        public string FabricatorGuid { get; }
        public string MedicalFabricatorGuid { get; }
        public string StorageContainerGuid { get; }
        public string RadioGuid { get; }
        public List<string> AssignedPlayers { get; } = new List<string>();

        public EscapePodModel(string guid, Vector3 location, string fabricatorGuid, string medicalFabricatorGuid, string storageContainerGuid, string radioGuid)
        {
            Guid = guid;
            Location = location;
            FabricatorGuid = fabricatorGuid;
            MedicalFabricatorGuid = medicalFabricatorGuid;
            StorageContainerGuid = storageContainerGuid;
            RadioGuid = radioGuid;
        }

        public override string ToString()
        {
            return "[EscapePodModel - Guid: " + Guid + " Location:" + Location + " FabricatorGuid: " + FabricatorGuid + " MedicalFabricatorGuid: " + MedicalFabricatorGuid + " StorageContainerGuid: " + StorageContainerGuid + " RadioGuid: " + RadioGuid + " AssignedPlayers: {" + AssignedPlayers.Join(" ") + "}]";
        }
    }
}
