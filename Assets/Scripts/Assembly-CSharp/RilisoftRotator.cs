using UnityEngine;

public class RilisoftRotator : MonoBehaviour
{
	public float rate = 10f;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void Update()
	{
		transform_0.Rotate(Vector3.forward, rate * Time.deltaTime, Space.Self);
	}
}
