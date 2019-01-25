using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoreRealism
{
    public class Main : IMod
    {
        public Main()
        {
            SetupKeyBinding();
        }

        public void onEnabled()
        {
            EventManager.Instance.OnStartPlayingPark += GameLoadedPark;

            if (GameController.Instance != null)
                GameLoadedPark();
        }

        public void onDisabled()
        {
            MoreRealismManager.Instance.RemoveController();
            EventManager.Instance.OnStartPlayingPark -= GameLoadedPark;
        }

        private void GameLoadedPark()
        {
            if (GameController.Instance.isLoadedFromFile && !GameController.Instance.isInScenarioEditor)
            {
                MoreRealismManager.Instance.controller.Load();
            }
            else
            {
                MoreRealismManager.Instance.RemoveController();
            }
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(Identifier);
            group.keyGroupName = Name;

            InputManager.Instance.registerKeyGroup(group);

            RegisterKey("openSettings", KeyCode.U, "Toggle More Realism settings window",
                "Use this key to toggle the settings window for the park you're currently in");
        }

        private void RegisterKey(string identifier, KeyCode keyCode, string name, string description = "")
        {
            KeyMapping key = new KeyMapping(Identifier + "/" + identifier, keyCode, KeyCode.None);
            key.keyGroupIdentifier = Identifier;
            key.keyName = name;
            key.keyDescription = description;
            InputManager.Instance.registerKeyMapping(key);
        }


        public string Name => "More Realism";

        public string Description => "Add more realism to mechanics in Parkitect.";

        public string Identifier => "TheMasterCado@MoreRealism";

    }
}