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
        public string QuestTarget { get; protected set; } //퀘스트 타겟 몬스터 이름
        public int QuestCount { get; protected set; } //퀘스트 카운트
        public int CurrentCount { get; protected set; } //몬스터 카운트
        public bool QuestClearAble { get; protected set; }


        public void QuestCheck()
        {
            if (CurrentCount >= QuestCount)
            {
                QuestClearAble = true;
                CurrentCount = QuestCount;
            }
        }

        public void AddQuestCount()
        {
            CurrentCount += 1;
        }

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
            if(IsActive)
            {
                Console.WriteLine($"진행도:{CurrentCount} / {QuestCount}");
            }
            if (QuestClearAble)
            {
                Console.WriteLine($"퀘스트 완료 가능!");
            }
        }      
    }


    //퀘스트를 더 추가할 수 있습니다
    public class Quest1 : Quest
    {
        public Quest1()
        {
            QuestName = "전투를 해보자";
            QuestLevel = 1;
            QuestReward = "100";
            QuestTarget = "미니언";
            QuestCount = 3;
            CurrentCount = 0;
            QuestDescription = $"{QuestTarget} {QuestCount}마리 쓰러뜨리기.";
            QuestClearAble = false; 
            IsCompleted = false;
            IsActive = false;
        }
    }

    public class Quest2 : Quest
    {
        public Quest2()
        {
            QuestName = "곤충채집 소년";
            QuestLevel = 2;
            QuestReward = "200";
            QuestTarget = "공허충";
            QuestCount = 3;
            CurrentCount = 0;
            QuestDescription = $"{QuestTarget} {QuestCount}마리 쓰러뜨리기.";
            QuestClearAble = false;
            IsCompleted = false;
            IsActive = false;
        }
    }
    public class Quest3 : Quest
    {
        public Quest3()
        {
            QuestName = "어떻게 부리가...";
            QuestLevel = 3;
            QuestReward = "200";
            QuestTarget = "칼날부리";
            QuestCount = 2;
            CurrentCount = 0;
            QuestDescription = $"{QuestTarget} {QuestCount}마리 쓰러뜨리기.";
            QuestClearAble = false;
            IsCompleted = false;
            IsActive = false;
        }
    }

    public class Quest4 : Quest
    {
        public Quest4()
        {
            QuestName = "돈을 벌어야해";
            QuestLevel = 4;
            QuestReward = "200";
            QuestTarget = "대포미니언";
            QuestCount = 2;
            CurrentCount = 0;
            QuestDescription = $"{QuestTarget} {QuestCount}마리 쓰러뜨리기.";
            QuestClearAble = false;
            IsCompleted = false;
            IsActive = false;
        }
    }
    public class Quest5 : Quest
    {
        public Quest5()
        {
            QuestName = "빨간망토 이야기";
            QuestLevel = 5;
            QuestReward = "200";
            QuestTarget = "어스름 늑대";
            QuestCount = 2;
            CurrentCount = 0;
            QuestDescription = $"{QuestTarget} {QuestCount}마리 쓰러뜨리기.";
            QuestClearAble = false;
            IsCompleted = false;
            IsActive = false;
        }
    }
}
