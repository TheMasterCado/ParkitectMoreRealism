using UnityEngine;

namespace MoreRealism
{
    public class ModSettings : SerializedRawObject
    {
        public ModSettings()
        {
            openWindow = KeyCode.U;
        }

        [Serialized] public KeyCode openWindow { get; set; }
    }
}