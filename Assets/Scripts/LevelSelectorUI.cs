using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text btnBackText;
    [SerializeField] private TMP_Text btnLevel1Text;
    [SerializeField] private TMP_Text btnLevel2Text;

    private void Start()
    {
        bool es = LanguageManager.IsSpanish();
        titleText.text    = es ? "Seleccionar nivel" : "Select level";
        btnBackText.text  = es ? "Volver"            : "Back";
        btnLevel1Text.text = es ? "Nivel 1"          : "Level 1";
        btnLevel2Text.text = es ? "Nivel 2"          : "Level 2";
    }

    public void OnLevel1() { SceneManager.LoadScene("Level1"); }
    public void OnLevel2() { SceneManager.LoadScene("Level2"); }

    public void OnBack()
    {
        SceneManager.LoadScene("SlotSelection");
    }
}