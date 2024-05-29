using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;

public class WebRpcResponse
{
	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private Dictionary<string, object> dictionary_0;

	public string String_0
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		private set
		{
			string_0 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
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
		private set
		{
			string_1 = value;
		}
	}

	public Dictionary<string, object> Dictionary_0
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

	public WebRpcResponse(OperationResponse operationResponse_0)
	{
		object value;
		operationResponse_0.Parameters.TryGetValue(209, out value);
		String_0 = value as string;
		operationResponse_0.Parameters.TryGetValue(207, out value);
		Int32_0 = ((value == null) ? (-1) : ((byte)value));
		operationResponse_0.Parameters.TryGetValue(208, out value);
		Dictionary_0 = value as Dictionary<string, object>;
		operationResponse_0.Parameters.TryGetValue(206, out value);
		String_1 = value as string;
	}

	public string ToStringFull()
	{
		return string.Format("{0}={2}: {1} \"{3}\"", String_0, SupportClass.DictionaryToString(Dictionary_0), Int32_0, String_1);
	}
}
