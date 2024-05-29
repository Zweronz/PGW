using System;
using BestHTTP.SignalR.Messages;
using BestHTTP.WebSocket;

namespace BestHTTP.SignalR.Transports
{
	public sealed class WebSocketTransport : TransportBase
	{
		private BestHTTP.WebSocket.WebSocket webSocket_0;

		public override bool Boolean_0
		{
			get
			{
				return true;
			}
		}

		public override TransportTypes TransportTypes_0
		{
			get
			{
				return TransportTypes.WebSocket;
			}
		}

		public WebSocketTransport(Connection connection_0)
			: base("webSockets", connection_0)
		{
		}

		public override void Connect()
		{
			if (webSocket_0 != null)
			{
				HTTPManager.ILogger_0.Warning("WebSocketTransport", "Start - WebSocket already created!");
				return;
			}
			if (base.TransportStates_0 != TransportStates.Reconnecting)
			{
				base.TransportStates_0 = TransportStates.Connecting;
			}
			RequestTypes type = ((base.TransportStates_0 != TransportStates.Reconnecting) ? RequestTypes.Connect : RequestTypes.Reconnect);
			Uri uri_ = base.IConnection_0.BuildUri(type, this);
			webSocket_0 = new BestHTTP.WebSocket.WebSocket(uri_);
			BestHTTP.WebSocket.WebSocket webSocket = webSocket_0;
			webSocket.onWebSocketOpenDelegate_0 = (OnWebSocketOpenDelegate)Delegate.Combine(webSocket.onWebSocketOpenDelegate_0, new OnWebSocketOpenDelegate(WSocket_OnOpen));
			BestHTTP.WebSocket.WebSocket webSocket2 = webSocket_0;
			webSocket2.onWebSocketMessageDelegate_0 = (OnWebSocketMessageDelegate)Delegate.Combine(webSocket2.onWebSocketMessageDelegate_0, new OnWebSocketMessageDelegate(WSocket_OnMessage));
			BestHTTP.WebSocket.WebSocket webSocket3 = webSocket_0;
			webSocket3.onWebSocketClosedDelegate_0 = (OnWebSocketClosedDelegate)Delegate.Combine(webSocket3.onWebSocketClosedDelegate_0, new OnWebSocketClosedDelegate(WSocket_OnClosed));
			BestHTTP.WebSocket.WebSocket webSocket4 = webSocket_0;
			webSocket4.onWebSocketErrorDescriptionDelegate_0 = (OnWebSocketErrorDescriptionDelegate)Delegate.Combine(webSocket4.onWebSocketErrorDescriptionDelegate_0, new OnWebSocketErrorDescriptionDelegate(WSocket_OnError));
			base.IConnection_0.PrepareRequest(webSocket_0.HTTPRequest_0, type);
			webSocket_0.Open();
		}

		protected override void SendImpl(string string_1)
		{
			if (webSocket_0 != null && webSocket_0.Boolean_0)
			{
				webSocket_0.Send(string_1);
			}
		}

		public override void Stop()
		{
			if (webSocket_0 != null && webSocket_0.Boolean_0)
			{
				webSocket_0.onWebSocketOpenDelegate_0 = null;
				webSocket_0.onWebSocketMessageDelegate_0 = null;
				webSocket_0.onWebSocketClosedDelegate_0 = null;
				webSocket_0.onWebSocketErrorDescriptionDelegate_0 = null;
				webSocket_0.Close();
				webSocket_0 = null;
			}
		}

		protected override void Started()
		{
		}

		protected override void Aborted()
		{
			if (webSocket_0 != null && webSocket_0.Boolean_0)
			{
				webSocket_0.Close();
				webSocket_0 = null;
			}
		}

		private void WSocket_OnOpen(BestHTTP.WebSocket.WebSocket webSocket_1)
		{
			if (webSocket_1 == webSocket_0)
			{
				HTTPManager.ILogger_0.Information("WebSocketTransport", "WSocket_OnOpen");
				OnConnected();
			}
		}

		private void WSocket_OnMessage(BestHTTP.WebSocket.WebSocket webSocket_1, string string_1)
		{
			if (webSocket_1 == webSocket_0)
			{
				IServerMessage serverMessage = TransportBase.Parse(base.IConnection_0.IJsonEncoder_0, string_1);
				if (serverMessage != null)
				{
					base.IConnection_0.OnMessage(serverMessage);
				}
			}
		}

		private void WSocket_OnClosed(BestHTTP.WebSocket.WebSocket webSocket_1, ushort ushort_0, string string_1)
		{
			if (webSocket_1 == webSocket_0)
			{
				string text = ushort_0 + " : " + string_1;
				HTTPManager.ILogger_0.Information("WebSocketTransport", "WSocket_OnClosed " + text);
				if (base.TransportStates_0 == TransportStates.Closing)
				{
					base.TransportStates_0 = TransportStates.Closed;
				}
				else
				{
					base.IConnection_0.Error(text);
				}
			}
		}

		private void WSocket_OnError(BestHTTP.WebSocket.WebSocket webSocket_1, string string_1)
		{
			if (webSocket_1 == webSocket_0)
			{
				if (base.TransportStates_0 != TransportStates.Closing && base.TransportStates_0 != TransportStates.Closed)
				{
					HTTPManager.ILogger_0.Error("WebSocketTransport", "WSocket_OnError " + string_1);
					base.IConnection_0.Error(string_1);
				}
				else
				{
					AbortFinished();
				}
			}
		}
	}
}
