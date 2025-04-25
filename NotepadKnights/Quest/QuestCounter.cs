namespace NotepadKnights
{
    class QuestCounter
    {
        List<Quest> questCount;    //퀘스트의 최대 카운트

        public QuestCounter(List<Quest> activequests)
        {
            BattleManager.OnMonsterKilled += HandleMonsterKilled;
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
