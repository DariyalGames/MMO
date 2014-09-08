using UnityEngine;
using System.Collections;


namespace Dariyal.MMO.Core
{
    [RequireComponent(typeof(Collider))]
    public class BaseHooks : MonoBehaviour
    {
        public UnityEngine.Camera ActiveCamera;

        public virtual bool IsClicked(UnityEngine.Camera activeCamera, Vector3 clickLocation)
        {
            Ray ray = activeCamera.ScreenPointToRay(clickLocation);
            RaycastHit hit;
            if (collider.Raycast(ray, out hit, 100))
            {
                Debug.Log("Hit detected on object " + name + " at point " + hit.point);
                return true;
            }

            return false;
        }
    }
}
