using System;
using System.Runtime.CompilerServices;

public class AuthenticationValues
{
	public CustomAuthenticationType customAuthenticationType_0;

	public string string_0;

	public string string_1;

	[CompilerGenerated]
	private object object_0;

	public object Object_0
	{
		[CompilerGenerated]
		get
		{
			return object_0;
		}
		[CompilerGenerated]
		private set
		{
			object_0 = value;
		}
	}

	public virtual void SetAuthPostData(string string_2)
	{
		Object_0 = ((!string.IsNullOrEmpty(string_2)) ? string_2 : null);
	}

	public virtual void SetAuthPostData(byte[] byte_0)
	{
		Object_0 = byte_0;
	}

	public virtual void SetAuthParameters(string string_2, string string_3)
	{
		string_0 = "username=" + Uri.EscapeDataString(string_2) + "&token=" + Uri.EscapeDataString(string_3);
	}

	public override string ToString()
	{
		return string_0 + " s: " + string_1;
	}
}
