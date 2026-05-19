using UnityEngine;

public class EndImpactEffect : MonoBehaviour
{
    public int fragmentCount = 24;
    public float fragmentSpeed = 8f;
    public float fragmentLifeTime = 1.5f;

    public CameraShakeEffect cameraShake;
    public float shakeTime = 0.25f;
    public float shakePower = 0.25f;

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        PlayerMove playerMove = other.GetComponent<PlayerMove>();

        if (playerMove == null)
        {
            playerMove = other.GetComponentInParent<PlayerMove>();
        }

        if (playerMove == null)
        {
            return;
        }

        used = true;

        PlayImpact(playerMove.transform.position);

        playerMove.gameObject.SetActive(false);
    }

    public void PlayImpact(Vector3 pos)
    {
        if (cameraShake != null)
        {
            cameraShake.Shake(shakeTime, shakePower);
        }

        SpawnFragments(pos);
    }

    private void SpawnFragments(Vector3 pos)
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            GameObject frag = new GameObject("PlayerFragment");

            frag.transform.position = pos;
            frag.transform.localScale = Vector3.one * Random.Range(0.12f, 0.3f);

            SpriteRenderer sr = frag.AddComponent<SpriteRenderer>();
            sr.sprite = CreateSprite();
            sr.color = new Color(Random.Range(0.15f, 0.35f), 0f, 0f, 1f);

            Rigidbody2D rb = frag.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1.2f;

            TrailRenderer trail = frag.AddComponent<TrailRenderer>();
            trail.time = 0.35f;
            trail.startWidth = 0.12f;
            trail.endWidth = 0f;
            trail.material = new Material(Shader.Find("Sprites/Default"));
            trail.startColor = new Color(0.35f, 0f, 0f, 0.9f);
            trail.endColor = new Color(0.05f, 0f, 0f, 0f);

            Vector2 dir = Random.insideUnitCircle.normalized;

            if (dir.y < -0.2f)
            {
                dir.y = Mathf.Abs(dir.y);
            }

            rb.velocity = dir * Random.Range(fragmentSpeed * 0.6f, fragmentSpeed * 1.3f);
            rb.angularVelocity = Random.Range(-500f, 500f);

            Destroy(frag, fragmentLifeTime);
        }
    }

    private Sprite CreateSprite()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();

        return Sprite.Create(
            tex,
            new Rect(0, 0, 1, 1),
            Vector2.one * 0.5f
        );
    }
}