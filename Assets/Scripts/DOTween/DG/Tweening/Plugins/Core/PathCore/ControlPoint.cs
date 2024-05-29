using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	[Serializable]
	public struct ControlPoint
	{
		public Vector3 a;

		public Vector3 b;

		public ControlPoint(Vector3 a, Vector3 b)
		{
			this.a = a;
			this.b = b;
		}
	}
}
