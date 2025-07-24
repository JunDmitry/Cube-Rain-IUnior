using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private const float MinInterval = 1e-4f;

    [SerializeField, Min(MinInterval)] private float _baseInterval = MinInterval;
    [SerializeField, Min(0)] private float _cycleLengthInSeconds;
    [SerializeField] private AnimationCurve _curve;

    private float _elapsedCycleSeconds;

    public float Interval => _baseInterval * _curve.Evaluate(_elapsedCycleSeconds / _cycleLengthInSeconds);

    private void Update()
    {
        _elapsedCycleSeconds = (_elapsedCycleSeconds + Time.deltaTime) % _cycleLengthInSeconds;
    }
}