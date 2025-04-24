namespace NotepadKnights;

public class Inventory
{
    public List<Item> EquippableItems { get; private set; } // 무기, 방어구
    public List<Item> ConsumableItems { get; private set; } // 포션 등 일회용
    public Dictionary<Item, int> EquippableItemCounts { get; private set; }
    public Dictionary<Item, int> ConsumableItemCounts { get; private set; }

    public Inventory()
    {
        EquippableItems = new List<Item>();
        ConsumableItems = new List<Item>();
        EquippableItemCounts = new Dictionary<Item, int>();
        ConsumableItemCounts = new Dictionary<Item, int>();
        InitialItems();
    }

    public void AddItem(Item item)
    {
        if (item.Type == ItemType.Potion)
        {
            if (!ConsumableItemCounts.TryAdd(item, 1))
            {
                ConsumableItemCounts[item]++;
            }
            else
            {
                ConsumableItems.Add(item);
            }
        }
        else
        {
            if (!EquippableItemCounts.TryAdd(item, 1))
            {
                EquippableItemCounts[item]++;
            }
            else
            {
                EquippableItems.Add(item);
            }
        }
    }

    public void SelectItem(int index)
    {
        ItemType itemType = EquippableItems[index].Type;
        foreach (Item equippableItem in EquippableItems)
        {
            if (equippableItem.Type == itemType &&
                EquippableItems[index].Name != equippableItem.Name &&
                equippableItem.IsSelected)
            {
                equippableItem.IsSelected = false;
            }
        }
        EquippableItems[index].IsSelected = !EquippableItems[index].IsSelected;
    }

    // 판매 또는 아이템 사용
    public Item SellOrRemoveItem(int index, bool equipMode)
    {
        var itemList = equipMode ? EquippableItems : ConsumableItems;
        var countDict = equipMode ? EquippableItemCounts : ConsumableItemCounts;

        Item item = itemList[index];
        countDict[item]--;

        if (countDict[item] == 0)
        {
            item.IsSelected = false;
            countDict.Remove(item);
            itemList.RemoveAt(index);
        }

        return item;
    }

    public void DisplayInventory(bool equipMode, bool goldVisible)
    {
        Console.WriteLine("[아이템 목록]");
        int index = 1;
        var targetList = equipMode ? EquippableItems : ConsumableItems;
        var countDict = equipMode ? EquippableItemCounts : ConsumableItemCounts;

        foreach (Item item in targetList)
        {
            string indexText = $"{index++}.";
            string selected = item.IsSelected ? "[E]" : "";
            string typeText = item.Type switch
            {
                ItemType.Weapon => "공격력",
                ItemType.Armor => "방어력",
                ItemType.Potion => "회복력",
                _ => "?",
            };
            string countText = $"수량 : {countDict[item]}";
            string goldText = goldVisible ? $"{item.Price} G" : "";
            Console.WriteLine($"- {indexText} {selected,-3}{item.Name} | {typeText} +{item.Point} | {item.State} | {countText} | {goldText}");
        }
    }
    
    private void InitialItems()
    {
        var potion = new Item($"{"회복 포션", -10}\t", ItemType.Potion, 30, $"{ "체력을 +30만큼 회복시켜주는 포션입니다.", -32 }\t", 300);
        AddItem(potion);
    }
}