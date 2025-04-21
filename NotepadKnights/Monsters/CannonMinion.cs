using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights.Monsters
{
    internal class CannonMinion : Monster
    {
        public CannonMinion()
        {
            name = "대포미니언";
            maxHp = 25;
            isDead = false;
        }
    }
}
