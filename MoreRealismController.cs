using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismController : SerializedMonoBehaviour
    {
        public List<BaseWindow> windows = new List<BaseWindow>();
        public MoreRealismSettings settings = null;
        public bool isLoaded = false;

        public void Load()
        {
            if (isLoaded)
                return;

            if (!GameController.Instance.hasSerializedObject(this))
                GameController.Instance.addSerializedObject(this);

            if (settings == null)
                settings = new MoreRealismSettings();

            windows.Add(new MainWindow(this));
            windows.Add(new MessageBoxWindow(this));

            isLoaded = true;
            Debug.Log("[MoreRealism] Loaded park mod settings for park");
        }

        public void Unload()
        {
            isLoaded = false;
            windows.Clear();
        }

        public void RemoveFromSave()
        {
            GameController.Instance.removeSerializedObject(this);
        }

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

                if (settings.dayNightCycleEnabled)
                    CheckDayNightCycle();
            }
        }

        private void CheckDayNightCycle()
        {
            if (ParkInfo.ParkTime >= settings.nextDayNightSwitchTime)
            {
                GameController.Instance.park.weatherController.IsNight = !GameController.Instance.park.weatherController.IsNight;
                settings.nextDayNightSwitchTime = ParkInfo.ParkTime + settings.cycleLenghtMonths * 300;
            }
        }

        public void SaveSettings()
        {
            settings.nextDayNightSwitchTime = ParkInfo.ParkTime + settings.cycleLenghtMonths * 300;
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

        public override void deserialize(SerializationContext context, Dictionary<string, object> values)
        {
            Debug.Log("[MoreRealism] Yo i'm deserializing");
            settings = new MoreRealismSettings();
            settings.deserialize(context, values);
            base.deserialize(context, values);
        }

        public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {
            settings.serialize(context, values);
            base.serialize(context, values);
        }
    }
}