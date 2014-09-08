using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Dariyal.MessagePassing;

namespace Dariyal.MMO.City.Buildings
{
	public class Forger : Building
	{

        public Forger(BuildingHooks hooks, Vector3 location)
            : base(hooks, location)
        {
        }

        public override void Update()
        {
            
        }

        public override bool IsClicked(Camera camera, Vector3 clickPosition)
        {
            bool result = base.IsClicked(camera, clickPosition);

            if (result)
                OnClick();

            return result;
        }

        private void OnClick()
        {
            //Messenger.Broadcast("forger_clicked");
        }
    }
}
