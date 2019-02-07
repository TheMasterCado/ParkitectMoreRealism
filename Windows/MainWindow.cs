using System;
using System.Globalization;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class MainWindow : BaseWindow
    {
        private bool _dayNightCycleEnabled, _kickOutGuestsAtNight, _closeEverythingAtNight;
        private string _cycleLenghtMonths;


        public MainWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism";
            WindowRect = new Rect(100, 100, 300, 250);
        }

        public override void OnOpen()
        {
            //Get settings from controller
            _dayNightCycleEnabled = Controller.settings.dayNightCycleEnabled;
            _kickOutGuestsAtNight = Controller.settings.kickOutGuestsAtNight;
            _closeEverythingAtNight = Controller.settings.closeEverythingAtNight;
            _cycleLenghtMonths = Controller.settings.cycleLenghtMonths.ToString().Replace(",", ".");
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GameController.Instance.park.settings.freeRideEntranceFees = 
                GUILayout.Toggle(GameController.Instance.park.settings.freeRideEntranceFees, "  Automatically set rides as free");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("-----------------------------------");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _dayNightCycleEnabled = GUILayout.Toggle(_dayNightCycleEnabled, "  Enable day/night cycle");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _cycleLenghtMonths = GUILayout.TextField(_cycleLenghtMonths);
            GUILayout.Label("  Day/night length (months)");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
             if(!(_kickOutGuestsAtNight = GUILayout.Toggle(_kickOutGuestsAtNight, "  Kick out guests at night")))
                _closeEverythingAtNight = false;
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (_closeEverythingAtNight = GUILayout.Toggle(_closeEverythingAtNight, "  Close everything at night"))
                _kickOutGuestsAtNight = true;
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Debug"))
                Controller.GetWindow<DebuggingWindow>().OpenWindow();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save"))
            {
                decimal cycleLenghtMonths_num;
                _cycleLenghtMonths = _cycleLenghtMonths.Replace(",", ".");
                if(decimal.TryParse(_cycleLenghtMonths, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out cycleLenghtMonths_num)) {
                    if (cycleLenghtMonths_num > 0)
                    {
                        Controller.settings.cycleLenghtMonths = Math.Round(cycleLenghtMonths_num, 1);
                        Controller.settings.dayNightCycleEnabled = _dayNightCycleEnabled;
                        Controller.settings.kickOutGuestsAtNight = _kickOutGuestsAtNight;
                        Controller.settings.closeEverythingAtNight = _closeEverythingAtNight;

                        Controller.SaveSettings();
                        Controller.closeAllWindows();
                        Controller.GetWindow<MessageBoxWindow>().Show("Settings saved, the cycle have been reset");
                    }
                    else
                        Controller.GetWindow<MessageBoxWindow>().Show("Please enter number higher than 0");
                }
                else
                    Controller.GetWindow<MessageBoxWindow>().Show("Please enter a valid number");

            }
            GUILayout.EndHorizontal();
        }
    }
}