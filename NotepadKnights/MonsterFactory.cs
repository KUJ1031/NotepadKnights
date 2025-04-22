using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    internal class MonsterFactory
    {
        public List<Monster> createMonsters = new List<Monster>(); // 랜덤하게 생성한 몬스터를 넣을 List
        Random random = new Random();

        // MonsterFactory 객체 생성 시 몬스터도 생성
        public MonsterFactory()
        {
            InitializeMonsters();
        }

        public void InitializeMonsters()
        {
            createMonsters.Clear();     // 이전 데이터 삭제

            int monsterCount = random.Next(1, 5);   // 1 ~ 4마리 사이 생성

            for (int i = 0; i < monsterCount; i++)
            {
                Monster monster = CreateRandomMonster();
                createMonsters.Add(monster);
            }
        }

        /*
		 * 세 종류의 몬스터를 랜덤하게 생성시켜줄 메서드
		 */
        public Monster CreateRandomMonster()
        {
            int type = random.Next(0, 3);   // 0: 미니언, 1 : 공허충, 2 : 대포미니언

            switch (type)
            {
                default:
                case 0:
                    return new Monsters.Minion();   // Monsters 폴더 내부 클래스 호출
                case 1:
                    return new Monsters.Voiling();
                case 2:
                    return new Monsters.CannonMinion();
            }
        }
    }
}
