namespace NotepadKnights;

public class Inventory
{
    public List<Item> Items { get; private set; }

    public Inventory()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
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
            string indexText = selectMod ? $"{index++}. " : "";
            string selected = item.IsSelected ? "[E] " : "";
            string typeText = item.Type == ItemType.Attack ? "공격력" : "방어력";
            string goldText = goldVisible ? $"{item.Price} G" : "";
            Console.WriteLine($"- {indexText}{selected}{item.Name}\t| {typeText} +{item.Point}\t| {item.State}\t| {goldText}");
        }
    }
}