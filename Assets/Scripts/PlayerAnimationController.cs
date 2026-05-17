using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private readonly int dashTriggeredHash = Animator.StringToHash("DashTriggered");
    private readonly int doubleJumpTriggeredHash = Animator.StringToHash("DoubleJumpTriggered");

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
        if (animator == null)
            animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerMovement.OnDashStarted += HandleDash;
        playerMovement.OnDoubleJumped += HandleDoubleJump;
    }

    private void OnDisable()
    {
        playerMovement.OnDashStarted -= HandleDash;
        playerMovement.OnDoubleJumped -= HandleDoubleJump;
    }

    private void HandleDash()
    {
        animator.SetTrigger(dashTriggeredHash);
    }

    private void HandleDoubleJump()
    {
        animator.SetTrigger(doubleJumpTriggeredHash);
    }

    private void Update()
    {
        animator.SetFloat(speedHash, Mathf.Abs(playerMovement.MoveInput));
        animator.SetBool(isGroundedHash, playerMovement.IsGrounded);
        animator.SetFloat(verticalVelocityHash, playerMovement.VerticalVelocity);
    }

    private void LateUpdate()
    {
        if (playerMovement.FacingDirection < 0f)
            spriteRenderer.flipX = true;
        else if (playerMovement.FacingDirection > 0f)
            spriteRenderer.flipX = false;
    }
}