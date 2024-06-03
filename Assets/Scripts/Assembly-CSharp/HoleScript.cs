using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class HoleScript : MonoBehaviour
{
	public float maxliveTime = 3f;

	public BulletType type;

	private float float_0 = -1f;

	private Transform transform_0;

	[CompilerGenerated]
	private bool bool_0;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	private void Awake()
	{
		Boolean_0 = false;
	}

	public void Init()
	{
		transform_0 = base.transform;
		transform_0.position = new Vector3(-10000f, -10000f, -10000f);
	}

	public void StartShowHole(Vector3 vector3_0, Quaternion quaternion_0, bool bool_1)
	{
		Boolean_0 = bool_1;
		float_0 = maxliveTime;
		transform_0.position = vector3_0;
		transform_0.rotation = quaternion_0;
	}

	private void Update()
	{
		if (!(float_0 < 0f))
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				transform_0.position = new Vector3(-10000f, -10000f, -10000f);
				Boolean_0 = false;
			}
		}
	}
}
