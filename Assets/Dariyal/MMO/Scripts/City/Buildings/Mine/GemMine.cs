﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dariyal.MMO.City.Buildings
{
	public class GemMine : IBuilding
	{
        GemMineHooks _hooks;

        public GemMine(GemMineHooks hooks, Vector3 location)
        {
            _hooks = hooks;
            Location = location;
        }

        public void Update()
        {
            
        }

        public Vector3 Location
        {
            get
            {
                return _hooks.transform.localPosition;
            }
            set
            {
                _hooks.transform.localPosition = value;
            }
        }

        public Transform Parent
        {
            get
            {
                return _hooks.transform.parent;
            }
            set
            {
                _hooks.transform.parent = value;
            }
        }
    }
}
