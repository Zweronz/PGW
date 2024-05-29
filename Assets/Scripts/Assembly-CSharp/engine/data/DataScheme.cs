using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf;

namespace engine.data
{
	[ProtoContract]
	public class DataScheme
	{
		[CompilerGenerated]
		private List<DataSchemeFileInfo> list_0;

		[ProtoMember(1)]
		public List<DataSchemeFileInfo> List_0
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
	}
}
