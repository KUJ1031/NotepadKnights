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
        float criticalProbability = 0.15f;
        float dodgeProbability = 0.1f;

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Critical 발동!"); Console.ResetColor(); Thread.Sleep(1000);
                Console.WriteLine("이번 공격의 데미지가 1.6배 상승합니다.\n"); Thread.Sleep(1000);
                onCritical = false;
            }
            return finalDamage;
        }
        public int PlayerDefense(int EnemyAttack, int PlayerHp, int PlayerDefense)
        {
            CalcDodge();
            if (onDodge)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"그러나 {Program.playerStatus.Name}이(가) 공격을 회피하였다!\n"); Console.ResetColor(); Thread.Sleep(1000);
                onDodge = false;
            }
            else
            {
                Program.player.msg = "";
                int finalDamage = EnemyAttack - PlayerDefense;
                if (finalDamage < 0) finalDamage = 0;
                PlayerHp -= finalDamage;
                if (PlayerHp < 0) PlayerHp = 0;
                Console.WriteLine($"\n{Program.playerStatus.Name}에게 {EnemyAttack}만큼의 공격을 가했다!"); Thread.Sleep(1000);
                Console.WriteLine($"그러나 {Program.playerStatus.Name}의 방어력 {PlayerDefense}만큼의 데미지를 경감!"); Thread.Sleep(1000);
                Console.WriteLine($"총 {Program.playerStatus.Name}에게 {finalDamage} 피해를 입혔다!");
            }
            return PlayerHp;
        }

        public float EnemyDefense(float PlayerAttack, float EnemyHp)
        {
            CalcDodge();
            if (onDodge)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("그러나 적이 공격을 회피하였다!\n"); Console.ResetColor();
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
