using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;

    [HideInInspector]
    public int currentHealth;

    protected bool dead = false;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (dead)
        {
            return;
        }

        currentHealth -= damage;

        Debug.Log(gameObject.name + " Ã¼·Â: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        if (dead)
        {
            return;
        }

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    protected virtual void Die()
    {
        dead = true;

        Debug.Log(gameObject.name + " »ç¸Á");
    }
}