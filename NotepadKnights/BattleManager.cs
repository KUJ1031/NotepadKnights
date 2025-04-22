using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    interface IRandomProvider
    {
        int GetRandom(int min, int max);
    }
    internal class BattleManager
	{
        private MonsterFactory monsterFactory = new MonsterFactory();    // 임시 static 오브젝트 생성
        private Player player;
        
        public void ExecuteEnemyPhase()
        {
            foreach(Monster monster in monsterFactory.createMonsters)
            {
                int monsterAtk = monster.DealDamage();
                Console.Clear();
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{player.name} 을(를) 맞췄습니다.   [데미지 : {player.defense - monster.Atk}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {player.hp} -> {player.hp - monsterAtk}");
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
        }
	}
}
