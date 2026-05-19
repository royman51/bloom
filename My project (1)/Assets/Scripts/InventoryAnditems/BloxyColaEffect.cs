using System.Collections;
using UnityEngine;

public class BloxyColaEffect : MonoBehaviour
{
    private Coroutine effectRoutine;

    public void StartColaEffect(
        float speedBonus,
        float duration,
        float targetFOV,
        float fovChangeSpeed,
        float orthographicSizeMultiplier
    )
    {
        if (effectRoutine != null)
        {
            StopCoroutine(effectRoutine);
        }

        effectRoutine = StartCoroutine(
            EffectRoutine(
                speedBonus,
                duration,
                targetFOV,
                fovChangeSpeed,
                orthographicSizeMultiplier
            )
        );
    }

    private IEnumerator EffectRoutine(
        float speedBonus,
        float duration,
        float targetFOV,
        float fovChangeSpeed,
        float orthographicSizeMultiplier
    )
    {
        PlayerMove move = GetComponent<PlayerMove>();
        Camera cam = Camera.main;

        float originalFOV = 60f;
        float originalOrthoSize = 5f;

        if (cam != null)
        {
            originalFOV = cam.fieldOfView;
            originalOrthoSize = cam.orthographicSize;
        }

        if (move != null)
        {
            move.SetSpeedBonus(speedBonus);
        }

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (cam != null)
            {
                if (cam.orthographic == false)
                {
                    cam.fieldOfView = Mathf.Lerp(
                        cam.fieldOfView,
                        targetFOV,
                        Time.deltaTime * fovChangeSpeed
                    );
                }
                else
                {
                    cam.orthographicSize = Mathf.Lerp(
                        cam.orthographicSize,
                        originalOrthoSize * orthographicSizeMultiplier,
                        Time.deltaTime * fovChangeSpeed
                    );
                }
            }

            yield return null;
        }

        if (move != null)
        {
            move.SetSpeedBonus(0f);
        }

        float backTimer = 0f;

        while (backTimer < 0.4f)
        {
            backTimer += Time.deltaTime;

            if (cam != null)
            {
                if (cam.orthographic == false)
                {
                    cam.fieldOfView = Mathf.Lerp(
                        cam.fieldOfView,
                        originalFOV,
                        Time.deltaTime * fovChangeSpeed
                    );
                }
                else
                {
                    cam.orthographicSize = Mathf.Lerp(
                        cam.orthographicSize,
                        originalOrthoSize,
                        Time.deltaTime * fovChangeSpeed
                    );
                }
            }

            yield return null;
        }

        if (cam != null)
        {
            if (cam.orthographic == false)
            {
                cam.fieldOfView = originalFOV;
            }
            else
            {
                cam.orthographicSize = originalOrthoSize;
            }
        }

        effectRoutine = null;
    }
}