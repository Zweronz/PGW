using UnityEngine;

public class LagRotation : MonoBehaviour
{
	public int updateOrder;

	public float speed = 10f;

	public bool ignoreTimeScale;

	private Transform transform_0;

	private Quaternion quaternion_0;

	private Quaternion quaternion_1;

	private void OnEnable()
	{
		transform_0 = base.transform;
		quaternion_0 = transform_0.localRotation;
		quaternion_1 = transform_0.rotation;
	}

	private void Update()
	{
		Transform parent = transform_0.parent;
		if (parent != null)
		{
			float num = ((!ignoreTimeScale) ? Time.deltaTime : RealTime.Single_1);
			quaternion_1 = Quaternion.Slerp(quaternion_1, parent.rotation * quaternion_0, num * speed);
			transform_0.rotation = quaternion_1;
		}
	}
}
