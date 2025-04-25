namespace NotepadKnights
{
    //18조 TextRPG [Notepad Knights] 협업 공간입니다.
    internal class Program
    {
        public static PlayerStatus playerStatus = new PlayerStatus();
        public static Player player = new Player();
        public static MainMenuModule mainMenu = new MainMenuModule(player);
        public static QuestUI quest;
        public static Healing healing = new Healing();

        public static BattleManager battleManager = new BattleManager();
        public static InventoryManager InventoryManager = new InventoryManager();
        public static StoreManager StoreManager = new StoreManager();
        
        static void Main(string[] args)
        {
                quest = new QuestUI(playerStatus);
                mainMenu.Intro();
            while (true)
            {
                switch(mainMenu.Village())
                {
                    case 1:
                        //스테이터스
                        Console.Clear();
                        playerStatus.ShowStatus();
                        break;
                    case 2:
                        //인벤토리
                        InventoryManager.Run();
                        break;
                    case 3:
                        //상점
                        StoreManager.Run();
                        break;
                    case 4:
                        //전투
                        battleManager.Run();
                        break;
                    case 5:
                        //회복하기
                        healing.IntoHealing();
                        break;
                    case 6:
                        //퀘스트
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
