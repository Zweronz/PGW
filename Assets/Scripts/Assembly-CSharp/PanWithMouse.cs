using UnityEngine;

public class PanWithMouse : MonoBehaviour
{
	public Vector2 degrees = new Vector2(5f, 3f);

	public float range = 1f;

	private Transform transform_0;

	private Quaternion quaternion_0;

	private Vector2 vector2_0 = Vector2.zero;

	private void Start()
	{
		transform_0 = base.transform;
		quaternion_0 = transform_0.localRotation;
	}

	private void Update()
	{
		float single_ = RealTime.Single_1;
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (range < 0.1f)
		{
			range = 0.1f;
		}
		float x = Mathf.Clamp((mousePosition.x - num) / num / range, -1f, 1f);
		float y = Mathf.Clamp((mousePosition.y - num2) / num2 / range, -1f, 1f);
		vector2_0 = Vector2.Lerp(vector2_0, new Vector2(x, y), single_ * 5f);
		transform_0.localRotation = quaternion_0 * Quaternion.Euler((0f - vector2_0.y) * degrees.y, vector2_0.x * degrees.x, 0f);
	}
}
