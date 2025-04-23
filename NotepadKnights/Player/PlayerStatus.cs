using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class PlayerStatus
    {

        public string Name { get; private set; }
        public string Job { get; private set; }
        public int Level { get; private set; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }
        public int Attack { get; private set; } // 공격력
        public int Defense { get; private set; } // 방어력
        public bool IsDie { get; private set; }
        public Monster Target = new Monster();// 현재 공격중인 적
        public bool isAttack { get; private set; } // 공격할지 선택
        public bool isSelectMonster { get; private set; } // 공격할 몬스터를 골랐는지


        public void InitializePlayer()
        {
            Name = "Chad";
            Job = "전사";
            Level = 1;
            Hp = 100;
            Attack = 10;
            Defense = 10;
        }
        public void IsAttackChange()
        {
            isAttack = !isAttack;
        }
        public int GetAttack()
        {
            return Attack; 
        }
    }
}
