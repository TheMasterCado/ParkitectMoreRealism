using System;
using System.Collections.Generic;
using System.IO;
using MiniJSON;
using UnityEngine;

namespace MoreRealism
{
    public class Configuration
    {
        private static GUIStyle ToggleButtonStyleNormal;
        private static GUIStyle ToggleButtonStyleToggled;
        private readonly string _path;

        private int keySelectionId = -1;


        public Configuration()
        {
            _path = FilePaths.getFolderPath("more_realism.config");
            settings = new MoreRealism.ModSettings();
        }

        public MoreRealism.ModSettings settings { get; }

        public void Save()
        {
            var context = new SerializationContext(SerializationContext.Context.Savegame);

            using (var stream = new FileStream(_path, FileMode.Create))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Json.Serialize(Serializer.serialize(context, settings)));
                }
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_path))
                    using (var reader = new StreamReader(_path))
                    {
                        string jsonString;

                        var context = new SerializationContext(SerializationContext.Context.Savegame);
                        while ((jsonString = reader.ReadLine()) != null)
                        {
                            var values = (Dictionary<string, object>) Json.Deserialize(jsonString);
                            Serializer.deserialize(context, settings, values);
                        }

                        reader.Close();
                    }
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't properly load settings file! " + e);
            }
        }


        public void DrawGUI()
        {
            if (ToggleButtonStyleNormal == null)
            {
                ToggleButtonStyleNormal = "Button";
                ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
                ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Open Window:");
            settings.openWindow = KeyToggle(settings.openWindow, 0);
            GUILayout.EndHorizontal();
        }

        public KeyCode KeyToggle(KeyCode character, int id)
        {
            if (GUILayout.Button(character.ToString(),
                keySelectionId == id ? ToggleButtonStyleToggled : ToggleButtonStyleNormal)) keySelectionId = id;

            if (keySelectionId == id)
            {
                KeyCode e;
                if (FetchKey(out e))
                {
                    keySelectionId = -1;
                    return e;
                }
            }

            return character;
        }

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
    }
}