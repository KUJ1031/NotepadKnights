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
       
        public void CompleteQuest(int k)
        {
            //퀘스트의 active = false, completed = true로 변경
            ableQuestList[k].CompleteQuest();
            Console.WriteLine($"퀘스트 '{ableQuestList[k].QuestName}'이(가) 완료되었습니다!");
        }

        public void QuestCount()
        {
            //퀘스트 진행사항 어케하죠?
        }
    }
}
