using UnityEngine;
using System.Collections;

public class CameraShakeEffect : MonoBehaviour
{
    private Vector3 originalPos;
    private Coroutine shakeCoroutine;

    public void Shake(float time, float power)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine(time, power));
    }

    private IEnumerator ShakeRoutine(float time, float power)
    {
        originalPos = transform.position;

        float timer = 0f;

        while (timer < time)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-power, power);
            float y = Random.Range(-power, power);

            transform.position = originalPos + new Vector3(x, y, 0f);

            yield return null;
        }

        transform.position = originalPos;
    }
}