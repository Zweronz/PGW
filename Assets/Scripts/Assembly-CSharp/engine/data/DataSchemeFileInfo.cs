using System.Runtime.CompilerServices;
using ProtoBuf;

namespace engine.data
{
	[ProtoContract]
	public sealed class DataSchemeFileInfo
	{
		[ProtoMember(1)]
		public string string_0;

		[CompilerGenerated]
		private uint uint_0;

		[ProtoMember(2)]
		public uint UInt32_0
		{
			[CompilerGenerated]
			get
			{
				return uint_0;
			}
			[CompilerGenerated]
			set
			{
				uint_0 = value;
			}
		}
	}
}
