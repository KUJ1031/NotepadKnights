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
    // Console 출력에 필요하다면 말씀해주세요.
    public int PlayerExp { get; private set; } = 0;

    public void AddExp(int expGained)
    {
        PlayerExp += expGained;

        while (CanLevelUp(Program.playerStatus.Level, PlayerExp))
        {
            // Player.cs에 LevelUp() 함수 구현 필요
            // Level += 1, 공격력 += 0.5, 방어력 += 1
            // Program.Player.LevelUp();
            PlayerExp -= requireExp[Program.playerStatus.Level];
        }
    }
    
    private bool CanLevelUp(int level, int exp)
    {
        return requireExp.ContainsKey(level) && exp >= requireExp[level];
    }
}