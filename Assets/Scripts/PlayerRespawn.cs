using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Spawn inicial (si no hay checkpoint activado)")]
    [SerializeField] private Transform defaultSpawn;

    [Header("Tiempos")]
    [SerializeField] private float delayBeforeTeleport = 0.6f;
    [SerializeField] private float invulnerabilityTime  = 1.2f;

    [Header("Feedback visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color          hitColor       = Color.red;

    private Transform      currentCheckpoint;
    private Rigidbody2D    rb;
    private PlayerMovement movement;
    private bool           isInvulnerable = false;

    public bool IsInvulnerable => isInvulnerable;

    private void Awake()
    {
        rb       = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();

        // Busca el SpriteRenderer automáticamente si no se asignó en el Inspector
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

        // 1. Bloquea input y para el personaje
        movement.SetInputEnabled(false);
        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        // 2. Flip hacia el lado contrario al que miraba
        FlipSprite();

        // 3. Pone el sprite en rojo
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = hitColor;

        // 4. Espera antes de teletransportar
        yield return new WaitForSeconds(delayBeforeTeleport);

        // 5. Restaura color original antes de teletransportar
        spriteRenderer.color = originalColor;

        // 6. Teletransporte
        Transform target = currentCheckpoint != null ? currentCheckpoint : defaultSpawn;
        transform.position = target.position;

        // 7. Resetea velocidad tras teletransportar
        rb.linearVelocity  = Vector2.zero;
        rb.angularVelocity = 0f;

        // 8. Devuelve el control
        movement.SetInputEnabled(true);

        // 9. Periodo de invulnerabilidad
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void FlipSprite()
    {
        // Invierte la escala X del transform para dar la vuelta al sprite
        // Esto es compatible con cualquier sistema de flip que ya tengas
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}