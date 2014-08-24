using UnityEngine;
using System.Collections.Generic;
using ModestTree.Zenject;
using Dariyal.MMO.Core.Ruleset;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.Battle.Army
{
    public class UnitManager : IInitializable
    {
        List<BaseUnit> _attackingArmy;
        List<BaseUnit> _defendingArmy;

        BattleController _battleController;

        public UnitManager(BattleController battleController)
        {
            _battleController = battleController;
            _attackingArmy = new List<BaseUnit>();
            _defendingArmy = new List<BaseUnit>();
        }

        public void Initialize()
        {
            //Add event listeners;
            Messenger.AddListener("game_start", OnGameStart);
        }

        void OnGameStart()
        {
            //start the game
        }
    }
}
