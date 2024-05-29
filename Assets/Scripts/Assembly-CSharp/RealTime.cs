using UnityEngine;

public class RealTime : MonoBehaviour
{
	private static RealTime realTime_0;

	private float float_0;

	private float float_1;

	public static float Single_0
	{
		get
		{
			if (realTime_0 == null)
			{
				Spawn();
			}
			return realTime_0.float_0;
		}
	}

	public static float Single_1
	{
		get
		{
			if (realTime_0 == null)
			{
				Spawn();
			}
			return realTime_0.float_1;
		}
	}

	private static void Spawn()
	{
		GameObject gameObject = new GameObject("_RealTime");
		Object.DontDestroyOnLoad(gameObject);
		realTime_0 = gameObject.AddComponent<RealTime>();
		realTime_0.float_0 = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float_1 = Mathf.Clamp01(realtimeSinceStartup - float_0);
		float_0 = realtimeSinceStartup;
	}
}
