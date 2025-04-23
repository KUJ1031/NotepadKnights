using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class QuestFrame
    {
        public string QuestName;
        public string QuestDescription;
        public int QuestLevel;
        public int QuestReward;
        public bool IsActive;
        public bool IsCompleted;
        
        public void ShowQuest()
        {
            Console.WriteLine($"퀘스트 이름: {QuestName}");
            Console.WriteLine($"퀘스트 설명: {QuestDescription}");
            Console.WriteLine($"퀘스트 레벨: {QuestLevel}");
            Console.WriteLine($"퀘스트 보상: {QuestReward}");
            Console.WriteLine($"퀘스트 완료 여부: {(IsCompleted ? "완료" : "미완료")}");
        }
        public void CompleteQuest()
        {
            IsActive = false;
            IsCompleted = true;
            Console.WriteLine($"퀘스트 '{QuestName}'이(가) 완료되었습니다!");
        }
    }
    public class Quest1 : QuestFrame
    {
        public Quest1()
        {
            QuestName = "첫 번째 퀘스트";
            QuestDescription = "첫 번째 퀘스트를 완료하세요.";
            QuestLevel = 1;
            QuestReward = 100;
            IsCompleted = false;
        }
    }
}
