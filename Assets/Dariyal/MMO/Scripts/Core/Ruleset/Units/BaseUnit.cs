using UnityEngine;
using System.Collections;
using Dariyal.MMO.Battle.HexGrid;
using ModestTree.Zenject;

namespace Dariyal.MMO.Core.Units
{
    public class BaseUnit
    {
        UnitHooks _hooks;

        //add common attributes here.
        public Vector3 Position 
        {
            get { return _hooks.transform.position; }
            set { _hooks.transform.position = value; } 
        }
        public Transform Parent
        {
            get { return _hooks.transform.parent; }
            set { _hooks.transform.parent = value; }
        }
        public UnityEngine.Camera ActiveCamera
        {
            set { _hooks.ActiveCamera = value; }
        }
        public bool IsPlaceable { get; set; }

        public BaseUnit(UnitHooks hooks)
        {
            _hooks = hooks;
            Log.Info("UnitCreated");
        }

        public void MoveTo(Vector3 destination)
        {
            _hooks.MoveTo(destination);
        }
    }
}
