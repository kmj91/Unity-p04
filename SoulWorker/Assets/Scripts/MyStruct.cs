using UnityEngine;
using MyEnum;

namespace MyStruct
{
    public struct KeyInfo
    {
        public KeyInfo(KeyState state, KeyCode key, float time)
        {
            this.state = state;
            this.key = key;
            this.time = time;
        }

        public KeyState state;
        public KeyCode key;
        public float time;
    }
}