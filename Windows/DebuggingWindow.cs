using System;
using UnityEngine;

namespace MoreRealism.Windows
{
    public class DebuggingWindow : BaseWindow
    {

        public DebuggingWindow(MoreRealismController mrController) : base(mrController)
        {
            windowName = "More Realism - Debug";
            WindowRect = new Rect(20, 20, 250, 250);
        }

        public override void DrawContent()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(string.Format("MoreRealism {0}", MoreRealismController.VERSION));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("-----------------------------------");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            GUILayout.Label(string.Format("Park Time : {0}", ParkInfo.ParkTime));
            GUILayout.Label(string.Format("Next day/night switch : {0}", Controller.settings.nextDayNightSwitchTime));
            GUILayout.Label(string.Format("Next park state switch : {0}", Controller.settings.nextParkStateSwitchTime));
            GUILayout.Label(string.Format("Next shops/attractions state switch : {0}", Controller.settings.nextEverythingStateSwitchTime));
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
