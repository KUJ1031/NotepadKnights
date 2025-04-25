namespace NotepadKnights;

public static class InputManager
{
    public static int ReadInt(int start, int end)
    {
        Console.WriteLine("\n���Ͻô� �ൿ�� �Է����ּ���.");
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int command) && command >= start && command <= end)
            {
                return command;
            }
            Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �Է����ּ���.");
        }
    }
}