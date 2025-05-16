using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    [SerializeField] private Vector3 _leftBottom;
    [SerializeField] private Vector3 _rightTop;

    private void OnValidate()
    {
        if (_leftBottom.x > _rightTop.x)
            _leftBottom.x = _rightTop.x;

        if (_leftBottom.y > _rightTop.y)
            _leftBottom.y = _rightTop.y;

        if (_leftBottom.z > _rightTop.z)
            _leftBottom.z = _rightTop.z;
    }

    public Vector3 GetPosition()
    {
        return new(Random.Range(_leftBottom.x, _rightTop.x),
                   Random.Range(_leftBottom.y, _rightTop.y),
                   Random.Range(_leftBottom.z, _rightTop.z));
    }
}