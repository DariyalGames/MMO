using System;
using UnityEngine;

namespace Dariyal.MMO.City.Buildings
{
    [RequireComponent(typeof(BoxCollider))]
	public class OrbHooks : MonoBehaviour
	{
        public bool IsClicked(Camera camera, Vector3 clickPosition)
        {
            Ray ray = camera.ScreenPointToRay(clickPosition);
            RaycastHit hit;
            if (collider.Raycast(ray, out hit, 100))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Hit detected on object " + name + " at point " + hit.point);
                    return true;
                }
            }

            return false;
        }
	}
}
