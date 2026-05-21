using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectorUI : MonoBehaviour
{
    [Header("Título con imagen (ES / EN)")]
    [SerializeField] private GameObject titleImageES;
    [SerializeField] private GameObject titleImageEN;

    [Header("Botones de texto")]
    [SerializeField] private TMP_Text btnBackText;
    [SerializeField] private TMP_Text btnLevel1Text;

    private void Start()
    {
        ApplyLanguage();
        LanguageManager.OnLanguageChanged += ApplyLanguage;
    }

    private void OnDestroy()
    {
        LanguageManager.OnLanguageChanged -= ApplyLanguage;
    }

    private void ApplyLanguage()
    {
        bool es = LanguageManager.IsSpanish();

        titleImageES.SetActive(es);
        titleImageEN.SetActive(!es);

        btnBackText.text   = es ? "Volver"      : "Back";
        btnLevel1Text.text = es ? "Mazmorras"   : "Dungeons";
    }

    public void OnLevel1() { SceneManager.LoadScene("Seccion1-Mazmorras"); }

    public void OnBack()   { SceneManager.LoadScene("SlotSelection"); }
}