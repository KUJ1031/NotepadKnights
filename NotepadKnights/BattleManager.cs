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
        MonsterFactory monsterFactory;

        public void IntoBattle()
        {
            monsterFactory = new MonsterFactory();
            ShowBattleMenu();
        }
        public void ExecutePlayerPhase()
        {
            // 스킬을 가져온 뒤
            float playerDamager = Skill.SkillUse(Program.playerStatus.Mp);

            // 변경된 값을 적용하고,
            Program.playerStatus.Attack = playerDamager;


            // 스킬을 사용하여 공격한다
            // ExecuteAttack(playerDamager);

            Monster playerTarget = Program.playerStatus.Target;

            // 적이 죽었다면
            if (playerTarget.CurrentHp == 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            // 적이 살아있다면
            else
            {
                playerDamager = Program.playerStatus.Attack;

                // 몬스터 피격
                playerTarget.CurrentHp = atkAndDef.EnemyDefense(playerDamager, playerTarget.CurrentHp);
                if (playerTarget.CurrentHp <= 0)
                {
                    playerTarget.IsDead = true;
                }

                // 공격 이후 UI
                DisplayAttackResult(playerDamager, Program.player.msg);
            }

            // 플레이어 승리 확인
            CheckVictory();
        }

        public void ExecuteEnemyPhase()
        {
            foreach (Monster monster in monsterFactory.createMonsters)
            {
                Console.Clear();
                Console.WriteLine($"[적의 턴]\n");
                if (monster.CurrentHp <= 0 || Program.playerStatus.Hp == 0) { continue; }
                Console.WriteLine($"Lv.{monster.Level} {monster.Name}의 공격!"); Thread.Sleep(1000);
                int monsterAtk = monster.DealDamage();
                int playerHpAfterDamaged = atkAndDef.PlayerDefense(monsterAtk, Program.playerStatus.Hp, Program.playerStatus.Defense);
                
               // Console.WriteLine($"{Program.playerStatus.Name} 을(를) 맞췄습니다.   [데미지 : {monster.Atk - Program.playerStatus.Defense}]");
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
            if (Program.playerStatus.KilledMonsterCount >= monsterFactory.createMonsters.Count)
            {
                Console.Clear();

                Console.WriteLine("\nBattle!! - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {Program.playerStatus.KilledMonsterCount}마리를 잡았습니다.\n");
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"현재 체력 : {Program.playerStatus.Hp}\n");
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

                // 죽인 적의 수 0으로 초기화
                Program.playerStatus.KilledMonsterCount = 0;
                EndGame();
            }
        }
        // 게임 종료시
        void EndGame()
        {

            //  Program.playerStatus.IsAttack = false;
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
                        Program.InventoryManager.Run();
                        //인벤토리
                        break;
                    case 3:
                        Program. //상점
                        StoreManager.Run();
                        break;
                    case 4:
                        //전투
                        Program.battleManager.IntoBattle();
                        //Program.battleManager.ShowBattleMenu();
                        break;
                    case 5:
                        Program.healing.IntoHealing();
                        //회복하기
                        break;
                    case 6:
                        Program.quest.QuestWindow();
                        break;
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
                if (Program.playerStatus.KilledMonsterCount >= monsterFactory.createMonsters.Count)
                {
                    // 승리 화면 띄우기
                    CheckVictory();

                }
                // 적들이 죽지 않았다면 전투하기
                else
                {
                    Console.WriteLine("<전투!!>\n");
                    Console.WriteLine($"[{Program.playerStatus.Name} 턴]\n");

                    for (int i = 0; i < monsterFactory.createMonsters.Count; i++)
                    {
                        monsterIndexDisplay = Program.playerStatus.IsAttack ? (i + 1).ToString() : "";

                        var monster = monsterFactory.createMonsters[i];
                        string monsterHpTxt = monster.IsDead ? $"Dead" : $"HP {monster.CurrentHp}";
                        Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} {monsterHpTxt}");
                    }

                    Console.WriteLine("\n\n[내정보]");
                    Console.Write($"Lv.{Program.playerStatus.Level}\n이름 : {Program.playerStatus.Name}\n직업 :{Program.playerStatus.Job}\nMp : {Program.playerStatus.Mp}\n");
                    Console.WriteLine($"HP {Program.playerStatus.Hp}/{Program.playerStatus.MaxHp}\n");

                    // 아직 공격하지 않았다면
                    if (!Program.playerStatus.IsAttack)
                    {
                        Console.WriteLine("1. 공격\n");
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
            bool validInput = false;

            while (!validInput)
            {
                Console.Write(">>");
                int monsterCount = monsterFactory.createMonsters.Count;
                int choice = InputManager.ReadInt(0, monsterCount); // 입력은 여기서 관리

                if (!Program.playerStatus.IsAttack)
                {
                    if (choice == 1)
                    {
                        Program.playerStatus.IsAttack = true;
                        ShowBattleMenu();
                        validInput = true; // 루프 종료
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    if (choice == 0)
                    {
                        Program.playerStatus.IsAttack = false;
                        ShowBattleMenu();
                        validInput = true; // 루프 종료
                    }
                    else
                    {
                        var target = monsterFactory.createMonsters[choice - 1];
                        if (target != null && target.CurrentHp > 0)
                        {
                            Program.playerStatus.Target = target;
                            // 플레이어 공격 턴
                            ExecutePlayerPhase();
                            validInput = true; // 루프 종료
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                }
            }
        }

        // 현재 공격중인 적 찾기   
        public void SelectTarget(int num)
        {
            if (num >= 1 && num <= monsterFactory.createMonsters.Count)
            {
                var selectedMonster = monsterFactory.createMonsters[num - 1];
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


        // 공격의 결과를 보여주자
        public void DisplayAttackResult(float playerDamage, string msg)
        {

            Monster playerTarget = Program.playerStatus.Target;

            if (atkAndDef.onDodge == false)
            {
                Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}에게 {playerDamage}만큼의 데미지!");
            }                

            //  적이 죽었으면
            if (playerTarget.IsDead)
            {
                playerTarget = null;
                Console.WriteLine($"HP 0 ->Dead\n");
                Program.playerStatus.KilledMonsterCount++;

            }
            else
            {
                Console.WriteLine($"현재 [{playerTarget.Name}]의 HP : {playerTarget.CurrentHp}\n");
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
        // 적이 죽었을 경우
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
