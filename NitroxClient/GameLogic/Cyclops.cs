using NitroxClient.Communication;
using NitroxModel.Packets;
using System;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class Cyclops
    {
        private PacketSender packetSender;

        public Cyclops(PacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void ToggleInternalLight(Guid guid, bool isOn)
        {
            CyclopsToggleInternalLighting packet = new CyclopsToggleInternalLighting(packetSender.PlayerId, guid, isOn);
            packetSender.Send(packet);
        }

        public void ToggleFloodLights(Guid guid, bool isOn)
        {
            CyclopsToggleFloodLights packet = new CyclopsToggleFloodLights(packetSender.PlayerId, guid, isOn);
            packetSender.Send(packet);
        }

        public void BeginSilentRunning(Guid guid)
        {
            CyclopsBeginSilentRunning packet = new CyclopsBeginSilentRunning(packetSender.PlayerId, guid);
            packetSender.Send(packet);
        }

        public void ChangeEngineMode(Guid guid, CyclopsMotorMode.CyclopsMotorModes mode)
        {
            CyclopsChangeEngineMode packet = new CyclopsChangeEngineMode(packetSender.PlayerId, guid, mode);
            packetSender.Send(packet);
        }

        public void ToggleEngineState(Guid guid, bool isOn, bool isStarting)
        {
            CyclopsToggleEngineState packet = new CyclopsToggleEngineState(packetSender.PlayerId, guid, isOn, isStarting);
            packetSender.Send(packet);
        }

        public void ActivateHorn(Guid guid)
        {
            CyclopsActivateHorn packet = new CyclopsActivateHorn(packetSender.PlayerId, guid);
            packetSender.Send(packet);
        }

        public void ActivateShield(Guid guid)
        {
            CyclopsActivateShield packet = new CyclopsActivateShield(packetSender.PlayerId, guid);
            packetSender.Send(packet);
        }

        public void ChangeName(Guid guid, string name)
        {
            CyclopsChangeName packet = new CyclopsChangeName(packetSender.PlayerId, guid, name);
            packetSender.Send(packet);
        }

        public void ChangeColor(Guid guid, int index, Vector3 hsb, Color color)
        {
            CyclopsChangeColor packet = new CyclopsChangeColor(packetSender.PlayerId, index, guid, hsb, color);
            packetSender.Send(packet);
        }
    }
}
