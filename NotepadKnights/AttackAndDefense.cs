using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights
{
    internal class AttackAndDefense
    {
        // 치명타, 회피 확률 변수
        float criticalProbability = 0.15f;
        float dodgeProbability = 0.1f;

        // 치명타, 회피 발생 여부 확인 변수
        public bool onCritical = false;
        public bool onDodge = false;

        private Random random = new Random();

        public int Attack(int playerAttack)
        {
            CalcCritical();
            if (onCritical)
            {
                Console.WriteLine("치명적인 데미지!");
                playerAttack *= (int)1.6f;
            }
            return playerAttack;
        }
        public int Defense(int EnemyAttack, int PlayerDefense)
        {
            CalcDodge();
            if (onDodge)
            {
                Console.WriteLine("그러나 아무 일도 일어나지 않았다.");
            }
            else
            {
                PlayerDefense -= EnemyAttack;
            }
            return PlayerDefense;
        }

        public void CalcCritical()
        {
            float value = (float)random.NextDouble(); // 0.0 ~ 1.0
            onCritical = value < criticalProbability;
        }

        public void CalcDodge()
        {
            float value = (float)random.NextDouble();
            onDodge = value < dodgeProbability;
        }
    }
}
