using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class MainMenuModule
    {
        FixStatus fixStatus;
        public MainMenuModule(FixStatus fixStatus)
        {
            this.fixStatus = fixStatus;
        }
        public void Intro()
        {
            string? answer;
            Console.WriteLine("게임을 시작합니다.\n");
            Console.WriteLine("캐릭터를 생성합니다.\n");
            while (true)
            {
                Console.WriteLine("이름을 입력하세요: ");
                string name = Console.ReadLine() ?? "없음";

                Console.WriteLine("이름: " + name);
                Console.WriteLine("이름이 맞습니까?");
                Console.WriteLine("1. 맞습니다 / 2. 다시 입력합니다");

                answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.WriteLine("\n");
                    fixStatus.SetName(name);
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
            while (true)
            {
                Console.WriteLine("직업을 선택해주세요");
                Console.WriteLine("1. 전사  /  2: 도적");
                Console.Write("직업을 선택하세요: ");
                string? job = Console.ReadLine();
                if (job == "1")
                {
                    Console.WriteLine("전사를 선택하셨습니다.\n");
                    Console.WriteLine("전사로 시작합니다.");
                    Console.WriteLine("1. Yes / 2. No");
                    answer = Console.ReadLine();
                    if (answer == "1")
                    {
                        Console.WriteLine("전사로 시작합니다.");
                        fixStatus.SetJob("전사");
                        break;
                    }
                    else if (answer == "2")
                    {
                        Console.WriteLine("직업을 다시 선택하세요.\n");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    }
                }
                else if (job == "2")
                {
                    Console.WriteLine("도적을 선택하셨습니다.\n");
                    Console.WriteLine("도적으로 시작합니다.");
                    Console.WriteLine("1. Yes / 2. No");
                    answer = Console.ReadLine();
                    if (answer == "1")
                    {
                        Console.WriteLine("도적으로 시작합니다.");
                        fixStatus.SetJob("도적");
                        break;
                    }
                    else if (answer == "2")
                    {
                        Console.WriteLine("직업을 다시 선택하세요.\n");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.\n");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.\n");
                }
            }
        }
        public int Villiage(int value)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("1. 상태 보기 / 2. 인벤토리 / 3. 상점 / 4. 전투하기 / 5. 회복하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            bool isInt = int.TryParse(Console.ReadLine(),out value);
            if (!isInt)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                return Villiage(value);
            }
            return value;
        }
    }
}
