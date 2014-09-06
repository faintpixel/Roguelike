using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Screens.Gameplay
{
    public enum GameState
    {
        SelectingCharacter = 0,
        PlayerTurn = 100,
        PlayerTurnSelectingDoorToClose = 101,
        PlayerTurnSelectingTargetForSpell = 102,
        ComputerTurn = 200
    }
}
