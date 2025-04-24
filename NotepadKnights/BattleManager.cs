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
        AttackAndDefense atkAndDef = new AttackAndDefense();
        // 플레이어 차례
        public void ExecutePlayerPhase()
        {
            // 공격력을 변경한 뒤
            float playerDamager = Skill.SkillUse(Program.playerStatus.Mp);
            Program.playerStatus.SetAttack(playerDamager);

            // 스킬을 사용하여 공격한다
            Program.player.ExecuteAttack(playerDamager);

            // 적들이 다 죽었다면
            if (Program.playerStatus.KilledMonsterCount >= Program.monsterFactory.createMonsters.Count)
            {
                // 승리
                CheckVictory();
            }

        }

        public void ExecuteEnemyPhase()
        {
            foreach (Monster monster in Program.monsterFactory.createMonsters)
            {
                if (monster.CurrentHp <= 0) { continue; }
                int monsterAtk = monster.DealDamage();
                int playerHpAfterDamaged = atkAndDef.PlayerDefense(monsterAtk, Program.playerStatus.Hp, Program.playerStatus.Defense);
                
                Console.Clear();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Thread.Sleep(1000);
                Console.WriteLine($"{Program.playerStatus.Name} 을(를) 맞췄습니다.   [데미지 : {monster.Atk - Program.playerStatus.Defense}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"HP {Program.playerStatus.Hp} -> {playerHpAfterDamaged}");

                // 위의 공격 주고 받는 부분 출력 이후 계산
                Program.playerStatus.Hp = playerHpAfterDamaged;
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
            Console.Clear();
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

            EndGame();
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

            EndGame();
        }
        // 게임 종료시
        void EndGame()
        {
            string input = Console.ReadLine();

            if (input == "0")
            {
                // 마을로 돌아가기
                Program.mainMenu.Village();
            }
            else { }
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
