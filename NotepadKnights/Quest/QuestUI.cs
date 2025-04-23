using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class QuestUI
    {
        QuestManager questManager = new QuestManager();


        public void QuestWindow()
        {
            while (true)
            {
                Console.WriteLine("Quest!!\n");

                Console.WriteLine("1. 수주 가능 퀘스트");
                Console.WriteLine("2. 진행중인 퀘스트");
                Console.WriteLine("3. 완료한 퀘스트");

                if (int.TryParse(Console.ReadLine(), out int answer))
                {
                    switch (answer)
                    {
                        case 1:
                            ShowAbleQuest();
                           
                            break;
                        case 2:
                            ShowActiveQuest();
                            break;
                        case 3:
                            ShowCompletedQuest();
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 입력입니다.\n\n");
                            continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.\n\n");
                }
            }
        }
        public void Renew(int characterLevel)
        {
            questManager.QuestRenew(characterLevel);
        }

        public void ShowActiveQuest()
        {
            //현재 수주중인 퀘스트만 출력
            Console.Clear();
            questManager.activeQuestList = questManager.activeQuestList.OrderBy(q => q.QuestLevel).ToList();


            while (true)
            {
                Console.WriteLine("진행중인 퀘스트 목록:");
                for (int i = 0; i < questManager.activeQuestList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {questManager.activeQuestList[i].QuestName}");
                }


                Console.WriteLine("\n번호를 입력하면 퀘스트 정보가 표시됩니다.\n\n0. 나가기");
                if (int.TryParse(Console.ReadLine(), out int answer))
                {
                    if (answer - 1 < questManager.activeQuestList.Count && answer > 0)
                    {
                        //퀘스트 정보 출력
                        questManager.activeQuestList[answer - 1].ShowQuest();

                        //퀘스트 수주
                        Console.WriteLine($"퀘스트 '{questManager.activeQuestList[answer - 1].QuestName}'을(를) 수주하였습니다.");

                        //수주퀘스트 리스트에 추가, 수주가능 퀘스트 리스트에서 삭제
                        questManager.activeQuestList.Add(questManager.ableQuestList[answer - 1]);
                        questManager.ableQuestList.RemoveAt(answer - 1);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ShowCompletedQuest();
                }
            }

        }


        public void ShowAbleQuest()
        {
            //레벨에 따른 수주 가능한 퀘스트만 출력
            int k = 0;
            //퀘스트 레벨에 따라 정렬
            questManager.ableQuestList = questManager.ableQuestList.OrderBy(q => q.QuestLevel).ToList();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("퀘스트 목록:");
                for (int i = 0; i < questManager.ableQuestList.Count; i++)
                {
                    Console.WriteLine($"{k + 1}. {questManager.ableQuestList[i].QuestName}");
                }
                Console.WriteLine("\n0. 나가기");

                Console.WriteLine("\n퀘스트 번호를 입력하세요.\n>>");

                if (int.TryParse(Console.ReadLine() ,out int answer))
                {
                    if (answer == 0)
                        break;
                    if (answer - 1 < questManager.ableQuestList.Count && answer > 0)
                    {
                        //퀘스트 정보 출력
                        questManager.ableQuestList[answer - 1].ShowQuest();

                        //퀘스트 수주
                        Console.WriteLine($"퀘스트 '{questManager.ableQuestList[answer - 1].QuestName}'을(를) 수주하였습니다.");

                        //수주퀘스트 리스트에 추가, 수주가능 퀘스트 리스트에서 삭제
                        questManager.activeQuestList.Add(questManager.ableQuestList[answer - 1]);
                        questManager.ableQuestList.RemoveAt(answer - 1);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }


        public void ShowCompletedQuest()
        {
            //완료된 퀘스트만 출력
            int k = 0;
            Console.WriteLine("완료된 퀘스트 목록:");
            //퀘스트 레벨에 따라 정렬
            questManager.completedQuestList = questManager.completedQuestList.OrderBy(q => q.QuestLevel).ToList();
            //완료된 퀘스트는 회색으로 출력
            if (questManager.completedQuestList.Count == 0)
            {
                Console.WriteLine("완료한 퀘스트가 없습니다.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = 0; i < questManager.completedQuestList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {questManager.completedQuestList[i].QuestName}");
                }
                Console.ResetColor();
            }
            Console.WriteLine("\n0. 나가기");
            while (true)
            {
                string answer = Console.ReadLine();
                if (answer == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ShowCompletedQuest();
                }
            }
        }


        public void ShowQuestDetail(int i)//퀘스트의 번호를 인자로 주면 그 퀘스트 정보를 출력
        {
            Console.WriteLine($"퀘스트 이름: {questManager.allQuestList[i].QuestName}");
            Console.WriteLine($"퀘스트 설명: {questManager.allQuestList[i].QuestDescription}");
            Console.WriteLine($"퀘스트 레벨: {questManager.allQuestList[i].QuestLevel}");
            Console.WriteLine($"퀘스트 보상: {questManager.allQuestList[i].QuestReward}");
        }

    }
}
