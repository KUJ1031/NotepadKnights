using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights.Monsters
{
    public class CannonMinion : Monster
    {
        public CannonMinion()
        {
            Level = 5;
            Name = "대포미니언";
            MaxHp = 25;
            CurrentHp = MaxHp;
            Atk = 8;
        }
    }
}
