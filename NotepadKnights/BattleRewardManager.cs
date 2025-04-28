namespace NotepadKnights;

public class BattleRewardManager
{
    private Random rand = new Random();
    private readonly float _potionProbability = 0.8f;
    private readonly float _normalWeaponProbability = 0.5f;
    private readonly float _rareWeaponProbability = 0.05f;

    private readonly Item _potion =
        new Item($"{"회복 포션",-10}\t", ItemType.Potion, 30, $"{"체력을 +30만큼 회복시켜주는 포션입니다.",-32}\t", 1000);

    private readonly Item _normalWeapon =
        new Item($"{"단검",-10}\t", ItemType.Weapon, 3, $"{"작은 단검입니다.",-32}\t\t", 300);

    private readonly Item _rareWeapon =
        new Item($"{"롱 소드",-10}\t", ItemType.Weapon, 22, $"{"아주 강력한 롱 소드입니다.",-32}\t\t", 3300);

    public int RewardGold { get; private set; } = 0;
    public int PotionCount { get; private set; } = 0;
    public int NormalWeaponCount { get; private set; } = 0;
    public int RareWeaponCount { get; private set; } = 0;

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
        DisplayRewards();
        RewardGold = 0;
        PotionCount = 0;
        NormalWeaponCount = 0;
        RareWeaponCount = 0;
    }

    private void DisplayRewards()
    {
        Console.Clear();
        Console.WriteLine("[보상 목록]\n");
        Console.WriteLine($"{RewardGold}G 획득!");
        if (PotionCount > 0)
            Console.WriteLine($"{_potion.Name} {PotionCount}개 획득!");
        if (NormalWeaponCount > 0)
            Console.WriteLine($"{_normalWeapon.Name} {NormalWeaponCount}개 획득!");
        if (RareWeaponCount > 0)
            Console.WriteLine($"{_rareWeapon.Name} {RareWeaponCount}개 획득!");

        InputManager.ReadInt(0, 0, "0. 확인");
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
        }
        else if (probability < _normalWeaponProbability)
        {
            NormalWeaponCount++;
        }
    }
}