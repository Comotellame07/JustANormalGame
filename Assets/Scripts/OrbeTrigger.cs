using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OrbeTrigger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private float  fadeTime = 1.5f;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(ActivateOrbe(other.gameObject));
        }
    }

    private IEnumerator ActivateOrbe(GameObject player)
    {
        // Aquí puedes activar una animación de corrupción en el player si la tienes
        // player.GetComponent<Animator>()?.SetTrigger("Corrupt");

        // Fade a negro — usa el mismo FadeManager que ya tengas, o una imagen negra simple
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(nextScene);
    }
}