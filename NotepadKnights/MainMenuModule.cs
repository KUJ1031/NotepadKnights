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
            bool isMagicalGirlGone = false;

            
            while (true)
            {
                Console.WriteLine("[NotePad Knights]에 오신 것을 환영합니다.\n");
                Console.Write("당신의 이름을 입력하세요 : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.ResetColor();
                string name = Console.ReadLine();


                Console.WriteLine($"[{name}] ← 이 이름이 맞나요?\n");
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
                    //Program.playerStatus.SetName(name);
                    break;
                }
                else if (answer == 2)
                {
                    Console.WriteLine("이름을 다시 입력하세요.\n");
                    Console.Clear();
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("당신의 직업을 선택해주세요.");
                Console.WriteLine("1. 전사\n2: 도적");

                if (isMagicalGirlGone)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("3: 마법소녀(집에 감)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("3: 마법소녀");
                }

                int maxOption = isMagicalGirlGone ? 2 : 3;
                int answer = InputManager.ReadInt(1, maxOption);

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
                        Program.playerStatus.InitializePlayer();
                        break;
                    }
                    else
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
                        Program.playerStatus.InitializePlayer();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("직업을 다시 선택하세요.\n");
                    }
                }
                else if (answer == 3 && !isMagicalGirlGone)
                {
                    Console.WriteLine("마법소녀를 선택하셨습니다.\n");
                    Console.WriteLine("마법소녀로 시작합니다.");
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
                        Console.WriteLine("☆마법소녀는 제멋대로야☆ 이제 집에 가야겠어!☆★."); Thread.Sleep(1000);
                        Console.WriteLine("휘리릭 뿅!☆"); Thread.Sleep(1000);
                        Console.WriteLine("..."); Thread.Sleep(1000);
                        Console.WriteLine("마법소녀는 집에 가 버렸습니다..."); Thread.Sleep(1000); 
                        Console.WriteLine("직업을 다시 선택해주세요."); Thread.Sleep(1000); Console.ReadLine();

                        isMagicalGirlGone = true;
                    }
                    else
                    {
                        Console.WriteLine("직업을 다시 선택하세요.\n");
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
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 전투하기\n5. 회복하기\n6. 퀘스트\n");

            int choice = InputManager.ReadInt(1, 6);

            return choice;
        }
    }
}
