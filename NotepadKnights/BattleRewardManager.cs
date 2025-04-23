namespace NotepadKnights;

public class BattleRewardManager
{
    private Random rand = new Random();
    private readonly float _potionProbability = 0.2f;
    private readonly float _normalWeaponProbability = 0.2f;
    private readonly float _rareWeaponProbability = 0.05f;

    private readonly Item _potion =
        new Item($"{"?Œë³µ ?¬ì…˜",-10}", ItemType.Potion, 30, $"{"ì²´ë ¥??+30ë§Œí¼ ?Œë³µ?œì¼œì£¼ëŠ” ?¬ì…˜?…ë‹ˆ??",-30}", 1000);

    private readonly Item _normalWeapon =
        new Item($"{"?¨ê?",-10}", ItemType.Weapon, 3, $"{"?‘ì? ?¨ê??…ë‹ˆ??",-30}", 300);

    private readonly Item _rareWeapon =
        new Item($"{ "ë¡??Œë“œ", -10 }", ItemType.Weapon, 22, $"{ "?„ì£¼ ê°•ë ¥??ë¡??Œë“œ?…ë‹ˆ??", -30 }", 3300);
    public int RewardGold { get; private set; }
    public int PotionCount { get; private set; }
    public int NormalWeaponCount { get; private set; }
    public int RareWeaponCount { get; private set; }
    
    
    
    public void GetRewards(int monsterKillCount)
    {
        RewardGold = monsterKillCount * 100;

        for (int i = 0; i < monsterKillCount; i++)
        {
            TryDropPotion();
            TryDropWeapon();
        }

        Program.player.AddGold(RewardGold);
        AddItemMultipleTimes(_potion, PotionCount);
        AddItemMultipleTimes(_normalWeapon, NormalWeaponCount);
        AddItemMultipleTimes(_rareWeapon, RareWeaponCount);
    }
    
    private void AddItemMultipleTimes(Item item, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Program.player.Inventory.AddItem(item);
        }
    }

    private void TryDropPotion()
    {
        if (rand.NextDouble() < _potionProbability)
        {
            PotionCount++;
        }
    }
    
    private void TryDropWeapon()
    {
        double probability = rand.NextDouble();
        if (probability < _rareWeaponProbability)
        {
            RareWeaponCount++;
        } else if (probability < _normalWeaponProbability)
        {
            NormalWeaponCount++;
        }
    }
}