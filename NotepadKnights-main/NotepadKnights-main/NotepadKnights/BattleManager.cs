using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotepadKnights
{
    interface IRandomProvider
    {
        int GetRandom(int min, int max);
    }
    internal class BattleManager
    {
        public class MonsterFactory
        {
            public static List<Monster> monsters {get; set;}

            // 적들의 정보를 설정하자
            public void SetEnemysInfo()
            {
                monsters = new List<Monster>{

                    new Monster("미니언", 2, 15),
                    new Monster("대포미니언",  5, 25),
                    new Monster("공허충",3, 10),
                };
            }
        }
        public class Player : IRandomProvider
        {
            string name = "Chad";
            string job = "전사";
            int level = 1;
            int hp = 100;
            int gold;
            int attack; // 공격력
            int defense; // 방어력
            bool isDie;           
            string target;

            // 공격 UI
            public void BattleUI()
            {
                Console.WriteLine("Battle!!\n");

                foreach (var monster in MonsterFactory.monsters)
                {
                    Console.WriteLine($"Lv.{monster.Name} {monster.Level} HP {monster.HP}");
                }
                Console.WriteLine("\n\n[내정보]");
                Console.Write($"Lv.{level} {name} ({job})\n");
                Console.WriteLine($"HP 100/{hp}\n");

                // 공격할지 선택
                SelectAttackUI();
                Attack();
            }
            // 플레이어 공격 차례일때 
            // 공격할지 선택
            void SelectAttackUI()
            {
                Console.WriteLine("1. 공격\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>>");
            }
            // 플레이어 공격 차례일때 
            // 누구를 공격할지 선택
            void SelectMonsterUI()
            {
                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.\n>>");
            }
            // 플레이어 상태에 따른 공격
            public void Attack()
            {
                int selectedIndex = 0;
                bool running = true;
                string msg = "";

                while (running)
                {

                    // 사용자 키 입력 

                    var Key = Console.ReadKey(true).Key;

                    switch (Key)
                    {
                        // 
                        case ConsoleKey.UpArrow:
                            msg = "";

                            break;

                        case ConsoleKey.DownArrow:
                            msg = "";

                            break;

                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                          ;
                          //  running = false;

                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                         
                           // Console.Clear();
                            //BattleUI();
                            //SelectMonsterUI();
                            running = false;
                            break;
                        default:
                            msg = "잘못된 입력입니다.";
                            break;
                    }
                }


            }


            // 데미지 입음
            public void ApplyDamage(int damage)
            {
                if (isDie) { return; }

                hp -= damage;
                if (hp <= 0) { hp = 0; isDie = true; Lose(); }
            }
            // 적 공격하기
            public int DealDamage()
            {
                int playerDamage = GetRandom(attack - 10, attack + 10);
                Console.WriteLine($"{name} 의 공격!\n>>");
             //   Console.WriteLine($"{} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n>>");
                return playerDamage;
            }
            // 공격화면 UI
            void AttackUI(int playerDamage)
            {
                Console.Clear();

                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{name} 의 공격!\n");
                Console.Write($"Lv.{level} {target} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");
                Console.Write($"HP 100 -> {hp}\n");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine(">>");
            }
            // 종료
            void Lose()
            {
                Console.Clear();

                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("You Lose\n");
                Console.Write($"Lv.{level} {name}");
                Console.Write($"HP 100 -> {hp}\n");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine(">>");
            }
            public int GetRandom(int min, int max)
            {
                Random rand = new Random();
                int result = rand.Next(min, max);
                return result;
            }
        }
        public class Monster
        {
            public string Name { get; set; }
            public int Level { get; set; }
            public int HP { get; set; }
            public Monster(string name, int level, int hp)
            {
                Name = name;
                Level = level;
                HP = hp;
            }
        }
    }
}
