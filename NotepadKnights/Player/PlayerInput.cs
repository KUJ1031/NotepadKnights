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

            if (input == "0" && Program.playerStatus.isAttack)
            {
                Console.Clear();
                Program.playerStatus.IsAttackChange();
               Program.playerUI.ShowBattleMenu();
            }
            // 유효 숫자를 입력했다면
            else if (input == "1" || input == "2" || input == "3")
            {
                Console.Clear();

                // 공격 중이지 않다면
                if (!Program.playerStatus.isAttack)
                {
                    Program.playerStatus.IsAttackChange();
                    Program.playerUI.ShowBattleMenu();
                }
                // 공격 중이라면
                else
                {   // 타겟을 찾은 다음
                    Program.player.SelectTarget(int.Parse(input));

                    // 플레이어 공격턴 실행
                    battleManager.ExecutePlayerPhase();

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
