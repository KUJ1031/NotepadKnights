namespace NotepadKnights
{
    internal class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Atk { get; set; }
        private bool isDead = false;
        public bool IsDead { get { return isDead; } set { isDead = value; } }


        public int ApplyDamage(int damage)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                IsDead = true;
            }
            return CurrentHp;
        }

        public int DealDamage()
        { 
            if (!IsDead)
            {
                return Atk;
            }
            else
            {
                return 0;
            }
        }
    }
}
