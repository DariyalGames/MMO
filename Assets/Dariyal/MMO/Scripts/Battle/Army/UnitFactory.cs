using System;

using ModestTree;
using ModestTree.Zenject;

using Dariyal.MMO.Core.Units;
using UnityEngine;

namespace Dariyal.MMO.Battle
{
    public class UnitFactory
    {
        Instantiator _instantiator;

        public UnitFactory(Instantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public BaseUnit Create(UnitNames name, params object[] constructorArgs)
        {
            switch (name)
            {
                case UnitNames.StoneGolem:
                    return _instantiator.Instantiate<BaseUnit>(constructorArgs);

            }

            Assert.That(false);
            return null;
        }
    }

    [Serializable]
    public class UnitDetails
    {
        GameObject Prefab;
    }
}
