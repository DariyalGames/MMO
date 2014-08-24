using System;
using UnityEngine;
using Dariyal.Util.Messenger;


namespace Dariyal.MMO.City.Buildings
{
    public class Orb : IBuilding
    {
        OrbHooks _hooks;

        public Orb(OrbHooks hooks, Vector3 location)
        {
            _hooks = hooks;
            Location = location;
        }

        public void Update()
        {

        }

        public Vector3 Location
        {
            get { return _hooks.transform.localPosition; }
            set { _hooks.transform.localPosition = value; }
        }


        public Transform Parent
        {
            get { return _hooks.transform.parent; }
            set { _hooks.transform.parent = value; }
        }

        public bool IsClicked(Camera camera, Vector3 clickPosition)
        {
            bool result = _hooks.IsClicked(camera, clickPosition);

            if (result)
            {
                Messenger.Broadcast<Vector3>("orb_clicked", clickPosition);
            }

            return result;
        }
    }
}
