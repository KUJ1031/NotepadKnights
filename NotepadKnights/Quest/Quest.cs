using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class Quest
    {
        public string QuestName { get; protected set; }
        public string QuestDescription {  get; protected set; }
        public int QuestLevel { get; protected set; }
        public string QuestReward { get; protected set; }
        public bool IsActive { get; protected set; }
        public bool IsCompleted { get; protected set; }


        public void ToggleActive()
        {
            IsActive = !IsActive;
        }


        public void CompleteQuest()
        {
            IsCompleted = true;
        }

      
        public void ShowQuest()
        {
            Console.WriteLine($"퀘스트 이름: {QuestName}");
            Console.WriteLine($"퀘스트 설명: {QuestDescription}");
            Console.WriteLine($"퀘스트 레벨: {QuestLevel}");
            Console.WriteLine($"퀘스트 보상: {QuestReward}");
        }      
    }


    //퀘스트를 더 추가할 수 있습니다
    public class Quest1 : Quest
    {
        public Quest1()
        {
            QuestName = "첫 번째 퀘스트";
            QuestDescription = "첫 번째 퀘스트를 완료하세요.";
            QuestLevel = 1;
            QuestReward = "100";
            IsCompleted = false;
            IsActive = false;
        }
    }
}
