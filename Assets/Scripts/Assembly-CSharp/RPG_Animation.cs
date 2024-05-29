using UnityEngine;

public class RPG_Animation : MonoBehaviour
{
	public enum CharacterMoveDirection
	{
		None = 0,
		Forward = 1,
		Backward = 2,
		StrafeLeft = 3,
		StrafeRight = 4,
		StrafeForwardLeft = 5,
		StrafeForwardRight = 6,
		StrafeBackLeft = 7,
		StrafeBackRight = 8
	}

	public enum CharacterState
	{
		Idle = 0,
		Walk = 1,
		WalkBack = 2,
		StrafeLeft = 3,
		StrafeRight = 4,
		Jump = 5
	}

	public static RPG_Animation rpg_Animation_0;

	public CharacterMoveDirection currentMoveDir;

	public CharacterState currentState;

	private void Awake()
	{
		rpg_Animation_0 = this;
	}

	private void Update()
	{
		SetCurrentState();
		StartAnimation();
	}

	public void SetCurrentMoveDir(Vector3 vector3_0)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (vector3_0.z > 0f)
		{
			flag = true;
		}
		if (vector3_0.z < 0f)
		{
			flag2 = true;
		}
		if (vector3_0.x < 0f)
		{
			flag3 = true;
		}
		if (vector3_0.x > 0f)
		{
			flag4 = true;
		}
		if (flag)
		{
			if (flag3)
			{
				currentMoveDir = CharacterMoveDirection.StrafeForwardLeft;
			}
			else if (flag4)
			{
				currentMoveDir = CharacterMoveDirection.StrafeForwardRight;
			}
			else
			{
				currentMoveDir = CharacterMoveDirection.Forward;
			}
		}
		else if (flag2)
		{
			if (flag3)
			{
				currentMoveDir = CharacterMoveDirection.StrafeBackLeft;
			}
			else if (flag4)
			{
				currentMoveDir = CharacterMoveDirection.StrafeBackRight;
			}
			else
			{
				currentMoveDir = CharacterMoveDirection.Backward;
			}
		}
		else if (flag3)
		{
			currentMoveDir = CharacterMoveDirection.StrafeLeft;
		}
		else if (flag4)
		{
			currentMoveDir = CharacterMoveDirection.StrafeRight;
		}
		else
		{
			currentMoveDir = CharacterMoveDirection.None;
		}
	}

	public void SetCurrentState()
	{
		if (RPG_Controller.rpg_Controller_0.characterController.isGrounded)
		{
			switch (currentMoveDir)
			{
			case CharacterMoveDirection.None:
				currentState = CharacterState.Idle;
				break;
			case CharacterMoveDirection.Forward:
				currentState = CharacterState.Walk;
				break;
			case CharacterMoveDirection.Backward:
				currentState = CharacterState.WalkBack;
				break;
			case CharacterMoveDirection.StrafeLeft:
				currentState = CharacterState.StrafeLeft;
				break;
			case CharacterMoveDirection.StrafeRight:
				currentState = CharacterState.StrafeRight;
				break;
			case CharacterMoveDirection.StrafeForwardLeft:
				currentState = CharacterState.Walk;
				break;
			case CharacterMoveDirection.StrafeForwardRight:
				currentState = CharacterState.Walk;
				break;
			case CharacterMoveDirection.StrafeBackLeft:
				currentState = CharacterState.WalkBack;
				break;
			case CharacterMoveDirection.StrafeBackRight:
				currentState = CharacterState.WalkBack;
				break;
			}
		}
	}

	public void StartAnimation()
	{
		switch (currentState)
		{
		case CharacterState.Idle:
			Idle();
			break;
		case CharacterState.Walk:
			if (currentMoveDir == CharacterMoveDirection.StrafeForwardLeft)
			{
				StrafeForwardLeft();
			}
			else if (currentMoveDir == CharacterMoveDirection.StrafeForwardRight)
			{
				StrafeForwardRight();
			}
			else
			{
				Walk();
			}
			break;
		case CharacterState.WalkBack:
			if (currentMoveDir == CharacterMoveDirection.StrafeBackLeft)
			{
				StrafeBackLeft();
			}
			else if (currentMoveDir == CharacterMoveDirection.StrafeBackRight)
			{
				StrafeBackRight();
			}
			else
			{
				WalkBack();
			}
			break;
		case CharacterState.StrafeLeft:
			StrafeLeft();
			break;
		case CharacterState.StrafeRight:
			StrafeRight();
			break;
		}
	}

	private void Idle()
	{
		base.GetComponent<Animation>().CrossFade("idle");
	}

	private void Walk()
	{
		base.GetComponent<Animation>().CrossFade("walk");
	}

	private void StrafeForwardLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardleft");
	}

	private void StrafeForwardRight()
	{
		base.GetComponent<Animation>().CrossFade("strafeforwardright");
	}

	private void WalkBack()
	{
		base.GetComponent<Animation>().CrossFade("walkback");
	}

	private void StrafeBackLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafebackleft");
	}

	private void StrafeBackRight()
	{
		base.GetComponent<Animation>().CrossFade("strafebackright");
	}

	private void StrafeLeft()
	{
		base.GetComponent<Animation>().CrossFade("strafeleft");
	}

	private void StrafeRight()
	{
		base.GetComponent<Animation>().CrossFade("straferight");
	}

	public void Jump()
	{
		currentState = CharacterState.Jump;
		if (base.GetComponent<Animation>().IsPlaying("jump"))
		{
			base.GetComponent<Animation>().Stop("jump");
		}
		base.GetComponent<Animation>().CrossFade("jump");
	}
}
