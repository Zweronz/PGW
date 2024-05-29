using UnityEngine;
using engine.unity;

public class CharacterRotator : MonoBehaviour
{
	public GameObject Character;

	public float IdleTime = 3f;

	private Quaternion quaternion_0 = Quaternion.identity;

	private float float_0;

	private Quaternion quaternion_1 = Quaternion.identity;

	private bool bool_0;

	private bool bool_1;

	private void Start()
	{
		bool_1 = true;
	}

	public void OnPress(bool bool_2)
	{
		if (!(Character == null))
		{
			if (bool_1)
			{
				quaternion_0 = Character.transform.rotation;
			}
			bool_0 = bool_2;
		}
	}

	private void Update()
	{
		if (bool_0)
		{
			float axisRaw = InputManager.GetAxisRaw("Mouse X");
			if (axisRaw != 0f)
			{
				bool_1 = false;
				Character.transform.Rotate(Vector3.up, (0f - axisRaw) * 3f);
				float_0 = Time.realtimeSinceStartup;
			}
		}
		else if (Time.realtimeSinceStartup - float_0 > IdleTime && !bool_1)
		{
			if (!Character.transform.rotation.AlmostEquals(quaternion_0, 0.001f))
			{
				Character.transform.rotation = Quaternion.Slerp(Character.transform.rotation, quaternion_0, Time.deltaTime * 2f);
				return;
			}
			bool_1 = true;
			Character.transform.rotation = quaternion_0;
		}
	}
}
