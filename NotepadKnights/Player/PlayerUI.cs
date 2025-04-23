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
      //  MonsterFactory monsterFactory = new MonsterFactory();
        BattleManager battleManager = new BattleManager();


        // 공격하기 전 화면 UI
        public void ShowBattleMenu()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < Program.monsterFactory.createMonsters.Count; i++)
            {
                monsterIndexDisplay = Program.playerStatus.isAttack ? (i + 1).ToString() : "";

                var monster = Program.monsterFactory.createMonsters[i];
                Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} HP {monster.CurrentHp} ");
            }

            Console.WriteLine("\n\n[내정보]");
            Console.Write($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name} ({Program.playerStatus.Job})\n");
            Console.WriteLine($"HP 100/{Program.playerStatus.Hp}\n");

            // 아직 공격하지 않았다면
            if (!Program.playerStatus.isAttack)
            {
                Console.WriteLine("1. 공격\n");
                Console.Write("원하시는 행동을 입력해주세요.\n");
            }
            else
            {
                Console.WriteLine("0. 취소\n");
                Console.Write("대상을 선택해주세요.\n");
            }
            //  키입력에 따른 화면 변화
            playerInput.ScreenChanges();
        }
        // 공격 이후 UI
        public void DisplayAttackResult(int playerDamage, string msg)
        {
            Console.Clear();

            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{Program.playerStatus.Name} 의 공격!");

            Monster playerTarget = Program.playerStatus.Target;

            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");
            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}");

            //if (playerTarget.CurrentHp <= 0 || playerTarget.IsDead)
            //    Console.WriteLine($"HP 0 ->Dead\n");
            //else
            //    Console.WriteLine($"HP {playerTarget.CurrentHp}\n");
            Console.WriteLine(playerTarget.CurrentHp <= 0 || playerTarget.IsDead? $"HP 0 ->Dead\n" : $"HP {playerTarget.CurrentHp}\n");
            Console.WriteLine(msg+"\n");

            Console.WriteLine("0. 다음\n>>");

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
