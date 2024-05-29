using System.Collections.Generic;
using System.Reflection;
using engine.data;

[Obfuscation(Exclude = true)]
public sealed class ContentGroupStorage : BaseStorage<int, ContentGroupData>
{
	private static ContentGroupStorage _instance;

	public static ContentGroupStorage Get
	{
		get
		{
			return _instance ?? (_instance = new ContentGroupStorage());
		}
	}

	private ContentGroupStorage()
	{
	}

	protected override void OnCreate()
	{
		foreach (KeyValuePair<int, ContentGroupData> item in (IEnumerable<KeyValuePair<int, ContentGroupData>>)base.Storage)
		{
			item.Value.InitIndex();
		}
	}
}
