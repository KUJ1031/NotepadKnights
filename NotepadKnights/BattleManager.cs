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
        private static MonsterFactory monsterFactory = new MonsterFactory();    // 임시 static 오브젝트 생성

        public void ExecuteEnemyPhase()
        {
            foreach(Monster monster in monsterFactory.createMonsters)
            {
                monster.DealDamage();
            }
        }
	}
}
