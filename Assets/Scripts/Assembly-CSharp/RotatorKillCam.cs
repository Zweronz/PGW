using UnityEngine;

public class RotatorKillCam : MonoBehaviour
{
	public static bool bool_0;

	private void Start()
	{
		bool_0 = false;
		ReturnCameraToDefaultOrientation();
	}

	private void OnEnable()
	{
		bool_0 = false;
		ReturnCameraToDefaultOrientation();
	}

	private void OnPress(bool bool_1)
	{
		bool_0 = bool_1;
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (!(RPG_Camera.rpg_Camera_0 == null))
		{
			RPG_Camera.rpg_Camera_0.deltaMouseX += vector2_0.x;
		}
	}

	private void ReturnCameraToDefaultOrientation()
	{
		if (!(RPG_Camera.rpg_Camera_0 == null))
		{
			RPG_Camera.rpg_Camera_0.deltaMouseX = 0f;
			RPG_Camera.rpg_Camera_0.mouseY = 15f;
		}
	}
}
