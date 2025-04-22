using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static NotepadKnights.BattleManager;

namespace NotepadKnights
{
   public class Player : IRandomProvider
    {
        public string name { get; set; }
        public string job { get; set; }
        public int level { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public float attack { get; set; } // 공격력
        public int defense { get; set; } // 방어력
        public bool isDie { get; set; }
        string target;
        string index = "";
        public bool isAttack { get; set; } // 공격할지 선택
        public bool isSelectMonster { get; set; }// 공격할 몬스터를 골랐는지 여부

        // 공격 UI
        public void BattleUI()
        {
            Console.WriteLine("Battle!!\n");

            foreach (var monster in MonsterFactory.monsters)
            {
               
            }
            for (int i = 0; i < MonsterFactory.monsters.Count; i++)
            {
                if (isAttack) { index = (i+1).ToString(); }
                else { index = ""; }

                Console.WriteLine($"{index} Lv.{MonsterFactory.monsters[i].Name} {MonsterFactory.monsters[i].Level} HP {MonsterFactory.monsters[i].CurrentHp}");
            }
            Console.WriteLine("\n\n[내정보]");
            Console.Write($"Lv.{level} {name} ({job})\n");
            Console.WriteLine($"HP 100/{hp}\n");
            if (!isAttack)
            { // 공격할지 선택
                SelectAttackUI();
            }
            else
            {
                SelectMonsterUI();
            }
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
                //

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
                        Console.Clear();
                        isAttack = true;
                        BattleUI();
                        //
                      //  target = 
                      //  DealDamage(MonsterFactory.monsters[1].Name);
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
            int playerDamage = GetRandom((int)Math.Ceiling(-attack*0.1f), (int)Math.Ceiling(attack * 0.1f));
            Console.WriteLine($"{name} 의 공격!\n>>");
            Console.WriteLine($"{target} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n>>");
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
}
