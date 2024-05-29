using UnityEngine;

public class CameraPathSpeed : CameraPathPoint
{
	public float float_2 = 1f;

	public float Single_3
	{
		get
		{
			return float_2;
		}
		set
		{
			float_2 = Mathf.Max(value, 1E-07f);
		}
	}
}
