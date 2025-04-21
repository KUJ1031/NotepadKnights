namespace NotepadKnights
{
    internal class Monster
    {
        public string name { get; set; }
        public int level { get; set; }
        public int maxHp { get; set; }
        public int currentHp { get; set; }
        public bool isDead { get; set; }


        public int ApplyDamage(int damage)
        {
            return damage;
        }

        public int DealDamage()
        { 
            return 0;
        }
    }
}
