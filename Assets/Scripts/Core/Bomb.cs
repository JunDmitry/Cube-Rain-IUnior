using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Renderer))]
    public class Bomb : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _explodeForce;
        [SerializeField, Min(0)] private float _explodeRadius;

        private Renderer _renderer;
        private Color _initialColor;

        private Exploder _exploder;

        public Renderer Renderer => _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _initialColor = _renderer.material.color;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explodeRadius);
        }

        public void Reset()
        {
            _renderer.material.color = _initialColor;
        }

        public void Initialize(Exploder exploder)
        {
            _exploder = exploder;
        }

        public void Explode()
        {
            _exploder.Explode(transform.position, _explodeForce, _explodeRadius);
        }
    }
}