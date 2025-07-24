using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BombSpawner : Spawner<Bomb>
    {
        [SerializeField] private ColorChanger _colorChanger;
        [SerializeField] private PrefabIdentifier<Cube> _cubeIdentifier;

        private MonoPool<Cube> _cubePool;
        private Exploder _bombExploder;

        protected override void Awake()
        {
            base.Awake();

            _bombExploder = new();
            _cubePool = PoolContainer.Get<Cube>(_cubeIdentifier.Id);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _cubePool.Released += OnReleasedCube;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _cubePool.Released -= OnReleasedCube;
        }

        protected override void OnCreated(Bomb entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected override void OnGetted(Bomb entity)
        {
            entity.Initialize(_bombExploder);
            entity.gameObject.SetActive(true);
            StartCoroutine(ExplodeWithDelay(entity));
        }

        protected override void OnReleased(Bomb entity)
        {
            entity.Reset();
            entity.gameObject.SetActive(false);
        }

        private IEnumerator ExplodeWithDelay(Bomb entity)
        {
            float lifetime = Random.Range(MinLifetime, MaxLifetime);
            _colorChanger.ChangeToInvisible(entity.Renderer, lifetime);

            yield return new WaitForSeconds(lifetime);

            entity.Explode();
            SpawnPool.Release(entity);
        }

        private void OnReleasedCube(Cube obj)
        {
            Spawn(obj.transform.position);
        }

        private void Spawn(Vector3 position)
        {
            Bomb bomb = SpawnPool.Get();
            bomb.transform.position = position;
        }
    }
}