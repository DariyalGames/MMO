using System;

using ModestTree;
using ModestTree.Zenject;

namespace Dariyal.MMO.Battle
{
	public class BattleStateFactory
	{
        Instantiator _instantiator;

        public BattleStateFactory(Instantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public BattleState Create(BattleStates state, params object[] constructorArgs)
        {
            switch (state)
            {
                case BattleStates.WaitingToStart:
                    return _instantiator.Instantiate<BattleStateWaitingToStart>(constructorArgs);
                case BattleStates.AttackerPreparation:
                    return _instantiator.Instantiate<BattleStateAttackerPreparation>(constructorArgs);
            }

            Assert.That(false);
            return null;
        }
	}
}
