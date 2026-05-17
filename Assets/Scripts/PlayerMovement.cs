using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.2f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Better Jump")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Double Jump")]
    [SerializeField] private bool doubleJumpUnlocked = false;
    private bool hasDoubleJump = false;

    [Header("Dash")]
    [SerializeField] private bool dashUnlocked = false;
    [SerializeField] private float dashForce = 15f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldownGround = 0.4f; // cooldown pequeño en suelo
    private bool canDashGround = true;   // controla el cooldown en suelo
    private bool canDashAir = true;      // un solo dash en aire hasta tocar suelo

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isDashing = false;
    private float lastFacingDirection = 1f;

    public event Action OnDashStarted;
    public event Action OnDoubleJumped;

    public float MoveInput => moveInput;
    public float VerticalVelocity => rb != null ? rb.linearVelocity.y : 0f;
    public bool IsGrounded => isGrounded;
    public float FacingDirection => lastFacingDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameProgress.Instance != null)
        {
            if (GameProgress.Instance.HasDoubleJump) UnlockDoubleJump();
            if (GameProgress.Instance.HasDash) UnlockDash();
        }
    }

    public void UnlockDoubleJump() => doubleJumpUnlocked = true;
    public void UnlockDash() => dashUnlocked = true;

    private void Update()
    {
        if (isDashing) return;

        moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;

        if (moveInput > 0.01f) lastFacingDirection = 1f;
        else if (moveInput < -0.01f) lastFacingDirection = -1f;

        bool jumpKeyDown = Keyboard.current.spaceKey.wasPressedThisFrame ||
                           Keyboard.current.wKey.wasPressedThisFrame ||
                           Keyboard.current.upArrowKey.wasPressedThisFrame;

        if (jumpKeyDown)
        {
            if (isGrounded)
            {
                jumpPressed = true;
                hasDoubleJump = doubleJumpUnlocked;
            }
            else if (doubleJumpUnlocked && hasDoubleJump)
            {
                jumpPressed = true;
                hasDoubleJump = false;
                OnDoubleJumped?.Invoke();
            }
        }

        bool dashKeyDown = Keyboard.current.leftShiftKey.wasPressedThisFrame ||
                           Keyboard.current.rightShiftKey.wasPressedThisFrame;

        if (dashKeyDown && dashUnlocked && !isDashing)
        {
            // En suelo: usa cooldown de tiempo
            // En aire: usa el dash de aire (solo uno hasta tocar suelo)
            if (isGrounded && canDashGround)
                StartCoroutine(DashCoroutine());
            else if (!isGrounded && canDashAir)
                StartCoroutine(DashCoroutine());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        // Al aterrizar: restaura dash de aire
        if (isGrounded && !wasGrounded)
        {
            canDashAir = true;
        }

        // Al dejar el suelo sin saltar (caída libre): concede doble salto si corresponde
        if (!isGrounded && wasGrounded && doubleJumpUnlocked && !jumpPressed)
        {
            hasDoubleJump = true;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }

        if (rb.linearVelocity.y < 0)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        else if (rb.linearVelocity.y > 0 &&
                 !(Keyboard.current.spaceKey.isPressed ||
                   Keyboard.current.wKey.isPressed ||
                   Keyboard.current.upArrowKey.isPressed))
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;

        // Marca el recurso usado según dónde se hizo el dash
        if (isGrounded)
            canDashGround = false;
        else
            canDashAir = false;

        OnDashStarted?.Invoke();

        float dashDirection = moveInput != 0 ? Mathf.Sign(moveInput) : lastFacingDirection;
        rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Solo el dash de suelo tiene cooldown de tiempo
        if (!canDashGround)
        {
            yield return new WaitForSeconds(dashCooldownGround);
            canDashGround = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
