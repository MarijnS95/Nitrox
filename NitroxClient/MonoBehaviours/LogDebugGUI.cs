using System.Text;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    public class LogDebugGUI : MonoBehaviour
    {
        private static GUIStyle BOX_STYLE = "box";

        private void Awake()
        {
            if (main != null)
            {
                Destroy(this);
                return;
            }
            main = this;
            enabled = true;
        }

        private void Start()
        {
            Application.logMessageReceived += Application_logMessageReceived;
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
            float heightInsideScrollView = BOX_STYLE.CalcHeight(content, lastScrollViewSize.x - BOX_STYLE.padding.horizontal) - BOX_STYLE.padding.vertical;

            float margin = 5f;
            GUILayout.BeginArea(new Rect(margin, margin, Screen.width - margin * 2, Screen.height / 4));
            GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();
            showStacktrace = GUILayout.Toggle(showStacktrace, "Show stacktrace");
            if (GUILayout.Button("Clear"))
                sb.Length = 0;
            GUILayout.EndHorizontal();

            if (follow)
                scrollPos.y = heightInsideScrollView - lastScrollViewSize.y;
            var setTo = scrollPos.y;
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUIStyle.none, "verticalScrollbar", GUIStyle.none);
            //GUI.ScrollTo();
            GUILayout.Label(content);
            GUILayout.EndScrollView();
            lastScrollViewSize = GUILayoutUtility.GetLastRect().size;
            follow = scrollPos.y >= heightInsideScrollView - lastScrollViewSize.y;
            GUILayout.EndVertical();
            var bla = GUILayoutUtility.GetLastRect().height;
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(margin, Screen.height - margin - 100f, Screen.width / 4f, 100f));
            GUILayout.Label(string.Format("set: {0}, actual: {1}", setTo, scrollPos.y));
            GUILayout.EndArea();
        }

        private static LogDebugGUI main;

        private StringBuilder sb = new StringBuilder();
        private Vector2 scrollPos;
        private bool follow = true, showStacktrace = true;
        private Vector2 lastScrollViewSize;

        private const int MAX_LOG_SIZE = 16000;
    }
}
