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
          
        // 플레이어 차례
        public void ExecutePlayerPhase()
        {
            if(Program.playerStatus.Hp > 0 )
            {
                // 모든 적들이 죽었다면
                if (Program.playerStatus.KilledMonsterCount >= Program.monsterFactory.createMonsters.Count)
                {
                    // 승리 화면 띄우기
                    CheckVictory();
                }
                else
                {
                    // 공격한다
                    Program.player.ExecuteAttack();
                }
            }
            else
            {
                // 게임 패배
              
            }
        }

        private Player player;     

        public void ExecuteEnemyPhase()
        {
            foreach(Monster monster in Program.monsterFactory.createMonsters)
            {
                int monsterAtk = monster.DealDamage();
                Console.Clear();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{Program.playerStatus.Name} 을(를) 맞췄습니다.   [데미지 : {Program.playerStatus.Defense - monster.Atk}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"HP {Program.playerStatus.Hp} -> {Program.playerStatus.Hp - 100}");
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
            Program.playerUI.ShowBattleMenu();
        }
        // 플레이어의 승리 
        public void CheckVictory()
        {              
            Console.Clear();

            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {Program.playerStatus.KilledMonsterCount}마리를 잡았습니다.\n");
            Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
            Console.WriteLine($"HP 100 -> {Program.playerStatus.Hp}\n");
            Console.WriteLine("0. 다음\n");
            
        }
        public void CheckDefeat()
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
            Console.Write($"HP 100 -> {Program.playerStatus.Hp}\n");
            Console.WriteLine("0. 다음\n");
            Console.WriteLine(">>");
        }
    }
}
