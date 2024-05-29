using UnityEngine;

public class posNGUI : MonoBehaviour
{
	private static UIRoot uiroot_0;

	public static float float_0;

	public static float float_1;

	public static float float_2;

	private void Awake()
	{
		uiroot_0 = GetComponent<UIRoot>();
		float_1 = Mathf.Min(1366, Mathf.RoundToInt(768f * (float)Screen.height / (float)Screen.width));
		float_0 = 2f / float_1;
		float_2 = uiroot_0.transform.position.y;
		uiroot_0.gameObject.transform.localScale = new Vector3(float_0, float_0, float_0);
	}

	public static Vector3 getPosNGUI(Vector3 vector3_0)
	{
		return new Vector3(getPosX(vector3_0.x), getPosY(vector3_0.y), vector3_0.z * float_0);
	}

	public static float getPosX(float float_3)
	{
		return (float_3 - 384f) * float_0;
	}

	public static float getPosY(float float_3)
	{
		return float_2 + (0f - float_3 + float_1 * 0.5f) * float_0;
	}

	public static Vector3 getSize(Vector3 vector3_0)
	{
		return new Vector3(vector3_0.x * float_0, vector3_0.y * float_0, vector3_0.z * float_0);
	}

	public static float getSizeWidth(float float_3)
	{
		return float_3 * float_0;
	}

	public static float getSizeHeight(float float_3)
	{
		return float_3 * float_0;
	}

	public static Vector3 getEulerZ(float float_3)
	{
		return new Vector3(0f, 0f, float_3);
	}

	public static void setFillRect(GameObject gameObject_0)
	{
		gameObject_0.transform.localScale = new Vector3(770f, float_1 + 2f, 1f);
	}
}
