using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.Logger;
using BestHTTP.WebSocket;

namespace BestHTTP.SocketIO.Transports
{
	internal sealed class WebSocketTransport : ITransport
	{
		private Packet packet_0;

		private byte[] byte_0;

		[CompilerGenerated]
		private TransportStates transportStates_0;

		[CompilerGenerated]
		private SocketManager socketManager_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private BestHTTP.WebSocket.WebSocket webSocket_0;

		public TransportStates TransportStates_0
		{
			[CompilerGenerated]
			get
			{
				return transportStates_0;
			}
			[CompilerGenerated]
			private set
			{
				transportStates_0 = value;
			}
		}

		public SocketManager SocketManager_0
		{
			[CompilerGenerated]
			get
			{
				return socketManager_0;
			}
			[CompilerGenerated]
			private set
			{
				socketManager_0 = value;
			}
		}

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public BestHTTP.WebSocket.WebSocket WebSocket_0
		{
			[CompilerGenerated]
			get
			{
				return webSocket_0;
			}
			[CompilerGenerated]
			private set
			{
				webSocket_0 = value;
			}
		}

		public WebSocketTransport(SocketManager socketManager_1)
		{
			TransportStates_0 = TransportStates.Closed;
			SocketManager_0 = socketManager_1;
		}

		public void Open()
		{
			if (TransportStates_0 == TransportStates.Closed)
			{
				Uri uri_ = new Uri(string.Format("{0}?transport=websocket&sid={1}{2}", new UriBuilder("ws", SocketManager_0.Uri_0.Host, SocketManager_0.Uri_0.Port, SocketManager_0.Uri_0.PathAndQuery).Uri.ToString(), SocketManager_0.HandshakeData_0.String_0, SocketManager_0.SocketOptions_0.Boolean_2 ? string.Empty : SocketManager_0.SocketOptions_0.BuildQueryParams()));
				WebSocket_0 = new BestHTTP.WebSocket.WebSocket(uri_);
				WebSocket_0.onWebSocketOpenDelegate_0 = OnOpen;
				WebSocket_0.onWebSocketMessageDelegate_0 = OnMessage;
				WebSocket_0.onWebSocketBinaryDelegate_0 = OnBinary;
				WebSocket_0.onWebSocketErrorDelegate_0 = OnError;
				WebSocket_0.onWebSocketClosedDelegate_0 = OnClosed;
				WebSocket_0.Open();
				TransportStates_0 = TransportStates.Connecting;
			}
		}

		public void Close()
		{
			if (TransportStates_0 != TransportStates.Closed)
			{
				TransportStates_0 = TransportStates.Closed;
				WebSocket_0.Close();
				WebSocket_0 = null;
			}
		}

		public void Poll()
		{
		}

		private void OnOpen(BestHTTP.WebSocket.WebSocket webSocket_1)
		{
			HTTPManager.ILogger_0.Information("WebSocketTransport", "OnOpen");
			TransportStates_0 = TransportStates.Opening;
			Send(new Packet(TransportEventTypes.Ping, SocketIOEventTypes.Unknown, "/", "probe"));
		}

		private void OnMessage(BestHTTP.WebSocket.WebSocket webSocket_1, string string_0)
		{
			if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
			{
				HTTPManager.ILogger_0.Verbose("WebSocketTransport", "OnMessage: " + string_0);
			}
			try
			{
				Packet packet = new Packet(string_0);
				if (packet.Int32_0 == 0)
				{
					OnPacket(packet);
				}
				else
				{
					packet_0 = packet;
				}
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("WebSocketTransport", "OnMessage", ex);
			}
		}

		private void OnBinary(BestHTTP.WebSocket.WebSocket webSocket_1, byte[] byte_1)
		{
			if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
			{
				HTTPManager.ILogger_0.Verbose("WebSocketTransport", "OnBinary");
			}
			if (packet_0 == null)
			{
				return;
			}
			packet_0.AddAttachmentFromServer(byte_1, false);
			if (!packet_0.Boolean_0)
			{
				return;
			}
			try
			{
				OnPacket(packet_0);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("WebSocketTransport", "OnBinary", ex);
			}
			finally
			{
				packet_0 = null;
			}
		}

