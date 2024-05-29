using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	public int level;

	public Transform target;

	public float speed = 8f;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void LateUpdate()
	{
		if (target != null)
		{
			Vector3 forward = target.position - transform_0.position;
			float magnitude = forward.magnitude;
			if (magnitude > 0.001f)
			{
				Quaternion to = Quaternion.LookRotation(forward);
				transform_0.rotation = Quaternion.Slerp(transform_0.rotation, to, Mathf.Clamp01(speed * Time.deltaTime));
			}
		}
	}
}
