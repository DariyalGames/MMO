using System;
using System.Collections.Generic;

using UnityEngine;

using Dariyal.MessagePassing;
using Dariyal.MMO.Core.Units;

using ModestTree;
using ModestTree.Zenject;

namespace Dariyal.MMO.City.Buildings
{
    public class Orb : Building
    {
        Dictionary<UnitNames, int> _recruitedUnits;

        public Orb(BuildingHooks hooks, Vector3 location)
            : base(hooks, location)
        {
            //_hooks = hooks;
            Location = location;
            _recruitedUnits = new Dictionary<UnitNames, int>();

            Messenger.AddListener<Tuple<UnitNames, int>>("city:orb:recruited", OnRecruitUnit);
        }

        public override void Update()
        {

        }

        public override bool IsClicked(Camera camera, Vector3 location)
        {
            bool result = base.IsClicked(camera, location);

            if (result)
            {
                Messenger.Broadcast<Vector3>("city:orb:clicked", location);
            }

            return result;
        }

        void OnRecruitUnit(Tuple<UnitNames, int> recruitedUnits)
        {
            //Log.InfoFormat("Recruited {0} {1}", recruitedUnits.Second, recruitedUnits.First.ToString());
            if (_recruitedUnits.ContainsKey(recruitedUnits.First))
            {
                _recruitedUnits[recruitedUnits.First] += recruitedUnits.Second;
            }
            else
            {
                _recruitedUnits.Add(recruitedUnits.First, recruitedUnits.Second);
            }
        }
    }
}
