﻿using NitroxModel.DataStructures.Util;
using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleMovement : Movement
    {
        public TechType TechType { get; }
        public Vector3 AngularVelocity { get; }
        public Guid Guid { get; }
        public float SteeringWheelYaw { get; }
        public float SteeringWheelPitch { get; }
        public bool AppliedThrottle { get; }

        public VehicleMovement(String playerId, Vector3 playerPosition, Vector3 velocity, Quaternion rotation, Vector3 angularVelocity, TechType techType, Guid guid, float steeringWheelYaw, float steeringWheelPitch, bool appliedThrottle) : base(playerId, playerPosition, velocity, rotation, rotation, Optional<Guid>.Empty())

        {
            this.TechType = techType;
            this.AngularVelocity = angularVelocity;
            this.Guid = guid;

            this.SteeringWheelYaw = steeringWheelYaw;
            this.SteeringWheelPitch = steeringWheelPitch;
            this.AppliedThrottle = appliedThrottle;

            this.PlayerMustBeInRangeToReceive = false;
        }

        public override string ToString()
        {
            return "[VehicleMovement - TechType: " + TechType +
                " AngularVelocity: " + AngularVelocity +
                " Guid: " + Guid +
                " SteeringWheelYaw: " + SteeringWheelYaw +
                " SteeringWheelPitch: " + SteeringWheelPitch +
                " AppliedThrottle: " + AppliedThrottle +
                "]\n\t" + base.ToString();
        }
    }
}
