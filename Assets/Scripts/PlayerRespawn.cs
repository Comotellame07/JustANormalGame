using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Spawn inicial (si no hay checkpoint activado)")]
    [SerializeField] private Transform defaultSpawn;

    [Header("Tiempos")]
    [SerializeField] private float fallDuration       = 0.25f; // tiempo en rotar hasta tumbado
    [SerializeField] private float lyingDuration      = 0.35f; // tiempo quieto tumbado
    [SerializeField] private float invulnerabilityTime = 1.2f;

    [Header("Feedback visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color          hitColor = new Color(1f, 0.3f, 0.3f, 1f);

    private Transform               currentCheckpoint;
    private Rigidbody2D             rb;
    private PlayerMovement          movement;
    private PlayerAnimationController animController;
    private bool                    isInvulnerable = false;

    public bool IsInvulnerable => isInvulnerable;

    private void Awake()
    {
        rb             = GetComponent<Rigidbody2D>();
        movement       = GetComponent<PlayerMovement>();
        animController = GetComponent<PlayerAnimationController>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetCheckpoint(Transform checkpoint) => currentCheckpoint = checkpoint;

    public void Respawn()
    {
        if (isInvulnerable) return;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        isInvulnerable = true;

        // 1. Bloquea input, detiene físicas y congela animaciones
        movement.SetInputEnabled(false);
        animController.SetAnimationEnabled(false);
        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        // 2. Color rojo
        Color originalColor  = spriteRenderer.color;
        spriteRenderer.color = hitColor;

        // 3. Determina hacia qué lado cae: contrario a donde mira
        //    FacingDirection: 1 = mira derecha → cae hacia atrás = rota +90° (queda tumbado a la izquierda)
        //                    -1 = mira izquierda → cae hacia atrás = rota -90°
        float targetAngle = movement.FacingDirection >= 0f ? 90f : -90f;

        // 4. Anima la rotación suavemente hasta quedar tumbado
        float elapsed       = 0f;
        float startAngle    = transform.eulerAngles.z;

        // Normaliza ángulo de salida al rango (-180, 180] para Lerp limpio
        if (startAngle > 180f) startAngle -= 360f;

        while (elapsed < fallDuration)
        {
            elapsed += Time.deltaTime;
            float t  = Mathf.SmoothStep(0f, 1f, elapsed / fallDuration);
            float z  = Mathf.LerpAngle(startAngle, targetAngle, t);
            transform.rotation = Quaternion.Euler(0f, 0f, z);
            yield return null;
        }

        // Asegura que queda exactamente a 90°
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);

        // 5. Espera tumbado
        yield return new WaitForSeconds(lyingDuration);

        // 6. Restaura color y rotación, teletransporta
        spriteRenderer.color = originalColor;
        transform.rotation   = Quaternion.identity;

        Transform target   = currentCheckpoint != null ? currentCheckpoint : defaultSpawn;
        transform.position = target.position;

        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        // 7. Restaura control y animaciones
        animController.SetAnimationEnabled(true);
        movement.SetInputEnabled(true);

        // 8. Invulnerabilidad
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}