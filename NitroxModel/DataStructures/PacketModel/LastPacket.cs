﻿using NitroxModel.Helper;
using NitroxModel.Packets;
using System;
using UnityEngine;

namespace NitroxModel.DataStructures.PacketModel
{
    public class LastPacket
    {
        private Packet lastPacket;
        private float lastSentAt = 0;
        private readonly float secondsBetweenSends;

        public LastPacket(float secondsBetweenSends)
        {
            this.secondsBetweenSends = secondsBetweenSends;
        }

        public void UpdateAndSend(Packet newPacket, Action<Packet> sender)
        {
            Validate.NotNull(newPacket, "Update packet cannot be null.");

            // TODO: Either store multiple packets based on their target, or remove TargetEquals.
            // This is for cases where multiple packets intended for different targets are sent,
            // in which case we need a ratelimiter per target.
            if (lastPacket == null || lastPacket.TargetEquals(newPacket))
            {
                lastPacket = newPacket;
                Send(sender);
            }
        }

        public void Send(Action<Packet> sender)
        {
            if (lastPacket == null)
            {
                return;
            }

            float d = Time.time - lastSentAt;
            if (d > secondsBetweenSends)
            {
                Logger.Log.Debug("Sending packet {0} because it's time: {1}s", lastPacket, d);
                sender(lastPacket);
                lastSentAt = Time.time;
                lastPacket = null;
            }
            else
            {
                Logger.Log.Debug("Not sending {0} yet, delta is: {1}s", lastPacket, d);
            }
        }
    }
}
