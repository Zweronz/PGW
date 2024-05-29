using System;
using UnityEngine;
using UnityScript.Lang;

[Serializable]
public class Scale : MonoBehaviour
{
	public ParticleEmitter[] particleEmitters;

	public float scale;

	[SerializeField]
	private float[] float_0;

	[SerializeField]
	private float[] float_1;

	[SerializeField]
	private Vector3[] vector3_0;

	[SerializeField]
	private Vector3[] vector3_1;

	[SerializeField]
	private Vector3[] vector3_2;

	[SerializeField]
	private Vector3[] vector3_3;

	[SerializeField]
	private bool bool_0;

	public Scale()
	{
		scale = 1f;
		bool_0 = true;
	}

	public virtual void UpdateScale()
	{
		int num = particleEmitters.Length;
		if (bool_0)
		{
			float_0 = new float[num];
			float_1 = new float[num];
			vector3_0 = new Vector3[num];
			vector3_1 = new Vector3[num];
			vector3_2 = new Vector3[num];
			vector3_3 = new Vector3[num];
		}
		for (int i = 0; i < particleEmitters.Length; i++)
		{
			if (bool_0)
			{
				float_0[i] = particleEmitters[i].minSize;
				float_1[i] = particleEmitters[i].maxSize;
				vector3_0[i] = particleEmitters[i].worldVelocity;
				vector3_1[i] = particleEmitters[i].localVelocity;
				vector3_2[i] = particleEmitters[i].rndVelocity;
				vector3_3[i] = particleEmitters[i].transform.localScale;
			}
			particleEmitters[i].minSize = float_0[i] * scale;
			particleEmitters[i].maxSize = float_1[i] * scale;
			particleEmitters[i].worldVelocity = vector3_0[i] * scale;
			particleEmitters[i].localVelocity = vector3_1[i] * scale;
			particleEmitters[i].rndVelocity = vector3_2[i] * scale;
			particleEmitters[i].transform.localScale = vector3_3[i] * scale;
		}
		bool_0 = false;
	}

	public virtual void Main()
	{
	}
}
