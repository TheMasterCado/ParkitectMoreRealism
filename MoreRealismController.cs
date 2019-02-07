using System.Collections;
using System.Collections.Generic;
using MoreRealism.Windows;
using UnityEngine;

namespace MoreRealism
{
    public class MoreRealismController : SerializedMonoBehaviour
    {
        public static string Version = "1.1";
        public static MoreRealismController Instance;

        public List<BaseWindow> windows = new List<BaseWindow>();
        public MoreRealismSettings settings = null;
        public bool isLoaded = false;

        private bool _prefabFlag = false;

        private void Start()
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
            if (_prefabFlag || GameController.Instance.isInScenarioEditor)
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
                settings.nextDayNightSwitchTime = (int)(ParkInfo.ParkTime + settings.cycleLenghtMonths * 300);
            }

            if (settings.kickOutGuestsAtNight && ParkInfo.ParkTime >= settings.nextParkStateSwitchTime)
            {
                if (GameController.Instance.park.weatherController.IsNight)
                {
                    GameController.Instance.park.setState(Park.State.CLOSED_SEND_GUESTS_HOME);
                    settings.nextParkStateSwitchTime = settings.nextDayNightSwitchTime;
                }
                else
                {
                    GameController.Instance.park.setState(Park.State.OPENED);
                    settings.nextParkStateSwitchTime = (int)(settings.nextDayNightSwitchTime + settings.cycleLenghtMonths * 150);
                }

            }

            if (settings.closeEverythingAtNight && ParkInfo.ParkTime >= settings.nextEverythingStateSwitchTime)
            {
                int compareDiff;
                if (settings.cycleLenghtMonths < 1)
                    compareDiff = (int)(settings.cycleLenghtMonths * 60 + 2);
                else
                    compareDiff = 65;
                if (settings.nextParkStateSwitchTime - settings.nextEverythingStateSwitchTime > compareDiff)
                {
                    foreach (Attraction attr in GameController.Instance.park.getAttractions())
                    {
                        if (attr.isOpened())
                            attr.setState(Attraction.State.CLOSED);
                    }
                    foreach (Shop sh in GameController.Instance.park.getShops())
                    {
                        sh.close();
                    }
                    if (settings.cycleLenghtMonths < 1)
                        settings.nextEverythingStateSwitchTime = (int)(settings.nextParkStateSwitchTime - (settings.cycleLenghtMonths * 60));
                    else
                        settings.nextEverythingStateSwitchTime = settings.nextParkStateSwitchTime - 60;
                }
                else
                {
                    foreach (Attraction attr in GameController.Instance.park.getAttractions())
                    {
                        if (attr.canOpen(out string b))
                            attr.setState(Attraction.State.OPENED);
                    }
                    foreach (Shop sh in GameController.Instance.park.getShops())
                    {
                        sh.open();
                    }
                    if (settings.cycleLenghtMonths < 1)
                        settings.nextEverythingStateSwitchTime = (int)(settings.nextParkStateSwitchTime + (settings.cycleLenghtMonths * 60) + settings.cycleLenghtMonths * 450);
                    else
                        settings.nextEverythingStateSwitchTime = (int)(settings.nextParkStateSwitchTime + 60 + settings.cycleLenghtMonths * 450);
                }
            }


        }

        public void SaveSettings()
        {
            if (_prefabFlag)
                return;

            GameController.Instance.park.weatherController.IsNight = false;

            settings.nextDayNightSwitchTime = (int)(ParkInfo.ParkTime + settings.cycleLenghtMonths * 300);

            if (settings.kickOutGuestsAtNight)
            {
               settings.nextParkStateSwitchTime = (int)(settings.nextDayNightSwitchTime + settings.cycleLenghtMonths * 150);

                if (settings.closeEverythingAtNight)
                {
                    settings.nextEverythingStateSwitchTime = settings.nextParkStateSwitchTime + 60;
                }
            }
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

        public void closeAllWindows()
        {
            foreach (BaseWindow w in windows)
                w.CloseWindow();
        }

        public override string getReferenceName()
        {
            return "MoreRealismController";
        }
    }
}