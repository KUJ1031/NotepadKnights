namespace NotepadKnights
{
    //18조 TextRPG [Notepad Knights] 협업 공간입니다.
    internal class Program
    {

        public static PlayerUI playerUI;
       // public static Player player;
        public static PlayerStatus playerStatus;
        public static BattleManager battleManager = new BattleManager();

        public static Player player = new Player();
        public static MainMenuModule mainMenu = new MainMenuModule(player);


        public interface IModule
        {

        }

        public interface IInputService
        {

        }

        public interface IRenderer
        {

        }


        public static void InLobby()
        {
            Console.Clear();
            Console.WriteLine("로비에 입장하였습니다. [Notepad Knights]에 오신 것을 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("(1) 현재 스테이터스 확인");
            Console.WriteLine("(2) 전투 시작");
            Console.WriteLine("");
            Console.Write("원하시는 행동을 선택해주세요. : ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    //상태 보기 로직
                    Console.WriteLine("상태 보기 로직");
                    break;

                case 2:
                    //전투 로직
                    Console.WriteLine("전투 로직");
                    // Console.Clear();

                    playerStatus.InitializePlayer(); 
                    playerUI.ShowBattleMenu();           
                    break;

                case 3:
                    //도전 기능 추가 시
                    break;

                case 4:
                    //도전 기능 추가 시
                    break;

                case 5:
                    //도전 기능 추가 시
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요."); Console.ReadLine();
                    InLobby();
                    break;
            }
        }
        
        static void Main(string[] args)
        {
            player = new Player();
            playerStatus = new PlayerStatus();
            playerUI = new PlayerUI();
            
            mainMenu.Intro();
            while (true)
            {
                switch(mainMenu.Village())
                {
                    case 1:
                        //상태보기
                        break;
                    case 2:
                        //인벤토리
                        break;
                    case 3:
                        //상점
                        break;
                    case 4:
                        //전투
                        battleManager.EnterBattle();
                        playerStatus.InitializePlayer();
                        playerUI.ShowBattleMenu();
                        break;
                    case 5:
                        //회복하기
                        break;
                    case 6:
                        //추가사항
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        break;
                }
            }
        }
    }
}
