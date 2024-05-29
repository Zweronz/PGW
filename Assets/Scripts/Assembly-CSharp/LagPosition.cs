using UnityEngine;

public class LagPosition : MonoBehaviour
{
	public int updateOrder;

	public Vector3 speed = new Vector3(10f, 10f, 10f);

	public bool ignoreTimeScale;

	private Transform transform_0;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private void OnEnable()
	{
		transform_0 = base.transform;
		vector3_1 = transform_0.position;
		vector3_0 = transform_0.localPosition;
	}

	private void Update()
	{
		Transform parent = transform_0.parent;
		if (parent != null)
		{
			float num = ((!ignoreTimeScale) ? Time.deltaTime : RealTime.Single_1);
			Vector3 vector = parent.position + parent.rotation * vector3_0;
			vector3_1.x = Mathf.Lerp(vector3_1.x, vector.x, Mathf.Clamp01(num * speed.x));
			vector3_1.y = Mathf.Lerp(vector3_1.y, vector.y, Mathf.Clamp01(num * speed.y));
			vector3_1.z = Mathf.Lerp(vector3_1.z, vector.z, Mathf.Clamp01(num * speed.z));
			transform_0.position = vector3_1;
		}
	}
}
