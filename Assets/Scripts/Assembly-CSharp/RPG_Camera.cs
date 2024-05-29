using System;
using UnityEngine;

public class RPG_Camera : MonoBehaviour
{
	public struct ClipPlaneVertexes
	{
		public Vector3 vector3_0;

		public Vector3 vector3_1;

		public Vector3 vector3_2;

		public Vector3 vector3_3;
	}

	public static RPG_Camera rpg_Camera_0;

	public Transform cameraPivot;

	public float TimeRotationCam = 15f;

	public float distance = 5f;

	public float distanceMin = 1f;

	public float distanceMax = 30f;

	public float mouseSpeed = 8f;

	public float mouseScroll = 15f;

	public float mouseSmoothingFactor = 0.08f;

	public float camDistanceSpeed = 0.7f;

	public float camBottomDistance = 1f;

	public float firstPersonThreshold = 0.8f;

	public float characterFadeThreshold = 1.8f;

	private float float_0 = 180f;

	public bool isDragging;

	private Vector3 vector3_0;

	public float desiredDistance;

	public float offsetMaxDistance;

	public float offsetY;

	public float lastDistance;

	public float mouseX;

	public float deltaMouseX;

	private float float_1;

	private float float_2;

	public float mouseY;

	public float mouseYSmooth;

	private float float_3;

	private float float_4 = -89.5f;

	private float float_5 = 89.5f;

	private float float_6;

	private bool bool_0;

	private bool bool_1;

	private static float float_7;

	private static float float_8;

	private static float float_9;

	private static float float_10;

	public Vector2 controlVector;

	public Vector3 curTargetEulerAngles;

	private bool bool_2 = true;

	private bool bool_3;

	private RaycastHit raycastHit_0;

	public static Camera camera_0;

	private float float_11;

	public LayerMask collisionLayer;

	private void Awake()
	{
		rpg_Camera_0 = this;
		camera_0 = GetComponent<Camera>();
		float_11 = camera_0.nearClipPlane;
	}

	private void Start()
	{
		distance = Mathf.Clamp(distance, distanceMin, distanceMax);
		desiredDistance = distance;
		float_7 = camera_0.fieldOfView / 2f * ((float)Math.PI / 180f);
		float_8 = camera_0.aspect;
		float_9 = camera_0.nearClipPlane * Mathf.Tan(float_7);
		float_10 = float_9 * float_8;
		mouseY = 15f;
	}

	private void OnDestroy()
	{
		camera_0 = null;
	}

	public static void CameraSetup()
	{
		GameObject gameObject;
		if (camera_0 != null)
		{
			gameObject = camera_0.gameObject;
		}
		else
		{
			gameObject = new GameObject("Main Camera");
			gameObject.AddComponent<Camera>();
			gameObject.tag = "MainCamera";
		}
		if (!gameObject.GetComponent("RPG_Camera"))
		{
			gameObject.AddComponent<RPG_Camera>();
		}
		RPG_Camera rPG_Camera = gameObject.GetComponent("RPG_Camera") as RPG_Camera;
		GameObject gameObject2 = GameObject.Find("cameraPivot");
		rPG_Camera.cameraPivot = gameObject2.transform;
	}

	private void Update()
	{
		if (!(cameraPivot == null))
		{
			if (!RotatorKillCam.bool_0 && distance < 2f)
			{
				deltaMouseX += Time.deltaTime * float_0;
			}
			curTargetEulerAngles = cameraPivot.eulerAngles;
			GetInput();
			GetDesiredPosition();
			PositionUpdate();
		}
	}

