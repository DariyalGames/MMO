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

        public Building Create(BuildingTypes type, params object[] constructorArgs)
        {
            switch(type)
            {
                case BuildingTypes.Orb:
                    return _instantiator.Instantiate<Orb>(constructorArgs);
                case BuildingTypes.Forger:
                    return _instantiator.Instantiate<Forger>(constructorArgs);
                case BuildingTypes.GemMine:
                    return _instantiator.Instantiate<GemMine>(constructorArgs);
                case BuildingTypes.CrystalMine:
                    return _instantiator.Instantiate<CrystalMine>(constructorArgs);
                case BuildingTypes.Merchant:
                    return _instantiator.Instantiate<Merchant>(constructorArgs);
            }

            Assert.That(false);
            return null;
        }
	}
}
