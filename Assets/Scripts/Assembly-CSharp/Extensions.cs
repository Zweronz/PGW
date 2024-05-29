using System.Collections;
using ExitGames.Client.Photon;
using UnityEngine;

public static class Extensions
{
	public static PhotonView[] GetPhotonViewsInChildren(this GameObject gameObject_0)
	{
		return gameObject_0.GetComponentsInChildren<PhotonView>(true);
	}

	public static PhotonView GetPhotonView(this GameObject gameObject_0)
	{
		return gameObject_0.GetComponent<PhotonView>();
	}

	public static bool AlmostEquals(this Vector3 vector3_0, Vector3 vector3_1, float float_0)
	{
		return (vector3_0 - vector3_1).sqrMagnitude < float_0;
	}

	public static bool AlmostEquals(this Vector2 vector2_0, Vector2 vector2_1, float float_0)
	{
		return (vector2_0 - vector2_1).sqrMagnitude < float_0;
	}

	public static bool AlmostEquals(this Quaternion quaternion_0, Quaternion quaternion_1, float float_0)
	{
		return Quaternion.Angle(quaternion_0, quaternion_1) < float_0;
	}

	public static bool AlmostEquals(this float float_0, float float_1, float float_2)
	{
		return Mathf.Abs(float_0 - float_1) < float_2;
	}

	public static void Merge(this IDictionary idictionary_0, IDictionary idictionary_1)
	{
		if (idictionary_1 == null || idictionary_0.Equals(idictionary_1))
		{
			return;
		}
		foreach (object key in idictionary_1.Keys)
		{
			idictionary_0[key] = idictionary_1[key];
		}
	}

	public static void MergeStringKeys(this IDictionary idictionary_0, IDictionary idictionary_1)
	{
		if (idictionary_1 == null || idictionary_0.Equals(idictionary_1))
		{
			return;
		}
		foreach (object key in idictionary_1.Keys)
		{
			if (key is string)
			{
				idictionary_0[key] = idictionary_1[key];
			}
		}
	}

	public static string ToStringFull(this IDictionary idictionary_0)
	{
		return SupportClass.DictionaryToString(idictionary_0, false);
	}

	public static ExitGames.Client.Photon.Hashtable StripToStringKeys(this IDictionary idictionary_0)
	{
		ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
		foreach (DictionaryEntry item in idictionary_0)
		{
			if (item.Key is string)
			{
				hashtable[item.Key] = item.Value;
			}
		}
		return hashtable;
	}

	public static void StripKeysWithNullValues(this IDictionary idictionary_0)
	{
		object[] array = new object[idictionary_0.Count];
		int num = 0;
		foreach (object key2 in idictionary_0.Keys)
		{
			array[num++] = key2;
		}
		foreach (object key in array)
		{
			if (idictionary_0[key] == null)
			{
				idictionary_0.Remove(key);
			}
		}
	}

	public static bool Contains(this int[] int_0, int int_1)
	{
		if (int_0 == null)
		{
			return false;
		}
		int num = 0;
		while (true)
		{
			if (num < int_0.Length)
			{
				if (int_0[num] == int_1)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}
}
