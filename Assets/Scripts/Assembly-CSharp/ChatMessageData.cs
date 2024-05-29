using System;
using ProtoBuf;

[ProtoContract]
public sealed class ChatMessageData
{
	public static class ChatMessageType
	{
		public const int int_0 = -1;

		public const int int_1 = 0;

		public const int int_2 = 1;

		public const int int_3 = 2;

		public const int int_4 = 3;

		public const int int_5 = 4;

		public const int int_6 = 5;

		public const int int_7 = 6;
	}

	private static readonly string string_0 = "...";

	[ProtoMember(1)]
	public string string_1;

	[ProtoMember(2)]
	public int int_0;

	[ProtoMember(3)]
	public int int_1;

	[ProtoMember(4)]
	public string string_2;

	[ProtoMember(5)]
	public int int_2;

	[ProtoMember(6)]
	public string string_3;

	[ProtoMember(7)]
	public string string_4;

	[ProtoMember(8)]
	public string string_5;

	public DateTime dateTime_0;

	public string NormalizedSenderNick(int int_3 = 0)
	{
		if (int_3 != 0 && !string.IsNullOrEmpty(string_2) && string_2.Length >= int_3)
		{
			return string_2.Substring(0, int_3) + string_0;
		}
		return string_2;
	}

	public string NormalizedReceiverNick(int int_3 = 0)
	{
		if (int_3 != 0 && !string.IsNullOrEmpty(string_3) && string_3.Length >= int_3)
		{
			return string_3.Substring(0, int_3) + string_0;
		}
		return string_3;
	}
}
