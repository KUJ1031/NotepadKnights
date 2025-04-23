using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class QuestManager
    {
        List<QuestFrame> allQuestList = new List<QuestFrame>();
        List<QuestFrame> ableQuestList = new List<QuestFrame>();
        List<QuestFrame> activeQuestList = new List<QuestFrame>();

        public QuestManager()
        {
            // 모든 퀘스트를 가진 리스트 초기화
            allQuestList.Add(new Quest1());
            //allQuestList.Add(new Quest2()); 
            //allQuestList.Add(new Quest3());
            //allQuestList.Add(new Quest4());
            //allQuestList.Add(new Quest5());
        }

        public void QuestRenew(int characterLevel)
        {
            {
                //모든 퀘스트를 가진 리스트에서 캐릭터 레벨에 맞는 퀘스트를 ableQuestList에 추가
                //레벨업시 실행시키면서 퀘스트를 갱신
                foreach (var quest in allQuestList)
                {
                    if (quest.QuestLevel <= characterLevel)
                    {
                        ableQuestList.Add(quest);
                    }
                }
            }
        }

        public void ShowActiveQuest()
        {
            //진행중인 퀘스트만 출력
            Console.WriteLine("진행중인 퀘스트 목록:");
            for (int i = 0; i < activeQuestList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {activeQuestList[i].QuestName}");
            }
        }

        public void ShowAbleQuest()
        {
            //레벨에 따른 가능한 퀘스트만 출력
            int k = 0;
            Console.WriteLine("퀘스트 목록:");
            for (int i = 0; i < ableQuestList.Count; i++)
            {
                if (ableQuestList[i].IsCompleted == false)
                {
                    Console.WriteLine($"{k + 1}. {ableQuestList[i].QuestName}");
                    k++;
                }
            }
        }


        public void ShowCompletedQuest()
        {
            //완료된 퀘스트만 출력
            int k = 0;
            Console.WriteLine("완료된 퀘스트 목록:");
            for (int i = 0; i < ableQuestList.Count; i++)
            {
                
                if (ableQuestList[i].IsCompleted == true)
                {
                    Console.WriteLine($"{k + 1}. {ableQuestList[i].QuestName}");
                    k++;
                }
            }
        }


        //            Console.ForegroundColor = ConsoleColor.Gray;
        //            Console.WriteLine($"{i + 1}. {ableQuestList[i].QuestName} (완료)");
        //            Console.ResetColor();
        public void ShowQuestDetail(int i)
        {
            Console.WriteLine($"퀘스트 이름: {allQuestList[i].QuestName}");
            Console.WriteLine($"퀘스트 설명: {allQuestList[i].QuestDescription}");
            Console.WriteLine($"퀘스트 레벨: {allQuestList[i].QuestLevel}");
            Console.WriteLine($"퀘스트 보상: {allQuestList[i].QuestReward}");
            Console.WriteLine($"퀘스트 완료 여부: {(allQuestList[i].IsCompleted ? "완료" : "미완료")}");
        }
        public void CompleteQuest(int k)
        {
            ableQuestList[k].IsActive = false;
            ableQuestList[k].IsCompleted = true;
            Console.WriteLine($"퀘스트 '{ableQuestList[k].QuestName}'이(가) 완료되었습니다!");
        }

        public void QuestCount()
        {
            //퀘스트 진행사항 어케하죠?
        }
    }
}
