using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class CallMainScene
    {
        FixStatus fixStatus;
        public CallMainScene(FixStatus fixStatus)
        {
            this.fixStatus = fixStatus;
        }
        public void Start()
        {
            Console.WriteLine("게임을 시작합니다.\n");
            Console.WriteLine("캐릭터를 생성합니다.\n");
            while (true)
            {
                Console.WriteLine("이름을 입력하세요: ");
                string name = Console.ReadLine();
                
                Console.WriteLine("이름: " + name);
                Console.WriteLine("이름이 맞습니까?");
                Console.WriteLine("1. 맞습니다 / 2. 다시 입력합니다");
                
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.WriteLine("\n");
                    break;
                }
                else if (answer == "2")
                {
                    Console.WriteLine("이름을 다시 입력하세요.\n");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.\n");
                }
            }
        }
    }
}
