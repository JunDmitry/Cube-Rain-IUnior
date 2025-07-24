using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Difficulty _difficulty;
    [SerializeField] private PositionRandomizer _positionRandomizer;
    [SerializeField] private ColorChanger _colorChanger;

    private void Start()
    {
        Spawn();
    }

    protected override void OnCreated(Cube entity)
    {
        entity.gameObject.SetActive(false);
    }

    protected override void OnGetted(Cube entity)
    {
        entity.transform.SetPositionAndRotation(_positionRandomizer.GetPosition(), Random.rotation);
        entity.FirstCollidedWithPlatform += OnFirstCollidedWithPlatform;
        entity.gameObject.SetActive(true);
    }

    protected override void OnReleased(Cube entity)
    {
        entity.FirstCollidedWithPlatform -= OnFirstCollidedWithPlatform;
        entity.Reset();
        entity.gameObject.SetActive(false);
    }

    private void Spawn()
    {
        StartCoroutine(BeginSpawn());
    }

    private IEnumerator BeginSpawn()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_difficulty.Interval);
            SpawnPool.Get();
        }
    }

    private void OnFirstCollidedWithPlatform(Cube cube)
    {
        _colorChanger.Change(cube.Renderer);
        StartCoroutine(ReleaseWithDelay(cube));
    }

    private IEnumerator ReleaseWithDelay(Cube cube)
    {
        float lifetime = Random.Range(MinLifetime, MaxLifetime);

        yield return new WaitForSeconds(lifetime);

        SpawnPool.Release(cube);
    }
}