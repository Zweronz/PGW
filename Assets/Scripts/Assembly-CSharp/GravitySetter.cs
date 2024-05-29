using UnityEngine;

public class GravitySetter : MonoBehaviour
{
	public static readonly float float_0 = -9.81f;

	public static readonly float float_1 = -6.54f;

	public static readonly float float_2 = -4.9049997f;

	private void OnLevelWasLoaded(int int_0)
	{
		if (Application.loadedLevelName.Equals("Space"))
		{
			Physics.gravity = new Vector3(0f, float_1, 0f);
		}
		else if (Application.loadedLevelName.Equals("Matrix"))
		{
			Physics.gravity = new Vector3(0f, float_2, 0f);
		}
		else
		{
			Physics.gravity = new Vector3(0f, float_0, 0f);
		}
	}
}
