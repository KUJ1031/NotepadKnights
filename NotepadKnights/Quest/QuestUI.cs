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
        PlayerStatus playerStatus;
        public QuestUI(PlayerStatus playerStatus)
        {
            this.playerStatus = playerStatus;
        }

        public void QuestWindow()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quest!!\n");
                Renew();
                Console.WriteLine("1. 수주 가능 퀘스트");
                Console.WriteLine("2. 진행중인 퀘스트");
                Console.WriteLine("3. 완료한 퀘스트");
                Console.WriteLine("\n\n0. 나가기\n");


                int answer = InputManager.ReadInt(0, 3);

                if (answer == 1)
                {
                    ShowAbleQuest();
                }
                else if (answer == 2)
                {
                    ShowActiveQuest();
                }
                else if (answer == 3)
                {
                    ShowCompletedQuest();
                }
                else
                    break;
            }
        }
        public void Renew()
        {
            questManager.QuestRenew(playerStatus.Level);
        }


        public void ShowActiveQuest()
        {
            //현재 수주중인 퀘스트만 출력

            while(true)
            {
                Console.Clear();

                Console.WriteLine("진행중인 퀘스트 목록:");
                if (questManager.activeQuestList.Count == 0)
                {
                    Console.WriteLine("현재 진행중인 퀘스트가 없습니다");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    for (int i = 0; i < questManager.activeQuestList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {questManager.activeQuestList[i].QuestName}");
                    }

                    Console.WriteLine("\n번호를 입력하면 퀘스트 정보가 표시됩니다.\n\n0. 나가기");
                    int answer = InputManager.ReadInt(0, questManager.activeQuestList.Count);
                    if (answer == 0)
                        break;
                    if (answer - 1 < questManager.activeQuestList.Count && answer > 0)
                    {
                        //퀘스트 정보 출력
                        Console.Clear();
                        questManager.activeQuestList[answer - 1].ShowQuest();
                        Console.WriteLine("\n1. 퀘스트 완료\n2. 퀘스트 포기\n\n0. 나가기");
                        int answer2 = InputManager.ReadInt(0, 2);
                        if (answer2 == 1)
                        {
                            if (questManager.activeQuestList[answer-1].QuestClearAble)//퀘스트 완료 로직으로 완료 여부 체크
                            {
                                questManager.CompleteQuest(answer - 1);
                            }
                            else
                            {
                                Console.WriteLine("퀘스트를 완료할 수 없습니다.");
                                Console.ReadLine();
                            }
                            break;
                        }
                        else if (answer2 == 2)
                        {
                            //퀘스트포기(active off activelist에서 제거 ablelist에 추가)
                            questManager.CancelQuest(answer - 1);
                            Console.Clear();
                        }
                    }
                }
            }
        }
              


        public void ShowAbleQuest()
        {
            //레벨에 따른 수주 가능한 퀘스트만 출력
            int k = 0;
            //퀘스트 레벨에 따라 정렬
            while (true)
            {
                Console.Clear();
                Console.WriteLine("퀘스트 목록:");
                if (questManager.ableQuestList.Count == 0)
                {
                    Console.WriteLine("현재 가능한 퀘스트가 없습니다");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    for (int i = 0; i < questManager.ableQuestList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {questManager.ableQuestList[i].QuestName}");
                    }
                    Console.WriteLine("\n0. 나가기");

                    Console.WriteLine("\n퀘스트 번호를 입력하세요.\n>>");

                    int answer = InputManager.ReadInt(0, questManager.ableQuestList.Count);

                    if (answer == 0)
                        break;
                    if (answer - 1 < questManager.ableQuestList.Count && answer > 0)
                    {
                        //퀘스트 정보 출력
                        questManager.ableQuestList[answer - 1].ShowQuest();
                        Console.WriteLine("\n1. 퀘스트 수주\n\n\n0. 나가기");
                        int answer2 = InputManager.ReadInt(0, 1);
                        //퀘스트 수주
                        if (answer2 == 1)
                        {
                            //퀘스트 수주
                            questManager.ActiveQuest(answer - 1);
                            break;
                        }
                        else
                            break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
            }
        }


        public void ShowCompletedQuest()
        {
            //완료된 퀘스트만 출력
            Console.Clear();
            Console.WriteLine("완료된 퀘스트 목록:");
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
                    break;
                }
                Console.WriteLine("잘못된 입력입니다.");
                ShowCompletedQuest();
            }
        }
    }
}
