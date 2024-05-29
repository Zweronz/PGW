using ProtoBuf;

namespace engine.network
{
	[ProtoContract]
	public sealed class NetworkCommandInfo
	{
		[ProtoMember(1, DataFormat = DataFormat.ZigZag)]
		public int int_0 = NetworkCommandResultCode.int_0;

		[ProtoMember(2)]
		public string string_0;

		[ProtoMember(3, DataFormat = DataFormat.ZigZag)]
		public int int_1 = -1;

		[ProtoMember(4)]
		public double double_0;

		public override string ToString()
		{
			return string.Format("\n --- code: {0},\n --- message: {1},\n --- sequence: {2}", int_0, string_0, int_1);
		}
	}
}
