using System;
using UnityEngine;

namespace MoreRealism
{
    public class Main : IMod
    {
        private GameObject _controllerGO;
        private MoreRealismController _controllerPrefab;

        public Main()
        {
            SetupKeyBinding();
        }

        public void onEnabled()
        {
            GameObject controllerPrefabGO = new GameObject();
            _controllerPrefab = controllerPrefabGO.AddComponent<MoreRealismController>();
            _controllerPrefab.SetAsPrefab();
            AssetManager.Instance.registerObject(_controllerPrefab);

            _controllerGO = new GameObject();
            MoreRealismController.Instance = _controllerGO.AddComponent<MoreRealismController>();
        }

        public void onDisabled()
        {
            MoreRealismController.Instance.Kill();
            AssetManager.Instance.unregisterObject(_controllerPrefab);
            _controllerPrefab.Kill();
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