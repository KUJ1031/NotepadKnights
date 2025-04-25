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
        public int Gold { get; set; } = 100000;
        public float Attack { get; set; } // 공격력
        public int Defense { get; set; } // 방어력
        public int ExtraAttack { get; set; } = 0;
        public int ExtraDefense { get; set; } = 0;
        public bool IsDie { get; set; }

        public Monster Target = new Monster();// 현재 공격중인 적
        public bool IsAttack { get; set; } // 공격할지 선택
        public bool IsSelectMonster { get; set; } // 공격할 몬스터를 골랐는지
        public int KilledMonsterCount { get; set; } // 죽인 몬스터 수
        public bool UseSkill { get; set; } // 죽인 몬스터 수
        public void InitializePlayer(string name, string job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Hp = 100;
            MaxHp = 100;
            Mp = 100;
            MaxExp = 10;
            Attack = 10;
            Defense = 5;
        }     

        // PlayerStatus의 정보를 한꺼번에 넘겨주는 메서드
        public void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("확인하고 싶은 정보를 선택해주세요.");
            Console.WriteLine("1.현재 스테이터스 2. 스킬 정보");
            
            int select = InputManager.ReadInt(1, 2);
            if (select == 1)
            {
                Console.WriteLine($"현재 [{Name}]의 스테이터스");
                Console.WriteLine($"--------------------------------------------------");
                Console.WriteLine($"이름 : {Name}");
                Console.WriteLine($"직업 : {Job}");
                Console.WriteLine($"레벨 : {Level}\n");
                Console.WriteLine($"체력 : {Hp}/{MaxHp}");
                Console.WriteLine($"공격력 : {Attack} (+{ExtraAttack})");
                Console.WriteLine($"방어력 : {Defense} (+{ExtraDefense})");
                Console.WriteLine($"Mp : {Mp}\n");
                Console.WriteLine($"경험치 : {Exp}/{MaxExp}");
                Console.WriteLine($"골드 : {Gold}");
                Console.WriteLine($"--------------------------------------------------\n");
                
            }
            else if (select == 2)
            {
                Skill.SkillDescription();
            }
            Console.WriteLine("아무 키나 눌러 종료.");
            Console.ReadLine();
        }
    }
}
