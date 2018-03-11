using System;
using System.Reflection;
using NitroxClient.GameLogic.PlayerModelBuilder.Abstract;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NitroxClient.GameLogic.PlayerModelBuilder
{
    public class PlayerPingBuilder : IPlayerModelBuilder
    {
        private static Color[] newColorOptions;

        public void Build(INitroxPlayer player)
        {
            GameObject signalBase = Object.Instantiate(Resources.Load("VFX/xSignal")) as GameObject;
            signalBase.name = "signal" + player.PlayerName;
            signalBase.transform.localScale = new Vector3(.5f, .5f, .5f);
            signalBase.transform.localPosition += new Vector3(0, 0.8f, 0);
            signalBase.transform.SetParent(player.PlayerModel.transform, false);

            PingInstance ping = signalBase.GetComponent<PingInstance>();
            ping.SetLabel("Player " + player.PlayerName);
            ping.pingType = PingType.Signal;

            SetPingColor(player, ping);
        }

        private static void SetPingColor(INitroxPlayer player, PingInstance ping)
        {
            FieldInfo field = typeof(PingManager).GetField("colorOptions", BindingFlags.Static | BindingFlags.Public);

            if (newColorOptions == null)
            {
                // This condition is necessary.
                // Apparently the array is somehow transformed to a byte array,
                // at least that's what's reported by subsequent calls reading from this field.

                newColorOptions = PingManager.colorOptions;
            }

            // A (static) reference to newColorOptions has to be kept because SetValue to the readonly
            // PingManager.colorOptions field does not count as a proper handle.
            // Not doing so causes it to be garbage collected, resulting in an engine crash the next
            // time it's accessed (not even a NullReferenceException).

            // ASSUMPTION: This readonly field is intialized at declaration, so it might've been optimized
            // such that this field is not taken into account when checking for unreferenced objects.

            Array.Resize(ref newColorOptions, newColorOptions.Length + 1);
            newColorOptions[newColorOptions.Length - 1] = player.PlayerSettings.PlayerColor;

            // Replace the normal colorOptions with our colorOptions (has one color more with the player-color). Set the color of the ping with this. Then replace it back.
            field.SetValue(null, newColorOptions);
            ping.SetColor(newColorOptions.Length - 1);
        }
    }
}
