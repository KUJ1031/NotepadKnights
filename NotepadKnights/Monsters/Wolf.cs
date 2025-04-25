namespace NotepadKnights.Monsters
{
    public class Wolf : Monster
    {
        public Wolf()
        {
            Level = 6;
            Name = "어스름 늑대";
            MaxHp = 30;
            CurrentHp = MaxHp;
            Atk = 12;
        }
    }
}
