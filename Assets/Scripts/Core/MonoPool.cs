using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MonoPool<T> : Pool
        where T : MonoBehaviour
    {
        private readonly Factory<T> _factory;
        private readonly Stack<T> _pool;

        public event Action<T> Created;

        public event Action<T> Getted;

        public event Action<T> Released;

        public int ObjectsArriveToSceneCount { get; private set; }
        public int ObjectsCreatedCount { get; private set; }
        public int ObjectsActiveCount { get; private set; }

        protected MonoPool(object factory, int initialSize) : base(initialSize)
        {
            _factory = (Factory<T>)factory;
            _pool = new();

            Initialize();
        }

        public T Get()
        {
            T obj = _pool.Count == 0 ? Create() : _pool.Pop();
            ObjectsArriveToSceneCount++;
            ObjectsActiveCount++;
            Getted?.Invoke(obj);

            return obj;
        }

        public void Release(T obj)
        {
            _pool.Push(obj);
            ObjectsActiveCount--;
            Released?.Invoke(obj);
        }

        private void Initialize()
        {
            T obj;

            while (_pool.Count < InitialSize)
            {
                obj = Create();
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
        }

        private T Create()
        {
            T obj = _factory.Create();
            ObjectsCreatedCount++;
            Created?.Invoke(obj);

            return obj;
        }
    }
}