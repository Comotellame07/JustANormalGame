using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject door;

    [Header("Feedback visual")]
    [SerializeField] private Vector2 pressDirection = Vector2.down;
    [SerializeField] private float   pressDistance  = 0.1f;
    [SerializeField] private float   pressDuration  = 0.12f;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(PressAnimation());

            if (door != null) door.SetActive(false);
        }
    }

    private IEnumerator PressAnimation()
    {
        Vector3 origin = transform.position;
        Vector3 target = origin + new Vector3(
            pressDirection.normalized.x * pressDistance,
            pressDirection.normalized.y * pressDistance,
            0f
        );

        // Ida
        float elapsed = 0f;
        while (elapsed < pressDuration)
        {
            transform.position = Vector3.Lerp(origin, target, elapsed / pressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        // Vuelta
        elapsed = 0f;
        while (elapsed < pressDuration)
        {
            transform.position = Vector3.Lerp(target, origin, elapsed / pressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = origin;
    }
}