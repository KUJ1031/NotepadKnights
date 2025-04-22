namespace NotepadKnights
{
    internal class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int MaxHp { get; private set; }
        public int CurrentHp { get; private set; }
        public int Atk { get; private set; }
        public bool IsDead { get; private set; } = false;
        public bool IsSelected { get; private set; } = false; // 배틀시 플레이어의 공격 대상이 됐는지를 체크

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
