using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismController : MonoBehaviour
    {
        public List<BaseWindow> windows = new List<BaseWindow>();

        public void Load()
        {
            Debug.Log("Started MoreRealism");

            windows.Add(new MainWindow(this));
        }

        private void OnDestroy()
        { }

        private void Update()
        {
            if (Input.GetKeyDown(Main.configuration.settings.openWindow))
            {
                Debug.Log("Toggled MoreRealism MainWindow");

                BaseWindow mainWindow = GetWindow<MainWindow>();
                Debug.Log(mainWindow.ToString());
                mainWindow.ToggleWindowState();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                foreach (var window in windows)
                    if (window.isOpen)
                        window.CloseWindow();
        }

        public T GetWindow<T>() where T : BaseWindow
        {
            foreach (var window in windows)
                if (window is T)
                    return (T) window;

            return null;
        }

        private void OnGUI()
        {
            foreach (var window in windows)
                if (window.isOpen)
                    window.DrawWindow();
        }
    }
}