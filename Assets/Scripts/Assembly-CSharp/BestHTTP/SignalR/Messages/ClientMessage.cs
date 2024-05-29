using BestHTTP.SignalR.Hubs;

namespace BestHTTP.SignalR.Messages
{
	public struct ClientMessage
	{
		public readonly Hub hub_0;

		public readonly string string_0;

		public readonly object[] object_0;

		public readonly ulong ulong_0;

		public readonly OnMethodResultDelegate onMethodResultDelegate_0;

		public readonly OnMethodFailedDelegate onMethodFailedDelegate_0;

		public readonly OnMethodProgressDelegate onMethodProgressDelegate_0;

		public ClientMessage(Hub hub_1, string string_1, object[] object_1, ulong ulong_1, OnMethodResultDelegate onMethodResultDelegate_1, OnMethodFailedDelegate onMethodFailedDelegate_1, OnMethodProgressDelegate onMethodProgressDelegate_1)
		{
			hub_0 = hub_1;
			string_0 = string_1;
			object_0 = object_1;
			ulong_0 = ulong_1;
			onMethodResultDelegate_0 = onMethodResultDelegate_1;
			onMethodFailedDelegate_0 = onMethodFailedDelegate_1;
			onMethodProgressDelegate_0 = onMethodProgressDelegate_1;
		}
	}
}
