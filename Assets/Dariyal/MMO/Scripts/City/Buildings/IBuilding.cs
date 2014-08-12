using System;
using UnityEngine;


namespace Dariyal.MMO.City.Buildings
{
    public interface IUpgradable
    {
        void Upgrade();
    }

    public interface IBuilding
    {
        void Update();
        
        Vector3 Location { get; set; }

        Transform Parent { get; set; }
    }
}
