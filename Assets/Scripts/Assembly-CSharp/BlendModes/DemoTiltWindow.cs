using UnityEngine;

namespace BlendModes
{
	public class DemoTiltWindow : MonoBehaviour
	{
		public Vector2 TileRange = new Vector2(5f, 3f);

		private Quaternion quaternion_0;

		private Vector2 vector2_0 = Vector2.zero;

		private void Start()
		{
			quaternion_0 = base.transform.localRotation;
		}

		private void Update()
		{
			Vector3 mousePosition = Input.mousePosition;
			float num = (float)Screen.width * 0.5f;
			float num2 = (float)Screen.height * 0.5f;
			float x = Mathf.Clamp((mousePosition.x - num) / num, -1f, 1f);
			float y = Mathf.Clamp((mousePosition.y - num2) / num2, -1f, 1f);
			vector2_0 = Vector2.Lerp(vector2_0, new Vector2(x, y), Time.deltaTime * 5f);
			base.transform.localRotation = quaternion_0 * Quaternion.Euler((0f - vector2_0.y) * TileRange.y, vector2_0.x * TileRange.x, 0f);
		}
	}
}
