using SQLite;

[Table("saves")]
public class SaveData
{
    [PrimaryKey]
    [Column("slot_id")]
    public int SlotId { get; set; }

    [Column("exists")]
    public bool Exists { get; set; } = false;

    [Column("current_level")]
    public int CurrentLevel { get; set; } = 1;

    [Column("is_completed")]
    public bool IsCompleted { get; set; } = false;

    [Column("double_jump")]
    public bool DoubleJump { get; set; } = false;

    [Column("dash")]
    public bool Dash { get; set; } = false;

    [Column("checkpoint_id")]
    public int CheckpointId { get; set; } = -1; // -1 = sin checkpoint guardado
}