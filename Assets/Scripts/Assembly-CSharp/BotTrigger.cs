using System;
using System.Collections;
using UnityEngine;

public class BotTrigger : MonoBehaviour
{
	public bool shouldDetectPlayer = true;

	private bool bool_0;

	private BotAI botAI_0;

	private GameObject gameObject_0;

	private Player_move_c player_move_c_0;

	private GameObject gameObject_1;

	private MonsterParams monsterParams_0;

	private Transform transform_0;

	private void Awake()
	{
		if (Defs.bool_4)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		transform_0 = base.transform;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				gameObject_1 = transform.gameObject;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		monsterParams_0 = gameObject_1.GetComponent<MonsterParams>();
		botAI_0 = GetComponent<BotAI>();
		gameObject_0 = GameObject.FindGameObjectWithTag("Player");
		if (gameObject_0 != null)
		{
			player_move_c_0 = gameObject_0.GetComponent<SkinName>().Player_move_c_0;
		}
	}

	private void Update()
	{
		if (!shouldDetectPlayer)
		{
			return;
		}
		if (!bool_0)
		{
			bool bool_;
			float float_;
			GameObject turretState = GetTurretState(out bool_, out float_);
			float num = Vector3.Distance(transform_0.position, gameObject_0.transform.position);
			bool flag = !player_move_c_0.Boolean_14 && num <= monsterParams_0.Single_4;
			Transform transform = null;
			if (flag && bool_)
			{
				transform = ((!(num < float_)) ? turretState.transform : gameObject_0.transform);
			}
			else
			{
				if (flag)
				{
					transform = gameObject_0.transform;
				}
				if (bool_)
				{
					transform = turretState.transform;
				}
			}
			if (transform != null)
			{
				botAI_0.SetTarget(transform, true);
				bool_0 = true;
			}
			return;
		}
		bool flag2;
		if (!(flag2 = botAI_0.Target == null) && botAI_0.Target.CompareTag("Player"))
		{
			if (player_move_c_0.Boolean_14)
			{
				flag2 = true;
			}
			else
			{
				float num2 = Vector3.SqrMagnitude(base.transform.position - gameObject_0.transform.position);
				float num3 = monsterParams_0.Single_4 * monsterParams_0.Single_4;
				if (bool_0 && num2 > num3)
				{
					flag2 = true;
				}
			}
		}
		if (!flag2 && botAI_0.Target.CompareTag("Turret"))
		{
			TurretController component = botAI_0.Target.GetComponent<TurretController>();
			if (component != null && component.Boolean_2 && component.Boolean_1)
			{
				flag2 = true;
			}
		}
		if (flag2)
		{
			botAI_0.SetTarget(null, false);
			bool_0 = false;
		}
	}

	private GameObject GetTurretState(out bool bool_1, out float float_0)
	{
		bool_1 = false;
		float_0 = float.MaxValue;
		GameObject gameObject = GameObject.FindGameObjectWithTag("Turret");
		if (gameObject == null)
		{
			return null;
		}
		TurretController component = gameObject.GetComponent<TurretController>();
		if (!(component == null) && !component.Boolean_2 && component.Boolean_1)
		{
			float_0 = Vector3.Distance(transform_0.position, gameObject.transform.position);
			bool_1 = float_0 <= monsterParams_0.Single_4;
			return gameObject;
		}
		return null;
	}
}
