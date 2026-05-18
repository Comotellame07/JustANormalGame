using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("ID único por checkpoint en esta escena")]
    public int checkpointId;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite         spriteActive;
    [SerializeField] private Sprite         spriteInactive;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive(bool active)
    {
        IsActive              = active;
        spriteRenderer.sprite = active ? spriteActive : spriteInactive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (IsActive) return;

        CheckpointManager.Instance?.ActivateCheckpoint(this);
    }
}