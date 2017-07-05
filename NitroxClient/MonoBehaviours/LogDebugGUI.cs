using System.Text;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    public class LogDebugGUI : MonoBehaviour
    {

        public void Start()
        {
            Application.logMessageReceived += Application_logMessageReceived;
            //enabled = false;
            Debug.Log("Attached!");
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (showStacktrace && !string.IsNullOrEmpty(stackTrace))
                condition += "\n" + stackTrace;
            sb.AppendLine(condition);
        }

        private void OnGUI()
        {
            if (sb.Length > MAX_LOG_SIZE)
                sb.Remove(0, sb.Length - MAX_LOG_SIZE);

            var content = new GUIContent(sb.ToString());
            float contentHeight = new GUIStyle("box").CalcSize(content).y;

            float margin = 5f;
            GUILayout.BeginArea(new Rect(margin, margin, Screen.width - margin * 2, Screen.height / 4));
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            showStacktrace = GUILayout.Toggle(showStacktrace, "Show stacktrace");
            if (GUILayout.Button("Clear"))
                sb.Length = 0;
            GUILayout.EndHorizontal();

            if (follow)
                scrollPos.y = contentHeight - lastScrollviewHeight;

            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUIStyle.none, "verticalScrollbar", "box");
            GUILayout.Label(content);
            GUILayout.EndScrollView();
            lastScrollviewHeight = GUILayoutUtility.GetLastRect().height;
            follow = scrollPos.y >= contentHeight - lastScrollviewHeight;
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        private StringBuilder sb = new StringBuilder();
        private Vector2 scrollPos;
        private bool follow = true, showStacktrace = true;
        private float lastScrollviewHeight;

        private const int MAX_LOG_SIZE = 16000;
    }
}
