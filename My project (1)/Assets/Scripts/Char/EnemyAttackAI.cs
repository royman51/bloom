using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public Transform target;

    public float detectRange = 6f;
    public float attackRange = 1.2f;

    public float moveSpeed = 2.5f;

    public int damage = 10;
    public float attackCooldown = 1f;

    private Rigidbody2D rb;
    private float attackTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectRange)
        {
            if (distance > attackRange)
            {
                MoveToPlayer();
            }
            else
            {
                StopMove();
                AttackPlayer();
            }
        }
        else
        {
            StopMove();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 dir = target.position - transform.position;
        dir.Normalize();

        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    private void StopMove()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void AttackPlayer()
    {
        if (attackTimer > 0f)
        {
            return;
        }

        PlayerHealth health = target.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        attackTimer = attackCooldown;
    }
}