	private void GetInput()
	{
		if ((double)distance > 0.1)
		{
			bool_0 = Physics.Linecast(base.transform.position, base.transform.position - Vector3.up * camBottomDistance, collisionLayer);
		}
		bool flag = bool_0 && base.transform.position.y - cameraPivot.transform.position.y <= 0f;
		mouseY = ClampAngle(mouseY, -89.5f, 89.5f);
		float_1 = Mathf.SmoothDamp(float_1, mouseX, ref float_2, mouseSmoothingFactor);
		mouseYSmooth = Mathf.SmoothDamp(mouseYSmooth, mouseY, ref float_3, mouseSmoothingFactor);
		if (flag)
		{
			float_4 = mouseY;
		}
		else
		{
			float_4 = -89.5f;
		}
		mouseYSmooth = ClampAngle(mouseYSmooth, float_4, float_5);
		if (desiredDistance > distanceMax)
		{
			desiredDistance = distanceMax;
		}
		if (desiredDistance < distanceMin)
		{
			desiredDistance = distanceMin;
		}
		controlVector = Vector2.zero;
	}

	private void GetDesiredPosition()
	{
		distance = desiredDistance;
		vector3_0 = GetCameraPosition(mouseYSmooth, mouseX, distance);
		bool_1 = false;
		float num = CheckCameraClipPlane(cameraPivot.position, vector3_0);
		if (num != -1f)
		{
			distance = num;
			vector3_0 = GetCameraPosition(mouseYSmooth, mouseX, distance);
			bool_1 = true;
		}
		distance -= camera_0.nearClipPlane;
		if (lastDistance < distance || !bool_1)
		{
			distance = Mathf.SmoothDamp(lastDistance, distance, ref float_6, camDistanceSpeed);
		}
		if (distance < distanceMin)
		{
			distance = distanceMin;
		}
		lastDistance = distance;
		vector3_0 = GetCameraPosition(mouseYSmooth, mouseX, distance);
		if (distance < 4f && raycastHit_0.normal != Vector3.zero)
		{
			vector3_0 -= raycastHit_0.normal * offsetY * (4f - distance) * 0.25f;
			bool_3 = true;
			return;
		}
		bool_3 = false;
		if (camera_0.nearClipPlane != float_11)
		{
			camera_0.nearClipPlane = float_11;
		}
	}

	private void PositionUpdate()
	{
		base.transform.position = vector3_0;
		if (distance > distanceMin)
		{
			base.transform.LookAt(cameraPivot);
			base.transform.eulerAngles -= new Vector3(2f, 0f, 0f);
		}
	}

	public void UpdateMouseX()
	{
		if (!(cameraPivot == null))
		{
			for (mouseX = 150f + deltaMouseX + cameraPivot.rotation.eulerAngles.y; mouseX > 360f; mouseX -= 360f)
			{
			}
		}
	}

	private void CharacterFade()
	{
		if (RPG_Animation.rpg_Animation_0 == null)
		{
			return;
		}
		if (distance < firstPersonThreshold)
		{
			RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().enabled = false;
		}
		else if (distance < characterFadeThreshold)
		{
			RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().enabled = true;
			float num = 1f - (characterFadeThreshold - distance) / (characterFadeThreshold - firstPersonThreshold);
			if (RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.a != num)
			{
				RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color = new Color(RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.r, RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.g, RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.b, num);
			}
		}
		else
		{
			RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().enabled = true;
			if (RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.a != 1f)
			{
				RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color = new Color(RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.r, RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.g, RPG_Animation.rpg_Animation_0.GetComponent<Renderer>().material.color.b, 1f);
			}
		}
	}

	private Vector3 GetCameraPosition(float float_12, float float_13, float float_14)
	{
		Vector3 vector = new Vector3(0f, 0f, 0f - float_14);
		Quaternion quaternion = Quaternion.Euler(float_12, float_13, 0f);
		return cameraPivot.position + quaternion * vector;
	}

