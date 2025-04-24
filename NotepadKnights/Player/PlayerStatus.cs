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
        public int Hp { get; set; }
        public int MaxHp { get; private set; }
        public int Mp { get; private set; }
        public int Exp { get; private set; } // 현재 경험치
        public int MaxExp { get; private set; } // 최대 경험치
        public int Gold { get; private set; }
        public float Attack { get; private set; } // 공격력
        public int Defense { get; private set; } // 방어력
        public bool IsDie { get; private set; }
        public Monster Target = new Monster();// 현재 공격중인 적
        public bool IsAttack { get; private set; } // 공격할지 선택
        public bool IsSelectMonster { get; private set; } // 공격할 몬스터를 골랐는지
        public int KilledMonsterCount { get; private set; } // 죽인 몬스터 수

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
        // 혹시 몰라서 넣음 , 이름 변경
        public void SetName(string name)
        {
            Name = name;
        }
        // 직업 변경
        public void SetJob(string job)
        {
            Job = job;
        }
        // 레벨 변경
        public void SetLevel(int level)
        {
            Level = level;
        }

        // 현재 경험치 변경
        public void SetExp(int exp)
        {
            Exp = exp;
        }
        // 최대 경험치 변경
        public void SeMaxExp(int maxExp)
        {
            MaxExp = maxExp;
        }
        // 데미지 변경
        public void SetAttack(float attack)
        {
            Attack = attack;
        }
        // 방어력 변경
        public void SetDefense(int defense)
        {
            Defense = defense;
        }
        // 골드 변경
        public void SetGold(int gold)
        {
            Gold = gold;
        }
        public void SetIsAttack(bool isAttack)
        {
            IsAttack = isAttack;
        }
        // 죽인 몬스터의 수 변경
        public void SetKilledMonsterCount(int killedMonsterCount)
        {
            KilledMonsterCount = killedMonsterCount;
        }
        //public int GetAttack()
        //{
        //    return Attack; 
        //}
    }
}
