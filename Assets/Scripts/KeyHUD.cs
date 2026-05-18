using UnityEngine;
using TMPro;

public class KeyHUD : MonoBehaviour
{
    public static KeyHUD Instance;

    [SerializeField] private TMP_Text keyCountText;
    // El sprite de la llave ponlo como Image de UI directamente en el Canvas,
    // este script solo gestiona el texto X/5

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHUD(int collected, int required)
    {
        if (keyCountText != null)
            keyCountText.text = $"{collected}/{required}";
    }
}