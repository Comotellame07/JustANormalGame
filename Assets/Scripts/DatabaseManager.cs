using UnityEngine;
using SQLite;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private SQLiteConnection db;
    private const int TOTAL_SLOTS = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitDatabase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitDatabase()
    {
        string path = Application.persistentDataPath + "/savegame.db";
        db = new SQLiteConnection(path);
        db.CreateTable<SaveData>();

        for (int i = 0; i < TOTAL_SLOTS; i++)
        {
            var existing = db.Find<SaveData>(i);
            if (existing == null)
            {
                db.Insert(new SaveData { SlotId = i });
            }
        }
    }

    public SaveData GetSlot(int slotId)
    {
        return db.Find<SaveData>(slotId);
    }

    public void CreateNewSave(int slotId)
    {
        var save = db.Find<SaveData>(slotId);
        if (save == null) return;

        save.Exists       = true;
        save.CurrentLevel = 1;
        save.IsCompleted  = false;
        save.DoubleJump   = false;
        save.Dash         = false;

        db.Update(save);
    }

    public void UpdateSave(int slotId, int currentLevel, bool doubleJump, bool dash)
    {
        var save = db.Find<SaveData>(slotId);
        if (save == null) return;

        save.CurrentLevel = currentLevel;
        save.DoubleJump   = doubleJump;
        save.Dash         = dash;

        db.Update(save);
    }

    public void MarkCompleted(int slotId)
    {
        var save = db.Find<SaveData>(slotId);
        if (save == null) return;

        save.IsCompleted = true;
        db.Update(save);
    }

    public void DeleteSave(int slotId)
    {
        var save = db.Find<SaveData>(slotId);
        if (save == null) return;

        save.Exists       = false;
        save.CurrentLevel = 1;
        save.IsCompleted  = false;
        save.DoubleJump   = false;
        save.Dash         = false;

        db.Update(save);
    }

    private void OnDestroy()
    {
        db?.Close();
    }
}