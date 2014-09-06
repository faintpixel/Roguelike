using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DodongosQuest.Magic;


namespace DodongosQuest.Creatures
{
    public class WanderingSpellCasterTurnStrategy : ITurnStrategy
    {
        private int _speed;
        private int _countdownToNextTurn;
        private ISpell _spell;

        public WanderingSpellCasterTurnStrategy(int speed, World world)
        {
            _speed = speed;
            _countdownToNextTurn = speed;
            _spell = SpellFactory.CreateFireboltSpell(world);
        }

        public void TakeTurn(ICreature creature, World world)
        {
            _countdownToNextTurn -= 1;
            if (_countdownToNextTurn <= 0)
            {
                _countdownToNextTurn = _speed;
                bool turnOver = false;

                if (world.CanPlayerSeeWorldIndex(creature.WorldIndex) && RandomNumberProvider.CheckIfChanceOccurs(25) && world.GetStraightLineDistance(creature.WorldIndex, world.Player.WorldIndex) <= 5)
                {
                    _spell.CastSpell(creature, world.Player.WorldIndex);
                }
                else
                {
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
}
