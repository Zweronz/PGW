using System;
using System.Runtime.CompilerServices;
using UnityEngine;

internal sealed class TimerData
{
	public string string_0;

	public string string_1;

	public Color color_0;

	private float float_0;

	[CompilerGenerated]
	private bool bool_0;

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

	public float Single_0
	{
		get
		{
			return Math.Max(0f, float_0 - Time.time);
		}
		set
		{
			Boolean_0 = false;
			float_0 = Time.time + value;
		}
	}
}
