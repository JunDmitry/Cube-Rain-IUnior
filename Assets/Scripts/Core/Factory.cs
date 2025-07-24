using UnityEngine;

namespace Assets.Scripts
{
    public class Factory<T>
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;

        public Factory(T prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        private Factory(MonoBehaviour prefab, Transform container) : this((T)prefab, container)
        {
        }

        public T Create()
        {
            T obj = GameObject.Instantiate(_prefab, _container);

            return obj;
        }
    }
}