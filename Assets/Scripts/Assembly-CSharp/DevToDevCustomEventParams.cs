using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class DevToDevCustomEventParams : IDisposable
{
	private readonly object object_0;

	[CompilerGenerated]
	private Dictionary<string, double> dictionary_0;

	[CompilerGenerated]
	private Dictionary<string, string> dictionary_1;

	[CompilerGenerated]
	private Dictionary<string, long> dictionary_2;

	public Dictionary<string, double> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_0;
		}
		[CompilerGenerated]
		private set
		{
			dictionary_0 = value;
		}
	}

	public Dictionary<string, string> Dictionary_1
	{
		[CompilerGenerated]
		get
		{
			return dictionary_1;
		}
		[CompilerGenerated]
		private set
		{
			dictionary_1 = value;
		}
	}

	public Dictionary<string, long> Dictionary_2
	{
		[CompilerGenerated]
		get
		{
			return dictionary_2;
		}
		[CompilerGenerated]
		private set
		{
			dictionary_2 = value;
		}
	}

	public object Object_0
	{
		get
		{
			return object_0;
		}
	}

	public void Put(string string_0, string string_1)
	{
	}

	public void Put(string string_0, int int_0)
	{
	}

	public void Put(string string_0, float float_0)
	{
	}

	public void Put(string string_0, double double_0)
	{
	}

	public void Put(string string_0, long long_0)
	{
	}

	public void Put(string string_0, DateTime dateTime_0)
	{
	}

	public void Dispose()
	{
	}
}
