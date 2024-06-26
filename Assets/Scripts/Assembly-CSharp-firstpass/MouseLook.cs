using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public enum RotationAxes
	{
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxes axes;

	public float sensitivityX = 15f;

	public float sensitivityY = 15f;

	public float minimumX = -360f;

	public float maximumX = 360f;

	public float minimumY = -60f;

	public float maximumY = 60f;

	private float float_0;

	private void Update()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			float y = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			float_0 += Input.GetAxis("Mouse Y") * sensitivityY;
			float_0 = Mathf.Clamp(float_0, minimumY, maximumY);
			base.transform.localEulerAngles = new Vector3(0f - float_0, y, 0f);
		}
		else if (axes == RotationAxes.MouseX)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * sensitivityX, 0f);
		}
		else
		{
			float_0 += Input.GetAxis("Mouse Y") * sensitivityY;
			float_0 = Mathf.Clamp(float_0, minimumY, maximumY);
			base.transform.localEulerAngles = new Vector3(0f - float_0, base.transform.localEulerAngles.y, 0f);
		}
	}

	private void Start()
	{
		if ((bool)base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}
