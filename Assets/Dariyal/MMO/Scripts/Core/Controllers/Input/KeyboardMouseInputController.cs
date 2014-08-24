using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree.Zenject;
using UnityEngine;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.Core.Input
{
    public class KeyboardMouseInputController : ITickable
	{
        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Messenger.Broadcast<Vector3>("input_click", UnityEngine.Input.mousePosition);
            }
        }
    }
}
