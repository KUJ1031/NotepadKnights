namespace NotepadKnights;

public class BattleRewardManager
{
    private Random rand = new Random();
    private readonly float _potionProbability = 0.2f;
    private readonly float _normalWeaponProbability = 0.2f;
    private readonly float _rareWeaponProbability = 0.05f;

    private readonly Item _potion =
        new Item($"{"?�복 ?�션",-10}", ItemType.Potion, 30, $"{"체력??+30만큼 ?�복?�켜주는 ?�션?�니??",-30}", 1000);

    private readonly Item _normalWeapon =
        new Item($"{"?��?",-10}", ItemType.Weapon, 3, $"{"?��? ?��??�니??",-30}", 300);

    private readonly Item _rareWeapon =
        new Item($"{ "�??�드", -10 }", ItemType.Weapon, 22, $"{ "?�주 강력??�??�드?�니??", -30 }", 3300);
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