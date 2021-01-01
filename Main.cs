using System;
using UnityEngine;

namespace MoreRealism
{
    public class Main : AbstractMod
    {
        private GameObject _controllerGO;
        private MoreRealismController _controllerPrefab;

        public Main()
        {
            SetupKeyBinding();
        }

        public override void onEnabled()
        {
            GameObject controllerPrefabGO = new GameObject();
            _controllerPrefab = controllerPrefabGO.AddComponent<MoreRealismController>();
            _controllerPrefab.SetAsPrefab();
            AssetManager.Instance.registerObject(_controllerPrefab);

            _controllerGO = new GameObject();
            MoreRealismController.Instance = _controllerGO.AddComponent<MoreRealismController>();
            EventManager.Instance.OnStartPlayingPark += MoreRealismController.Instance.Load;
        }

        public override void onDisabled()
        {
            if (MoreRealismController.Instance != null)
            {
                EventManager.Instance.OnStartPlayingPark -= MoreRealismController.Instance.Load;
                MoreRealismController.Instance.TryKill();
            }
            AssetManager.Instance.unregisterObject(_controllerPrefab);
           _controllerPrefab.TryKill();
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(getIdentifier());
            group.keyGroupName = getName();

            InputManager.Instance.registerKeyGroup(group);

            RegisterKey("openSettings", KeyCode.U, "Toggle More Realism settings window",
                "Use this key to toggle the settings window for the park you're currently in");
        }

        private void RegisterKey(string identifier, KeyCode keyCode, string name, string description = "")
        {
            KeyMapping key = new KeyMapping(getIdentifier() + "/" + identifier, keyCode, KeyCode.None);
            key.keyGroupIdentifier = getIdentifier();
            key.keyName = name;
            key.keyDescription = description;
            InputManager.Instance.registerKeyMapping(key);
        }

        public override bool isMultiplayerModeCompatible()
        {
            return true;
        }

        public override bool isRequiredByAllPlayersInMultiplayerMode()
        {
            return true;
        }

        public override string getName()
        {
            return "More Realism";
        }

        public override string getDescription()
        {
            return "Add new mechanics to make the game more realistic";
        }

        public override string getIdentifier()
        {
            return "TheMasterCado@MoreRealism";
        }

        public override string getVersionNumber()
        {
            return MoreRealismController.Version;
        }
    }
}