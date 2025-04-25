namespace NotepadKnights.Monsters
{
    public class Raptor : Monster
    {
        public Raptor()
        {
            Level = 4;
            Name = "칼날부리";
            MaxHp = 15;
            CurrentHp = MaxHp;
            Atk = 10;
        }
    }
}
