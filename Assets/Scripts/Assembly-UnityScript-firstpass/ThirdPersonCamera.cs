using System;
using UnityEngine;

[Serializable]
public class ThirdPersonCamera : MonoBehaviour
{
	public Transform cameraTransform;

	private Transform transform_0;

	public float distance;

	public float height;

	public float angularSmoothLag;

	public float angularMaxSpeed;

	public float heightSmoothLag;

	public float snapSmoothLag;

	public float snapMaxSpeed;

	public float clampHeadPositionScreenSpace;

	public float lockCameraTimeout;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private float float_0;

	private float float_1;

	private bool bool_0;

	private ThirdPersonController thirdPersonController_0;

	private float float_2;

	public ThirdPersonCamera()
	{
		distance = 7f;
		height = 3f;
		angularSmoothLag = 0.3f;
		angularMaxSpeed = 15f;
		heightSmoothLag = 0.3f;
		snapSmoothLag = 0.2f;
		snapMaxSpeed = 720f;
		clampHeadPositionScreenSpace = 0.75f;
		lockCameraTimeout = 0.2f;
		vector3_0 = Vector3.zero;
		vector3_1 = Vector3.zero;
		float_2 = 100000f;
	}

	public virtual void Awake()
	{
		if (!cameraTransform && (bool)Camera.main)
		{
			cameraTransform = Camera.main.transform;
		}
		if (!cameraTransform)
		{
			Debug.Log("Please assign a camera to the ThirdPersonCamera script.");
			enabled = false;
		}
		transform_0 = transform;
		if ((bool)transform_0)
		{
			thirdPersonController_0 = (ThirdPersonController)transform_0.GetComponent(typeof(ThirdPersonController));
		}
		if ((bool)thirdPersonController_0)
		{
			CharacterController characterController = (CharacterController)transform_0.GetComponent<Collider>();
			vector3_1 = characterController.bounds.center - transform_0.position;
			vector3_0 = vector3_1;
			vector3_0.y = characterController.bounds.max.y - transform_0.position.y;
		}
		else
		{
			Debug.Log("Please assign a target to the camera that has a ThirdPersonController script attached.");
		}
		Cut(transform_0, vector3_1);
	}

	public virtual void DebugDrawStuff()
	{
		Debug.DrawLine(transform_0.position, transform_0.position + vector3_0);
	}

	public virtual float AngleDistance(float float_3, float float_4)
	{
		float_3 = Mathf.Repeat(float_3, 360f);
		float_4 = Mathf.Repeat(float_4, 360f);
		return Mathf.Abs(float_4 - float_3);
	}

	public virtual void Apply(Transform transform_1, Vector3 vector3_2)
	{
		if (!thirdPersonController_0)
		{
			return;
		}
		Vector3 vector = transform_0.position + vector3_1;
		Vector3 vector3_3 = transform_0.position + vector3_0;
		float y = transform_0.eulerAngles.y;
		float y2 = cameraTransform.eulerAngles.y;
		float num = y;
		if (Input.GetButton("Fire2"))
		{
			bool_0 = true;
		}
		if (bool_0)
		{
			if (!(AngleDistance(y2, y) >= 3f))
			{
				bool_0 = false;
			}
			y2 = Mathf.SmoothDampAngle(y2, num, ref float_1, snapSmoothLag, snapMaxSpeed);
		}
		else
		{
			if (!(thirdPersonController_0.GetLockCameraTimer() >= lockCameraTimeout))
			{
				num = y2;
			}
			if (!(AngleDistance(y2, num) <= 160f) && thirdPersonController_0.IsMovingBackwards())
			{
				num += 180f;
			}
			y2 = Mathf.SmoothDampAngle(y2, num, ref float_1, angularSmoothLag, angularMaxSpeed);
		}
		if (thirdPersonController_0.IsJumping())
		{
			float num2 = vector.y + height;
			if (num2 < float_2 || !(num2 - float_2 <= 5f))
			{
				float_2 = vector.y + height;
			}
		}
		else
		{
			float_2 = vector.y + height;
		}
		float y3 = cameraTransform.position.y;
		y3 = Mathf.SmoothDamp(y3, float_2, ref float_0, heightSmoothLag);
		Quaternion quaternion = Quaternion.Euler(0f, y2, 0f);
		cameraTransform.position = vector;
		cameraTransform.position += quaternion * Vector3.back * distance;
		float y4 = y3;
		Vector3 position = cameraTransform.position;
		position.y = y4;
		cameraTransform.position = position;
		SetUpRotation(vector, vector3_3);
	}

	public virtual void LateUpdate()
	{
		Apply(transform, Vector3.zero);
	}

	public virtual void Cut(Transform transform_1, Vector3 vector3_2)
	{
		float num = heightSmoothLag;
		float num2 = snapMaxSpeed;
		float num3 = snapSmoothLag;
		snapMaxSpeed = 10000f;
		snapSmoothLag = 0.001f;
		heightSmoothLag = 0.001f;
		bool_0 = true;
		Apply(transform, Vector3.zero);
		heightSmoothLag = num;
		snapMaxSpeed = num2;
		snapSmoothLag = num3;
	}

	public virtual void SetUpRotation(Vector3 vector3_2, Vector3 vector3_3)
	{
		Vector3 position = cameraTransform.position;
		Vector3 vector = vector3_2 - position;
		Quaternion quaternion = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
		Vector3 forward = Vector3.forward * distance + Vector3.down * height;
		cameraTransform.rotation = quaternion * Quaternion.LookRotation(forward);
		Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
		Ray ray2 = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, clampHeadPositionScreenSpace, 1f));
		Vector3 point = ray.GetPoint(distance);
		Vector3 point2 = ray2.GetPoint(distance);
		float num = Vector3.Angle(ray.direction, ray2.direction);
		float num2 = num / (point.y - point2.y);
		float num3 = num2 * (point.y - vector3_2.y);
		if (!(num3 >= num))
		{
			num3 = 0f;
			return;
		}
		num3 -= num;
		cameraTransform.rotation *= Quaternion.Euler(0f - num3, 0f, 0f);
	}

	public virtual Vector3 GetCenterOffset()
	{
		return vector3_1;
	}

	public virtual void Main()
	{
	}
}