		private void OnError(BestHTTP.WebSocket.WebSocket webSocket_1, Exception exception_0)
		{
			string text = string.Empty;
			if (exception_0 != null)
			{
				text = exception_0.Message + " " + exception_0.StackTrace;
			}
			else
			{
				switch (webSocket_1.HTTPRequest_0.HTTPRequestStates_0)
				{
				case HTTPRequestStates.Finished:
					text = ((webSocket_1.HTTPRequest_0.HTTPResponse_0.Boolean_0 || webSocket_1.HTTPRequest_0.HTTPResponse_0.Int32_2 == 101) ? string.Format("Request finished. Status Code: {0} Message: {1}", webSocket_1.HTTPRequest_0.HTTPResponse_0.Int32_2.ToString(), webSocket_1.HTTPRequest_0.HTTPResponse_0.String_0) : string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", webSocket_1.HTTPRequest_0.HTTPResponse_0.Int32_2, webSocket_1.HTTPRequest_0.HTTPResponse_0.String_0, webSocket_1.HTTPRequest_0.HTTPResponse_0.String_1));
					break;
				case HTTPRequestStates.Error:
					text = (("Request Finished with Error! : " + webSocket_1.HTTPRequest_0.Exception_0 == null) ? string.Empty : (webSocket_1.HTTPRequest_0.Exception_0.Message + " " + webSocket_1.HTTPRequest_0.Exception_0.StackTrace));
					break;
				case HTTPRequestStates.Aborted:
					text = "Request Aborted!";
					break;
				case HTTPRequestStates.ConnectionTimedOut:
					text = "Connection Timed Out!";
					break;
				case HTTPRequestStates.TimedOut:
					text = "Processing the request Timed Out!";
					break;
				}
			}
			HTTPManager.ILogger_0.Error("WebSocketTransport", "OnError: " + text);
			((IManager)SocketManager_0).OnTransportError((ITransport)this, text);
		}

		private void OnClosed(BestHTTP.WebSocket.WebSocket webSocket_1, ushort ushort_0, string string_0)
		{
			HTTPManager.ILogger_0.Information("WebSocketTransport", "OnClosed");
			Close();
			((IManager)SocketManager_0).TryToReconnect();
		}

		public void Send(Packet packet)
		{
			if (TransportStates_0 == TransportStates.Closed || TransportStates_0 == TransportStates.Paused)
			{
				return;
			}
			string text = packet.Encode();
			if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
			{
				HTTPManager.ILogger_0.Verbose("WebSocketTransport", "Send: " + text);
			}
			if (packet.Int32_0 != 0 || (packet.List_0 != null && packet.List_0.Count != 0))
			{
				if (packet.List_0 == null)
				{
					throw new ArgumentException("packet.Attachments are null!");
				}
				if (packet.Int32_0 != packet.List_0.Count)
				{
					throw new ArgumentException("packet.AttachmentCount != packet.Attachments.Count. Use the packet.AddAttachment function to add data to a packet!");
				}
			}
			WebSocket_0.Send(text);
			if (packet.Int32_0 == 0)
			{
				return;
			}
			int num = packet.List_0[0].Length + 1;
			for (int i = 1; i < packet.List_0.Count; i++)
			{
				if (packet.List_0[i].Length + 1 > num)
				{
					num = packet.List_0[i].Length + 1;
				}
			}
			if (byte_0 == null || byte_0.Length < num)
			{
				Array.Resize(ref byte_0, num);
			}
			for (int j = 0; j < packet.Int32_0; j++)
			{
				byte_0[0] = 4;
				Array.Copy(packet.List_0[j], 0, byte_0, 1, packet.List_0[j].Length);
				WebSocket_0.Send(byte_0, 0uL, (ulong)(packet.List_0[j].Length + 1L));
			}
		}

		public void Send(List<Packet> packets)
		{
			for (int i = 0; i < packets.Count; i++)
			{
				Send(packets[i]);
			}
			packets.Clear();
		}

		private void OnPacket(Packet packet_1)
		{
			switch (packet_1.TransportEventTypes_0)
			{
			case TransportEventTypes.Message:
				if (packet_1.SocketIOEventTypes_0 == SocketIOEventTypes.Connect && TransportStates_0 == TransportStates.Opening)
				{
					TransportStates_0 = TransportStates.Open;
					if (!((IManager)SocketManager_0).OnTransportConnected((ITransport)this))
					{
						return;
					}
				}
				break;
			case TransportEventTypes.Pong:
				if (packet_1.String_1 == "probe")
				{
					HTTPManager.ILogger_0.Information("WebSocketTransport", "\"probe\" packet received, sending Upgrade packet");
					Send(new Packet(TransportEventTypes.Upgrade, SocketIOEventTypes.Event, "/", string.Empty));
				}
				break;
			}
			((IManager)SocketManager_0).OnPacket(packet_1);
		}
	}
}
