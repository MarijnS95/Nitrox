﻿using System;
using NitroxModel.DataStructures;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class Movement : Packet
    {
        public string PlayerId { get; }
        public Vector3 Position { get; }
        public Vector3 Velocity { get; }
        public Quaternion BodyRotation { get; }
        public Quaternion AimingRotation { get; }
        public Optional<string> SubGuid { get; }

        public Movement(string playerId, Vector3 position, Vector3 velocity, Quaternion bodyRotation, Quaternion aimingRotation, Optional<string> subGuid)
        {
            PlayerId = playerId;
            Position = position;
            Velocity = velocity;
            BodyRotation = bodyRotation;
            AimingRotation = aimingRotation;
            SubGuid = subGuid;
        }

        public override string ToString()
        {
            return "[Movement - PlayerId: " + PlayerId + " Position: " + Position + " Velocity: " + Velocity + " Body rotation: " + BodyRotation + " Camera rotation: " + AimingRotation + " SubGuid: " + SubGuid + "]";
        }
    }
}
