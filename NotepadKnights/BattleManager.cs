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
        private MonsterFactory monsterFactory = new MonsterFactory();    // 임시 static 오브젝트 생성
        AttackAndDefense attackAndDefense = new AttackAndDefense();
     
        // 플레이어 차례
        public void ExecutePlayerPhase()
        {
            // 공격한다
            Program.Player.Attack();
        }
        public void ExecuteEnemyPhase()
        {
            foreach(Monster monster in monsterFactory.createMonsters)
            {
                monster.DealDamage();
            }
        }
        public void CheckVictory()
        {

        }
        public void CheckDefeat()
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write($"Lv.{Program.Player.Level} {Program.Player.Name}");
            Console.Write($"HP 100 -> {Program.Player.Hp}\n");
            Console.WriteLine("0. 다음\n");
            Console.WriteLine(">>");
        }
    }
}
