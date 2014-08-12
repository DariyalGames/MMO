using System;
using UnityEngine;


namespace Dariyal.MMO.City.Buildings
{
    public class Barracks : IBuilding
    {
        BarracksHooks _hooks;

        public Barracks(BarracksHooks hooks, Vector3 location)
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
    }
}
