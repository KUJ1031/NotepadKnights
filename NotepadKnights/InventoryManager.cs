namespace NotepadKnights;

public class InventoryManager
{
    public void Run()
    {
        while (true)
        {
            Console.Clear();
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
                    Program.player.Inventory.DisplayInventory(true, false);
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
                    Program.player.Inventory.DisplayInventory(false, false);
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
            Program.player.Inventory.DisplayInventory(true, false);
            Console.WriteLine("\n0. 나가기");

            int select = InputManager.ReadInt(0, Program.player.Inventory.EquippableItems.Count);
            if (select == 0) break;
                
            Program.player.Inventory.SelectItem(select - 1);
        }
    }

    private void ManageUseItem()
    {
        while (true)
        {
            Console.Clear();
            Program.player.Inventory.DisplayInventory(false, false);
            Console.WriteLine("\n0. 나가기");

            int select = InputManager.ReadInt(0, Program.player.Inventory.ConsumableItems.Count);
            if (select == 0) break;
                
            Program.player.Inventory.SellOrRemoveItem(select - 1, false);
        }
    }
}