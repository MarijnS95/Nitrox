using System;
using System.Collections;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{

    public class MultiplayerDebugGUI : MonoBehaviour
    {
        private void Awake()
        {
            if (main != null)
            {
                Destroy(this);
                return;
            }
            main = this;
        }

        private void Start()
        {
            StartCoroutine(UpdatePacketFlow());
        }

        private IEnumerator UpdatePacketFlow()
        {
            while (true)
            {
                yield return new WaitForSeconds(packetFlowUpdateInterval);
                try
                {
                    previousStatus = currentStatus;
                    currentStatus = Multiplayer.Status;
                }
                catch (NullReferenceException) { }
            }
        }

        private void OnGUI()
        {
            // Ugh I don't like this stateful GUILayout.
            float width = Screen.width * .3f;
            float margin = 20f;
            GUI.color = Color.white;
            GUILayout.BeginArea(new Rect(margin, Screen.height - margin - 100f, Screen.width / 4f, 100f));
            GUILayout.BeginVertical("box");
            var s = GUI.skin.GetStyle("Label");
            s.alignment = TextAnchor.UpperCenter;
            s.fontStyle = FontStyle.Bold;
            GUILayout.Label("Multiplayer");
            s.alignment = TextAnchor.UpperLeft;
            s.fontStyle = FontStyle.Normal;
            if (currentStatus.IsConnected)
            {
                GUILayout.Label(string.Format(
                    "Packets sent: {0} ({1:F2}p/s)\n" +
                    "Packets received: {2} ({3:F2}p/s)",
                    currentStatus.PacketsSent,
                    (currentStatus.PacketsSent - previousStatus.PacketsSent) / packetFlowUpdateInterval,
                    currentStatus.PacketsReceived,
                    (currentStatus.PacketsReceived - previousStatus.PacketsReceived) / packetFlowUpdateInterval
                    ));
            }
            else
            {
                GUI.color = Color.red;
                GUILayout.Label("Not connected");
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private static MultiplayerDebugGUI main = null;

        private float packetFlowUpdateInterval = 0.15f;

        private MultiplayerStatus previousStatus, currentStatus;
    }
}
