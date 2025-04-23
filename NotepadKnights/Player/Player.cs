using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class Player
    {
        MonsterFactory monsterFactory = new MonsterFactory();
        AttackAndDefense attackAndDefense = new AttackAndDefense();

        // 현재 공격중인 적 찾기   
        public void SelectTarget(int num)
        {
            if (num >= 1 && num <= monsterFactory.createMonsters.Count)
            {
                Program.playerStatus.Target = monsterFactory.createMonsters[num - 1];
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 공격
        public void ExecuteAttack()
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
                int attackPower = Program.playerStatus.GetAttack();

                // 공격한다
                attackPower = attackAndDefense.Attack(attackPower);
                playerTarget.CurrentHp = attackAndDefense.EnemyDefense(attackPower, playerTarget.CurrentHp);

                // 공격 이후 UI
                Program.playerUI.DisplayAttackResult(attackPower);
            }
        } 
    }
}
