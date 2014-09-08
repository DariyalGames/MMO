using System;

using ModestTree;
using ModestTree.Zenject;

using Dariyal.MessagePassing;
using UnityEngine;

namespace Dariyal.MMO.Battle
{
    public enum CameraTypes
    {
        Action,
        Strategic,
    }

    public enum BattleSceneStates
    {
        WaitingToStart,
        Playing,
        GameEnded,
    }

	public class BattleController : IInitializable, ITickable
	{
        private BattleSceneStates _state = BattleSceneStates.WaitingToStart;
        private Settings _settings;
        private Camera _activeCamera;
        private Battleground _battleground;
        //Properties
        public Camera ActiveCamera
        {
            get { return _activeCamera; }
            private set
            {
                if (_activeCamera != null)
                    _activeCamera.enabled = false;
                _activeCamera = value;
                _activeCamera.enabled = true;
            }
        }

        public CameraTypes ActiveCameraType { get; private set; }

        public BattleSceneStates State
        {
            get { return _state; }
            set
            {
                var previousstate = _state;
                _state = value;
                if (_state != previousstate)
                {
                    //Messenger.Broadcast<BattleSceneStates>("battle:state:changed", _state);
                }
            }
        }

        //constructor
        public BattleController(Settings settings/*, Battleground battleground*/)
        {
            _settings = settings;
            //_battleground = battleground;
        }

        //zenject related
        public void Initialize()
        {
            _settings.StrategicCamera.enabled = false;
            ActiveCamera = _settings.MainCamera;

            Messenger.AddListener("battle:gui:swapcamera", SwapCamera);
        }

        public void Tick()
        {
            //throw new NotImplementedException();
        }

        //Events
        void SwapCamera()
        {
            if (_activeCamera == _settings.MainCamera)
            {
                ActiveCamera = _settings.StrategicCamera;
                ActiveCameraType = CameraTypes.Strategic;
            }
            else
            {
                ActiveCamera = _settings.MainCamera;
                ActiveCameraType = CameraTypes.Action;
            }
        }

        //Settings class
        [Serializable]
        public class Settings
        {
            public Camera MainCamera;
            public Camera StrategicCamera;
        }
    }
}
