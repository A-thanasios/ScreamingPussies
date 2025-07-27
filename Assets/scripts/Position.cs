using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public struct Position
    {
        public int x;
        public int y;

        public Vector2 GetVector() => new Vector2(x, y);
    }
}