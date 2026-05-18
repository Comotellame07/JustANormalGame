using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovement  playerMovement;
    [SerializeField] private Animator        animator;
    [SerializeField] private SpriteRenderer  spriteRenderer;

    private readonly int speedHash                = Animator.StringToHash("Speed");
    private readonly int isGroundedHash           = Animator.StringToHash("IsGrounded");
    private readonly int verticalVelocityHash     = Animator.StringToHash("VerticalVelocity");
    private readonly int isDashingHash            = Animator.StringToHash("IsDashing");
    private readonly int doubleJumpTriggeredHash  = Animator.StringToHash("DoubleJumpTriggered");

    private bool animationEnabled = true;

    private void Awake()
    {
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (animator       == null) animator       = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandleDoubleJump()
    {
        animator.SetTrigger(doubleJumpTriggeredHash);
    }

    /// <summary>
    /// Llama con false para congelar el Animator y el flipX durante la animación de muerte.
    /// Llama con true para restaurar el control normal.
    /// </summary>
    public void SetAnimationEnabled(bool enabled)
    {
        animationEnabled = enabled;
    }

    private void Update()
    {
        if (!animationEnabled) return;

        animator.SetFloat(speedHash,            Mathf.Abs(playerMovement.MoveInput));
        animator.SetBool (isGroundedHash,        playerMovement.IsGrounded);
        animator.SetFloat(verticalVelocityHash,  playerMovement.VerticalVelocity);
        animator.SetBool (isDashingHash,         playerMovement.IsDashing);
    }

    private void LateUpdate()
    {
        if (!animationEnabled) return;

        if      (playerMovement.FacingDirection < 0f) spriteRenderer.flipX = true;
        else if (playerMovement.FacingDirection > 0f) spriteRenderer.flipX = false;
    }
}