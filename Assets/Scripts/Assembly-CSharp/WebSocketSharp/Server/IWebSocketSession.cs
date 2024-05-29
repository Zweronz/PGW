using System;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public interface IWebSocketSession
	{
		WebSocketContext WebSocketContext_0 { get; }

		string String_0 { get; }

		string String_1 { get; }

		DateTime DateTime_0 { get; }

		WebSocketState WebSocketState_0 { get; }
	}
}
