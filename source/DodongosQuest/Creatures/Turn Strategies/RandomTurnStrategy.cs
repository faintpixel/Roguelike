using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DodongosQuest.Creatures
{
    public class RandomTurnStrategy : ITurnStrategy 
    {
     
        private int _speed;
        private int _countdownToNextTurn;

        public RandomTurnStrategy(int speed)
        {
            _speed = speed;
            _countdownToNextTurn = speed;
         
        }       

        public void TakeTurn(ICreature creature, World world)
        {
            _countdownToNextTurn -= 1;
            if (_countdownToNextTurn <= 0)
            {
                _countdownToNextTurn = _speed;
                bool turnOver = false;
                while (turnOver != true)
                {
                    int roll = RandomNumberProvider.GetRandomNumber(1, 4);
                    if (roll == 1)
                        turnOver = world.MoveCreatureInDirectionSuccessful(Direction.North, creature);
                    else if (roll == 2)
                        turnOver = world.MoveCreatureInDirectionSuccessful(Direction.South, creature);
                    else if (roll == 3)
                        turnOver = world.MoveCreatureInDirectionSuccessful(Direction.East, creature);
                    else if (roll == 4)
                        turnOver = world.MoveCreatureInDirectionSuccessful(Direction.West, creature);
                   }
            }
        }
    }
}
