using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotepadKnights
{
    interface IRandomProvider
    {
        int GetRandom(int min, int max);
    }
    internal class BattleManager
    {
        public class MonsterFactory
        {
            public static List<Monster> monsters {get; set;}

            // 적들의 정보를 설정하자
            public void SetEnemysInfo()
            {
                monsters = new List<Monster>{

                    new Monster("미니언", 2, 15),
                    new Monster("대포미니언",  5, 25),
                    new Monster("공허충",3, 10),
                };
            } 
        }
    }
}
