using UnityEngine;

public class SetPositionZero : MonoBehaviour
{
	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
		transform_0.localPosition = Vector3.zero;
	}

	private void Update()
	{
		transform_0.localPosition = Vector3.zero;
	}
}
