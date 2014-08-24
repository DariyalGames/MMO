using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.City.Buildings
{
	public class Forger : IBuilding
	{
        ForgerHooks _hooks;

        public Forger(ForgerHooks hooks, Vector3 location)
        {
            _hooks = hooks;
            Location = location;
        }

        public void Update()
        {
            
        }

        public Vector3 Location
        {
            get
            {
                return _hooks.transform.localPosition;
            }
            set
            {
                _hooks.transform.localPosition = value;
            }
        }

        public Transform Parent
        {
            get
            {
                return _hooks.transform.parent;
            }
            set
            {
                _hooks.transform.parent = value;
            }
        }


        public bool IsClicked(Camera camera, Vector3 clickPosition)
        {
            bool result = _hooks.IsClicked(camera, clickPosition);

            if (result)
                OnClick();

            return result;
        }

        private void OnClick()
        {
            //Messenger.Broadcast("forger_clicked");
        }
    }
}
