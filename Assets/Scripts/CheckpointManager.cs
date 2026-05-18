using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] private Checkpoint[] checkpoints;

    // Spawn inicial de la escena (donde aparece el jugador si no hay checkpoint)
    [SerializeField] private Transform defaultSpawn;

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

        PlayerRespawn respawn = FindFirstObjectByType<PlayerRespawn>();

        if (GameProgress.Instance != null && GameProgress.Instance.CheckpointId >= 0)
        {
            // Hay checkpoint guardado: actívalo y teletransporta al jugador
            Checkpoint saved = FindById(GameProgress.Instance.CheckpointId);
            if (saved != null)
            {
                saved.SetActive(true);
                activeCheckpoint = saved;

                if (respawn != null)
                {
                    respawn.SetCheckpoint(saved.transform);
                    respawn.transform.position = saved.transform.position;
                }
            }
        }
        else
        {
            // Sin checkpoint: teletransporta al spawn inicial
            if (respawn != null && defaultSpawn != null)
                respawn.transform.position = defaultSpawn.position;
        }
    }

    public void ActivateCheckpoint(Checkpoint newCheckpoint)
    {
        if (activeCheckpoint != null && activeCheckpoint != newCheckpoint)
            activeCheckpoint.SetActive(false);

        newCheckpoint.SetActive(true);
        activeCheckpoint = newCheckpoint;

        GameProgress.Instance?.SetCheckpoint(newCheckpoint.checkpointId);

        PlayerRespawn respawn = FindFirstObjectByType<PlayerRespawn>();
        if (respawn != null)
            respawn.SetCheckpoint(newCheckpoint.transform);
    }

    private Checkpoint FindById(int id)
    {
        foreach (var cp in checkpoints)
            if (cp.checkpointId == id) return cp;
        return null;
    }
}