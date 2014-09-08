using System;
using System.Collections.Generic;

using UnityEngine;

using ModestTree.Zenject;
using ModestTree;

using Dariyal.MessagePassing;

namespace Dariyal.MMO.Battle
{

    public class Battleground : IInitializable, ITickable
    {        
        private BattleState _state = null;
        private BattleStateFactory _stateFactory;
 
        public Battleground(BattleStateFactory factory)
        {
            _stateFactory = factory;
        }

        public void Initialize()
        {
            //_currentState = BattleSceneStates.WaitingToStart;
            _state = _stateFactory.Create(BattleStates.WaitingToStart, this);
        }

        public void Tick()
        {
            
        }      
    }
}
