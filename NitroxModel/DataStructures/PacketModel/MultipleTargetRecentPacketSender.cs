using NitroxModel.Helper;
using NitroxModel.Packets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NitroxModel.DataStructures.PacketModel
{
    public class MultipleTargetRecentPacketSender : IMostRecentPacketSender
    {
        private class Target
        {
            internal Packet lastPacket;
            internal float lastSentAt = 0;
        }

        private readonly Dictionary<object, Target> targets = new Dictionary<object, Target>();
        private readonly float secondsBetweenSends;

        public MultipleTargetRecentPacketSender(float secondsBetweenSends)
        {
            this.secondsBetweenSends = secondsBetweenSends;
        }

        public void UpdateAndSend(Packet newPacket, Action<Packet> sender)
        {
            Validate.NotNull(newPacket, "Update packet cannot be null.");

            object targetKey = ((ITargetedPacket)newPacket).GetTarget();
            Target target;
            if (!targets.TryGetValue(targetKey, out target))
            {
                targets[targetKey] = target = new Target();
            }
            target.lastPacket = newPacket;
            Send(sender);
        }

        public void Send(Action<Packet> sender)
        {
            foreach (var target in targets.Values)
            {
                if (target.lastPacket == null)
                {
                    continue;
                }

                float d = Time.time - target.lastSentAt;
                if (d > secondsBetweenSends)
                {
                    Logger.Log.Debug("Sending packet {0} because it's time: {1}s", target.lastPacket, d);
                    sender(target.lastPacket);
                    target.lastSentAt = Time.time;
                    target.lastPacket = null;
                }
                else
                {
                    Logger.Log.Debug("Not sending {0} yet, delta is: {1}s", target.lastPacket, d);
                }
            }
        }
    }
}
