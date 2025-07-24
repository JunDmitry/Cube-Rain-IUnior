using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
    public class PoolContainer : MonoBehaviour
    {
        [SerializeField] private PoolData[] _poolDatas;

        private Dictionary<int, Pool> _pools;

        private bool _initialized;

        private void Awake()
        {
            Initialize();
        }

        public MonoPool<T> Get<T>(int prefabId)
            where T : MonoBehaviour
        {
            return (MonoPool<T>)_pools[prefabId];
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            _pools = new Dictionary<int, Pool>();
            Pool pool;
            int prefabId;

            foreach (PoolData poolData in _poolDatas)
            {
                prefabId = poolData.Prefab.GetInstanceID();

                if (_pools.TryGetValue(prefabId, out pool) == false)
                {
                    pool = CreatePool(poolData);
                    _pools[prefabId] = pool;
                }
            }
        }

        private Pool CreatePool(PoolData poolData)
        {
            Type objType = poolData.Prefab.GetType();
            Type factoryType = typeof(Factory<>).MakeGenericType(objType);
            Type poolType = typeof(MonoPool<>).MakeGenericType(objType);

            object factory = CreateObject(factoryType, new object[] { poolData.Prefab, poolData.Container });
            Pool pool = (Pool)CreateObject(poolType, new object[] { factory, poolData.InitialSize });

            return pool;
        }

        private object CreateObject(Type type, object[] args)
        {
            return Activator.CreateInstance(type, BindingFlags.NonPublic | BindingFlags.Instance, null, args, CultureInfo.InvariantCulture);
        }
    }
}