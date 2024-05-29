using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopPositionParams : MonoBehaviour
{
	public float scaleShop = 150f;

	public Vector3 positionShop;

	public Vector3 rotationShop;

	public int tier = 10;

	public string localizeKey;

	public string prefabName;

	[CompilerGenerated]
	private bool bool_0;

	public string String_0
	{
		get
		{
			return LocalizationStore.Get(localizeKey);
		}
	}

	public string String_1
	{
		get
		{
			return LocalizationStore.GetByDefault(localizeKey);
		}
	}

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
}
