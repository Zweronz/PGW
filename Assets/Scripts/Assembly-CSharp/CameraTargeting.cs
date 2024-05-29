using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTargeting : MonoBehaviour
{
	public LayerMask targetingLayerMask = -1;

	private float float_0 = float.PositiveInfinity;

	private Camera camera_0;

	private void Awake()
	{
		camera_0 = GetComponent<Camera>();
	}

	private void Update()
	{
		TargetingRaycast();
	}

	public void TargetingRaycast()
	{
		Vector3 mousePosition = Input.mousePosition;
		Transform transform = null;
		if (camera_0 != null)
		{
			Ray ray = camera_0.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0f));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, float_0, targetingLayerMask.value))
			{
				transform = hitInfo.collider.transform;
			}
		}
		if (!(transform != null))
		{
			return;
		}
		HighlightableObject componentInChildren = transform.root.GetComponentInChildren<HighlightableObject>();
		if (componentInChildren != null)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				componentInChildren.FlashingOn(2f);
			}
			if (Input.GetButtonUp("Fire2"))
			{
				componentInChildren.FlashingOff();
			}
			if (Input.GetButtonUp("Fire3"))
			{
				componentInChildren.FlashingSwitch();
			}
			componentInChildren.On(Color.red);
		}
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, Screen.height - 100, 500f, 100f), "Left mouse button - turn on flashing on object under mouse cursor\nMiddle mouse button - switch flashing on object under mouse cursor\nRight mouse button - turn off flashing on object under mouse cursor\n'Tab' - fade in/out constant highlighting\n'Q' - turn on/off constant highlighting immediately\n'Z' - turn off all types of highlighting immediately");
	}
}
