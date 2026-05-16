using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            if (door != null) door.SetActive(false);
        }
    }
}