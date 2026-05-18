using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SlotButtonUI : MonoBehaviour
{
    [SerializeField] private TMP_Text    slotLabel;
    [SerializeField] private TMP_Text    slotInfo;
    [SerializeField] private GameObject  deleteButton;

    private int      slotIndex;
    private SaveData slotData;

    public void Setup(int index, SaveData data)
    {
        slotIndex = index;
        slotData  = data;

        bool es = LanguageManager.IsSpanish();

        if (!data.Exists)
        {
            slotLabel.text = es ? $"Ranura {index + 1}" : $"Slot {index + 1}";
            slotInfo.text  = es ? "— Vacío —" : "— Empty —";
            deleteButton.SetActive(false);
        }
        else if (data.IsCompleted)
        {
            slotLabel.text = es ? $"Ranura {index + 1}" : $"Slot {index + 1}";
            slotInfo.text  = es ? "✓ Completado" : "✓ Completed";
            deleteButton.SetActive(true);
        }
        else
        {
            slotLabel.text = es ? $"Ranura {index + 1}" : $"Slot {index + 1}";

            string seccion   = es ? "Mazmorras"   : "Dungeons";
            string cpLabel   = es ? "Checkpoint"  : "Checkpoint";
            string keyLabel  = es ? "Llaves"      : "Keys";

            string cpLine  = data.CheckpointId >= 0
                ? $"{cpLabel} {data.CheckpointId}"
                : (es ? "Sin checkpoint" : "No checkpoint");

            string keyLine = $"{keyLabel} {data.KeysCollected}/5";

            slotInfo.text = $"{seccion}\n{cpLine}\n{keyLine}";
            deleteButton.SetActive(true);
        }
    }

    public void OnClickSlot()
    {
        if (!slotData.Exists)
        {
            DatabaseManager.Instance.CreateNewSave(slotIndex);
            SaveData fresh = DatabaseManager.Instance.GetSlot(slotIndex);
            GameProgress.Instance.LoadFromSlot(fresh);
            SceneManager.LoadScene("Seccion1-Mazmorras");
        }
        else if (slotData.IsCompleted)
        {
            GameProgress.Instance.LoadFromSlot(slotData);
            SceneManager.LoadScene("LevelSelector");
        }
        else
        {
            GameProgress.Instance.LoadFromSlot(slotData);
            SceneManager.LoadScene("Seccion1-Mazmorras");
        }
    }

    public void OnDeleteSlot()
    {
        DatabaseManager.Instance.DeleteSave(slotIndex);
        SaveData fresh = DatabaseManager.Instance.GetSlot(slotIndex);
        Setup(slotIndex, fresh);
    }
}