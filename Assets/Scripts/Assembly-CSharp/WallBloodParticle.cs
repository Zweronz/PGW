using System.Runtime.CompilerServices;
using UnityEngine;

public class WallBloodParticle : MonoBehaviour
{
	public GameObject myObject;

	public BulletType type;

	public float MaxLiveTime = 0.1f;

	private float float_0 = -1f;

	private Transform transform_0;

	private ParticleSystem particleSystem_0;

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

	private void Start()
	{
		if (!(myObject == null))
		{
			transform_0 = base.transform;
			transform_0.position = new Vector3(-10000f, -10000f, -10000f);
			particleSystem_0 = myObject.GetComponent<ParticleSystem>();
			if (particleSystem_0 != null)
			{
				particleSystem_0.enableEmission = false;
			}
			myObject.SetActive(false);
		}
	}

	public void StartShowParticle(Vector3 vector3_0, Quaternion quaternion_0, bool bool_1)
	{
		Boolean_0 = bool_1;
		float_0 = MaxLiveTime;
		transform_0.position = vector3_0;
		transform_0.rotation = quaternion_0;
		myObject.SetActive(true);
		if (particleSystem_0 != null)
		{
			particleSystem_0.enableEmission = true;
		}
	}

	private void Update()
	{
		if (float_0 < 0f)
		{
			return;
		}
		float_0 -= Time.deltaTime;
		if (float_0 < 0f)
		{
			transform_0.position = new Vector3(-10000f, -10000f, -10000f);
			if (particleSystem_0 != null)
			{
				particleSystem_0.enableEmission = false;
			}
			myObject.SetActive(false);
			Boolean_0 = false;
		}
	}
}
