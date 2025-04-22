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

        //  생성자 추가
        public Monster()
        {
            Name = "";
            MaxHp = 10;
            CurrentHp = MaxHp;
        }
        //  생성자 추가
        public Monster(string name, int level, int maxHp)
        {
            Name = name;
            Level = level;
            MaxHp = maxHp;
            CurrentHp = maxHp; // 기본값 세팅
            Atk = level * 3;   // 예시 공격력 로직
        }
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
