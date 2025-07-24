using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class PoolView<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private const string CreateTextFormat = "Created {0}";
        private const string ArriveTextFormat = "Arrived {0}";
        private const string ActiveTextFormat = "Active {0}";

        [SerializeField] private PoolContainer _poolContainer;
        [SerializeField] private PrefabIdentifier<T> _identifier;

        [SerializeField] private TextMeshProUGUI _arriveToSceneCountText;
        [SerializeField] private TextMeshProUGUI _createCountText;
        [SerializeField] private TextMeshProUGUI _activeCountText;

        private MonoPool<T> _pool;

        private void Awake()
        {
            _poolContainer.Initialize();
            _pool = _poolContainer.Get<T>(_identifier.Id);
        }

        private void OnEnable()
        {
            _pool.Created += OnCreated;
            _pool.Getted += OnGetted;
            _pool.Released += OnReleased;

            OnCreated(null);
            OnGetted(null);
            OnReleased(null);
        }

        private void OnDisable()
        {
            _pool.Created -= OnCreated;
            _pool.Getted -= OnGetted;
            _pool.Released -= OnReleased;
        }

        private void OnCreated(T entity)
        {
            _createCountText.text = string.Format(CreateTextFormat, _pool.ObjectsCreatedCount);
        }

        private void OnGetted(T entity)
        {
            _arriveToSceneCountText.text = string.Format(ArriveTextFormat, _pool.ObjectsArriveToSceneCount);
            _activeCountText.text = string.Format(ActiveTextFormat, _pool.ObjectsActiveCount);
        }

        private void OnReleased(T entity)
        {
            _activeCountText.text = string.Format(ActiveTextFormat, _pool.ObjectsActiveCount);
        }
    }
}