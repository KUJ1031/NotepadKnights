using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotepadKnights
{
    public class Player
    {
        public Inventory Inventory { get; private set; }

        AttackAndDefense attackAndDefense = new AttackAndDefense();
        public string msg;

        public Player()
        {
            Inventory = new Inventory();
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
                    Program.playerUI.ShowBattleMenu();
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

                // 공격력의 범위를 조정하고, 랜덤 범위 내에서 공격력을 설정
                attackPower = GenerateRandomAttackPower(attackPower);

                // 공격한다
                attackPower = attackAndDefense.Attack(attackPower);
                playerTarget.CurrentHp = attackAndDefense.EnemyDefense(attackPower, playerTarget.CurrentHp);

                // 공격 이후 UI
                Program.playerUI.DisplayAttackResult(attackPower, msg);
            }
        }
        public float GenerateRandomAttackPower(float attackPower)
        {
            // 공격력의 범위를 조정하고, 랜덤 범위 내에서 공격력을 설정
            float error = MathF.Ceiling(attackPower * 0.1f);
            Random random = new Random();
            int min = Math.Max(0, (int)(attackPower - error));
            int max = Math.Max(min + 1, (int)(attackPower + error));
            attackPower = random.Next(min, max);

            return attackPower;
        }
        // 레벨업
        public void LevelUp()
        {
            // Level += 1, 공격력 += 0.5, 방어력 += 1
            Program.playerStatus.SetLevel(Program.playerStatus.Level+1);
            Program.playerStatus.SetAttack(Program.playerStatus.Attack + 0.5f);
            Program.playerStatus.SetDefense(Program.playerStatus.Defense +1);
        }
        // 골드 추가
        public void AddGold(int RewardGold)
        {
            Program.playerStatus.SetGold(Program.playerStatus.Gold + RewardGold);
        }
        // 인벤토리 아이템 추가
        public void Additem(Item item)
        {
            Inventory.AddItem(item);
        }

    }
}
