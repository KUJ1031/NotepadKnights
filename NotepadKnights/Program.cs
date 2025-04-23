namespace NotepadKnights
{
    //18조 TextRPG [Notepad Knights] 협업 공간입니다.
    internal class Program
    {
        public static Player Player = new Player();
        public static MainMenuModule mainMenu = new MainMenuModule(Player);

        public interface IModule
        {

        }

        public interface IInputService
        {

        }

        public interface IRenderer
        {

        }
  
        static void Main(string[] args)
        {

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
