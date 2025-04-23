using NotepadKnights.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    public class AttackAndDefense
    {
        // 치명타, 회피 확률 변수
        float criticalProbability = 0.15f;
        float dodgeProbability = 0.1f;

        // 치명타, 회피 발생 여부 확인 변수
        public bool onCritical = false;
        public bool onDodge = false;

        private Random random = new Random();

        public int Attack(int Damage)
        {
            CalcCritical();
            if (onCritical)
            {
                // 여기 이렇게 수정해도 될까요? Player에게 메세지를 전달해주는 식으로 수정하지 않으면 보이지 않더라구요...
                Program.player.msg = "치명적인 데미지!\n";
                //Console.WriteLine("치명적인 데미지!");
                Damage *= (int)1.6f;
            }
            return Damage;
        }
        public int PlayerDefense(int EnemyAttack, int PlayerHp, int PlayerDefense)
        {
            CalcDodge();
            if (onDodge)
            {
                Program.player.msg = ("그러나 아무 일도 일어나지 않았다.\n");
               // Console.WriteLine("그러나 아무 일도 일어나지 않았다.");
            }
            else
            {
                Program.player.msg = "";
                int finalDamage = EnemyAttack - PlayerDefense;
                if (finalDamage < 0) finalDamage = 0;
                PlayerHp -= finalDamage;
            }
            return PlayerHp;
        }

        public int EnemyDefense(int PlayerAttack, int EnemyHp)
        {
            CalcDodge();
            if (onDodge)
            {
                Program.player.msg = ("그러나 아무 일도 일어나지 않았다.\n");
              //  Console.WriteLine("그러나 아무 일도 일어나지 않았다.");
            }
            else
            {
                Program.player.msg = "";
                EnemyHp -= PlayerAttack;
            }
            return EnemyHp;
        }

        public void CalcCritical()
        {
            float value = (float)random.NextDouble();
            onCritical = value < criticalProbability;
        }

        public void CalcDodge()
        {
            float value = (float)random.NextDouble();
            onDodge = value < dodgeProbability;
        }

    }
}
