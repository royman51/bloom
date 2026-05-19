using UnityEngine;

public class EnemyHealth : Health
{
    public EndImpactEffect impactEffect;

    protected override void Die()
    {
        if (dead)
        {
            return;
        }

        base.Die();

        if (impactEffect != null)
        {
            impactEffect.PlayImpact(transform.position);
        }

        Destroy(gameObject);
    }
}