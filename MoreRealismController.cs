using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismController : SerializedMonoBehaviour
    {
        public static MoreRealismController Instance;

        public List<BaseWindow> windows = new List<BaseWindow>();
        public MoreRealismSettings settings = null;
        public bool isLoaded = false;

        private bool _prefabFlag = false;

        public void Start()
        {
            if (_prefabFlag)
                return;

            EventManager.Instance.OnStartPlayingPark += Load;
        }

        public void SetAsPrefab()
        {
            _prefabFlag = true;
        }

        public void Load()
        {
            if (_prefabFlag)
                return;

            if (isLoaded)
                this.Unload();

            if (settings == null)
                settings = new MoreRealismSettings();

            windows.Add(new MainWindow(this));
            windows.Add(new MessageBoxWindow(this));
            windows.Add(new DebuggingWindow(this));

            GameController.Instance.addSerializedObject(this);

            isLoaded = true;
        }

        public void Unload()
        {
            if (_prefabFlag)
                return;

            isLoaded = false;
            windows.Clear();
        }

        private void Update()
        {
            if (_prefabFlag)
                return;

            if (isLoaded)
            {
                // Check for settings window input
                if (InputManager.getKeyUp("TheMasterCado@MoreRealism/openSettings"))
                {
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
            if (_prefabFlag)
                return;

            settings.nextDayNightSwitchTime = ParkInfo.ParkTime + settings.cycleLenghtMonths * 300;
        }

        public T GetWindow<T>() where T : BaseWindow
        {
            if (_prefabFlag)
                return null;

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
            if (_prefabFlag)
                return;

            settings = new MoreRealismSettings();
            settings.deserialize(context, values);
            base.deserialize(context, values);

            Instance.Kill();
            Instance = this;
        }

        public override void serialize(SerializationContext context, Dictionary<string, object> values)
        {
            if (_prefabFlag)
                return;

            settings.serialize(context, values);
            base.serialize(context, values);
        }

        public override string getReferenceName()
        {
            return "MoreRealismController";
        }
    }
}