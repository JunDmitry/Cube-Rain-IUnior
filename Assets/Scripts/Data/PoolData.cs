using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class PoolData
    {
        public MonoBehaviour Prefab;
        public Transform Container;
        [Min(0)] public int InitialSize;
    }
}