	private float CheckCameraClipPlane(Vector3 vector3_1, Vector3 vector3_2)
	{
		float num = -1f;
		ClipPlaneVertexes clipPlaneAt = GetClipPlaneAt(vector3_2);
		if (Physics.Linecast(vector3_1, vector3_2, out raycastHit_0, collisionLayer) && IsIgnorCollider(raycastHit_0))
		{
			num = raycastHit_0.distance - camera_0.nearClipPlane;
		}
		else if (Physics.Linecast(vector3_1 - base.transform.right * float_10 + base.transform.up * float_9, clipPlaneAt.vector3_0, out raycastHit_0, collisionLayer) && IsIgnorCollider(raycastHit_0))
		{
			if (raycastHit_0.distance < num || num == -1f)
			{
				num = Vector3.Distance(raycastHit_0.point + base.transform.right * float_10 - base.transform.up * float_9, vector3_1);
			}
		}
		else if (Physics.Linecast(vector3_1 + base.transform.right * float_10 + base.transform.up * float_9, clipPlaneAt.vector3_1, out raycastHit_0, collisionLayer) && IsIgnorCollider(raycastHit_0))
		{
			if (raycastHit_0.distance < num || num == -1f)
			{
				num = Vector3.Distance(raycastHit_0.point - base.transform.right * float_10 - base.transform.up * float_9, vector3_1);
			}
		}
		else if (Physics.Linecast(vector3_1 - base.transform.right * float_10 - base.transform.up * float_9, clipPlaneAt.vector3_2, out raycastHit_0, collisionLayer) && IsIgnorCollider(raycastHit_0))
		{
			if (raycastHit_0.distance < num || num == -1f)
			{
				num = Vector3.Distance(raycastHit_0.point + base.transform.right * float_10 + base.transform.up * float_9, vector3_1);
			}
		}
		else if (Physics.Linecast(vector3_1 + base.transform.right * float_10 - base.transform.up * float_9, clipPlaneAt.vector3_3, out raycastHit_0, collisionLayer) && IsIgnorCollider(raycastHit_0) && (raycastHit_0.distance < num || num == -1f))
		{
			num = Vector3.Distance(raycastHit_0.point - base.transform.right * float_10 + base.transform.up * float_9, vector3_1);
		}
		return num;
	}

	private bool IsIgnorCollider(RaycastHit raycastHit_1)
	{
		if (raycastHit_1.collider.tag != "Player" && raycastHit_1.collider.tag != "Vision" && raycastHit_1.collider.tag != "colliderPoint" && raycastHit_1.collider.tag != "Helicopter")
		{
			return true;
		}
		return false;
	}

	private float ClampAngle(float float_12, float float_13, float float_14)
	{
		while (float_12 < -360f || float_12 > 360f)
		{
			if (float_12 < -360f)
			{
				float_12 += 360f;
			}
			if (float_12 > 360f)
			{
				float_12 -= 360f;
			}
		}
		return Mathf.Clamp(float_12, float_13, float_14);
	}

	public ClipPlaneVertexes GetClipPlaneAt(Vector3 vector3_1)
	{
		ClipPlaneVertexes result = default(ClipPlaneVertexes);
		if (camera_0 == null)
		{
			return result;
		}
		Transform transform = camera_0.transform;
		float nearClipPlane = camera_0.nearClipPlane;
		result.vector3_0 = vector3_1 - transform.right * float_10;
		result.vector3_0 += transform.up * float_9;
		result.vector3_0 += transform.forward * nearClipPlane;
		result.vector3_1 = vector3_1 + transform.right * float_10;
		result.vector3_1 += transform.up * float_9;
		result.vector3_1 += transform.forward * nearClipPlane;
		result.vector3_2 = vector3_1 - transform.right * float_10;
		result.vector3_2 -= transform.up * float_9;
		result.vector3_2 += transform.forward * nearClipPlane;
		result.vector3_3 = vector3_1 + transform.right * float_10;
		result.vector3_3 -= transform.up * float_9;
		result.vector3_3 += transform.forward * nearClipPlane;
		return result;
	}

	public void RotateWithCharacter()
	{
		float num = Input.GetAxis("Horizontal") * RPG_Controller.rpg_Controller_0.turnSpeed;
		mouseX += num;
	}
}
