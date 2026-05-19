using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 7f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform wallCheck;
    public float wallCheckDistance = 0.45f;
    public LayerMask wallLayer;

    public float wallJumpXPower = 9f;
    public float wallJumpYPower = 8f;
    public float wallJumpMoveLockTime = 0.25f;

    public float wallSlideSpeed = -1.5f;

    private Rigidbody2D rb;
    private InputController input;
    private PlayerMove move;

    private bool grounded;
    private bool wall;
    private int wallDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputController>();
        move = GetComponent<PlayerMove>();

        input.OnJumpEvent += Jump;
    }

    private void Update()
    {
        grounded = false;
        wall = false;
        wallDir = 0;

        if (groundCheck != null)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
            {
                grounded = true;
            }
        }

        if (wallCheck != null)
        {
            RaycastHit2D r = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);
            RaycastHit2D l = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, wallLayer);

            if (r.collider != null)
            {
                wall = true;
                wallDir = 1;
            }

            if (l.collider != null)
            {
                wall = true;
                wallDir = -1;
            }
        }

        if (wall == true && grounded == false)
        {
            if (rb.velocity.y < wallSlideSpeed)
            {
                rb.velocity = new Vector2(0f, wallSlideSpeed);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if (grounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        else
        {
            if (wall == true)
            {
                rb.velocity = Vector2.zero;

                if (wallDir == 1)
                {
                    rb.AddForce(new Vector2(-wallJumpXPower, wallJumpYPower), ForceMode2D.Impulse);
                }
                else
                {
                    if (wallDir == -1)
                    {
                        rb.AddForce(new Vector2(wallJumpXPower, wallJumpYPower), ForceMode2D.Impulse);
                    }
                }

                if (move != null)
                {
                    move.LockMove(wallJumpMoveLockTime);
                }

                wall = false;
                wallDir = 0;
            }
        }
    }

    private void OnDestroy()
    {
        if (input != null)
        {
            input.OnJumpEvent -= Jump;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.left * wallCheckDistance);
        }
    }
}