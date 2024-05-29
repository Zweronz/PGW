using System;
using UnityEngine;

[Serializable]
public class ChangeWater : MonoBehaviour
{
	public GameObject[] waters;

	private int int_0;

	public virtual void Update()
	{
		if (Input.anyKeyDown)
		{
			if (int_0 < waters.Length - 1)
			{
				int_0++;
			}
			else
			{
				int_0 = 0;
			}
			int i = 0;
			GameObject[] array = waters;
			for (int length = array.Length; i < length; i++)
			{
				array[i].SetActive(false);
			}
			waters[int_0].SetActive(true);
		}
	}

	public virtual void Main()
	{
	}
}
