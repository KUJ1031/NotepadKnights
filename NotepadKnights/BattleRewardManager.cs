namespace NotepadKnights;

public class BattleRewardManager
{
    private Random rand = new Random();
    private readonly float _potionProbability = 0.8f;
    private readonly float _normalWeaponProbability = 0.5f;
    private readonly float _rareWeaponProbability = 0.1f;

    private readonly Item _potion =
        new Item($"{"ȸ�� ����",-10}\t", ItemType.Potion, 30, $"{"ü���� +30��ŭ ȸ�������ִ� �����Դϴ�.",-32}\t", 1000);

    private readonly Item _normalWeapon =
        new Item($"{"�ܰ�",-10}\t", ItemType.Weapon, 3, $"{"���� �ܰ��Դϴ�.",-32}\t\t", 300);

    private readonly Item _rareWeapon =
        new Item($"{"�� �ҵ�",-10}\t", ItemType.Weapon, 22, $"{"���� ������ �� �ҵ��Դϴ�.",-32}\t\t", 3300);
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
        }
        else if (probability < _normalWeaponProbability)
        {
            NormalWeaponCount++;
        }
    }
}