using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    public class DebugManager : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.AddComponent<MultiplayerDebugGUI>();
            gameObject.AddComponent<LogDebugGUI>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
                Toggle<MultiplayerDebugGUI>();
            if (Input.GetKeyDown(KeyCode.F4))
                Toggle<LogDebugGUI>();
        }

        private void Toggle<T>() where T : Behaviour
        {
            foreach (T t in FindObjectsOfType<T>())
                if (t != null)
                    t.enabled = !t.enabled;
        }
    }
}
