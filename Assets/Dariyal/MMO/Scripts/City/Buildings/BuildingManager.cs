using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ModestTree;
using ModestTree.Zenject;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.City.Buildings
{
    public enum BuildingTypes
    {
        Orb,
        Forger,
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
        CityController _cityController;

        public BuildingManager(Settings settings, BuildingFactory factory, CityController cityController)
        {
            _settings = settings;
            _buildings = new List<IBuilding>();
            _factory = factory;
            _cityController = cityController;
        }


        public void Initialize()
        {
            //Initialize buidlings
            //Add the barracks.
            IBuilding orb = _factory.Create(BuildingTypes.Orb, _settings.Orb.Placeholder.localPosition);
            orb.Parent = _settings.BuildingsParent;
            _buildings.Add(orb);

            //Addthe Gem Mine.
            IBuilding forger = _factory.Create(BuildingTypes.Forger, _settings.Forger.Placeholder.localPosition);
            forger.Parent = _settings.BuildingsParent;
            _buildings.Add(forger);

            Messenger.AddListener<Vector3>("input_click", OnClick);
        }

        public void Tick()
        {
            //Update buildings etc;
            foreach (IBuilding building in _buildings)
            {
                building.Update();
            }
        }

        void OnClick(Vector3 clickLocation)
        {
            foreach (IBuilding building in _buildings)
            {
                if (building.IsClicked(_cityController.MainCamera, clickLocation))
                    break;
            }
        }

        [Serializable]
        public class Settings
        {
            public Transform BuildingsParent;
            public BuildingDetails Orb;
            public BuildingDetails Forger;
        }
    }
}
