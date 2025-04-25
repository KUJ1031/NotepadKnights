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
        Program.playerStatus.Exp += expGained;
        while (CanLevelUp(Program.playerStatus.Level, Program.playerStatus.Exp))
        {
            Program.player.LevelUp();
            Program.playerStatus.Exp -= _requireExp[Program.playerStatus.Level - 1];
        }
        Program.playerStatus.MaxExp = _requireExp[Program.playerStatus.Level];
    }
    
    private bool CanLevelUp(int level, int exp)
    {
        return _requireExp.ContainsKey(level) && exp >= _requireExp[level];
    }
}