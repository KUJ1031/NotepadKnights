using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;

namespace NotepadKnights
{
    //플레이어, 몬스터 치명타or회피 메서드

    public class CriticalAndHide
    {
        // 치명타, 회피 확률 변수
        float criticalProbability = 0.15f;
        float dodgeProbability = 0.15f;

        // 치명타, 회피 발생 여부 확인 변수
        public bool onCritical = false;
        public bool onDodge = false;

        private Random random = new Random();

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