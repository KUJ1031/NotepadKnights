using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class QuestManager
    {
        public List<Quest> allQuestList = new List<Quest>();
        public List<Quest> ableQuestList = new List<Quest>();
        public List<Quest> activeQuestList = new List<Quest>();
        public List<Quest> completedQuestList = new List<Quest>();

        public QuestManager()
        {
            // 모든 퀘스트를 가진 리스트 초기화
            allQuestList.Add(new Quest1());
            allQuestList.Add(new Quest2()); 
            allQuestList.Add(new Quest3());
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
                    //플레이어 레벨이 퀘스트 레벨보다 높아지고, 수주 퀘스트 리스트에 없고, 클리어하지 않고, 진행중이 아닌퀘스트 추가
                    if (quest.QuestLevel <= characterLevel 
                        && !ableQuestList.Any(q => q.QuestName == quest.QuestName) 
                        && quest.IsCompleted == false 
                        && quest.IsActive == false)
                    {
                        ableQuestList.Add(quest);
                    }
                }
                //퀘스트를 레벨순으로 정렬
                SortList();
            }
        }
       
        public void CompleteQuest(int k)
        {
            //퀘스트의 completed = true로 변경
            activeQuestList[k].CompleteQuest();
            Console.WriteLine($"퀘스트 '{activeQuestList[k].QuestName}'이(가) 완료되었습니다!");
            //완료된 퀘스트를 completedQuestList에 추가, 활성화된 퀘스트 리스트에서 삭제
            completedQuestList.Add(activeQuestList[k]);
            activeQuestList[k].ToggleActive();
            activeQuestList.RemoveAt(k);
            SortList();
            Console.ReadLine();
        }

        public void CancelQuest(int k)
        {
            //퀘스트의 completed = false로 변경
            Console.WriteLine($"퀘스트 '{ableQuestList[k].QuestName}'이(가) 취소되었습니다!");
            //완료된 퀘스트를 completedQuestList에 추가, 활성화된 퀘스트 리스트에서 삭제
            ableQuestList.Add(activeQuestList[k]);
            activeQuestList[k].ToggleActive();
            activeQuestList.RemoveAt(k);
            SortList();
            Console.ReadLine();

        }

        public void ActiveQuest(int k)
        {
            activeQuestList.Add(ableQuestList[k]);
            ableQuestList[k].ToggleActive();
            ableQuestList.RemoveAt(k);
            
            Console.WriteLine($"퀘스트 '{activeQuestList[activeQuestList.Count - 1].QuestName}'이(가) 수주되었습니다!");
            SortList();
            Console.ReadLine();

        }

        public void QuestCount()
        {
            //퀘스트 진행사항 어케하죠?
        }

        public void SortList()
        {
            //퀘스트 리스트를 레벨순으로 정렬
            ableQuestList = ableQuestList.OrderBy(q => q.QuestLevel).ToList();
            activeQuestList = activeQuestList.OrderBy(q => q.QuestLevel).ToList();
            completedQuestList = completedQuestList.OrderBy(q => q.QuestLevel).ToList();
        }
    }
}
