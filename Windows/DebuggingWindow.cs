using System;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class DebuggingWindow : BaseWindow
    {

        public DebuggingWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism - Debug";
            WindowRect = new Rect(20, 20, 200, 250);
        }

        public override void DrawContent()
        {
            GUILayout.BeginVertical();
            GUILayout.Label(string.Format("Park Time : {0}", ParkInfo.ParkTime));
            GUILayout.Label(string.Format("Next day/night switch : {0}", Controller.settings.nextDayNightSwitchTime));
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Speed x10"))
            {
                GameController.Instance.setGameSpeed(10);
            }
            GUILayout.EndHorizontal();
        }
    }
}
