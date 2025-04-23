using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class FixStatus
    {
        TempStatus Status;
        public FixStatus(TempStatus sharedStatus)
        {
            Status = sharedStatus;
        }
        public void SetName(string name)
        {
            Status.name = name;
        }
        public void SetJob(string job)
        {
            Status.job = job;
        }
        public void AddLevel(int level)
        {
            Status.level += level;
        }
        public void AddHP(int hp)
        {
            Status.hp += hp;
        }
        public void AddAttack(int attack)
        {
            Status.attack += attack;
        }
        public void AddDefense(int defense)
        {
            Status.defense += defense;
        }
        public void AddEquipATK(int equipATK)
        {
            Status.equipATK += equipATK;
        }
        public void AddEquipDEF(int equipDEF)
        {
            Status.equipDEF += equipDEF;
        }
        public void AddGold(int gold)
        {
            Status.gold += gold;
        }
        //public void showStatus()
        //{
        //    Console.WriteLine("레벨: " + Status.level);
        //    Console.WriteLine($"{Status.name} {Status.job}");
        //    Console.WriteLine("체력: " + Status.hp);
        //    Console.WriteLine($"공격력: {Status.attack} + {Status.equipATK}");
        //    Console.WriteLine($"방어력: {Status.defense} + {Status.equipDEF}");
        //    Console.WriteLine("체력: " + Status.hp);
        //    Console.WriteLine("소지금: " + Status.gold);
        //}
    }
}
