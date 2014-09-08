using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ModestTree;
using ModestTree.Zenject;
using Dariyal.MessagePassing;

namespace Dariyal.MMO.City.Buildings
{
    public enum BuildingTypes
    {
        Orb,
        Forger,
        GemMine,
        CrystalMine,
        Merchant,
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
        List<Building> _buildings;
        BuildingFactory _factory;
        CityController _cityController;

        public BuildingManager(Settings settings, BuildingFactory factory, CityController cityController)
        {
            _settings = settings;
            _buildings = new List<Building>();
            _factory = factory;
            _cityController = cityController;
        }


        public void Initialize()
        {
            //Initialize buidlings
            //Add the barracks.
            Building orb = _factory.Create(BuildingTypes.Orb, _settings.Orb.Placeholder.localPosition);
            orb.Parent = _settings.BuildingsParent;
            _buildings.Add(orb);

            //Addthe Forger.
            Building forger = _factory.Create(BuildingTypes.Forger, _settings.Forger.Placeholder.localPosition);
            forger.Parent = _settings.BuildingsParent;
            _buildings.Add(forger);

            //Addthe Gem Mine.
            Building gemmine = _factory.Create(BuildingTypes.GemMine, _settings.GemMine.Placeholder.localPosition);
            gemmine.Parent = _settings.BuildingsParent;
            _buildings.Add(gemmine);

            //Addthe Crystal Mine.
            Building crysmine = _factory.Create(BuildingTypes.CrystalMine, _settings.CrystalMine.Placeholder.localPosition);
            crysmine.Parent = _settings.BuildingsParent;
            _buildings.Add(crysmine);

            //Addthe Merchant.
            Building merchant = _factory.Create(BuildingTypes.Merchant, _settings.Merchant.Placeholder.localPosition);
            merchant.Parent = _settings.BuildingsParent;
            _buildings.Add(merchant);

            Messenger.AddListener<Vector3>("input:click", OnClick);
        }

        public void Tick()
        {
            //Update buildings etc;
            foreach (Building building in _buildings)
            {
                building.Update();
            }
        }

        void OnClick(Vector3 clickLocation)
        {
            foreach (Building building in _buildings)
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
            public BuildingDetails GemMine;
            public BuildingDetails CrystalMine;
            public BuildingDetails Merchant;
        }
    }
}
