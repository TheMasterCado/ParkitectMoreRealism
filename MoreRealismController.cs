using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismController : SerializedMonoBehaviour
    {
        public List<BaseWindow> windows = new List<BaseWindow>();
        public MoreRealismSettings settings;
        public bool isLoaded = false;

        public void Load()
        {
            Debug.Log("MoreRealism -> Started park");
            settings = GameController.Instance.parkGO.GetComponent<MoreRealismSettings>();
            if(settings == null)
            {
                GameController.Instance.park;
            }
            windows.Add(new MainWindow(this));
            windows.Add(new MessageBoxWindow(this));

            EventManager.Instance.OnMonthChanged += checkDayNightCycle;
        }

        private void OnDestroy()
        { }

        private void Update()
        {
            if(isLoaded)
            {
                // Check for settings window input
                if (InputManager.getKeyUp("TheMasterCado@MoreRealism/openSettings"))
                {
                    Debug.Log("Toggled MoreRealism MainWindow");

                    BaseWindow mainWindow = GetWindow<MainWindow>();
                    mainWindow.ToggleWindowState();
                }

                if (Input.GetKeyUp(KeyCode.Escape))
                    foreach (var window in windows)
                        if (window.isOpen)
                            window.CloseWindow();
            }
        }

        private void checkDayNightCycle()
        {

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