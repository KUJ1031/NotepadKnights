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

        // 난이도 적용시 필요한 가중치 관련 코드
        // 각 몬스터별 가중치에 따라 나올 확률을 조정해줄 Dictionary
        private Dictionary<int, int> weightByDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                default:
                case 1:
                    return new Dictionary<int, int>()
                    {
                        { 0, 70 },
                        { 1, 20 },
                        { 2, 10 }
                    };
                case 2:
                    return new Dictionary<int, int>()
                    {
                        { 0, 50 },
                        { 1, 30 },
                        { 2, 20 }
                    };
                case 3:
                    return new Dictionary<int, int>()
                    {
                        { 1, 70 },
                        { 2, 20 },
                        { 3, 10 }
                    };
                case 4:
                    return new Dictionary<int, int>()
                    {
                        { 2, 50 },
                        { 3, 30 },
                        { 4, 20 }
                    };
            }
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


        ////////////////// 아래는 난이도에 따라 몬스터를 생성하는 로직입니다. //////////////////
        /*
         * 난이도에 따라 몬스터 생성하는 로직
         * 추후에 난이도 기능 생성하면 적용할 코드입니다!
         */
        public void InitializeMonstersOnDifficulty(int difficultyLevel)
        {
            createMonsters.Clear();     // 이전 데이터 삭제

            // 난이도 1,3 => 1 ~ 4마리 생성
            // 난이도 2,4 => 1 ~ 5마리 생성
            /*
             * 난이도 1,2 => 몬스터 케이스 0 ~ 2
             * 난이도 3,4 => 몬스터 케이스 0 ~ 4
             */
            if (difficultyLevel == 1 || difficultyLevel == 3)
            {
                int monsterCount = random.Next(1, 5);   // 1 ~ 4마리 사이 생성

                for (int i = 0; i < monsterCount; i++)
                {
                    Monster monster = CreateRandomMonster();
                    createMonsters.Add(monster);
                }
            }
            else if (difficultyLevel == 2 || difficultyLevel == 4)
            {
                int monsterCount = random.Next(2, 5);   // 2 ~ 4마리 사이 생성

                for (int i = 0; i < monsterCount; i++)
                {
                    Monster monster = CreateRandomMonster();
                    createMonsters.Add(monster);
                }
            }
        }

        /*
         * * 난이도에 따라 세 종류의 몬스터를 랜덤하게 생성시켜줄 메서드
         */
        public Monster CreateRandomMonsterOnDifficulty(int difficultyLevel)
        {
            if (difficultyLevel <= 2)
            {
                Dictionary<int, int> lowLevel = weightByDifficulty(difficultyLevel);    // 0: 미니언, 1 : 공허충, 2 : 대포미니언
                int typeOfDifficulty = CreatMonsterOnDifficulty(lowLevel);

                switch (typeOfDifficulty)
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
            else if (difficultyLevel == 3)
            {
                Dictionary<int, int> MiddleLevel = weightByDifficulty(difficultyLevel);     // 1: 공허충, 2: 칼날부리, 3: 대포미니언
                int typeOfDifficulty = CreatMonsterOnDifficulty(MiddleLevel);

                switch (typeOfDifficulty)
                {
                    default:
                    case 1:
                        return new Monsters.Voiling();   // Monsters 폴더 내부 클래스 호출
                    case 2:
                        return new Monsters.Raptor();
                    case 3:
                        return new Monsters.CannonMinion();
                }
            }
            else
            {
                Dictionary<int, int> HighLevel = weightByDifficulty(difficultyLevel);       // 2: 칼날부리, 3: 대포미니언, 4: 어스름늑대
                int typeOfDifficulty = CreatMonsterOnDifficulty(HighLevel);

                switch (typeOfDifficulty)
                {
                    default:
                    case 1:
                        return new Monsters.Raptor();   // Monsters 폴더 내부 클래스 호출
                    case 2:
                        return new Monsters.CannonMinion();
                    case 3:
                        return new Monsters.Wolf();
                }
            }
        }

        // 가중치를 기반으로 한 랜덤 생성 함수
        public int CreatMonsterOnDifficulty(Dictionary<int, int> weightDictionary)
        {
            int totalWeight = 0;    // 전체 가중치
            foreach (var weight in weightDictionary.Values)
            {
                totalWeight += weight;
            }

            int randomNumber = random.Next(0, totalWeight);
            int cumulative = 0;     // 가중치 누적

            foreach (var kvp in weightDictionary)
            {
                cumulative += kvp.Value;
                if (randomNumber < cumulative)
                {
                    return kvp.Key;
                }
            }

            foreach (var kvp in weightDictionary)
            {
                return kvp.Key;
            }

            throw new InvalidCastException("Dictionary가 비어있습니다.");
        }
    }
}
