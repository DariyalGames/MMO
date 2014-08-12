using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ModestTree;
using ModestTree.Zenject;

namespace Dariyal.MMO.City.Buildings
{
    public enum BuildingTypes
    {
        Barracks,
        GemMine,
    }

    [Serializable]
    public class BuildingDetails
    {
        public GameObject Prefab;
        public Transform Placeholder;
    }

    public class BuildingManager : IInitializable, ITickable
    {
        Settings _settings;
        List<IBuilding> _buildings;
        BuildingFactory _factory;


        public BuildingManager(Settings settings, BuildingFactory factory)
        {
            _settings = settings;
            _buildings = new List<IBuilding>();
            _factory = factory;
        }


        public void Initialize()
        {
            //Initialize buidlings
            //Add the barracks.
            IBuilding barracks = _factory.Create(BuildingTypes.Barracks, _settings.Barracks.Placeholder.localPosition);
            barracks.Parent = _settings.BuildingsParent;
            _buildings.Add(barracks);

            //Addthe Gem Mine.
            IBuilding gemmine = _factory.Create(BuildingTypes.GemMine, _settings.GemMine.Placeholder.localPosition);
            gemmine.Parent = _settings.BuildingsParent;
            _buildings.Add(gemmine);
        }

        public void Tick()
        {
            //Update buildings etc;
            foreach (IBuilding building in _buildings)
            {
                building.Update();
            }
        }

        [Serializable]
        public class Settings
        {
            public Transform BuildingsParent;
            public BuildingDetails Barracks;
            public BuildingDetails GemMine;
        }
    }
}
