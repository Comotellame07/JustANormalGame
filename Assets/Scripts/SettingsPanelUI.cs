using UnityEngine;
using TMPro;

public class SettingsPanelUI : MonoBehaviour
{
    [Header("Título con imagen (ES / EN)")]
    [SerializeField] private GameObject titleImageES;
    [SerializeField] private GameObject titleImageEN;

    [Header("Botones de texto")]
    [SerializeField] private TMP_Text btnSpanishText;
    [SerializeField] private TMP_Text btnEnglishText;
    [SerializeField] private TMP_Text btnBackText;

    [SerializeField] private MainMenuUI mainMenuUI;

    private void OnEnable()
    {
        ApplyLanguage();
        LanguageManager.OnLanguageChanged += ApplyLanguage;
    }

    private void OnDisable()
    {
        LanguageManager.OnLanguageChanged -= ApplyLanguage;
    }

    private void ApplyLanguage()
    {
        bool es = LanguageManager.IsSpanish();

        titleImageES.SetActive(es);
        titleImageEN.SetActive(!es);

        btnSpanishText.text = "Español";
        btnEnglishText.text = "English";
        btnBackText.text    = es ? "Volver" : "Back";
    }

    public void OnSelectSpanish()  { LanguageManager.SetSpanish(); }
    public void OnSelectEnglish()  { LanguageManager.SetEnglish(); }
    public void OnBack()           { mainMenuUI.OnCloseSettings(); }
}