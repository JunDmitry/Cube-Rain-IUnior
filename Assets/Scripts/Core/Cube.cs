using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private bool _isCollidedWithPlatform;
    private Renderer _renderer;
    private Color _initialColor;
    private Rigidbody _attachedRigidbody;

    public event Action<Cube> FirstCollidedWithPlatform;

    internal bool IsCollidedWithPlatform => _isCollidedWithPlatform;
    public Renderer Renderer => _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
        _attachedRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _isCollidedWithPlatform == false)
        {
            _isCollidedWithPlatform = true;
            FirstCollidedWithPlatform?.Invoke(this);
        }
    }

    public void Reset()
    {
        _isCollidedWithPlatform = false;
        _renderer.material.color = _initialColor;
        transform.rotation = Quaternion.identity;
        _attachedRigidbody.velocity = Vector3.zero;
        _attachedRigidbody.angularVelocity = Vector3.zero;
    }
}