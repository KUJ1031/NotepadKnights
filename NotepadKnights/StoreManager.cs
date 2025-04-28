namespace NotepadKnights;

public class StoreManager
{
    private Store _store = Program.player.store;
    private Inventory _inventory = Program.player.Inventory;

    private void DisplayPurchaseOptions()
    {
        Console.Clear();
        Console.WriteLine($"[보유 골드] {Program.playerStatus.Gold} G");
        _store.DisplayStore();
        Console.WriteLine("\n0. 나가기");
    }

    private void DisplaySellOptions(bool equipMode)
    {
        Console.Clear();
        Console.WriteLine($"[보유 골드] {Program.playerStatus.Gold} G");
        _inventory.DisplayInventory(equipMode, true);
        Console.WriteLine("\n0. 나가기");
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[보유 골드] {Program.playerStatus.Gold} G");
            _store.DisplayStore();
            Console.WriteLine("\n0. 나가기\n1. 아이템 구매\n2. 무기 / 방어구 판매\n3. 아이템 판매");

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
                Console.WriteLine("재고가 없습니다.");
                continue;
            }

            Item selectedItem = _store.Items[select - 1];

            if (Program.playerStatus.Gold < selectedItem.Price)
            {
                Console.WriteLine("Gold가 부족합니다.");
                continue;
            }

            _inventory.AddItem(selectedItem);
            _store.SellItem(select - 1);
            DisplayPurchaseOptions();
            Console.WriteLine("구매를 완료했습니다!");
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
            Console.WriteLine("판매가 완료되었습니다.");
        }
    }
}