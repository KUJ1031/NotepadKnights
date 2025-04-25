namespace NotepadKnights;

public class StoreManager
{
    private Store _store = Program.player.store;
    private Inventory _inventory = Program.player.Inventory;

    private void DisplayPurchaseOptions()
    {
        Console.Clear();
        Console.WriteLine($"[���� ���] {Program.playerStatus.Gold} G");
        _store.DisplayStore();
        Console.WriteLine("\n0. ������");
    }

    private void DisplaySellOptions(bool equipMode)
    {
        Console.Clear();
        Console.WriteLine($"[���� ���] {Program.playerStatus.Gold} G");
        _inventory.DisplayInventory(equipMode, true);
        Console.WriteLine("\n0. ������");
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[���� ���] {Program.playerStatus.Gold} G");
            _store.DisplayStore();
            Console.WriteLine("\n0. ������\n1. ������ ����\n2. ���� / �� �Ǹ�\n3. ������ �Ǹ�");

            int input = InputManager.ReadInt(0, 3);
            switch (input)
            {
                case 0:
                    return;
                case 1:
                    Purchase();
                    break;
                case 2:
                    Sell(true);
                    break;
                case 3:
                    Sell(false);
                    break;
            }
        }
    }

    private void Purchase()
    {
        DisplayPurchaseOptions();
        while (true)
        {
            int select = InputManager.ReadInt(0, _store.Items.Count());
            if (select == 0) return;

            if (_store.NumberOfItems[_store.Items[select - 1]] == 0)
            {
                Console.WriteLine("��� �����ϴ�.");
                continue;
            }

            Item selectedItem = _store.Items[select - 1];

            if (Program.playerStatus.Gold < selectedItem.Price)
            {
                Console.WriteLine("Gold�� �����մϴ�.");
                continue;
            }

            _inventory.AddItem(selectedItem);
            _store.SellItem(select - 1);
            DisplayPurchaseOptions();
            Console.WriteLine("���Ÿ� �Ϸ��߽��ϴ�!");
        }
    }

    private void Sell(bool equipMode)
    {
        DisplaySellOptions(equipMode);
        var itemList = equipMode ? _inventory.EquippableItems : _inventory.ConsumableItems;

        while (true)
        {
            int select = InputManager.ReadInt(0, itemList.Count);
            if (select == 0) return;

            Program.player.AddGold(itemList[select - 1].Price * 85 / 100);
            Item item = _inventory.SellOrRemoveItem(select - 1, equipMode);
            _store.PurchaseItem(item.Name);
            DisplaySellOptions(equipMode);
            Console.WriteLine("�ǸŰ� �Ϸ�Ǿ����ϴ�.");
        }
    }
}