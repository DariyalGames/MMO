using UnityEngine;
using System;
using System.Collections;
using ModestTree.Zenject;
using Dariyal.MessagePassing;
using ModestTree;
using Dariyal.MMO.Core.Units;


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
            Messenger.AddListener<Tuple<UnitNames, int>>("city:orb:recruited", OnRecruitUnit);
        }

        void OnRecruitUnit(Tuple<UnitNames, int> recruitedUnits)
        {
            Log.InfoFormat("Recruited {0} {1}", recruitedUnits.Second, recruitedUnits.First.ToString()); 
        }

        [Serializable]
        public class Settings
        {
            public Camera MainCamera;
        }
    }
}
