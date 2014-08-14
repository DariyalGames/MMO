using System;
using UnityEngine;
using ModestTree.Zenject;
using Dariyal.MMO.Core.Input;

namespace Dariyal.MMO.Battle
{
	public class IsometricCameraController : IInitializable, ITickable
	{
        IInputController _input;
        Settings _settings;

        public IsometricCameraController(Settings settings, IInputController input)
        {
            _input = input;
            _settings = settings;
        }

        public void Initialize()
        {
            _settings.camera.orthographicSize = _settings.zoomMaxSize;
        }

        public void Tick()
        {
            if (_input.CameraZoom < 0)
            {
                _settings.camera.orthographicSize = Mathf.Max(_settings.camera.orthographicSize - _settings.scrollSpeed, _settings.zoomMinSize);
            }
            if (_input.CameraZoom > 0)
            {
                _settings.camera.orthographicSize = Mathf.Min(_settings.camera.orthographicSize + _settings.scrollSpeed, _settings.zoomMaxSize);
            }

            //Debug.Log(_input.CameraTransalation * _settings.scrollSpeed);
            

            //if (_settings.scrollZone.Contains(_settings.target.transform.position))
            //{
            //    _settings.target.transform.position += (_input.CameraTransalation * _settings.scrollSpeed);
            //}
        }

        [Serializable]
        public class Settings
        {
            public Camera camera;
            public GameObject target;
            public int zoomMinSize;
            public int zoomMaxSize;
            public float zoomSpeed;
            public float scrollSpeed;
            /*
            public Polygon scrollZone;

            [Serializable]
            public class Polygon
            {
                public Vector2 topRight;
                public Vector2 topLeft;
                public Vector2 bottomRight;
                public Vector2 bottomLeft;

                public bool Contains(Vector3 point)
                {
                    bool result = false;

                    return result;
                }
            }
             * */
        }
    }
}
