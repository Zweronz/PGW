using System.Runtime.CompilerServices;
using UnityEngine;

public class InputToEvent : MonoBehaviour
{
	private GameObject gameObject_0;

	public static Vector3 vector3_0;

	public bool DetectPointedAtGameObject;

	[CompilerGenerated]
	private static GameObject gameObject_1;

	public static GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_1;
		}
		[CompilerGenerated]
		private set
		{
			gameObject_1 = value;
		}
	}

	private void Update()
	{
		if (DetectPointedAtGameObject)
		{
			GameObject_0 = RaycastObject(Input.mousePosition);
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				Press(touch.position);
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				Release(touch.position);
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				Press(Input.mousePosition);
			}
			if (Input.GetMouseButtonUp(0))
			{
				Release(Input.mousePosition);
			}
		}
	}

	private void Press(Vector2 vector2_0)
	{
		gameObject_0 = RaycastObject(vector2_0);
		if (gameObject_0 != null)
		{
			gameObject_0.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
		}
	}

	private void Release(Vector2 vector2_0)
	{
		if (gameObject_0 != null)
		{
			GameObject gameObject = RaycastObject(vector2_0);
			if (gameObject == gameObject_0)
			{
				gameObject_0.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
			gameObject_0.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
			gameObject_0 = null;
		}
	}

	private GameObject RaycastObject(Vector2 vector2_0)
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(base.GetComponent<Camera>().ScreenPointToRay(vector2_0), out hitInfo, 200f))
		{
			vector3_0 = hitInfo.point;
			return hitInfo.collider.gameObject;
		}
		return null;
	}
}
