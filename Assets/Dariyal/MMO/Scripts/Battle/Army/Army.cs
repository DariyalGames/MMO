using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ModestTree;
using ModestTree.Zenject;

using Dariyal.MMO.Battle;
using Dariyal.MMO.Core.Ruleset;
using Dariyal.MessagePassing;
using Dariyal.MMO.Core.Services;
using Dariyal.MMO.Core.Units;

namespace Dariyal.MMO.Battle
{
    public class Army : IInitializable
    {
        IEnumerable<BaseUnit> _units;
        Battleground _battleController;
        IDataService _dataService;
        UnitFactory _factory;

        public string DataName { get; set; }

        IEnumerable<BaseUnit> PlaceableUnits { get { return _units.Where(o => o.IsPlaceable); } }
        public BaseUnit CurrentPlaceableUnit { get { return (PlaceableUnits.Count() > 0) ? PlaceableUnits.First() : null; } }

        public Army(Battleground battleController, IDataService dataService)
        {
            _battleController = battleController;
            _dataService = dataService;
            _units = new List<BaseUnit>();
        }

        public void Initialize()
        {
            //Initialize
            IEnumerable data = _dataService.GetData(DataName) as IEnumerable;
            foreach (KeyValuePair<UnitNames,int> dataEntry in data)
            {

            }
        }

        public void PlaceUnit(Vector3 location)
        {
            CurrentPlaceableUnit.Position = location;
        }
    }
}
