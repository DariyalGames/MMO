using UnityEngine;
using System.Collections;
using ModestTree.Zenject;
using System;
using Dariyal.Util.Messenger;

namespace Dariyal.MMO.Core.Input
{
    public class TouchInputController : ITickable
    {
        public TouchInputController()
        {
        }

        public void Tick()
        {
            // -- Tap: quick touch & release
            // ------------------------------------------------
            if (UnityEngine.Input.touchCount == 1)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
                {
                    Messenger.Broadcast<Vector3>("input:click", touch.position);
                }
            }

            //TODO rest;
        }
    }
}
