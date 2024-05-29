using UnityEngine;

public class ScrollWheelEvents : MonoBehaviour
{
	public delegate void ScrollStartStopHandler(bool bool_0);

	private static ScrollWheelEvents scrollWheelEvents_0;

	public static ScrollStartStopHandler scrollStartStopHandler_0;

	private bool bool_0;

	private void Awake()
	{
		if (scrollWheelEvents_0 != null)
		{
			Debug.LogWarning("ScrollWheelEvents is pseudo-singleton, destroying new instance");
			Object.Destroy(this);
		}
		else
		{
			scrollWheelEvents_0 = this;
		}
	}

	public static void CheckInstance()
	{
		if (scrollWheelEvents_0 == null)
		{
			GameObject gameObject = new GameObject("ScrollWheelEvents");
			gameObject.AddComponent(typeof(ScrollWheelEvents));
		}
	}

	private void Update()
	{
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			if (!bool_0)
			{
				bool_0 = true;
				if (scrollStartStopHandler_0 != null)
				{
					scrollStartStopHandler_0(true);
				}
			}
		}
		else if (bool_0)
		{
			bool_0 = false;
			if (scrollStartStopHandler_0 != null)
			{
				scrollStartStopHandler_0(false);
			}
		}
	}
}
