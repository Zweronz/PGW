using System.Runtime.CompilerServices;
using UnityEngine;
using engine.helpers;

public class MonsterParams : MonoBehaviour
{
	public int monsterId;

	private int int_0;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	[CompilerGenerated]
	private float float_3;

	[CompilerGenerated]
	private float float_4;

	[CompilerGenerated]
	private float float_5;

	[CompilerGenerated]
	private float float_6;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	public float Single_0
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		private set
		{
			float_0 = value;
		}
	}

	public float Single_1
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		private set
		{
			float_1 = value;
		}
	}

	public float Single_2
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		private set
		{
			float_2 = value;
		}
	}

	public float Single_3
	{
		[CompilerGenerated]
		get
		{
			return float_3;
		}
		[CompilerGenerated]
		private set
		{
			float_3 = value;
		}
	}

	public float Single_4
	{
		[CompilerGenerated]
		get
		{
			return float_4;
		}
		[CompilerGenerated]
		private set
		{
			float_4 = value;
		}
	}

	public float Single_5
	{
		[CompilerGenerated]
		get
		{
			return float_5;
		}
		[CompilerGenerated]
		private set
		{
			float_5 = value;
		}
	}

	public float Single_6
	{
		[CompilerGenerated]
		get
		{
			return float_6;
		}
		[CompilerGenerated]
		private set
		{
			float_6 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		private set
		{
			int_1 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		private set
		{
			int_2 = value;
		}
	}

	private void Awake()
	{
		MonsterData objectByKey = MonsterStorage.Get.Storage.GetObjectByKey(monsterId);
		if (objectByKey == null)
		{
			Log.AddLine("[MonsterParams::Start create wrong monster]", Log.LogLevel.ERROR);
			Object.Destroy(base.transform.parent);
			return;
		}
		Single_0 = objectByKey.Single_0;
		Single_1 = objectByKey.Single_1;
		Single_2 = objectByKey.Single_2;
		Single_3 = objectByKey.Single_3;
		Single_4 = objectByKey.Single_4;
		Single_5 = objectByKey.Single_5;
		if (objectByKey.Single_7 < objectByKey.Single_8)
		{
			Single_5 += Random.Range(objectByKey.Single_7, objectByKey.Single_8);
		}
		Single_6 = objectByKey.Single_6;
		Int32_0 = objectByKey.Int32_1;
		Int32_1 = objectByKey.Int32_2;
		Single_5 *= MonstersController.Single_0;
		Single_6 *= MonstersController.Single_1;
		Single_0 *= MonstersController.Single_2;
		checkParams();
	}

	private void Start()
	{
	}

	public void changeHealth(float float_7)
	{
		Single_0 = Mathf.Max(Single_0 + float_7, 0f);
	}

	public void LastMonster()
	{
		Single_5 = Mathf.Max(Single_5, 3f);
		Single_4 = 150f;
	}

	private void checkParams()
	{
		if (Single_0 <= 0f)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - health monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_0 = 1f;
		}
		if (Single_1 <= 0f)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - damage monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_1 = 1f;
		}
		if (Single_2 <= 0f)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - attackDistance monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_2 = 3f;
		}
		if ((double)Single_3 <= 0.01)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - timeToHit monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_3 = 2f;
		}
		if (Single_4 <= 3f)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - detectRadius monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_4 = 17f;
		}
		if ((double)Single_5 <= 0.1)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - attackingSpeed monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_5 = 1f;
		}
		if ((double)Single_6 <= 0.1)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - notAttackingSpeed monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Single_6 = 1f;
		}
		if (Int32_0 < 0)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - scorePerKill monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Int32_0 = 0;
		}
		if (Int32_1 < 0)
		{
			Log.AddLine(string.Format("[MonsterParams::checkParams] wrong param - scorePerDamage monsterId = {0}", monsterId), Log.LogLevel.ERROR);
			Int32_1 = 0;
		}
	}
}
