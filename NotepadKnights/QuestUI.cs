using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class QuestUI
    {
        QuestManager questManager = new QuestManager();

        public void ShowActiveQuest()
        {
            //현재 수주중인 퀘스트만 출력
            Console.WriteLine("진행중인 퀘스트 목록:");
            for (int i = 0; i < questManager.activeQuestList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questManager.activeQuestList[i].QuestName}");
            }
        }


        public void ShowAbleQuest()
        {
            //레벨에 따른 수주 가능한 퀘스트만 출력
            int k = 0;
            Console.WriteLine("퀘스트 목록:");
            for (int i = 0; i < questManager.ableQuestList.Count; i++)
            {
                if (questManager.ableQuestList[i].IsCompleted == false)
                {
                    Console.WriteLine($"{k + 1}. {questManager.ableQuestList[i].QuestName}");
                    k++;
                }
            }
        }


        public void ShowCompletedQuest()
        {
            //완료된 퀘스트만 출력
            int k = 0;
            Console.WriteLine("완료된 퀘스트 목록:");
            //완료된 퀘스트는 회색으로 출력
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < questManager.ableQuestList.Count; i++)
            {

                if (questManager.ableQuestList[i].IsCompleted == true)
                {
                    Console.WriteLine($"{k + 1}. {questManager.ableQuestList[i].QuestName}");
                    k++;
                }
            }
            Console.ResetColor();
        }


        public void ShowQuestDetail(int i)//퀘스트의 번호를 인자로 주면 그 퀘스트 정보를 출력
        {
            Console.WriteLine($"퀘스트 이름: {questManager.allQuestList[i].QuestName}");
            Console.WriteLine($"퀘스트 설명: {questManager.allQuestList[i].QuestDescription}");
            Console.WriteLine($"퀘스트 레벨: {questManager.allQuestList[i].QuestLevel}");
            Console.WriteLine($"퀘스트 보상: {questManager.allQuestList[i].QuestReward}");
            Console.WriteLine($"퀘스트 완료 여부: {(questManager.allQuestList[i].IsCompleted ? "완료" : "미완료")}");
        }

    }
}
