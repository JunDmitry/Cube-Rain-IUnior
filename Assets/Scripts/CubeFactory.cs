using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _container;

    private void Awake()
    {
        if (_container == null)
            _container = transform;
    }

    public Cube Create()
    {
        Cube cube = Instantiate(_prefab, _container);
        cube.gameObject.SetActive(false);

        return cube;
    }
}