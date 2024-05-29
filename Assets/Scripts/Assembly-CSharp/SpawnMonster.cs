using System;
using System.Collections;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
	public bool shouldMove = true;

	public bool isCycle;

	public GameObject[] _targetCyclePoints;

	private int int_0;

	private float float_0 = 5f;

	private Vector2 vector2_0;

	private float float_1 = -1f;

	private float float_2 = 2f;

	private float float_3 = 17f;

	private Vector3 vector3_0;

	private float float_4;

	private GameObject gameObject_0;

	private MonsterParams monsterParams_0;

	private UnityEngine.AI.NavMeshAgent navMeshAgent_0;

	public bool Boolean_0
	{
		get
		{
			return shouldMove;
		}
		set
		{
			if (shouldMove != value)
			{
				shouldMove = value;
				if (shouldMove)
				{
					ResetPars();
					SendMessage("Walk");
				}
			}
		}
	}

	private void Awake()
	{
		if (Defs.bool_4)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		navMeshAgent_0 = GetComponent<UnityEngine.AI.NavMeshAgent>();
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				gameObject_0 = transform.gameObject;
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
		monsterParams_0 = gameObject_0.GetComponent<MonsterParams>();
		if (!isCycle)
		{
			vector2_0 = new Vector2(base.transform.position.x, base.transform.position.z);
		}
		Boolean_0 = false;
		Boolean_0 = true;
	}

	private void Update()
	{
		if (shouldMove && float_1 <= Time.time)
		{
			navMeshAgent_0.ResetPath();
			vector3_0 = new Vector3(0f - float_3 + UnityEngine.Random.Range(0f, float_3 * 2f), base.transform.position.y, 0f - float_3 + UnityEngine.Random.Range(0f, float_3 * 2f));
			float_1 = Time.time + Vector3.Distance(base.transform.position, vector3_0) / monsterParams_0.Single_6 + float_2;
			base.transform.LookAt(vector3_0);
			navMeshAgent_0.SetDestination(vector3_0);
			navMeshAgent_0.speed = monsterParams_0.Single_6;
		}
	}

	private void ResetPars()
	{
		int_0 = 0;
		float_1 = -1f;
	}
}
