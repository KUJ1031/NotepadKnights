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
        private bool isSelected = false;
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        public bool IsSelected { get { return isSelected; } set { isSelected = value; } }   // 배틀시 플레이어의 공격 대상이 됐는지를 체크

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
