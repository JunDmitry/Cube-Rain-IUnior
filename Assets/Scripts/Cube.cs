using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _isCollidedWithPlatform;
    private Renderer _renderer;
    private Color _initialColor;

    public event Action<Cube> FirstCollidedWithPlatform;

    public Renderer Renderer => _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
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
    }
}