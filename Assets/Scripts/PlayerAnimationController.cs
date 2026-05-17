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
    private readonly int isDashingHash = Animator.StringToHash("IsDashing");
    private readonly int doubleJumpTriggeredHash = Animator.StringToHash("DoubleJumpTriggered");

    private void Reset()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetFloat(speedHash, Mathf.Abs(playerMovement.MoveInput));
        animator.SetBool(isGroundedHash, playerMovement.IsGrounded);
        animator.SetFloat(verticalVelocityHash, playerMovement.VerticalVelocity);
        animator.SetBool(isDashingHash, playerMovement.IsDashing);

        if (playerMovement.DoubleJumpTriggered)
        {
            animator.SetTrigger(doubleJumpTriggeredHash);
            playerMovement.ConsumeDoubleJumpTrigger();
        }
    }

    private void LateUpdate()
    {
        if (playerMovement.FacingDirection < 0f)
            spriteRenderer.flipX = true;
        else if (playerMovement.FacingDirection > 0f)
            spriteRenderer.flipX = false;
    }
}