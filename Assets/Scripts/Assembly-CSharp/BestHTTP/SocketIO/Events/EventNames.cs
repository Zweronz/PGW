using System;

namespace BestHTTP.SocketIO.Events
{
	public static class EventNames
	{
		public const string string_0 = "connect";

		public const string string_1 = "disconnect";

		public const string string_2 = "event";

		public const string string_3 = "ack";

		public const string string_4 = "error";

		public const string string_5 = "binaryevent";

		public const string string_6 = "binaryack";

		private static string[] string_7 = new string[8] { "unknown", "connect", "disconnect", "event", "ack", "error", "binaryevent", "binaryack" };

		private static string[] string_8 = new string[8] { "unknown", "open", "close", "ping", "pong", "message", "upgrade", "noop" };

		private static string[] string_9 = new string[10] { "connect", "connect_error", "connect_timeout", "disconnect", "error", "reconnect", "reconnect_attempt", "reconnect_failed", "reconnect_error", "reconnecting" };

		public static string GetNameFor(SocketIOEventTypes socketIOEventTypes_0)
		{
			return string_7[(int)(socketIOEventTypes_0 + 1)];
		}

		public static string GetNameFor(TransportEventTypes transportEventTypes_0)
		{
			return string_8[(int)(transportEventTypes_0 + 1)];
		}

		public static bool IsBlacklisted(string string_10)
		{
			int num = 0;
			while (true)
			{
				if (num < string_9.Length)
				{
					if (string.Compare(string_9[num], string_10, StringComparison.OrdinalIgnoreCase) == 0)
					{
						break;
					}
					num++;
					continue;
				}
				return false;
			}
			return true;
		}
	}
}
