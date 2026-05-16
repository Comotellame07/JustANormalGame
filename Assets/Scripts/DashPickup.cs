using UnityEngine;

public class DashPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().UnlockDash();
            GameProgress.Instance?.UnlockDash();
            Destroy(gameObject);
        }
    }
}