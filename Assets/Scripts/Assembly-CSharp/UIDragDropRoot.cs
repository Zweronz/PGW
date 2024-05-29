using UnityEngine;

public class UIDragDropRoot : MonoBehaviour
{
	public static Transform transform_0;

	private void OnEnable()
	{
		transform_0 = base.transform;
	}

	private void OnDisable()
	{
		if (transform_0 == base.transform)
		{
			transform_0 = null;
		}
	}
}
