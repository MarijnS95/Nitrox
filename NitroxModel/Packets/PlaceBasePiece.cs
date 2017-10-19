﻿using NitroxModel.DataStructures.Util;
using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class PlaceBasePiece : PlayerActionPacket
    {
        public Guid Guid { get; }
        public Vector3 ItemPosition { get; }
        public Quaternion Rotation { get; }
        public TechType TechType { get; }
        public Optional<Guid> ParentBaseGuid { get; }
        public Vector3 CameraPosition { get; }
        public Quaternion CameraRotation { get; }

        public PlaceBasePiece(String playerId, Guid guid, Vector3 itemPosition, Quaternion rotation, Vector3 cameraPosition, Quaternion cameraRotation, TechType techType, Optional<Guid> parentBaseGuid) : base(playerId, itemPosition)
        {
            this.Guid = guid;
            this.ItemPosition = itemPosition;
            this.Rotation = rotation;
            this.TechType = techType;
            this.CameraPosition = cameraPosition;
            this.CameraRotation = cameraRotation;
            this.ParentBaseGuid = parentBaseGuid;
        }

        public override string ToString()
        {
            return "[PlaceBasePiece - ItemPosition: " + ItemPosition + " Guid: " + Guid + " Rotation: " + Rotation + " CameraPosition: " + CameraPosition + "CameraRotation: " + CameraRotation + " TechType: " + TechType + " ParentBaseGuid: " + ParentBaseGuid + "]";
        }
    }
}
