using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace engine.network.auth
{
	[ProtoContract]
	public sealed class EnterServerResponse
	{
		[ProtoMember(1)]
		public int int_0;

		[ProtoMember(2)]
		public string string_0;

		[ProtoMember(3)]
		public double double_0;

		[ProtoMember(4)]
		public string string_1;

		[ProtoMember(5)]
		public Dictionary<string, string> dictionary_0;

		public static EnterServerResponse FromByte(byte[] byte_0)
		{
			if (byte_0 == null || byte_0.Length == 0)
			{
				throw new ArgumentNullException("Data from byte. Data in message is null or empty!");
			}
			return Serializer.Deserialize<EnterServerResponse>(new MemoryStream(byte_0));
		}
	}
}
