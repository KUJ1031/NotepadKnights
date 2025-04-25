namespace NotepadKnights;

public class InventoryManager
{
    private Inventory _inventory = Program.player.Inventory;
    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[�κ��丮]");
            Console.WriteLine("\n0. ������\n1. ����/��\n2. ������");
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
                    Console.WriteLine("\n0. ������\n1. ���� ����");

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
                    Console.WriteLine("\n0. ������\n1. ������ ���");

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
            Console.WriteLine("\n0. ������");

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
            Console.WriteLine("\n0. ������");

            int select = InputManager.ReadInt(0, _inventory.ConsumableItems.Count);
            if (select == 0) break;

            Item item = _inventory.SellOrRemoveItem(select - 1, false);
            // ȸ�� ���� ���� ����
            Program.playerStatus.Hp = Math.Min(Program.playerStatus.Hp + item.Point, 100);
        }
    }
}