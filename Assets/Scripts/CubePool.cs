using System;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeFactory _factory;
    [SerializeField, Min(0)] private int _initialCount;

    private Stack<Cube> _pool;

    public event Action<Cube> Getted;

    public event Action<Cube> Released;

    private void Awake()
    {
        _pool = new(_initialCount);
    }

    private void Start()
    {
        while (_pool.Count < _initialCount)
            _pool.Push(_factory.Create());
    }

    public Cube GetCube(Vector3 position, Quaternion rotation)
    {
        Cube cube = _pool.Count == 0 ? _factory.Create() : _pool.Pop();
        cube.transform.SetPositionAndRotation(position, rotation);
        cube.gameObject.SetActive(true);
        Getted?.Invoke(cube);

        return cube;
    }

    public void ReleaseCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.Reset();
        _pool.Push(cube);
        Released?.Invoke(cube);
    }
}