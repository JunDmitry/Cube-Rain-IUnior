using UnityEngine;

public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const float Precision = 1e-9f;

    [SerializeField] private float _speed;

    private float _horizontalDirection;
    private float _verticalDirection;

    private void FixedUpdate()
    {
        if (Mathf.Abs(_horizontalDirection) <= Precision && Mathf.Abs(_verticalDirection) <= Precision)
            return;

        Vector3 direction = new(_horizontalDirection, 0f, _verticalDirection);
        direction.Normalize();

        transform.Translate(_speed * Time.deltaTime * direction);
    }

    private void Update()
    {
        _horizontalDirection = Input.GetAxis(Horizontal);
        _verticalDirection = Input.GetAxis(Vertical);
    }
}