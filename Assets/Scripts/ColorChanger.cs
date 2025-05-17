using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;

    public void Change(Cube cube)
    {
        StartCoroutine(ChangeColorSmoothly(cube.Renderer.material, Random.ColorHSV()));
    }

    private IEnumerator ChangeColorSmoothly(Material material, Color color)
    {
        Color source = material.color;
        float elapsedSeconds = 0f;

        while (elapsedSeconds < _smoothDuration)
        {
            elapsedSeconds += Time.deltaTime;

            material.color = Color.Lerp(source, color, elapsedSeconds / _smoothDuration);
            yield return null;
        }
    }
}