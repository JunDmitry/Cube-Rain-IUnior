using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isCollidedWithPlatform;

    public event Action<Cube> FirstCollidedWithPlatform;

    public void Reset()
    {
        _isCollidedWithPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _isCollidedWithPlatform == false)
        {
            _isCollidedWithPlatform = true;
            FirstCollidedWithPlatform?.Invoke(this);
        }
    }
}