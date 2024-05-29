using System;
using System.IO;
using System.Reflection;
using ProtoBuf;

namespace engine.network
{
	[ProtoContract]
	public sealed class NetworkCommandWrapper
	{
		[ProtoMember(1)]
		public int int_0;

		[ProtoMember(2)]
		public byte[] byte_0;

		private static MethodInfo methodInfo_0;

		public static byte[] ToByte<T>(T gparam_0) where T : AbstractNetworkCommand, new()
		{
			NetworkCommandWrapper networkCommandWrapper = new NetworkCommandWrapper();
			networkCommandWrapper.int_0 = NetworkCommands.GetTypeIdByType(gparam_0.GetType());
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize(memoryStream, gparam_0);
				networkCommandWrapper.byte_0 = memoryStream.ToArray();
			}
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				Serializer.Serialize(memoryStream2, networkCommandWrapper);
				return memoryStream2.ToArray();
			}
		}

		public static AbstractNetworkCommand FromByte(byte[] byte_1)
		{
			if (byte_1 != null && byte_1.Length != 0)
			{
				NetworkCommandWrapper networkCommandWrapper = Serializer.Deserialize<NetworkCommandWrapper>(new MemoryStream(byte_1));
				Type typeById = NetworkCommands.GetTypeById(networkCommandWrapper.int_0);
				if (methodInfo_0 == null)
				{
					methodInfo_0 = typeof(Serializer).GetMethod("Deserialize", new Type[1] { typeof(Stream) });
				}
				return (AbstractNetworkCommand)methodInfo_0.MakeGenericMethod(typeById).Invoke(null, new object[1]
				{
					new MemoryStream(networkCommandWrapper.byte_0)
				});
			}
			throw new ArgumentNullException("Data from byte. Data in message is null or empty!");
		}
	}
}
