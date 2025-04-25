namespace NotepadKnights
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public float CurrentHp { get; set; }
        public int Atk { get; set; }
        public bool IsDead { get; set; } = false;
        public bool IsSelected { get; private set; } = false; // 배틀시 플레이어의 공격 대상이 됐는지를 체크

        // 몬스터가 공격할 경우
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

