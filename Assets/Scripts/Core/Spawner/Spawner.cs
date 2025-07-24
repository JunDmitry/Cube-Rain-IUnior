using Assets.Scripts;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
    where T : UnityEngine.MonoBehaviour
{
    [SerializeField, Min(0f)] protected float MinLifetime;
    [SerializeField] protected float MaxLifetime;
    [SerializeField] protected PoolContainer PoolContainer;
    [SerializeField] private PrefabIdentifier<T> _spawnIdentifier;

    protected MonoPool<T> SpawnPool;

    protected virtual void Awake()
    {
        PoolContainer.Initialize();
        SpawnPool = PoolContainer.Get<T>(_spawnIdentifier.Id);
    }

    protected virtual void OnEnable()
    {
        SpawnPool.Created += OnCreated;
        SpawnPool.Getted += OnGetted;
        SpawnPool.Released += OnReleased;
    }

    protected virtual void OnDisable()
    {
        SpawnPool.Created -= OnCreated;
        SpawnPool.Getted -= OnGetted;
        SpawnPool.Released -= OnReleased;
    }

    private void OnValidate()
    {
        if (MinLifetime > MaxLifetime)
            MaxLifetime = MinLifetime;
    }

    protected abstract void OnCreated(T entity);

    protected abstract void OnGetted(T entity);

    protected abstract void OnReleased(T entity);
}