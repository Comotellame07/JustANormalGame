using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite         spriteInactive;
    [SerializeField] private Sprite         spriteActive;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (!other.CompareTag("Player")) return;

        activated = true;

        // Actualiza el checkpoint en el jugador
        other.GetComponent<PlayerRespawn>()?.SetCheckpoint(transform);

        // Cambia el sprite si tienes uno configurado
        if (spriteRenderer != null && spriteActive != null)
            spriteRenderer.sprite = spriteActive;
    }
}