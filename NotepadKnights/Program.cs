namespace NotepadKnights
{
    //18조 TextRPG [Notepad Knights] 협업 공간입니다.
    internal class Program
    {

        public static PlayerUI playerUI;
       // public static Player player;
        public static PlayerStatus playerStatus;
        public static MonsterFactory monsterFactory;

        public static Player player = new Player();
        public static MainMenuModule mainMenu = new MainMenuModule(player);
        public static Healing healing = new Healing();
        public static QuestUI quest;
        
        static void Main(string[] args)
        {
                player = new Player();
                playerStatus = new PlayerStatus();
                playerUI = new PlayerUI();
                monsterFactory = new MonsterFactory();
                quest = new QuestUI(playerStatus);
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
                        playerStatus.InitializePlayer();
                        playerUI.ShowBattleMenu();
                        break;
                    case 5:
                        healing.IntoHealing();
                        //회복하기
                        break;
                    case 6:
                        quest.QuestWindow();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        break;
                }
            }

        }
    }
}
