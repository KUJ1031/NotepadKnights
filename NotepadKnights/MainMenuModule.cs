using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotepadKnights
{
    public class MainMenuModule
    {
        Player player;
        public MainMenuModule(Player playerInstance)
        {
            player = playerInstance;
        }
        public void Intro()
        {
            Console.WriteLine("게임을 시작합니다.\n");
            Console.WriteLine("캐릭터를 생성합니다.\n");
            while (true)
            {
                Console.WriteLine("이름을 입력하세요: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(">>");
                Console.ResetColor();
                string name = Console.ReadLine() ?? "없음";


                Console.WriteLine("이름: " + name);
                Console.WriteLine("이름이 맞습니까?");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("1. 맞습니다 ");
                Console.ResetColor();
                Console.Write("/");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" 2. 다시 입력합니다");
                Console.ResetColor();

                int answer = InputManager.ReadInt(1, 2);

                if (answer == 1)
                {
                    Console.WriteLine("\n");
                    Program.playerStatus.SetName(name);
                    break;
                }
                else if (answer == 2)
                {
                    Console.WriteLine("이름을 다시 입력하세요.\n");
                }
            }

            while (true)
            {
                Console.WriteLine("직업을 선택해주세요");
                Console.WriteLine("1. 전사  /  2: 도적");
                Console.Write("직업을 선택하세요: ");
                int answer = InputManager.ReadInt(1, 2);
                if (answer == 1)
                {
                    Console.WriteLine("전사를 선택하셨습니다.\n");
                    Console.WriteLine("전사로 시작합니다.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("1. Yes ");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 2. No");
                    Console.ResetColor();

                    answer = InputManager.ReadInt(1, 2);

                    if (answer == 1)
                    {
                        Console.WriteLine("전사로 시작합니다.");
                        // 잠깐 테스트를 위해 변경했어요.
                        // Program.player.Job = "전사";
                        Program.playerStatus.SetJob("전사");
                        Program.playerStatus.InitializePlayer();
                        break;
                    }
                    else if (answer == 2)
                    {
                        Console.WriteLine("직업을 다시 선택하세요.\n");
                    }
                }
                else if (answer == 2)
                {
                    Console.WriteLine("도적을 선택하셨습니다.\n");
                    Console.WriteLine("도적으로 시작합니다.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("1. Yes ");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 2. No");
                    Console.ResetColor();

                    answer = InputManager.ReadInt(1, 2);

                    if (answer == 1)
                    {
                        Console.WriteLine("도적으로 시작합니다.");
                        // 잠깐 테스트를 위해 변경했어요.
                        // player.Job = "도적";
                        Program.playerStatus.SetJob("도적");
                        Program.playerStatus.InitializePlayer();
                        break;
                    }
                    else if (answer == 2)
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
        public int Village()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.ResetColor();

            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("1. 상태 보기 / 2. 인벤토리 / 3. 상점 / 4. 전투하기 / 5. 회복하기 / 6. 퀘스트\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int choice = InputManager.ReadInt(1, 6);

            return choice;
        }
    }
}
