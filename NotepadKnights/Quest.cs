using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    internal class Quest
    {
        public string QuestName { get; private set; }
        public string QuestDescription { get; private set; }
        public int QuestLevel { get; private set; }
        public int QuestReward { get; private set; }
        public bool IsCompleted { get; private set; }
        public Quest(string questName, string questDescription, int questLevel, int questReward)
        {
            QuestName = questName;
            QuestDescription = questDescription;
            QuestLevel = questLevel;
            QuestReward = questReward;
            IsCompleted = false;
        }
        public void CompleteQuest()
        {
            IsCompleted = true;
            Console.WriteLine($"퀘스트 '{QuestName}'이(가) 완료되었습니다!");
        }
    }
}
