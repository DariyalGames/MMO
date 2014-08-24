using UnityEngine;
using System;
using System.Collections;
using ModestTree.Zenject;
using Dariyal.Util.Messenger;


namespace Dariyal.MMO.City
{
    public class CityController : IInitializable
    {
        public Camera MainCamera { get { return _settings.MainCamera; } }

        Settings _settings;

        public CityController(Settings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            Messenger.AddListener<int>("unit_recruited", OnRecruitUnit);
        }

        void OnRecruitUnit(int noOfUnits)
        {
            Log.InfoFormat("Recruited {0} Stone Golems", noOfUnits); 
        }

        [Serializable]
        public class Settings
        {
            public Camera MainCamera;
        }
    }
}
