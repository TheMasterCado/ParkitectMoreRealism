using System;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class MainWindow : BaseWindow
    {
        private bool _dayNightCycleEnabled, _autoFreeRides, _kickOutGuestsAtNight, _closeEverythingAtNight;
        private string _cycleLenghtMonths;


        public MainWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism";
            WindowRect = new Rect(20, 20, 300, 250);

            //Get settings from controller
            _dayNightCycleEnabled = mrController.settings.dayNightCycleEnabled;
            _autoFreeRides = mrController.settings.autoFreeRides;
            _kickOutGuestsAtNight = mrController.settings.kickOutGuestsAtNight;
            _closeEverythingAtNight = mrController.settings.closeEverythingAtNight;
            _cycleLenghtMonths = mrController.settings.cycleLenghtMonths.ToString();
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            _dayNightCycleEnabled = GUILayout.Toggle(_dayNightCycleEnabled, "  Enable day/night cycle");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _cycleLenghtMonths = GUILayout.TextField(_cycleLenghtMonths);
            GUILayout.Label("  Day/night length (months)");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _autoFreeRides = GUILayout.Toggle(_autoFreeRides, "  Automatically set rides as free");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _kickOutGuestsAtNight = GUILayout.Toggle(_kickOutGuestsAtNight, "  Kick out guests at night");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _closeEverythingAtNight = GUILayout.Toggle(_closeEverythingAtNight, "  Close everything at night");
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Debug"))
                Controller.GetWindow<DebuggingWindow>().OpenWindow();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save"))
            {
                int cycleLenghtMonths_int;
                if(int.TryParse(_cycleLenghtMonths, out cycleLenghtMonths_int)) {

                    Controller.settings.cycleLenghtMonths = cycleLenghtMonths_int;
                    Controller.settings.dayNightCycleEnabled = _dayNightCycleEnabled;
                    Controller.settings.autoFreeRides = _autoFreeRides;
                    Controller.settings.kickOutGuestsAtNight = _kickOutGuestsAtNight;
                    Controller.settings.closeEverythingAtNight = _closeEverythingAtNight;

                    Controller.SaveSettings();
                }
                else
                    Controller.GetWindow<MessageBoxWindow>().Show("Please enter a valid integer");

            }
            GUILayout.EndHorizontal();
        }
    }
}