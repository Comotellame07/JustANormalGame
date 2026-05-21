using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Título (único, no cambia con idioma)")]
    [SerializeField] private GameObject titleImage;

    [Header("Botones de texto")]
    [SerializeField] private TMP_Text btnPlayText;
    [SerializeField] private TMP_Text btnSettingsText;
    [SerializeField] private TMP_Text btnExitText;

    [Header("Panel Ajustes")]
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
        titleImage.SetActive(true);
        ApplyLanguage();
        LanguageManager.OnLanguageChanged += ApplyLanguage;
    }

    private void OnDestroy()
    {
        LanguageManager.OnLanguageChanged -= ApplyLanguage;
    }

    public void OnPlay()          { SceneManager.LoadScene("SlotSelection"); }

    public void OnSettings()
    {
        titleImage.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnCloseSettings()
    {
        settingsPanel.SetActive(false);
        titleImage.SetActive(true);
    }

    public void OnExitGame()      { Application.Quit(); }

    private void ApplyLanguage()
    {
        bool es = LanguageManager.IsSpanish();
        btnPlayText.text     = es ? "Jugar"   : "Play";
        btnSettingsText.text = es ? "Ajustes" : "Settings";
        btnExitText.text     = es ? "Salir"   : "Exit";
    }
}