using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoreRealism
{
    public class Main : IMod
    {
        private GameObject _go;

        public Main()
        {
            setupKeyBinding();
        }

        public void onEnabled()
        {
            _go = new GameObject();
            _go.AddComponent<MoreRealismController>();
            EventManager.Instance.OnStartPlayingPark += gameLoadedPark;
        }

        public void onDisabled()
        {
            Object.Destroy(_go);
        }

        private void gameLoadedPark()
        {
            _go.GetComponent<MoreRealismController>().Load();
        }

        private void setupKeyBinding()
        {
            KeyGroup group = new KeyGroup(Identifier);
            group.keyGroupName = Name;

            InputManager.Instance.registerKeyGroup(group);

            registerKey("openSettings", KeyCode.U, "Toggle More Realism settings window",
                "Use this key to toggle the settings window for the park you're currently in");
        }

        private void registerKey(string identifier, KeyCode keyCode, string name, string description = "")
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