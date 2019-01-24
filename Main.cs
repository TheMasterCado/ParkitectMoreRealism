using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MoreRealism
{
    public class Main : IMod, IModSettings
    {
        private GameObject _go;
        public string Identifier { get; set; }
        public static Configuration configuration { get; private set; }

        public void onEnabled()
        {
            if (configuration == null)
            {
                configuration = new Configuration();
                configuration.Load();
                configuration.Save();
            }

            _go = new GameObject();
            var modController = _go.AddComponent<MoreRealismController>();
            modController.Load();
        }

        public void onDisabled()
        {
            Object.Destroy(_go);
        }


        public string Name => "More Realism";

        public string Description => "Add more realism to mechanics in Parkitect.";

        string IMod.Identifier => "MoreRealism";


        #region Implementation of IModSettings

        private bool FetchKey(out KeyCode outKey)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(key))
                {
                    outKey = key;
                    return true;
                }

            outKey = KeyCode.A;
            return false;
        }


        public void onDrawSettingsUI()
        {
            configuration.DrawGUI();
        }

        public void onSettingsOpened()
        {
            if (configuration == null)
                configuration = new Configuration();
            configuration.Load();
        }

        public void onSettingsClosed()
        {
            configuration.Save();
        }

        #endregion
    }
}