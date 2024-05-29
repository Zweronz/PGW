using UnityEngine;

public class Spin : MonoBehaviour
{
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	public bool ignoreTimeScale;

	private Rigidbody rigidbody_0;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
		rigidbody_0 = base.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (rigidbody_0 == null)
		{
			ApplyDelta((!ignoreTimeScale) ? Time.deltaTime : RealTime.Single_1);
		}
	}

	private void FixedUpdate()
	{
		if (rigidbody_0 != null)
		{
			ApplyDelta(Time.deltaTime);
		}
	}

	public void ApplyDelta(float float_0)
	{
		float_0 *= 360f;
		Quaternion quaternion = Quaternion.Euler(rotationsPerSecond * float_0);
		if (rigidbody_0 == null)
		{
			transform_0.rotation *= quaternion;
		}
		else
		{
			rigidbody_0.MoveRotation(rigidbody_0.rotation * quaternion);
		}
	}
}
