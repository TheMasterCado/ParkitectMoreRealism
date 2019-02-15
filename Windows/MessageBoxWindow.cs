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
            WindowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(message);
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Close"))
            {
                CloseWindow();
                this.WindowRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100);
            }
            GUILayout.EndHorizontal();
        }

        public void Show(string mes)
        {
            message = mes;
            this.OpenWindow();
        }

        public void Show(string mes, int width, int height)
        {
            this.WindowRect = new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height);
            this.Show(mes);
        }
    }
}
