using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotepadKnights;

namespace NotepadKnights
{
    public class Skill
    {
        public string SkillName { get; private set; }
        public int SkillPower { get; private set; }
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
        public static readonly Skill skillWarrior02 = new Skill("대검 휘두르기", 8, 15, "대검을 크게 휘둘러 랜덤으로 2명의 적을 공격합니다.");
        public static readonly Skill skillWarrior03 = new Skill("바람의 상처", 22, 20, "하나의 적에게 큰 피해를 입힙니다.");
        public static readonly Skill skillWarrior04 = new Skill("갈(喝)", 32, 35, "MP를 크게 소모해 하나의 적에게 엄청난 피해를 입힙니다.");

        /*
        public static readonly Skill skillThief01 = new Skill("간결한 베기", 11, 5, "바람처럼 적을 그어 피해를 입힙니다..");
        public static readonly Skill skillThief02 = new Skill("수리검 투척", 6, 11, "단도를 두 번 던져 랜덤으로 2명의 적을 공격합니다.");
        public static readonly Skill skillThief03 = new Skill("목 긋기", 26, 25, "상대의 급소를 노려 큰 피해를 입힙니다.");
        public static readonly Skill skillThief04 = new Skill("은밀한 습격", 42, 50, "MP를 크게 소모해 하나의 적에게 엄청난 피해를 입힙니다.");
        */

        // 외부에서 접근 가능한 전사 스킬 리스트
        public static readonly List<Skill> WarriorSkills = new List<Skill>
        {
            skillWarrior01, skillWarrior02, skillWarrior03, skillWarrior04
        //  skillThief01, skillThief02, skillThief03, skillThief04,

        };

        public static int SkillUse(int Mp)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("사용할 스킬을 선택해주세요.\n");
                for (int i = 0; i < Skill.WarriorSkills.Count; i++)
                {
                    var skill = Skill.WarriorSkills[i];
                    Console.WriteLine( $"{i + 1}. {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})\n");
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

                Mp -= selectedSkill.SkillMP;

                Console.WriteLine($"MP를 {selectedSkill.SkillMP}만큼 소모하여 [{selectedSkill.SkillName}] 스킬을 사용하였습니다.\n"); Thread.Sleep(1000);
                Console.WriteLine($"공격력 {selectedSkill.SkillPower}로 공격합니다.\n"); Thread.Sleep(1000);
                if (selectedSkill.SkillName == "대검 휘두르기")
                {
                    Console.WriteLine($"[{selectedSkill.SkillName}]의 효과 발동! 공격력 {selectedSkill.SkillPower}로 적을 한 번 더 공격합니다.\n"); Thread.Sleep(1000);
                }
                return selectedSkill.SkillPower;
            }
        }

        public static void SkillDescription()
        {
            int i = 1;
            foreach (Skill skill in Skill.WarriorSkills)
            {
                Console.WriteLine($"({i}) {skill.SkillName} (Power: {skill.SkillPower}, MP: {skill.SkillMP})\n");
                Console.WriteLine( $"설명: {skill.Description}\n");
                i++;
            }
        }
    }
}