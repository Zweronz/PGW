using System.Runtime.CompilerServices;

public class FriendInfo
{
	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private string string_1;

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		protected internal set
		{
			string_0 = value;
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
		protected internal set
		{
			bool_0 = value;
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
		protected internal set
		{
			string_1 = value;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return Boolean_0 && !string.IsNullOrEmpty(String_1);
		}
	}

	public override string ToString()
	{
		return string.Format("{0}\t is: {1}", String_0, (!Boolean_0) ? "offline" : ((!Boolean_1) ? "on master" : "playing"));
	}
}
