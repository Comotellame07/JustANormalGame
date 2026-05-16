using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public int  ActiveSlot    { get; private set; } = -1;
    public bool HasDoubleJump { get; private set; }
    public bool HasDash       { get; private set; }
    public int  CurrentLevel  { get; private set; } = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadFromSlot(SaveData data)
    {
    ActiveSlot    = data.SlotId;
    HasDoubleJump = data.DoubleJump;
    HasDash       = data.Dash;
    CurrentLevel  = data.CurrentLevel;
    }

    public void UnlockDoubleJump()
    {
        HasDoubleJump = true;
        PersistCurrent();
    }

    public void UnlockDash()
    {
        HasDash = true;
        PersistCurrent();
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        PersistCurrent();
    }

    public void MarkCompleted()
    {
        DatabaseManager.Instance?.MarkCompleted(ActiveSlot);
    }

    private void PersistCurrent()
    {
        DatabaseManager.Instance?.UpdateSave(ActiveSlot, CurrentLevel, HasDoubleJump, HasDash);
    }
}