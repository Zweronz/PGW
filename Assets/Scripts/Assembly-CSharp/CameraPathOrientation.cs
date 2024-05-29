using UnityEngine;

public class CameraPathOrientation : CameraPathPoint
{
	public Quaternion quaternion_0 = Quaternion.identity;

	public Transform transform_0;

	private void OnEnable()
	{
		base.hideFlags = HideFlags.HideInInspector;
	}
}
