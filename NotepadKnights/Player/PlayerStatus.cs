using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class PlayerStatus
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int Exp { get; set; } // 현재 경험치
        public int MaxExp { get; set; } // 최대 경험치
        public int Gold { get; set; }
        public float Attack { get; set; } // 공격력
        public int Defense { get; set; } // 방어력
        public bool IsDie { get; set; }

        public Monster Target = new Monster();// 현재 공격중인 적
        public bool IsAttack { get; set; } // 공격할지 선택
        public bool IsSelectMonster { get; set; } // 공격할 몬스터를 골랐는지
        public int KilledMonsterCount { get; set; } // 죽인 몬스터 수
        public void InitializePlayer()
        {
            Name = "Chad";
            Job = "전사";
            Level = 1;
            Hp = 100;
            MaxHp = 100;
            Mp = 100;
            Attack = 10;
            Defense = 5;
        }     
        // PlayerStatus의 정보를 한꺼번에 넘겨주는 메서드
        public (string Name, string Job, int Level, int Hp, int MaxHp, int Mp, int Exp, int MaxExp, int Gold, float Attack, int Defense) GetPlayerInfo()
        {
            return (Name, Job, Level, Hp, MaxHp, Mp, Exp, MaxExp, Gold, Attack, Defense);
        }
    }
}
