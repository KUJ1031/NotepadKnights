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
        public string skillName { get; private set; }
        public int skillPower { get; private set; }
        public int skillMP { get; private set; }
        public string description { get; private set; }

        // 생성자
        public Skill(string name, int power, int mp, string description)
        {
            this.skillName = name;
            this.skillPower = power;
            this.skillMP = mp;
            this.description = description;
        }

        // 정적 스킬 목록
        public static readonly Skill skillWarrior01 = new Skill("대검 베기", 15, 10, "대검으로 하나의 적에게 피해를 입힙니다.");
        public static readonly Skill skillWarrior02 = new Skill("대검 휘두르기", 9, 15, "대검을 크게 휘둘러 랜덤으로 2명의 적을 공격합니다.");
        public static readonly Skill skillWarrior03 = new Skill("바람의 상처", 22, 20, "하나의 적에게 큰 피해를 입힙니다.");
        public static readonly Skill skillWarrior04 = new Skill("갈(喝)", 32, 35, "MP를 크게 소모해 하나의 적에게 엄청난 피해를 입힙니다.");

        // 외부에서 접근 가능한 전사 스킬 리스트
        public static readonly List<Skill> WarriorSkills = new List<Skill>
        {
            skillWarrior01,
            skillWarrior02,
            skillWarrior03,
            skillWarrior04
        };


        // ↓(사용 예시) 타 클래스에서 사용 시 아래와 같이 Skill 객체 생성하고 사용하시면 됩니다! 
        // Skill skill = Skill.skillWarrior01;
        // Console.WriteLine($"{skill.skillName} (Power: {skill.skillPower}, MP: {skill.skillMP})");
        // Console.WriteLine($"설명: {skill.description}\n");


        /* ↓(사용 예시) 모든 스킬들을 나오게 하고 싶을 때
           foreach (Skill skill in Skill.WarriorSkills)
             {
                 Console.WriteLine($"{skill.skillName} (Power: {skill.skillPower}, MP: {skill.skillMP})");
                 Console.WriteLine($"설명: {skill.description}\n");
             }
        */
    }
}