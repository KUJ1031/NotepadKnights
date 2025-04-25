using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    //몬스터 죽는데다가 선언
    //public static event Action<string> OnMonsterKilled;
    //OnMonsterKilled?.Invoke(monster Name); 죽을때 신호 보내기

    
    class QuestCounter
    {
        List<Quest> questCount;    //퀘스트의 최대 카운트

        public QuestCounter(List<Quest> activequests)
        {
            //Program.OnMonsterKilled += HandleMonsterKilled;
            questCount = activequests;
        }
        public void HandleMonsterKilled(string monsterName)
        {
            for (int k=0; k < questCount.Count; k++)
            {
                if (questCount[k].QuestTarget == monsterName)
                {
                    questCount[k].AddQuestCount();
                    questCount[k].QuestCheck();
                }
            }
        }
    }
}
