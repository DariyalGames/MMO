using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModestTree.Zenject;
using UnityEngine;
using Dariyal.MessagePassing;

namespace Dariyal.MMO.Core.Input
{
    public class KeyboardMouseInputController : ITickable
	{
        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                Log.Info("Mouse Clicked at " + UnityEngine.Input.mousePosition);
                Messenger.Broadcast<Vector3>("input:click", UnityEngine.Input.mousePosition);
            }
        }
    }
}
