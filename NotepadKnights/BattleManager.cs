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
        private string monsterIndexDisplay = "";
        AttackAndDefense atkAndDef = new AttackAndDefense();
        // 플레이어 차례
        public void ExecutePlayerPhase()
        {
            // 스킬 공격력을 가져온 뒤
            float playerDamager = Skill.SkillUse(Program.playerStatus.Mp);

            // 변경된 값을 적용하고,
            Program.playerStatus.Attack = playerDamager;

            // 스킬을 사용하여 공격한다
            ExecuteAttack(playerDamager);

            // 플레이어 승리 확인
            CheckVictory();
        }

        public void IntoBattle()
        {

        }

        public void ExecuteEnemyPhase()
        {
            foreach (Monster monster in Program.monsterFactory.createMonsters)
            {
                if (monster.CurrentHp <= 0 || Program.playerStatus.Hp == 0) { continue; }
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

            // 플레이어 패배 확인
            CheckDefeat();
        }
        // 플레이어의 승리 
        public void CheckVictory()
        {
            // 적들이 다 죽었다면
            if (Program.playerStatus.KilledMonsterCount >= Program.monsterFactory.createMonsters.Count)
            {
                Console.Clear();
                Console.WriteLine(Program.monsterFactory.createMonsters);
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
        }
        // 플레이어 패배 확인
        public void CheckDefeat()
        {
            if (Program.playerStatus.Hp > 0)
            { // 플레이어 공격턴
                ShowBattleMenu();
            }
            else
            {
                // 패배화면 출력
                Console.Clear();

                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("You Lose\n");
                Console.Write($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.Write($"HP 100 -> {Program.playerStatus.Hp}\n");
                Console.WriteLine("0. 다음\n");
                Console.WriteLine(">>");

                EndGame();
            }
        }
        // 게임 종료시
        void EndGame()
        {
            // 죽인 적의 수 0으로 초기화
            Program.playerStatus.KilledMonsterCount = 0;
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
                        ShowBattleMenu();
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

        public void ShowBattleMenu()
        {
            Console.Clear();

            if (Program.playerStatus.Hp > 0)
            {
                // 모든 적들이 죽었다면
                if (Program.playerStatus.KilledMonsterCount >= Program.monsterFactory.createMonsters.Count)
                {
                    // 승리 화면 띄우기
                    CheckVictory();

                }
                // 적들이 죽지 않았다면 전투하기
                else
                {
                    Console.WriteLine("Battle!!\n");

                    for (int i = 0; i < Program.monsterFactory.createMonsters.Count; i++)
                    {
                        monsterIndexDisplay = Program.playerStatus.IsAttack ? (i + 1).ToString() : "";

                        var monster = Program.monsterFactory.createMonsters[i];
                        string monsterHpTxt = monster.IsDead ? $"Dead" : $"HP {monster.CurrentHp}";
                        Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} {monsterHpTxt}");
                    }

                    Console.WriteLine("\n\n[내정보]");
                    Console.Write($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name} ({Program.playerStatus.Job})\n");
                    Console.WriteLine($"HP {Program.playerStatus.Hp}/{Program.playerStatus.MaxHp}\n");

                    // 아직 공격하지 않았다면
                    if (!Program.playerStatus.IsAttack)
                    {
                        Console.WriteLine("1. 공격\n");
                        Console.Write("원하시는 행동을 입력해주세요.\n");
                    }
                    else
                    {
                        Console.WriteLine("0. 취소\n");
                        Console.Write("대상을 선택해주세요.\n");
                        Console.WriteLine(Program.player.msg + "\n");
                    }
                }
            }
            else
            {
                // 패배 
               CheckDefeat();
            }
            //  키입력에 따른 화면 변화
            ScreenChanges();
        }


        public void ScreenChanges()
        {
            // 사용자 키 입력 
            Console.Write(">>");
            string input = Console.ReadLine();

            // 공격 중이 아닐때
            if (!Program.playerStatus.IsAttack)
            {
                if (input == "1")
                {
                    // 공격 모드 진입
                    Console.Clear();
                    Program.playerStatus.IsAttack = true;
                     ShowBattleMenu();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ScreenChanges();
                }
            }
            else // 공격 중인 상태
            {
                if (input == "0")
                {
                    Console.Clear();
                    Program.playerStatus.IsAttack = false;
                    ShowBattleMenu();
                }
                else if (input == "1" || input == "2" || input == "3")
                {
                    Console.Clear();
                    SelectTarget(int.Parse(input));

                    if (Program.playerStatus.Target != null && Program.playerStatus.Target.CurrentHp > 0)
                    {
                        // 플레이어 공격턴 시작
                        ExecutePlayerPhase();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        ScreenChanges();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ScreenChanges();
                }
            }
        }

        // 현재 공격중인 적 찾기   
        public void SelectTarget(int num)
        {
            if (num >= 1 && num <= Program.monsterFactory.createMonsters.Count)
            {
                var selectedMonster = Program.monsterFactory.createMonsters[num - 1];
                // 몬스터가 살아있다면
                if (selectedMonster.CurrentHp > 0)
                {
                    Program.player.msg = "";
                    Program.playerStatus.Target = selectedMonster;
                }
                else // 몬스터가 이미 죽었으면
                {
                    // 다른 몬스터 선택하기
                    Program.player.msg = "잘못된 입력입니다.";
                    ShowBattleMenu();
                }
            }
        }

        // 공격
        public void ExecuteAttack(float attackPower)
        {
            Monster playerTarget = Program.playerStatus.Target;

            // 적이 죽었다면
            if (playerTarget.CurrentHp == 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            // 적이 살아있다면
            else
            {
                attackPower = Program.playerStatus.Attack;

                // 몬스터 피격
                playerTarget.CurrentHp = atkAndDef.EnemyDefense(attackPower, playerTarget.CurrentHp);

                // 공격 이후 UI
                DisplayAttackResult(attackPower, Program.player.msg);
            }
        }

        public void DisplayAttackResult(float playerDamage, string msg)
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{Program.playerStatus.Name} 의 공격!");

            Monster playerTarget = Program.playerStatus.Target;

            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");
            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}");
            playerTarget.ApplyDamage(playerDamage);

            //  적이 죽었으면
            if (playerTarget.IsDead)
            {
                playerTarget = null;
                Console.WriteLine($"HP 0 ->Dead\n");

                Program.playerStatus.KilledMonsterCount++;
            }
            else
            {
                Console.WriteLine($"HP {playerTarget.CurrentHp}\n");
            }

            Console.WriteLine(msg + "\n");
            Console.Write("0. 다음\n>>");

            string input = Console.ReadLine();
            while (input != "0")
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = Console.ReadLine();
            }
            // 이 이후 적 공격 턴 실행
            ExecuteEnemyPhase();
        }
        public void DieTarget()
        {
            Program.playerStatus.Target.CurrentHp = 0;
            Program.playerStatus.Target = null;

            Program.playerStatus.KilledMonsterCount++;
            Console.WriteLine($"HP 0 ->Dead\n");

            // 경헙치 업
            Program.player.ExpUp();
        }
    }
}
