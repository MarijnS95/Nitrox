using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    class DebugGui : MonoBehaviour
    {
        float margin = 5f;

        private readonly Dictionary<string, Action> debugDrawers = new Dictionary<string, Action>();

        [Conditional("DEBUG")]
        private void OnGUI()
        {
            if (debugDrawers.Count > 0)
            {
                GUILayout.BeginArea(new Rect(margin, margin, Screen.width / 3, Screen.height / 4));
                GUILayout.BeginVertical("box");
                foreach (var dd in debugDrawers.Values)
                    dd();
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
        }

        public Action this[string name]
        {
            get { return debugDrawers[name]; }
            set
            {
                if (value == null)
                {
                    debugDrawers.Remove(name);
                }
                else
                {
                    debugDrawers[name] = value;
                }
            }
        }

        public static void FormatLabel(string format, params object[] args)
        {
            GUILayout.Label(string.Format(format, args));
        }
    }
}
