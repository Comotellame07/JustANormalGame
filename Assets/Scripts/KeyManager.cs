using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [SerializeField] private int keysRequired = 5;
    [SerializeField] private GameObject door;

    private int keysCollected = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Restaura llaves guardadas al cargar la escena
        if (GameProgress.Instance != null)
            keysCollected = GameProgress.Instance.KeysCollected;

        KeyHUD.Instance?.UpdateHUD(keysCollected, keysRequired);

        if (keysCollected >= keysRequired && door != null)
            door.SetActive(false);
    }

    public void AddKey()
    {
        keysCollected++;
        GameProgress.Instance?.AddKey();
        KeyHUD.Instance?.UpdateHUD(keysCollected, keysRequired);

        if (keysCollected >= keysRequired && door != null)
            door.SetActive(false);
    }

    public int KeysCollected => keysCollected;
    public int KeysRequired  => keysRequired;
}