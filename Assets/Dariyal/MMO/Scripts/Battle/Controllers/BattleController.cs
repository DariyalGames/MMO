using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ModestTree.Zenject;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.Battle
{
    public class BattleController : IInitializable, ITickable
    {
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

        private Settings _settings;
        private Camera _activeCamera;
 
        public BattleController(Settings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            _settings.StrategicCamera.enabled = false;
            ActiveCamera = _settings.MainCamera;

			//Messenger.AddListener<Vector3>("input_click", OnClick);
        }

        public void Tick()
        {
            
        }

        [Serializable]
        public class Settings
        {
            public Camera MainCamera;
            public Camera StrategicCamera;
        }

        //Events
        void OnClick(Vector3 clickPosition)
        {
            SwapCamera();
        }

        void SwapCamera()
        {
            if (_activeCamera == _settings.MainCamera)
                ActiveCamera = _settings.StrategicCamera;
            else
                ActiveCamera = _settings.MainCamera;
        }
    }
}
