using System;
using UnityEngine;


namespace Dariyal.MMO.City.Buildings
{
    public interface IUpgradable
    {
        void Upgrade();
    }

    public abstract class Building
    {
        protected BuildingHooks _hooks;

        public abstract void Update();

        public Building(BuildingHooks hooks, Vector3 location)
        {
            _hooks = hooks;
            Location = location;
        }

        public virtual bool IsClicked(Camera camera, Vector3 location)
        {
            return _hooks.IsClicked(camera, location);
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
    }
}
