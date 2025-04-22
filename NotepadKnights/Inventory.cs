namespace NotepadKnights;

public class Inventory
{
    public List<Item> Items { get; private set; }
    public Dictionary<Item, int> NumberOfItems { get; private set; }

    public Inventory()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        if (!NumberOfItems.TryAdd(item, 1))
        {
            NumberOfItems[item]++;
        }
    }

    public void SelectItem(int index)
    {
        ItemType itemType = Items[index].Type;
        foreach (Item inventoryItem in Items)
        {
            if (inventoryItem.Type == itemType &&
                Items[index].Name != inventoryItem.Name &&
                inventoryItem.IsSelected)
            {
                inventoryItem.IsSelected = false;
            }
        }
        Items[index].IsSelected = !Items[index].IsSelected;
    }

    public Item SellItem(int index)
    {
        Item item = Items[index];
        NumberOfItems[item]--;
        item.IsSelected = false;
        Items.RemoveAt(index);
        return item;
    }

    public void DisplayInventory(bool selectMod, bool goldVisible)
    {
        Console.WriteLine("[아이템 목록]");
        int index = 1;
        foreach (Item item in Items)
        {
            string indexText = selectMod ? $"{index++}." : "";
            string selected = item.IsSelected ? "[E]" : "";
            string typeText = item.Type switch
            {
                ItemType.Weapon => "공격력",
                ItemType.Armor => "방어력",
                ItemType.Potion => "회복력",
                _ => "?",
            };
            string goldText = goldVisible ? $"{item.Price} G" : "";
            Console.WriteLine($"- {indexText} {selected, -3}{item.Name} | {typeText} {"+" + item.Point} | {item.State} | {goldText}");
        }
    }
}