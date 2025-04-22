using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights.Monsters
{
    internal class Minion : Monster
    {
        public Minion()
        {
            Level = 2;
            Name = "미니언";
            MaxHp = 15;
            CurrentHp = MaxHp;
            Atk = 5;
        }
    }
}
