using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("ID único por checkpoint en esta escena")]
    [SerializeField] public int checkpointId;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite         spriteActive;
    [SerializeField] private Sprite         spriteInactive;

    private bool isActive = false;

    public bool IsActive => isActive;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Llamado por CheckpointManager al cargar la escena o al activar este checkpoint.
    /// </summary>
    public void SetActive(bool active)
    {
        isActive               = active;
        spriteRenderer.sprite  = active ? spriteActive : spriteInactive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Si ya es el activo no hace falta hacer nada
        if (isActive) return;

        // Notifica al manager que este es el nuevo checkpoint activo
        CheckpointManager.Instance?.ActivateCheckpoint(this);
    }
}