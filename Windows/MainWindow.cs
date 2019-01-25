using System;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class MainWindow : BaseWindow
    {
        public MainWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism";
            WindowRect = new Rect(20, 20, 300, 400);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            Controller.settings.dayNightCycleEnabled = 
                GUILayout.Toggle(Controller.settings.dayNightCycleEnabled, "Enable day/night cycle :");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Day/night cycle length (months) :");
            try
            {
                Controller.settings.cycleLenghtMonths = int.Parse(GUILayout.TextField(Controller.settings.cycleLenghtMonths.ToString()));
            }
            catch(FormatException)
            {
                Controller.GetComponent<MessageBoxWindow>().show("Please enter a valid integer");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("Park Time : {0}", ParkInfo.ParkTime));
            GUILayout.EndHorizontal();

            /*GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save"))
            {
                GameController.Instance.park.parkInfo
            }
            GUILayout.EndHorizontal();*/
        }
    }
}