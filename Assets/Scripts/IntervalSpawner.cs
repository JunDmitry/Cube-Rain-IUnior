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

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _cubePool.Getted += OnGetted;
        _cubePool.Released += OnReleased;
        _coroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        _cubePool.Getted -= OnGetted;
        _cubePool.Released -= OnReleased;
        StopCoroutine(_coroutine);
    }

    private void OnValidate()
    {
        if (_minLifetime > _maxLifetime)
            _maxLifetime = _minLifetime;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitSeconds = new(_interval);

        while (enabled)
        {
            yield return waitSeconds;
            _cubePool.GetCube(_positionRandomizer.GetPosition(), Random.rotation);
        }
    }

    private void OnGetted(Cube cube)
    {
        cube.FirstCollidedWithPlatform += CollidedWithPlatform;
    }

    private void OnReleased(Cube cube)
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