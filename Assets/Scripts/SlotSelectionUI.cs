using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SlotSelectionUI : MonoBehaviour
{
    [SerializeField] private SlotButtonUI[] slotButtons; // 3 elementos
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text btnBackText;

    private void Start()
    {
        bool es = LanguageManager.IsSpanish();
        titleText.text  = es ? "Seleccionar partida" : "Select save";
        btnBackText.text = es ? "Volver" : "Back";

        for (int i = 0; i < slotButtons.Length; i++)
        {
            var data = DatabaseManager.Instance.GetSlot(i);
            slotButtons[i].Setup(i, data);
        }
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}