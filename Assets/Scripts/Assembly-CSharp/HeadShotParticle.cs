using UnityEngine;

public class HeadShotParticle : MonoBehaviour
{
	private float float_0 = -1f;

	public float maxliveTime = 1.5f;

	public bool isUseMine;

	private Transform transform_0;

	public ParticleEmitter myParticleSystem;

	private void Start()
	{
		transform_0 = base.transform;
		transform_0.position = new Vector3(-10000f, -10000f, -10000f);
		myParticleSystem.emit = false;
	}

	public void StartShowParticle(Vector3 vector3_0, Quaternion quaternion_0, bool bool_0)
	{
		isUseMine = bool_0;
		float_0 = maxliveTime;
		transform_0.position = vector3_0;
		transform_0.rotation = quaternion_0;
		myParticleSystem.emit = true;
	}

	private void Update()
	{
		if (!(float_0 < 0f))
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				transform_0.position = new Vector3(-10000f, -10000f, -10000f);
				myParticleSystem.emit = false;
				isUseMine = false;
			}
		}
	}
}