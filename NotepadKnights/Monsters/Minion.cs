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
            name = "미니언";
            maxHp = 15;
            isDead = false;
        }
    }
}
