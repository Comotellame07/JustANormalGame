using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Spawn inicial (si no hay checkpoint activado)")]
    [SerializeField] private Transform defaultSpawn;

    [Header("Tiempos")]
    [SerializeField] private float fallDuration        = 0.25f;
    [SerializeField] private float lyingDuration       = 0.35f;
    [SerializeField] private float invulnerabilityTime = 1.2f;

    [Header("Feedback visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color          hitColor = new Color(1f, 0.4f, 0.4f, 1f);

    private Transform                 currentCheckpoint;
    private Rigidbody2D               rb;
    private PlayerMovement            movement;
    private PlayerAnimationController animController;
    private bool                      isInvulnerable = false;

    public bool IsInvulnerable => isInvulnerable;

    private void Awake()
    {
        rb             = GetComponent<Rigidbody2D>();
        movement       = GetComponent<PlayerMovement>();
        animController = GetComponent<PlayerAnimationController>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        if (isInvulnerable) return;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        isInvulnerable = true;

        movement.SetInputEnabled(false);
        movement.SetHitState(true);
        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        spriteRenderer.flipX = !spriteRenderer.flipX;

        Color originalColor  = spriteRenderer.color;
        spriteRenderer.color = hitColor;

        // Rotación de caída hacia atrás
        float targetAngle = movement.FacingDirection >= 0f ? 90f : -90f;
        float elapsed     = 0f;
        float startAngle  = transform.eulerAngles.z;
        if (startAngle > 180f) startAngle -= 360f;

        while (elapsed < fallDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / fallDuration);
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(startAngle, targetAngle, t));
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);

        yield return new WaitForSeconds(lyingDuration);

        spriteRenderer.color = originalColor;
        transform.rotation   = Quaternion.identity;

        Transform target   = currentCheckpoint != null ? currentCheckpoint : defaultSpawn;
        transform.position = target.position;

        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        movement.SetHitState(false);
        movement.SetInputEnabled(true);

        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}