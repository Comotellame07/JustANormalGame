using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

        // No hace nada si el jugador ya está en periodo de invulnerabilidad
        if (respawn == null || respawn.IsInvulnerable) return;

        respawn.Respawn();
    }
}