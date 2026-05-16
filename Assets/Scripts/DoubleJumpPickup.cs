using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().UnlockDoubleJump();
            GameProgress.Instance?.UnlockDoubleJump();
            Destroy(gameObject);
        }
    }
}