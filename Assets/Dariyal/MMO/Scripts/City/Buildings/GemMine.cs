using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dariyal.MessagePassing;
using UnityEngine;

namespace Dariyal.MMO.City.Buildings
{
    class GemMine : Building
    {

        public GemMine(BuildingHooks hooks, Vector3 location)
            : base(hooks, location)
        {
        }

        public override void Update()
        {            
        }

        public override bool IsClicked(Camera camera, Vector3 location)
        {
            bool result = base.IsClicked(camera, location);

            if (result)
            {
                Messenger.Broadcast<Vector3>("city:gemmine:clicked", location);
            }

            return result;
        }
    }
}
