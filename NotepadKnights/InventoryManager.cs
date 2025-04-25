namespace NotepadKnights;

public class InventoryManager
{
    private Inventory _inventory = Program.player.Inventory;
    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[인벤토리]");
            Console.WriteLine("\n0. 나가기\n1. 무기/방어구\n2. 아이템");
            int input = InputManager.ReadInt(0, 2);
            if (input == 0)
            {
                break;
            }
            else if (input == 1)
            {
                while (true)
                {
                    Console.Clear();
                    _inventory.DisplayInventory(true, false);
                    Console.WriteLine("\n0. 나가기\n1. 장착 관리");

                    if (InputManager.ReadInt(0, 1) == 0) break;
                    ManageEquip();
                }

            }
            else if (input == 2)
            {
                while (true)
                {
                    Console.Clear();
                    _inventory.DisplayInventory(false, false);
                    Console.WriteLine("\n0. 나가기\n1. 아이템 사용");

                    if (InputManager.ReadInt(0, 1) == 0) break;
                    ManageUseItem();
                }
            }

        }
    }

    private void ManageEquip()
    {
        while (true)
        {
            Console.Clear();
            _inventory.DisplayInventory(true, false);
            Console.WriteLine("\n0. 나가기");

            int select = InputManager.ReadInt(0, _inventory.EquippableItems.Count);
            if (select == 0) break;

            _inventory.SelectItem(select - 1);
        }
    }

    private void ManageUseItem()
    {
        while (true)
        {
            Console.Clear();
            _inventory.DisplayInventory(false, false);
            Console.WriteLine("\n0. 나가기");

            int select = InputManager.ReadInt(0, _inventory.ConsumableItems.Count);
            if (select == 0) break;

            Item item = _inventory.SellOrRemoveItem(select - 1, false);
            // 회복 포션 적용 로직
            Program.playerStatus.Hp = Math.Min(Program.playerStatus.Hp + item.Point, 100);
        }
    }
}