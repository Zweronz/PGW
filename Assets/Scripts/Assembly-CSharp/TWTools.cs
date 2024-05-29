using UnityEngine;

public static class TWTools
{
	public static void SetActiveSelf(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	private static void Activate(Transform transform_0)
	{
		SetActiveSelf(transform_0.gameObject, true);
		int num = 0;
		int childCount = transform_0.GetChildCount();
		while (true)
		{
			if (num < childCount)
			{
				Transform child = transform_0.GetChild(num);
				if (!child.gameObject.activeSelf)
				{
					num++;
					continue;
				}
				break;
			}
			int i = 0;
			for (int childCount2 = transform_0.GetChildCount(); i < childCount2; i++)
			{
				Transform child2 = transform_0.GetChild(i);
				Activate(child2);
			}
			break;
		}
	}

	private static void Deactivate(Transform transform_0)
	{
		SetActiveSelf(transform_0.gameObject, false);
	}

	public static void SetActive(GameObject gameObject_0, bool bool_0)
	{
		if (bool_0)
		{
			Activate(gameObject_0.transform);
		}
		else
		{
			Deactivate(gameObject_0.transform);
		}
	}

	public static GameObject AddChild(GameObject gameObject_0)
	{
		GameObject gameObject = new GameObject();
		if (gameObject_0 != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = gameObject_0.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = gameObject_0.layer;
		}
		return gameObject;
	}

	public static GameObject AddChild(GameObject gameObject_0, GameObject gameObject_1)
	{
		GameObject gameObject = Object.Instantiate(gameObject_1) as GameObject;
		if (gameObject != null && gameObject_0 != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = gameObject_0.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = gameObject_0.layer;
		}
		return gameObject;
	}

	public static void Destroy(Object object_0)
	{
		if (object_0 != null)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(object_0);
			}
			else
			{
				Object.DestroyImmediate(object_0);
			}
		}
	}

	public static void DestroyImmediate(Object object_0)
	{
		if (object_0 != null)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(object_0);
			}
			else
			{
				Object.Destroy(object_0);
			}
		}
	}
}
