using System;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class MainWindow : BaseWindow
    {
        private bool _dayNightCycleEnabled;
        private string _cycleLenghtMonths;


        public MainWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism";
            WindowRect = new Rect(20, 20, 300, 400);

            //Get settings from controller
            _dayNightCycleEnabled = mrController.settings.dayNightCycleEnabled;
            _cycleLenghtMonths = mrController.settings.cycleLenghtMonths.ToString();
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            _dayNightCycleEnabled = GUILayout.Toggle(_dayNightCycleEnabled, "Enable day/night cycle :");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Day/night length (months) :");
            _cycleLenghtMonths = GUILayout.TextField(_cycleLenghtMonths);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("Park Time : {0}", ParkInfo.ParkTime));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("Next switch : {0}", Controller.settings.nextDayNightSwitchTime));
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save"))
            {
                int cycleLenghtMonths_int;
                if(int.TryParse(_cycleLenghtMonths, out cycleLenghtMonths_int)) {

                    Controller.settings.cycleLenghtMonths = cycleLenghtMonths_int;
                    Controller.settings.dayNightCycleEnabled = _dayNightCycleEnabled;

                    this.CloseWindow();
                    Controller.SaveSettings();
                }
                else
                    Controller.GetWindow<MessageBoxWindow>().Show("Please enter a valid integer");

            }
            GUILayout.EndHorizontal();
        }
    }
}