using System.Collections;
using UnityEngine;

public class IntervalSpawner : MonoBehaviour
{
    private const float MinInterval = 1e-4f;

    [SerializeField, Min(MinInterval)] private float _interval = MinInterval;
    [SerializeField, Min(0f)] private float _minLifetime;
    [SerializeField] private float _maxLifetime;

    [SerializeField] private PositionRandomizer _positionRandomizer;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CubePool _cubePool;

    private void OnEnable()
    {
        _cubePool.Getting += OnGetting;
        _cubePool.Releasing += OnReleasing;
        InvokeRepeating(nameof(Spawn), _interval, _interval);
    }

    private void OnDisable()
    {
        _cubePool.Getting -= OnGetting;
        _cubePool.Releasing -= OnReleasing;
        CancelInvoke(nameof(Spawn));
    }

    private void OnValidate()
    {
        if (_minLifetime > _maxLifetime)
            _maxLifetime = _minLifetime;
    }

    private void Spawn()
    {
        _cubePool.GetCube(_positionRandomizer.GetPosition(), Random.rotation);
    }

    private void OnGetting(Cube cube)
    {
        cube.FirstCollidedWithPlatform += CollidedWithPlatform;
    }

    private void OnReleasing(Cube cube)
    {
        cube.FirstCollidedWithPlatform -= CollidedWithPlatform;
    }

    private void CollidedWithPlatform(Cube cube)
    {
        _colorChanger.Change(cube);
        StartCoroutine(ReleaseWithDelay(cube));
    }

    private IEnumerator ReleaseWithDelay(Cube cube)
    {
        yield return null;
        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));

        _cubePool.ReleaseCube(cube);
    }
}