using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dariyal.MMO.Battle.HexGrid
{
	
    public class HexHooks : MonoBehaviour
	{
		public bool IsClicked(Camera Camera, Vector3 clickLocation)
		{
            Ray ray = camera.ScreenPointToRay(clickLocation);
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
