using UnityEngine;
using TMPro;

public class SettingsPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text btnSpanishText;
    [SerializeField] private TMP_Text btnEnglishText;
    [SerializeField] private TMP_Text btnBackText;

    [SerializeField] private MainMenuUI mainMenuUI;

    private void OnEnable()
    {
        RefreshTexts();
    }

    private void RefreshTexts()
    {
        bool es = LanguageManager.IsSpanish();
        titleText.text      = es ? "Idioma"   : "Language";
        btnSpanishText.text = "Español";
        btnEnglishText.text = "English";
        btnBackText.text    = es ? "Volver"   : "Back";
    }

    public void OnSelectSpanish()
    {
        LanguageManager.SetSpanish();
        RefreshTexts();
    }

    public void OnSelectEnglish()
    {
        LanguageManager.SetEnglish();
        RefreshTexts();
    }

    public void OnBack()
    {
        mainMenuUI.OnCloseSettings();
    }
}