using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool HasDoubleJump { get; private set; }
    public bool HasDash { get; private set; }
    public int LastLevelUnlocked { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockDoubleJump()
    {
        HasDoubleJump = true;
        PlayerPrefs.SetInt("HasDoubleJump", 1);
        PlayerPrefs.Save();
    }

    public void UnlockDash()
    {
        HasDash = true;
        PlayerPrefs.SetInt("HasDash", 1);
        PlayerPrefs.Save();
    }

    public void SetLastLevel(int level)
    {
        if (level > LastLevelUnlocked)
        {
            LastLevelUnlocked = level;
            PlayerPrefs.SetInt("LastLevel", level);
            PlayerPrefs.Save();
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        HasDoubleJump = false;
        HasDash = false;
        LastLevelUnlocked = 1;
    }

    private void LoadProgress()
    {
        HasDoubleJump = PlayerPrefs.GetInt("HasDoubleJump", 0) == 1;
        HasDash = PlayerPrefs.GetInt("HasDash", 0) == 1;
        LastLevelUnlocked = PlayerPrefs.GetInt("LastLevel", 1);
    }
}