using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Textos del menú")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text btnPlayText;
    [SerializeField] private TMP_Text btnSettingsText;
    [SerializeField] private TMP_Text btnExitText;

    [Header("Panel Ajustes")]
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
        ApplyLanguage();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("SlotSelection");
    }

    public void OnSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OnCloseSettings()
    {
        settingsPanel.SetActive(false);
        ApplyLanguage();
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    private void ApplyLanguage()
    {
        bool isSpanish = LanguageManager.IsSpanish();
        titleText.text      = isSpanish ? "Just a Normal Game" : "Just a Normal Game";
        btnPlayText.text    = isSpanish ? "Jugar"    : "Play";
        btnSettingsText.text = isSpanish ? "Ajustes"  : "Settings";
        btnExitText.text    = isSpanish ? "Salir"    : "Exit";
    }
}