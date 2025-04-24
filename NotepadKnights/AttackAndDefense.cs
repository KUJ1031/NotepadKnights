using NotepadKnights.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NotepadKnights
{
    public class AttackAndDefense
    {
        // 치명타, 회피 확률 변수
        float criticalProbability = 1f;
        float dodgeProbability = 1f;

        // 치명타, 회피 발생 여부 확인 변수
        public bool onCritical = false;
        public bool onDodge = false;

        private Random random = new Random();

        public float Attack(float baseDamage)
        {
            float finalDamage = baseDamage;

            CalcCritical();
            if (onCritical)
            {
                finalDamage *= 1.6f;
                finalDamage = MathF.Ceiling(finalDamage);
                Console.WriteLine("Critical!\n"); Thread.Sleep(1000);
                onCritical = false;
            }
            return finalDamage;
        }
        public int PlayerDefense(int EnemyAttack, int PlayerHp, int PlayerDefense)
        {
            CalcDodge();
            if (onDodge)
            {
                Program.player.msg = ($"그러나 {Program.playerStatus.Name}이(가) 공격을 회피하였다!\n");
                onDodge = false;
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

        public float EnemyDefense(float PlayerAttack, float EnemyHp)
        {
            CalcDodge();
            if (onDodge)
            {
                Program.player.msg = ("그러나 적이 공격을 회피하였다!\n");
            }
            else
            { 

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
