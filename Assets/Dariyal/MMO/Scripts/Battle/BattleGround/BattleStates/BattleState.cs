using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dariyal.MMO.Battle
{
    public enum BattleStates
    {
        WaitingToStart,
        AttackerPreparation,
        DefenderPreperation,
        AttackerTurn,
        DefenderTurn,
        GameEnded,
    }

	public abstract class BattleState
	{
        Battleground _controller;

        public BattleState(Battleground controller)
        {
            _controller = controller;
        }

        public abstract void Update();

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }
	}
}
