using UnityEngine;
using System.Collections;
using ModestTree.Zenject;

namespace Dariyal.MMO.Core.Units
{  
    [RequireComponent(typeof(UnitMovement))]
    [RequireComponent(typeof(CharacterController))]
    public class UnitHooks : BaseHooks
    {
        public void MoveTo(Vector3 destination)
        {
            Log.Info("starting movement to " + destination.ToString());
            UnitMovement movement = gameObject.GetComponent<UnitMovement>();
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = ActiveCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                movement.StartMovingTo(targetPoint);
            }
        }
    }
}
