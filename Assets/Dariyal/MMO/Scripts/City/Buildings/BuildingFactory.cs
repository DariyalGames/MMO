using System;
using ModestTree;
using ModestTree.Zenject;

namespace Dariyal.MMO.City.Buildings
{
	public class BuildingFactory
	{
        Instantiator _instantiator;

        public BuildingFactory(Instantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IBuilding Create(BuildingTypes type, params object[] constructorArgs)
        {
            switch(type)
            {
                case BuildingTypes.Orb:
                    return _instantiator.Instantiate<Orb>(constructorArgs);
                case BuildingTypes.Forger:
                    return _instantiator.Instantiate<Forger>(constructorArgs);
            }

            Assert.That(false);
            return null;
        }
	}
}
