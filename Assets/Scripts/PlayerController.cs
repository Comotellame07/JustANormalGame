using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.15f;

    [Header("Advanced")]
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool canDash = false;
    [SerializeField] private float dashForce = 14f;
    [SerializeField] private float dashDuration = 0.15f;

    private float moveInput;
    private bool jumpInput;
    private bool dashInput;

    private bool isGrounded;
    private bool canUseDoubleJump;
    private bool isDashing;
    private float dashTimer;
    private int facingDirection = 1;

    public float MoveInput => moveInput;
    public float VerticalVelocity => rb.linearVelocity.y;
    public bool IsGrounded => isGrounded;
    public bool IsDashing => isDashing;
    public int FacingDirection => facingDirection;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);

        if (moveInput > 0.01f)
            facingDirection = 1;
        else if (moveInput < -0.01f)
            facingDirection = -1;

        CheckGround();

        if (isGrounded)
            canUseDoubleJump = canDoubleJump;

        if (jumpInput)
            HandleJump();

        if (canDash && dashInput && !isDashing)
            StartDash();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
            else
            {
                rb.linearVelocity = new Vector2(facingDirection * dashForce, 0f);
                return;
            }
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (canUseDoubleJump)
        {
            canUseDoubleJump = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}