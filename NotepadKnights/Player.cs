using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class Player
    {
        public string  Name { get; private set; }
        public string Job { get; private set; }
        public int Level { get; private set; }
        public int Hp { get; private set; }
        public  int Gold { get; private set; }
        public int Attack { get; private set; } // 공격력
        public int Defense { get; private set; } // 방어력
        public bool IsDie { get; private set; }
        public Monster Target = new Monster();// 현재 공격중인 적
        public bool isAttack { get; private set; } // 공격할지 선택
        public  bool isSelectMonster { get; private set; } // 공격할 몬스터를 골랐는지

        private string monsterIndexDisplay = "";
        MonsterFactory monsterFactory = new MonsterFactory();
        AttackAndDefense attackAndDefense = new AttackAndDefense(); 
        BattleManager battleManager = new BattleManager();

        public void InitializePlayer()
        {
            Name = "Chad";
            Job = "전사";
            Level = 1;
            Hp = 100;
            Attack = 10;
            Defense = 10;
        }
        // 공격하기 전 화면 UI
        public void ShowBattleMenu()
        {
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsterFactory.createMonsters.Count; i++)
            {
                monsterIndexDisplay = isAttack?  (i + 1).ToString() : ""; 

                var monster = monsterFactory.createMonsters[i];
                Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} HP {monster.CurrentHp} ");
            }

            Console.WriteLine("\n\n[내정보]");
            Console.Write($"Lv.{Level} {Name} ({Job})\n");
            Console.WriteLine($"HP 100/{Hp}\n");

            // 아직 공격하지 않았다면
            if (!isAttack)
            { 
                Console.WriteLine("1. 공격\n");
                Console.Write("원하시는 행동을 입력해주세요.\n");
            }
            else
            {
                Console.WriteLine("0. 취소\n");
                Console.Write("대상을 선택해주세요.\n");
            }
            //  키입력에 따른 화면 변화
            ScreenChanges();
        }

        //  키입력에 따른 화면 변화
        public void ScreenChanges()
        {  
            // 사용자 키 입력 
            Console.Write(">>");         
            string input = Console.ReadLine();
       
                if (input == "0" && isAttack)
                {   
                    Console.Clear();
                    isAttack = false;
                    ShowBattleMenu(); 
                }
                // 유효 숫자를 입력했다면
            else if(input =="1" || input == "2" || input == "3")
            {
                Console.Clear();

                // 공격 중이지 않다면
                if (!isAttack)
                 {
                    isAttack = true;
                    ShowBattleMenu();
                }
                // 공격 중이라면
                else
                {   // 타겟을 찾은 다음
                    SelectTarget(int.Parse(input));

                    // 플레이어 공격턴 실행
                    battleManager.ExecutePlayerPhase();

                }
            }              
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                ScreenChanges();
            }
        }

        // 현재 공격중인 적 찾기   
        void SelectTarget(int num)
        {
            if (num >= 1 && num <= monsterFactory.createMonsters.Count)
            {
                Target = monsterFactory.createMonsters[num - 1];
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 공격
        public void ExecuteAttack()
        {
            // 적이 죽었다면
            if (Target.CurrentHp == 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            // 적이 살아있다면
            else
            {
                // 공격한다
                Attack = attackAndDefense.Attack(Attack);   
                Target.CurrentHp = attackAndDefense.EnemyDefense(Attack, Target.CurrentHp);

                // 공격 이후 UI
                DisplayAttackResult(Attack);
            }
        }


        // 공격 이후 UI
        void DisplayAttackResult(int playerDamage)
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{Name} 의 공격!");
            Console.WriteLine($"Lv.{Target.Level} {Target.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");

            Console.WriteLine($"Lv.{Target.Level} {Target.Name}");

            if (Target.CurrentHp <= 0 || Target.IsDead)
                Console.WriteLine($"HP 0 ->Dead\n");
            else
                Console.WriteLine($"HP {Target.CurrentHp}\n");
            //  Console.WriteLine(Target.CurrentHp <= 0 || Target.IsDead? $"HP 0 ->Dead\n" : $"HP {Target.CurrentHp}\n");
            Console.WriteLine("0. 다음\n>>");

           string  input =  Console.ReadLine();
            while (input != "0")
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Console.ReadLine();
            }
            // 이 이후 적 공격 턴 실행
            battleManager.ExecuteEnemyPhase();
        }
    }
}
