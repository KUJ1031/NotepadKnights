namespace NotepadKnights;

public class Item
{
    public string Name { get; private set; }
    public ItemType Type { get; private set; }
    public int Point { get; private set; }
    public bool IsSelected  { get; set; } = false;
    public string State  { get; private set; }
    public int Price  { get; private set; }

    public Item(string name, ItemType type, int point, string state, int price)
    {
        Name = name;
        Type = type;
        Point = point;
        State = state;
        Price = price;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Item other && Name == other.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
    
    public bool IsEquippable()
    {
        return Type == ItemType.Weapon || Type == ItemType.Armor;
    }
}