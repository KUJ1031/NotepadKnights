namespace NotepadKnights.Monsters
{
    public class Minion : Monster
    {
        public Minion()
        {
            Level = 2;
            Name = "미니언";
            MaxHp = 15;
            CurrentHp = MaxHp;
            Atk = 5;
        }
    }
}
