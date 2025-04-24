namespace NotepadKnights;

public static class InputManager
{
    public static int ReadInt(int start, int end)
    {
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int command) && command >= start && command <= end)
            {
                return command;
            }
            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
        }
    }
}