using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SlotSelectionUI : MonoBehaviour
{
    [Header("Título con imagen (ES / EN)")]
    [SerializeField] private GameObject titleImageES;
    [SerializeField] private GameObject titleImageEN;

    [Header("Botones de texto")]
    [SerializeField] private TMP_Text btnBackText;

    [SerializeField] private SlotButtonUI[] slotButtons;

    private void Start()
    {
        ApplyLanguage();
        LanguageManager.OnLanguageChanged += ApplyLanguage;

        for (int i = 0; i < slotButtons.Length; i++)
        {
            var data = DatabaseManager.Instance.GetSlot(i);
            slotButtons[i].Setup(i, data);
        }
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

        btnBackText.text = es ? "Volver" : "Back";
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}