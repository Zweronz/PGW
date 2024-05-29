using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;
using engine.data;

[StorageDataKey(typeof(string))]
[ProtoContract]
public class LocalizationData
{
	[CompilerGenerated]
	private Dictionary<string, string> dictionary_0;

	[ProtoMember(1)]
	public Dictionary<string, string> Dictionary_0
	{
		[CompilerGenerated]
		get
		{
			return dictionary_0;
		}
		[CompilerGenerated]
		set
		{
			dictionary_0 = value;
		}
	}
}
