using NitroxClient.MonoBehaviours;
using NitroxModel.DataStructures.PacketModel;
using NitroxModel.DataStructures.ServerModel;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.PacketModel;
using NitroxModel.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NitroxClient.Communication
{
    public class PacketSender
    {
        public bool Active { get; set; } = false;
        public String PlayerId { get; set; }

        private TcpClient client;
        private readonly HashSet<Type> suppressedPacketsTypes = new HashSet<Type>();
        private readonly Dictionary<Type, IMostRecentPacketSender> lastPackets;

        public PacketSender(TcpClient client)
        {
            this.client = client;
            lastPackets = typeof(Packet).Assembly
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract
                    && t.IsClass
                    && typeof(Packet).IsAssignableFrom(t)
                    && Attribute.IsDefined(t, typeof(RatelimitedAttribute)))
                .ToDictionary(packet => packet, packet =>
                {
                    var attr = (RatelimitedAttribute)Attribute.GetCustomAttribute(packet, typeof(RatelimitedAttribute));
                    if (attr.HasMultipleTargets)
                    {
                        Validate.IsTrue(typeof(ITargetedPacket).IsAssignableFrom(packet), $"{packet} uses a MultiTarget ratelimiter, but does not implement ITargetedPacket!");
                        return (IMostRecentPacketSender)new MultipleTargetRecentPacketSender(attr.SecondsPerPacket);
                    }
                    else
                    {
                        return new SingleTargetRecentPacketSender(attr.SecondsPerPacket);
                    }
                });
        }

        public void Authenticate()
        {
            Authenticate auth = new Authenticate(PlayerId);
            Send(auth);
        }

        public void UpdatePlayerLocation(Vector3 location, Vector3 velocity, Quaternion bodyRotation, Quaternion aimingRotation, Optional<VehicleModel> opVehicle, Optional<String> opSubGuid)
        {
            Movement movement;

            if (opVehicle.IsPresent())
            {
                VehicleModel vehicle = opVehicle.Get();
                movement = new VehicleMovement(PlayerId, vehicle.Position, vehicle.Velocity, vehicle.Rotation, vehicle.AngularVelocity, vehicle.TechType, vehicle.Guid, vehicle.SteeringWheelYaw, vehicle.SteeringWheelPitch, vehicle.AppliedThrottle);
            }
            else
            {
                movement = new Movement(PlayerId, location, velocity, bodyRotation, aimingRotation, opSubGuid);
            }

            Send(movement);
        }

        public void AnimationChange(AnimChangeType type, AnimChangeState state)
        {
            AnimationChangeEvent animEvent = new AnimationChangeEvent(PlayerId, (int)type, (int)state);
            Send(animEvent);
        }

        private void SendImmediately(Packet packet)
        {
            try
            {
                client.Send(packet);
            }
            catch (Exception ex)
            {
                Log.InGame($"Error sending {packet}: {ex.Message}");
                Log.Error("Error sending packet " + packet, ex);
            }
        }

        public void Send(Packet packet)
        {
            if (Active && !suppressedPacketsTypes.Contains(packet.GetType()))
            {
                IMostRecentPacketSender lp;
                if (lastPackets.TryGetValue(packet.GetType(), out lp))
                {
                    lp.UpdateAndSend(packet, SendImmediately);
                }
                else
                {
                    SendImmediately(packet);
                }
            }
        }

        public void UpdateRatelimitedPackets()
        {
            lastPackets.Values.ForEach(limiter => limiter.Send(SendImmediately));
        }

        public PacketSuppression<T> Suppress<T>()
        {
            return new PacketSuppression<T>(suppressedPacketsTypes);
        }
    }
}
