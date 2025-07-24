using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;

    public void Change(Renderer renderer)
    {
        StartCoroutine(ChangeColorSmoothly(renderer.material, Random.ColorHSV(), _smoothDuration));
    }

    public void ChangeToInvisible(Renderer renderer, float duration)
    {
        Color source = renderer.material.color;

        StartCoroutine(ChangeColorSmoothly(renderer.material, new(source.r, source.g, source.b, 0f), duration));
    }

    private IEnumerator ChangeColorSmoothly(Material material, Color color, float duration)
    {
        Color source = material.color;
        float elapsedSeconds = 0f;

        while (elapsedSeconds < duration)
        {
            elapsedSeconds += Time.deltaTime;

            material.color = Color.Lerp(source, color, elapsedSeconds / duration);
            yield return null;
        }
    }
}