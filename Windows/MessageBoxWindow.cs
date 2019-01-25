using UnityEngine;

namespace MoreRealism.Windows
{
    class MessageBoxWindow : BaseWindow
    {
        public string message = "No message set";

        public MessageBoxWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "MoreRealism - Message";
            drawCloseButton = false;
            WindowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 50);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(message);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Close")) CloseWindow();
            GUILayout.EndHorizontal();
        }

        public void Show(string mes)
        {
            message = mes;
            this.OpenWindow();
        }
    }
}
