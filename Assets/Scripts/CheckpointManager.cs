using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] private Checkpoint[] checkpoints;

    private Checkpoint activeCheckpoint = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Desactiva todos los sprites primero
        foreach (var cp in checkpoints)
            cp.SetActive(false);

        // Restaura el checkpoint guardado en GameProgress si existe
        if (GameProgress.Instance != null && GameProgress.Instance.CheckpointId >= 0)
        {
            Checkpoint saved = FindById(GameProgress.Instance.CheckpointId);
            if (saved != null)
            {
                saved.SetActive(true);
                activeCheckpoint = saved;
                ApplyToRespawn();
            }
        }
    }

    public void ActivateCheckpoint(Checkpoint newCheckpoint)
    {
        // Desactiva el anterior si había uno
        if (activeCheckpoint != null && activeCheckpoint != newCheckpoint)
            activeCheckpoint.SetActive(false);

        // Activa el nuevo
        newCheckpoint.SetActive(true);
        activeCheckpoint = newCheckpoint;

        // Guarda en GameProgress (que lo persiste en la BD)
        GameProgress.Instance?.SetCheckpoint(newCheckpoint.checkpointId);

        // Actualiza el PlayerRespawn
        ApplyToRespawn();
    }

    private void ApplyToRespawn()
    {
        PlayerRespawn respawn = FindFirstObjectByType<PlayerRespawn>();
        if (respawn != null && activeCheckpoint != null)
            respawn.SetCheckpoint(activeCheckpoint.transform);
    }

    private Checkpoint FindById(int id)
    {
        foreach (var cp in checkpoints)
            if (cp.checkpointId == id) return cp;
        return null;
    }
}