using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class PrefabIdentifier<T>
        where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;

        public int Id => _prefab.GetInstanceID();
    }
}