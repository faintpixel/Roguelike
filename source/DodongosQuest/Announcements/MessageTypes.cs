using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Announcements
{
    public enum MessageTypes
    {
        Other = 0,
        CreatureAttack = 1,
        PlayerAttack = 2,
        QuestInformation = 3,
        CreatureDeath = 4,
        PlayerDeath = 5,
        GetItem = 6,
        PlayerStatusChange = 7,
        CreatureStatusChange = 8,
        Spell = 9,
        SpellFailure = 10
    }
}
