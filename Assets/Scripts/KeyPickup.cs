// KeyPickup.cs
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyManager.Instance?.AddKey();
            Destroy(gameObject);
        }
    }
}