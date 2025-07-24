using Assets.Scripts.Utils;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const float Precision = 1e-9f;

    [SerializeField] private float _speed;
    [SerializeField] private InputReader _reader;

    private void FixedUpdate()
    {
        Vector3 direction = new(_reader.HorizontalDirection, 0f, _reader.VerticalDirection);
        direction.Normalize();

        if (Mathf.Abs(direction.x) <= Precision && Mathf.Abs(direction.z) <= Precision)
            return;

        transform.Translate(_speed * Time.deltaTime * direction);
    }
}