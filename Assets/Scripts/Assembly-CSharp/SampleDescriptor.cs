using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class SampleDescriptor
{
	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private Type type_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private string string_2;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private GameObject gameObject_0;

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

	public Type Type_0
	{
		[CompilerGenerated]
		get
		{
			return type_0;
		}
		[CompilerGenerated]
		set
		{
			type_0 = value;
		}
	}

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public string String_1
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public string String_2
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public GameObject GameObject_0
	{
		[CompilerGenerated]
		get
		{
			return gameObject_0;
		}
		[CompilerGenerated]
		set
		{
			gameObject_0 = value;
		}
	}

	public bool Boolean_2
	{
		get
		{
			return GameObject_0 != null;
		}
	}

	public SampleDescriptor(Type type_1, string string_3, string string_4, string string_5)
	{
		Type_0 = type_1;
		String_0 = string_3;
		String_1 = string_4;
		String_2 = string_5;
	}

	public void CreateUnityObject()
	{
		if (!(GameObject_0 != null))
		{
			GameObject_0 = new GameObject(String_0);
			GameObject_0.AddComponent(Type_0);
		}
	}

	public void DestroyUnityObject()
	{
		if (GameObject_0 != null)
		{
			UnityEngine.Object.Destroy(GameObject_0);
			GameObject_0 = null;
		}
	}
}
