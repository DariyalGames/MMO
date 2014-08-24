using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Dariyal.MMO.Core.Input
{
	public interface IInputController
	{
        Vector3 ClickPosition { get; }
	}
}
