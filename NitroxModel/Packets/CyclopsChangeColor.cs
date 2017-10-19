﻿using NitroxModel.DataStructures.PacketModel;
using NitroxModel.PacketModel;
using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    [Ratelimited(.200f)]
    public class CyclopsChangeColor : AuthenticatedPacket, ITargetedPacket
    {
        public String Guid { get; private set; }
        public int Index { get; private set; }
        public Vector3 HSB { get; private set; }
        public Color Color { get; private set; }

        public CyclopsChangeColor(String playerId, int index, String guid, Vector3 hsb, Color color) : base(playerId)
        {
            this.Guid = guid;
            this.Index = index;
            this.HSB = hsb;
            this.Color = color;
        }

        public object GetTarget()
        {
            return Guid;
        }

        public override string ToString()
        {
            return "[CyclopsChangeColor PlayerId: " + PlayerId + " Guid: " + Guid + " Index: " + Index + " hsb: " + HSB + " Color: " + Color + "]";
        }
    }
}
