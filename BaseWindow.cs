using UnityEngine;

namespace MoreRealism
{
    public class BaseWindow
    {
        protected readonly MoreRealismController Controller;

        private readonly int _id;
        protected bool drawCloseButton = true;
        public bool isOpen;
        private readonly Rect TitleBarRect = new Rect(0, 0, 200000000, 20);
        protected bool usesSkin = true;

        protected string windowName = "Base window - More Realism";
        protected Rect WindowRect = new Rect(20, 20, 200, 200);

        public BaseWindow(MoreRealismController controller)
        {
            _id = IdGenerator.GetId();
            Controller = controller;
        }

        public void ToggleWindowState()
        {
            isOpen = !isOpen;
            if (isOpen)
                this.OnOpen();
        }

        public void OpenWindow()
        {
            isOpen = true;
            this.OnOpen();
        }

        public void CloseWindow()
        {
            isOpen = false;
        }

        public void DrawWindow()
        {
            WindowRect = GUILayout.Window(_id, WindowRect, DrawMain, windowName);
        }

        public void DrawMain(int windowId)
        {
            if (drawCloseButton)
                if (GUI.Button(new Rect(WindowRect.width - 21, 6, 15, 15), "x"))
                    CloseWindow();
            GUI.BeginGroup(new Rect(0, 0, WindowRect.width, WindowRect.height));
            DrawContent();

            GUI.EndGroup();
            GUI.DragWindow(TitleBarRect);
        }

        public virtual void DrawContent()
        {
        }

        public virtual void OnOpen()
        {
        }
    }
}
