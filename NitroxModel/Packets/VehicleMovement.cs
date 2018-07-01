﻿using System;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleMovement : Movement
    {
        public VehicleModel Vehicle { get; }

        public VehicleMovement(string playerId, VehicleModel vehicle) : base(playerId, vehicle.Position, vehicle.Velocity, vehicle.Rotation, vehicle.Rotation, Optional<string>.Empty())
        {
            Vehicle = vehicle;
        }

        public override string ToString()
        {
            return "[VehicleMovement - vehicle: " + Vehicle +
                "]\n\t" + base.ToString();
        }
    }
}
