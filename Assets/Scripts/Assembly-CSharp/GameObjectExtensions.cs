using UnityEngine;

public static class GameObjectExtensions
{
	public static bool GetActive(this GameObject gameObject_0)
	{
		return gameObject_0.activeInHierarchy;
	}
}
