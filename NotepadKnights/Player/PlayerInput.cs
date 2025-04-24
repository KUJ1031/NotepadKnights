using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    internal class PlayerInput
    {
        BattleManager battleManager = new BattleManager();

        //  키입력에 따른 화면 변화
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
                    Program.playerStatus.SetIsAttack(true);
                    Program.playerUI.ShowBattleMenu();
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
                    Program.playerStatus.SetIsAttack(false);
                    Program.playerUI.ShowBattleMenu();
                }
                else if (input == "1" || input == "2" || input == "3")
                {
                    Console.Clear();
                    Program.player.SelectTarget(int.Parse(input));

                    if (Program.playerStatus.Target != null && Program.playerStatus.Target.CurrentHp > 0)
                    {
                        // 플레이어 공격턴 시작
                        battleManager.ExecutePlayerPhase();
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

    }
}
