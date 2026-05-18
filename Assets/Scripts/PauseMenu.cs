using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    [Header("Panel de pausa")]
    [SerializeField] private GameObject pausePanel;

    [Header("Textos (para idioma)")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text resumeText;
    [SerializeField] private TMP_Text mainMenuText;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) Resume();
            else          Pause();
        }
    }

    public void Pause()
    {
        isPaused       = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);

        bool es = LanguageManager.IsSpanish();
        if (titleText    != null) titleText.text    = es ? "Pausado"        : "Paused";
        if (resumeText   != null) resumeText.text   = es ? "Reanudar"       : "Resume";
        if (mainMenuText != null) mainMenuText.text = es ? "Menú principal" : "Main menu";
    }

    public void Resume()
    {
        isPaused       = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused       = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnOverlayClick()
    {
        if (isPaused) Resume();
    }
}