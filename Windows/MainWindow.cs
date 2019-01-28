using System;
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
            WindowRect = new Rect(20, 20, 300, 250);
        }

        public override void OnOpen()
        {
            //Get settings from controller
            _dayNightCycleEnabled = Controller.settings.dayNightCycleEnabled;
            _kickOutGuestsAtNight = Controller.settings.kickOutGuestsAtNight;
            _closeEverythingAtNight = Controller.settings.closeEverythingAtNight;
            _cycleLenghtMonths = Controller.settings.cycleLenghtMonths.ToString();
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
                int cycleLenghtMonths_int;
                if(int.TryParse(_cycleLenghtMonths, out cycleLenghtMonths_int)) {

                    Controller.settings.cycleLenghtMonths = cycleLenghtMonths_int;
                    Controller.settings.dayNightCycleEnabled = _dayNightCycleEnabled;
                    Controller.settings.kickOutGuestsAtNight = _kickOutGuestsAtNight;
                    Controller.settings.closeEverythingAtNight = _closeEverythingAtNight;

                    Controller.SaveSettings();
                    Controller.GetWindow<MessageBoxWindow>().Show("Settings saved, the cycle have been reset");
                }
                else
                    Controller.GetWindow<MessageBoxWindow>().Show("Please enter a valid integer");

            }
            GUILayout.EndHorizontal();
        }
    }
}