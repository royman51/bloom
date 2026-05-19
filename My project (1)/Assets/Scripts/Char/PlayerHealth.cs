using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    public float respawnDelay = 1.2f;

    public EndImpactEffect impactEffect;

    private Vector3 startPosition;

    private Rigidbody2D rb;

    private SpriteRenderer[] renderers;
    private Collider2D[] colliders;

    protected override void Awake()
    {
        base.Awake();

        startPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();

        renderers = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponentsInChildren<Collider2D>();
    }

    protected override void Die()
    {
        if (dead)
        {
            return;
        }

        base.Die();

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (impactEffect != null)
        {
            impactEffect.PlayImpact(transform.position);
        }

        SetVisible(false);
        SetCollision(false);

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }

        yield return new WaitForSeconds(respawnDelay);

        transform.position = startPosition;

        currentHealth = maxHealth;

        dead = false;

        if (rb != null)
        {
            rb.simulated = true;
            rb.velocity = Vector2.zero;
        }

        SetVisible(true);
        SetCollision(true);
    }

    private void SetVisible(bool visible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].enabled = visible;
            }
        }
    }

    private void SetCollision(bool enabled)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null)
            {
                colliders[i].enabled = enabled;
            }
        }
    }
}