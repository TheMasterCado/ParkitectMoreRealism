using UnityEngine;

namespace MoreRealism.Windows
{
    public class MainWindow : BaseWindow
    {
        private string cycleLengthMonths = "";
        private bool dayNightCycleEnabled = false;

        public MainWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism";
            WindowRect = new Rect(20, 20, 300, 400);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            dayNightCycleEnabled = 
                GUILayout.Toggle(dayNightCycleEnabled, "Enable day/night cycle :");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Day/night cycle length (months) :");
            cycleLengthMonths = GUILayout.TextField(cycleLengthMonths);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("Park Time : {0}", ParkInfo.ParkTime));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save"))
            {
                
            }
            GUILayout.EndHorizontal();
        }
    }
}