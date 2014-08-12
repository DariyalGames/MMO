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
                case BuildingTypes.Barracks:
                    return _instantiator.Instantiate<Barracks>(constructorArgs);
                case BuildingTypes.GemMine:
                    return _instantiator.Instantiate<GemMine>(constructorArgs);
            }

            Assert.That(false);
            return null;
        }
	}
}
