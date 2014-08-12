using System;
using UnityEngine;

namespace Dariyal.MMO.Core.Input
{
	public interface IInputController
	{
        Vector3 CameraTransalation { get; }
        float CameraZoom { get; }
	}
}
