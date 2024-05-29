using UnityEngine;

public class RPG_Controller : MonoBehaviour
{
	public static RPG_Controller rpg_Controller_0;

	public CharacterController characterController;

	public float walkSpeed = 10f;

	public float turnSpeed = 2.5f;

	public float jumpHeight = 10f;

	public float gravity = 20f;

	public float fallingThreshold = -6f;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private Vector3 vector3_2 = Vector3.zero;

	private void Awake()
	{
		rpg_Controller_0 = this;
		characterController = GetComponent("CharacterController") as CharacterController;
		RPG_Camera.CameraSetup();
	}

	private void Update()
	{
		if (!(Camera.main == null))
		{
			if (characterController == null)
			{
				Debug.Log("Error: No Character Controller component found! Please add one to the GameObject which has this script attached.");
				return;
			}
			GetInput();
			StartMotor();
		}
	}

	private void GetInput()
	{
		float num = 0f;
		float num2 = 0f;
		if (Input.GetButton("Horizontal Strafe"))
		{
			num = ((Input.GetAxis("Horizontal Strafe") < 0f) ? (-1f) : ((!(Input.GetAxis("Horizontal Strafe") > 0f)) ? 0f : 1f));
		}
		if (Input.GetButton("Vertical"))
		{
			num2 = ((Input.GetAxis("Vertical") < 0f) ? (-1f) : ((!(Input.GetAxis("Vertical") > 0f)) ? 0f : 1f));
		}
		if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			num2 = 1f;
		}
		vector3_0 = num * Vector3.right + num2 * Vector3.forward;
		if (RPG_Animation.rpg_Animation_0 != null)
		{
			RPG_Animation.rpg_Animation_0.SetCurrentMoveDir(vector3_0);
		}
		if (characterController.isGrounded)
		{
			vector3_1 = base.transform.TransformDirection(vector3_0);
			if (Mathf.Abs(vector3_0.x) + Mathf.Abs(vector3_0.z) > 1f)
			{
				vector3_1.Normalize();
			}
			vector3_1 *= walkSpeed;
			vector3_1.y = fallingThreshold;
			if (Input.GetButtonDown("Jump"))
			{
				vector3_1.y = jumpHeight;
				if (RPG_Animation.rpg_Animation_0 != null)
				{
					RPG_Animation.rpg_Animation_0.Jump();
				}
			}
		}
		vector3_2.y = Input.GetAxis("Horizontal") * turnSpeed;
	}

	private void StartMotor()
	{
		vector3_1.y -= gravity * Time.deltaTime;
		characterController.Move(vector3_1 * Time.deltaTime);
		base.transform.Rotate(vector3_2);
		if (!Input.GetMouseButton(0))
		{
			RPG_Camera.rpg_Camera_0.RotateWithCharacter();
		}
	}
}
