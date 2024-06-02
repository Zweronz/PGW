using System.Reflection;
using UnityEngine;

public class HookBullet : Bullet
{
	private Player_move_c player_move_c_0;

	private FirstPersonPlayerController firstPersonPlayerController_0;

	private bool bool_1;

	private float float_1;

	private Vector3 vector3_4 = Vector3.zero;

	private void Start()
	{
		float_0 = 5f;
		myTracer.SetActive(false);
	}

	public void StartBullet(Quaternion quaternion_0, Transform transform_2, Vector3 vector3_5, Player_move_c player_move_c_1, bool bool_2)
	{
		Invoke("RemoveSelf", float_0);
		base.Transform_0 = transform_2;
		base.Vector3_0 = transform_2.position;
		base.Vector3_1 = vector3_5;
		transform_0.rotation = quaternion_0;
		transform_0.position = base.Vector3_0;
		transform_0.localScale = new Vector3(transform_0.localScale.x, transform_0.localScale.y, (base.Vector3_1 - base.Vector3_0).magnitude * 0.61f);
		bool_1 = bool_2;
		bool_0 = true;
		myTracer.SetActive(true);
		player_move_c_0 = player_move_c_1;
		if (!bool_2)
		{
			firstPersonPlayerController_0 = player_move_c_1.mySkinName.firstPersonControlSharp;
			firstPersonPlayerController_0.Subscribe(FirstPersonPlayerController.EventType.HOOK_END, RemoveSelfOnEvent);
		}
		else
		{
			float_1 = (base.Vector3_1 - base.Vector3_0).magnitude;
		}
	}

	private void RemoveSelfOnEvent(int int_0)
	{
		RemoveSelf();
	}

	[Obfuscation(Exclude = true)]
	protected override void RemoveSelf()
	{
		CancelInvoke("RemoveSelf");
		myTracer.transform.position = vector3_1;
		myTracer.SetActive(false);
		bool_0 = false;
		if (firstPersonPlayerController_0 != null)
		{
			firstPersonPlayerController_0.Unsubscribe(FirstPersonPlayerController.EventType.HOOK_END, RemoveSelfOnEvent);
			firstPersonPlayerController_0 = null;
		}
	}

	protected override void Update()
	{
		if (!bool_0)
		{
			return;
		}
		if (!(player_move_c_0 == null) && !player_move_c_0.Boolean_3 && !player_move_c_0.Boolean_20)
		{
			Vector3 position = base.Transform_0.position;
			base.Vector3_0 = position;
			myTracer.transform.position = base.Vector3_0;
			vector3_4.Set(base.Vector3_1.x - base.Vector3_0.x, base.Vector3_1.y - base.Vector3_0.y, base.Vector3_1.z - base.Vector3_0.z);
			if (!bool_1)
			{
				transform_0.localScale = new Vector3(transform_0.localScale.x, transform_0.localScale.y, vector3_4.magnitude * 0.61f);
				return;
			}
			float_1 -= Time.deltaTime * 100f;
			if ((double)float_1 < 0.1)
			{
				RemoveSelf();
				return;
			}
			new Ray(base.Vector3_0, vector3_4).GetPoint(float_1);
			transform_0.localScale = new Vector3(transform_0.localScale.x, transform_0.localScale.y, vector3_4.magnitude * 0.61f);
		}
		else
		{
			if (!bool_1 && firstPersonPlayerController_0 != null)
			{
				firstPersonPlayerController_0.SetState(FirstPersonPlayerController.State.Default);
			}
			RemoveSelf();
		}
	}
}
