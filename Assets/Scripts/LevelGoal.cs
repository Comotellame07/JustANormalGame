using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private bool   isLastLevel = false;
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (levelCompleted) return;

        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            if (isLastLevel)
            {
                GameProgress.Instance?.MarkCompleted();
                SceneManager.LoadScene("Credits");
            }
            else
            {
                int next = GameProgress.Instance != null
                    ? GameProgress.Instance.CurrentLevel + 1
                    : 1;
                GameProgress.Instance?.SetLevel(next);
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}