// KeyManager.cs
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

    public void AddKey()
    {
        keysCollected++;
        if (keysCollected >= keysRequired && door != null)
            door.SetActive(false);
    }

    public int KeysCollected => keysCollected;
}