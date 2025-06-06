﻿namespace NotepadKnights
{
    public class Skill
    {
        public string SkillName { get; private set; }
        public float SkillPower { get; private set; }
        public float BaseSkillPower { get; set; } // 원본 유지용
        public int SkillMP { get; private set; }
        public string Description { get; private set; }

        // 생성자
        public Skill(string name, int power, int mp, string description)
        {
            this.SkillName = name;
            this.SkillPower = power;
            this.SkillMP = mp;
            this.Description = description;
        }

        // 정적 스킬 목록
        public static readonly Skill skillWarrior01 = new Skill("대검 베기", 15, 10, "대검으로 하나의 적에게 피해를 입힙니다.");
        public static readonly Skill skillWarrior02 = new Skill("대검 휘두르기", 8, 15, "대검을 크게 휘둘러 2번 연속으로 적을 공격합니다.");
        public static readonly Skill skillWarrior03 = new Skill("바람의 상처", 22, 20, "하나의 적에게 큰 피해를 입힙니다.");
        public static readonly Skill skillWarrior04 = new Skill("갈(喝)", 32, 35, "MP를 크게 소모해 하나의 적에게 엄청난 피해를 입힙니다.");

        public static readonly Skill skillThief01 = new Skill("간결한 베기", 11, 5, "바람처럼 적을 그어 피해를 입힙니다.");
        public static readonly Skill skillThief02 = new Skill("수리검 투척", 6, 11, "단도를 두 번 던져 2번 연속으로 공격합니다.");
        public static readonly Skill skillThief03 = new Skill("목 긋기", 26, 25, "상대의 급소를 노려 큰 피해를 입힙니다.");
        public static readonly Skill skillThief04 = new Skill("그림자 습격", 42, 50, "음속의 속도로 돌진하여 하나의 적에게 막대한 피해를 입힙니다.");

        // 외부에서 접근 가능한 전사 스킬 리스트
        public static readonly List<Skill> WarriorSkills = new List<Skill> { skillWarrior01, skillWarrior02, skillWarrior03, skillWarrior04 };
        public static readonly List<Skill> ThiefSkills = new List<Skill> { skillThief01, skillThief02, skillThief03, skillThief04 };

        public Skill(float power)
        {
            SkillPower = power;
            BaseSkillPower = power;
        }

        public static float SkillUse(int Mp)
        {
            while (true)
            {
                Console.Clear(); Console.WriteLine("사용할 스킬을 선택해주세요.\n");
                if (Program.playerStatus.Job == "전사")
                {
                    for (int i = 0; i < Skill.WarriorSkills.Count; i++)
                    {
                        var skill = Skill.WarriorSkills[i];
                        Console.WriteLine($"{i + 1}. {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})\n");
                    }
                    Console.Write(">>");
                    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > Skill.WarriorSkills.Count)
                    {
                        Console.WriteLine("잘못된 선택입니다.\n"); Thread.Sleep(1000);
                        continue;
                    }

                    var selectedSkill = Skill.WarriorSkills[choice - 1];

                    if (Mp < selectedSkill.SkillMP)
                    {
                        Console.WriteLine($"MP가 부족하여 사용할 수 없습니다. 현재 MP : {Mp}\n"); Thread.Sleep(1000);
                        continue;
                    }
                    if (selectedSkill.SkillName == "갈(喝)")
                    {
                        Console.WriteLine("바람이 울부짖고 있습니다...\n"); Thread.Sleep(1500);
                    }

                    Program.playerStatus.Mp -= selectedSkill.SkillMP;

                    Console.WriteLine($"MP를 {selectedSkill.SkillMP}만큼 소모하여 [{selectedSkill.SkillName}] 스킬을 사용하였습니다.\n"); Thread.Sleep(1000);

                    // 공격 오차 범위 계산
                    selectedSkill.SkillPower = GenerateRandomAttackPower(selectedSkill.SkillPower);

                    // 치명타 피해 추가
                    float randomPower = GenerateRandomAttackPower(selectedSkill.SkillPower);

                    AttackAndDefense attackAndDefense = new AttackAndDefense();
                    float finalDamage = attackAndDefense.Attack(randomPower);

                    Console.WriteLine($"{selectedSkill.SkillName}의 데미지 : [{finalDamage}]\n"); Thread.Sleep(1000);

                    if (selectedSkill.SkillName == "대검 휘두르기")
                    {
                        Console.WriteLine($"[{selectedSkill.SkillName}]의 효과 발동! 추가 공격을 가합니다.\n"); Thread.Sleep(1000);

                        float bonusDamage = attackAndDefense.Attack(GenerateRandomAttackPower(selectedSkill.SkillPower));
                        Console.WriteLine($"추가 공격 데미지 : [{bonusDamage}]\n"); Thread.Sleep(1000);

                        finalDamage += bonusDamage;
                    }

                    return finalDamage;
                }
                else if (Program.playerStatus.Job == "도적")
                {
                    for (int i = 0; i < Skill.ThiefSkills.Count; i++)
                    {
                        var skill = Skill.ThiefSkills[i];
                        Console.WriteLine($"{i + 1}. {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})\n");
                    }
                    Console.Write(">>");
                    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > Skill.ThiefSkills.Count)
                    {
                        Console.WriteLine("잘못된 선택입니다.\n"); Thread.Sleep(1000);
                        continue;
                    }

                    var selectedSkill = Skill.ThiefSkills[choice - 1];

                    if (Mp < selectedSkill.SkillMP)
                    {
                        Console.WriteLine($"MP가 부족하여 사용할 수 없습니다. 현재 MP : {Mp}\n"); Thread.Sleep(1000);
                        continue;
                    }
                    if (selectedSkill.SkillName == "그림자 습격")
                    {
                        Console.WriteLine("공기의 흐름이 달라지고 있습니다...\n"); Thread.Sleep(1500);
                    }

                    Program.playerStatus.Mp -= selectedSkill.SkillMP;

                    Console.WriteLine($"MP를 {selectedSkill.SkillMP}만큼 소모하여 [{selectedSkill.SkillName}] 스킬을 사용하였습니다.\n"); Thread.Sleep(1000);

                    // 공격 오차 범위 계산
                    selectedSkill.SkillPower = GenerateRandomAttackPower(selectedSkill.SkillPower);

                    // 치명타 피해 추가
                    float randomPower = GenerateRandomAttackPower(selectedSkill.SkillPower);

                    AttackAndDefense attackAndDefense = new AttackAndDefense();
                    float finalDamage = attackAndDefense.Attack(randomPower);

                    Console.WriteLine($"{selectedSkill.SkillName}의 데미지 : [{finalDamage}]\n"); Thread.Sleep(1000);

                    if (selectedSkill.SkillName == "수리검 투척")
                    {
                        Console.WriteLine($"[{selectedSkill.SkillName}]의 효과 발동! 추가 공격을 가합니다.\n"); Thread.Sleep(1000);

                        float bonusDamage = attackAndDefense.Attack(GenerateRandomAttackPower(selectedSkill.SkillPower));
                        Console.WriteLine($"추가 공격 데미지 : [{bonusDamage}]\n"); Thread.Sleep(1000);

                        finalDamage += bonusDamage;
                    }

                    return finalDamage;
                }
            }
        }

        public static void SkillDescription()
        {
            int i = 1;
            Console.WriteLine($"[{Program.playerStatus.Job} 스킬 정보]");
            if (Program.playerStatus.Job == "전사")
            {
                foreach (Skill skill in Skill.WarriorSkills)
                {
                    Console.WriteLine($"({i}) {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})");
                    Console.WriteLine($"설명: {skill.Description}\n");
                    i++;
                }
            }
            else if (Program.playerStatus.Job == "도적")
            {
                foreach (Skill skill in Skill.ThiefSkills)
                {
                    Console.WriteLine($"({i}) {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})");
                    Console.WriteLine($"설명: {skill.Description}\n");
                    i++;
                }
            }
        }

        // 공격력의 범위를 조정하고, 랜덤 범위 내에서 공격력을 설정
        public static float GenerateRandomAttackPower(float attackPower)
        {

            float error = MathF.Ceiling(attackPower * 0.1f);
            Random random = new Random();
            int min = Math.Max(0, (int)(attackPower - error));
            int max = Math.Max(min + 1, (int)(attackPower + error));
            attackPower = random.Next(min, max);

            return attackPower;
        }
    }
}