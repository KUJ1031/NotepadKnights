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

        // 플레이어 공격턴
        public void ExecutePlayerPhase()
        {
            // 스킬 공격력을 가져온 뒤
            float playerDamager = Skill.SkillUse(Program.playerStatus.Mp);

            // 치명타 피해량을 계산한다.
            // 공격력의 범위를 조정하고, 랜덤 범위 내에서 공격력을 설정
           // playerDamager = GenerateRandomAttackPower(playerDamager);
          //  playerDamager = attackAndDefense.Attack(playerDamager);

            // 변경된 값을 적용하고,
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
                Console.Clear();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{Program.playerStatus.Name} 을(를) 맞췄습니다.   [데미지 : {Program.playerStatus.Defense - monster.Atk}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"HP {Program.playerStatus.Hp} -> {Program.playerStatus.Hp - monster.Atk}");
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

            // 플레이어 공격턴
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

            // 보상받기
            BattleRewardManager battleRewardManager = new BattleRewardManager();
            battleRewardManager.GetRewards(Program.playerStatus.KilledMonsterCount);
            EndGame();
        }
        // 플레이어 패배
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
            // 죽인 적의 수 0으로 초기화
            Program.playerStatus.SetKilledMonsterCount(0);

            // 다시 마을로 돌아간다.
            string input = Console.ReadLine();
            while (true)
            {
                switch (Program.mainMenu.Village())
                {
                    case 1:
                        //상태보기
                        break;
                    case 2:
                        //인벤토리
                        break;
                    case 3:
                        //상점
                        break;
                    case 4:
                        //전투
                        Program.playerStatus.InitializePlayer();
                        Program.playerUI.ShowBattleMenu();
                        break;
                    case 5:
                        //회복하기
                        break;
                    case 6:
                    //추가사항
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        break;
                }
            }
        }


        
    }
}
