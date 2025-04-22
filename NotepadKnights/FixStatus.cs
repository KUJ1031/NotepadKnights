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
            Status.level = level;
        }
        public void AddHP(int hp)
        {
            Status.hp = hp;
        }
        public void AddAttack(int attack)
        {
            Status.attack = attack;
        }
        public void AddDefense(int defense)
        {
            Status.defense = defense;
        }
        public void AddEquipATK(int equipATK)
        {
            Status.equipATK = equipATK;
        }
        public void AddEquipDEF(int equipDEF)
        {
            Status.equipDEF = equipDEF;
        }
        public void AddGold(int gold)
        {
            Status.gold = gold;
        }


    }
}
