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
        public int PlayerDamage { get; private set; } // 공격력
        public int Defense { get; private set; } // 방어력
        public bool IsDie { get; private set; }
        public Monster Target = new Monster();// 현재 공격중인 적
        string index = "";
        public bool isAttack { get; private set; } // 공격할지 선택
        public  bool isSelectMonster { get; private set; } // 공격할 몬스터를 골랐는지

        MonsterFactory monsterFactory = new MonsterFactory();
        AttackAndDefense attackAndDefense = new AttackAndDefense(); 
        BattleManager battleManager = new BattleManager();

        // 공격하기 전 화면 UI
        public void BattleUI()
        {
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsterFactory.createMonsters.Count; i++)
            {
                if (isAttack) { index = (i + 1).ToString(); } else { index = ""; }
                Console.WriteLine($"{index} Lv.{monsterFactory.createMonsters[i].Level} {monsterFactory.createMonsters[i].Name} HP {monsterFactory.createMonsters[i].CurrentHp} ");
            }
            Console.WriteLine("\n\n[내정보]");
            Console.Write($"Lv.{Level} {Name} ({Job})\n");
            Console.WriteLine($"HP 100/{Hp}\n");

            // 아직 공격하지 않았다면
            if (!isAttack)
            { 
                Console.WriteLine("1. 공격\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>>");
            }
            else
            {
                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.\n>>");
            }
            ScreenChanges();
        }

        //  키입력에 따른 화면 변화
        public void ScreenChanges()
        {
            int selectedIndex = 0;
            bool running = true;
            string msg = "";

            while (running)
            {
                // 사용자 키 입력 
                var key = Console.ReadKey(true).Key;
               
                if (key == ConsoleKey.D1 || key == ConsoleKey.D2|| key == ConsoleKey.D3) // 숫자 '0' 키
                {
                    if(!isAttack)
                    {
                        isAttack = true;
                        Console.Clear();
                        BattleUI();
                       
                    }
                    else
                    {
                        Console.Clear();
                       
                        // 타겟을 찾은 다음
                        GetTarget(key);

                        // 적이 죽었다면
                        if (Target.CurrentHp == 0)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                        }
                        // 적이 살아있다면
                        else
                        {
                            // 공격
                            battleManager.ExecutePlayerPhase();
                        }
                        running = false;
                    }
                    
                    break;
                }
                // 취소 
                else if(key == ConsoleKey.D0 && isAttack)
                {
                    isAttack = false;
                    Console.Clear();
                    BattleUI();
                }
               else
                {
                    // msg = "잘못된 입력입니다.";
                    Console.WriteLine("잘못된 입력입니다.");
                }
               
            }
        }

        // 현재 공격중인 적 찾기
        void GetTarget(ConsoleKey key)
        {

            switch (key)
            {
                case ConsoleKey.D1:
                    Target = monsterFactory.createMonsters[0];
                    break;
                case ConsoleKey.D2:
                    Target = monsterFactory.createMonsters[1];
                    break;
                case ConsoleKey.D3:
                    Target = monsterFactory.createMonsters[2];
                    break;
                default:

                    Console.WriteLine("잘못된 입력입니다.");

                    break;
            }
        }

        // 공격
        public void Attack()
        {
            // 공격한다
            PlayerDamage = attackAndDefense.Attack(PlayerDamage);
            Target.CurrentHp = attackAndDefense.EnemyDefense(PlayerDamage, Target.CurrentHp);

            // 공격 UI 표시
            AttackUI(PlayerDamage);
        }


        // 공격 이후 UI
        void AttackUI(int playerDamage)
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

            Console.WriteLine("0. 다음\n");
            Console.WriteLine(">>");
        }

        // 난수 생성
        public int GetRandom(int min, int max)
        {
            Random rand = new Random();
            int result = rand.Next(min, max);
            return result;

        } 
    }
}
