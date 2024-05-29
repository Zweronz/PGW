using System;
using UnityEngine;

[Serializable]
public class RubbleCreater : MonoBehaviour
{
	public Rigidbody bits;

	public float vel;

	public float up;

	public int amount;

	public int amountRandom;

	public bool useUp;

	private AudioClip audioClip_0;

	public RubbleCreater()
	{
		vel = 20f;
		up = 20f;
		amount = 20;
		amountRandom = 5;
	}

	public virtual void Start()
	{
		CreateObject();
	}

	public virtual void CreateObject()
	{
		amount += UnityEngine.Random.Range(-amountRandom, amountRandom);
		for (int i = 0; i < amount; i++)
		{
			Rigidbody rigidbody = (Rigidbody)UnityEngine.Object.Instantiate(bits, transform.position, transform.rotation);
			float x = UnityEngine.Random.Range(0f - vel, vel);
			UnityEngine.Random.Range(5f, up);
			float z = UnityEngine.Random.Range(0f - vel, vel);
			if (useUp)
			{
				rigidbody.GetComponent<Rigidbody>().velocity = transform.TransformDirection(x, UnityEngine.Random.Range(up / 2f, up), z);
			}
			else
			{
				rigidbody.GetComponent<Rigidbody>().velocity = transform.TransformDirection(x, UnityEngine.Random.Range(0f - up, up), z);
			}
		}
	}

	public virtual void Main()
	{
	}
}
