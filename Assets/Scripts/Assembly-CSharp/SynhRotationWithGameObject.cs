using UnityEngine;

public class SynhRotationWithGameObject : MonoBehaviour
{
	public new Transform gameObject;

	private Transform transform_0;

	private void Start()
	{
		transform_0 = base.transform;
	}

	private void Update()
	{
		transform_0.rotation = gameObject.rotation;
	}
}
