using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dariyal.MMO.Core.Camera
{
	public class KeyboardCameraController : ICameraController
	{
        Settings _settings;

        public KeyboardCameraController(Settings settings)
        {
            _settings = settings;
        }

        public Vector3 CameraTransalation
        {
            get 
            { 
                //return UnityEngine.Input.GetAxis("Horizontal"); 
                float mouseX = UnityEngine.Input.mousePosition.x;
                float mouseY = UnityEngine.Input.mousePosition.y;

                Vector3 transalation = new Vector3(0, 0, 0);
 
                if (mouseX < _settings.scrollZone)
                {
                    transalation += new Vector3(-1, 0, 1);
                }
                if (mouseX > Screen.width - _settings.scrollZone)
                {
                    transalation += new Vector3(1, 0, -1);
                }
                if (mouseY < _settings.scrollZone)
                {
                    transalation += new Vector3(-1, 0, -1);
                }
                if (mouseY > Screen.height - _settings.scrollZone)
                {
                    transalation += new Vector3(1, 0, 1);
                }

                return transalation;
            }
        }

        public float CameraZoom
        {
            get { return UnityEngine.Input.GetAxis("Mouse ScrollWheel"); }
        }

        [Serializable]
        public class Settings
        {
            public float scrollZone;
        }
    }
}
