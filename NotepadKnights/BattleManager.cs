using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private MonsterFactory monsterFactory = new MonsterFactory();    // 임시 static 오브젝트 생성

        AttackAndDefense attackAndDefense = new AttackAndDefense();
     
        // 플레이어 차례
        public void ExecutePlayerPhase()
        {
            if(Program.Player.Hp > 0 )
            {  // 공격한다
                Program.Player.ExecuteAttack();
            }
            else
            {
                // 게임 패배
            }
        }

        private Player player;     

        public void ExecuteEnemyPhase()
        {
            foreach(Monster monster in monsterFactory.createMonsters)
            {
                int monsterAtk = monster.DealDamage();
                Console.Clear();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{Program.Player.Name} 을(를) 맞췄습니다.   [데미지 : {Program.Player.Defense - monster.Atk}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.Player.Level} {Program.Player.Name}");
                Console.WriteLine($"HP {Program.Player.Hp} -> {Program.Player.Hp - monsterAtk}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
   
                while (true)
                {
                    int select = int.Parse(Console.ReadLine());
                    if (select != 0)
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("몬스터들의 공격 차례가 끝났습니다.");
            Thread.Sleep(1000);

            // 플레이어 턴
            ExecutePlayerPhase();
        }
        public void CheckVictory()
        {

        }
        public void CheckDefeat()
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write($"Lv.{Program.Player.Level} {Program.Player.Name}");
            Console.Write($"HP 100 -> {Program.Player.Hp}\n");
            Console.WriteLine("0. 다음\n");
            Console.WriteLine(">>");
        }
    }
}
