using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;

[ProtoContract]
public sealed class NeedsData
{
	private static NeedData needData_0 = new NeedData();

	[CompilerGenerated]
	private List<NeedData> list_0;

	[ProtoMember(1)]
	public List<NeedData> List_0
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public bool Check()
	{
		return Check(out needData_0);
	}

	public bool Check(out NeedData needData_1)
	{
		if (List_0 == null)
		{
			needData_1 = null;
			return true;
		}
		int num = 0;
		while (true)
		{
			if (num < List_0.Count)
			{
				needData_1 = List_0[num];
				if (!needData_1.Check())
				{
					break;
				}
				num++;
				continue;
			}
			needData_1 = null;
			return true;
		}
		return false;
	}
}
