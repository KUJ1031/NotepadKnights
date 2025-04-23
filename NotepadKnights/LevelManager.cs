namespace NotepadKnights;

public class LevelManager
{
    private readonly Dictionary<int, int> requireExp = new Dictionary<int, int>()
    {
        { 1, 10 },
        { 2, 35 },
        { 3, 65 },
        { 4, 100 },
    };
    // Console ��¿� �ʿ��ϴٸ� �������ּ���.
    public int PlayerExp { get; private set; } = 0;

    public void AddExp(int expGained)
    {
        PlayerExp += expGained;

        while (CanLevelUp(Program.playerStatus.Level, PlayerExp))
        {
            // Player.cs�� LevelUp() �Լ� ���� �ʿ�
            // Level += 1, ���ݷ� += 0.5, ���� += 1
            // Program.Player.LevelUp();
            PlayerExp -= requireExp[Program.playerStatus.Level];
        }
    }
    
    private bool CanLevelUp(int level, int exp)
    {
        return requireExp.ContainsKey(level) && exp >= requireExp[level];
    }
}