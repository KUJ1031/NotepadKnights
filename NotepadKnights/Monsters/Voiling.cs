using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadKnights.Monsters
{
    internal class Voiling : Monster
    {
        public Voiling()
        {
            Level = 3;
            Name = "공허충";
            MaxHp = 10;
            CurrentHp = MaxHp;
            Atk = 9;
        }
    }
}
