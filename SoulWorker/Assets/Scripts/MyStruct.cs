using UnityEngine;
using MyEnum;

namespace MyStruct
{
    public struct KeyInfo
    {
        public KeyInfo(KeyCode key, float time)
        {
            this.key = key;
            backupKey = key;
            this.time = time;
        }

        public KeyCode key;
        public KeyCode backupKey;
        public float time;
    }
}