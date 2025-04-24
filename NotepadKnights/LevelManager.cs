namespace NotepadKnights;

public class LevelManager
{
    private readonly Dictionary<int, int> _requireExp = new Dictionary<int, int>()
    {
        { 1, 10 },
        { 2, 35 },
        { 3, 65 },
        { 4, 100 },
    };
    public int PlayerExp { get; private set; } = 0;

    public void AddExp(int expGained)
    {
        PlayerExp += expGained;

        while (CanLevelUp(Program.playerStatus.Level, PlayerExp))
        {
            Program.player.LevelUp();
            PlayerExp -= _requireExp[Program.playerStatus.Level];
        }
    }
    
    private bool CanLevelUp(int level, int exp)
    {
        return _requireExp.ContainsKey(level) && exp >= _requireExp[level];
    }
}