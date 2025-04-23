using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotepadKnights
{
    public class PlayerUI
    {
        private string monsterIndexDisplay = "";

        PlayerInput playerInput = new PlayerInput();
        BattleManager battleManager = new BattleManager();

        // 공격하기 전 화면 UI
        public void ShowBattleMenu()
        {
            Console.Clear();
          
            if (Program.playerStatus.Hp > 0)
            {
                // 모든 적들이 죽었다면
                if (Program.playerStatus.KilledMonsterCount >= Program.monsterFactory.createMonsters.Count)
                {
                    // 승리 화면 띄우기
                    battleManager.CheckVictory();

                }
                // 적들이 죽지 않았다면 전투하기
                else
                {
                    Console.WriteLine("Battle!!\n");

                    for (int i = 0; i < Program.monsterFactory.createMonsters.Count; i++)
                    {
                        monsterIndexDisplay = Program.playerStatus.IsAttack ? (i + 1).ToString() : "";

                        var monster = Program.monsterFactory.createMonsters[i];
                        Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} HP {monster.CurrentHp} ");
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
                battleManager.CheckDefeat();
            }
                //  키입력에 따른 화면 변화
                playerInput.ScreenChanges();
        }
        // 공격 이후 UI
        public void DisplayAttackResult(float playerDamage, string msg)
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{Program.playerStatus.Name} 의 공격!");

            Monster playerTarget = Program.playerStatus.Target;

            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");
            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}");

            //  적이 죽었으면
            if( playerTarget.CurrentHp <= 0 || playerTarget.IsDead )
            {
                Program.player.DieTarget();
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
            battleManager.ExecuteEnemyPhase();
        }
    }
}
