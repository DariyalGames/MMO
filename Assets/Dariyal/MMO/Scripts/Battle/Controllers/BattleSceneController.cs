using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ModestTree.Zenject;
using ModestTree;
using Dariyal.MMO.Battle.HexGrid;

namespace Dariyal.MMO.Battle
{
    public enum GameStates
    {
        WaitingToStart,
        Playing,
        GameOver,
    }

	public class BattleSceneController : IInitializable, ITickable
	{
        GameStates _state = GameStates.WaitingToStart;
        float _elapsedTime;

        HexGridManager _hexGrid;

        public BattleSceneController(HexGridManager hexgrid)
        {
            _hexGrid = hexgrid;
        }

        public float ElapsedTime
        {
            get { return _elapsedTime; }
        }

        public void Initialize()
        {
            Debug.Log("Started Game");
        }

        public void Tick()
        {
            switch (_state)
            {
                case GameStates.WaitingToStart:
                    {
                        UpdateStarting();
                        break;
                    }
                case GameStates.Playing:
                    {
                        UpdatePlaying();
                        break;
                    }
                case GameStates.GameOver:
                    {
                        UpdateGameOver();
                        break;
                    }
                default:
                    {
                        Assert.That(false);
                        break;
                    }
            }
        }

        private void UpdateGameOver()
        {
            Assert.That(_state == GameStates.GameOver);
            _hexGrid.Stop();
        }

        private void UpdatePlaying()
        {
            Assert.That(_state == GameStates.Playing);
            _elapsedTime += Time.deltaTime;
        }

        private void UpdateStarting()
        {
            Assert.That(_state == GameStates.WaitingToStart);

            _elapsedTime = 0;
            _hexGrid.Start();
            _state = GameStates.Playing;
        }
    }
}
