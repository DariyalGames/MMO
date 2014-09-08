using UnityEngine;
using System.Collections;

using ModestTree.Zenject;

using Dariyal.MessagePassing;
using Dariyal.MMO.Battle;

namespace Dariyal.MMO.Battle
{
    public class BattleGUIHandler : MonoBehaviour
    {
        [Inject]
        BattleController _controller;

        public GUISkin skin;

        // Use this for initialization
        void Start()
        {
        }

        void OnGUI()
        {
            GUI.skin = skin;

            if (_controller.ActiveCameraType == CameraTypes.Action)
            {
                if (GUI.Button(new Rect(10, 10, 150, 30), "Action Camera"))
                {
                    Messenger.Broadcast("battle:gui:swapcamera");
                }
            }
            else
            {
                if (GUI.Button(new Rect(10, 10, 150, 30), "Strategic Camera"))
                {
                    Messenger.Broadcast("battle:gui:swapcamera");
                }
            }
        }
    }
}
