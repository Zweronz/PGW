using UnityEngine;

namespace ZeichenKraftwerk
{
	public sealed class Rotator : MonoBehaviour
	{
		public Vector3 eulersPerSecond = new Vector3(0f, 0f, 1f);

		private Transform transform_0;

		public void Start()
		{
			transform_0 = base.transform;
		}

		private void Update()
		{
			transform_0.Rotate(eulersPerSecond * Time.deltaTime);
		}
	}
}
