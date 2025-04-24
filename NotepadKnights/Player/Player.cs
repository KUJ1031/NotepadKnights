using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotepadKnights
{
    public class Player
    {
        public Inventory Inventory { get; private set; }
        public Store store { get; private set; }

        public string msg;
  

        public Player()
        {
            Inventory = new Inventory();
            store = new Store();
        }
        // 레벨업
        public void LevelUp()
        {
            // Level += 1, 공격력 += 0.5, 방어력 += 1
            Program.playerStatus.Level++;
            Program.playerStatus.Attack += 0.5f;
            Program.playerStatus.Defense += 1;
        }
        // 골드 추가
        public void AddGold(int RewardGold)
        {
            Program.playerStatus.Gold += RewardGold;
        }
        // 인벤토리 아이템 추가
        public void Additem(Item item)
        {
            Inventory.AddItem(item);
        }
        // 경험치 업
        public void ExpUp()
        {
            LevelManager levelManager = new LevelManager();
            levelManager.AddExp(5);
        }
    }
}
