using System;
using BestHTTP;
using BestHTTP.WebSocket;
using UnityEngine;
using engine.events;
using engine.helpers;

namespace engine.network
{
	public class WebSocketConnection : BaseConnection
	{
		private WebSocket webSocket_0;

		private int int_0 = 3;

		private int int_1;

		private float float_0 = 5f;

		private float float_1;

		private new bool Boolean_0
		{
			get
			{
				if (!((BaseConnection)this).Boolean_0)
				{
					Log.AddLine("[WebSocketConnection::Reconnect. Reconnect not needed!]");
					return false;
				}
				if (Boolean_1)
				{
					Log.AddLine("[WebSocketConnection::Reconnect. Websocket already connection, reconnect not needed!]");
					return false;
				}
				return true;
			}
		}

		public override ConnectionType ConnectionType_0
		{
			get
			{
				return ConnectionType.WEB_SOCKET;
			}
		}

		public override bool Boolean_1
		{
			get
			{
				return webSocket_0 != null && webSocket_0.Boolean_0;
			}
		}

		public WebSocketConnection(string string_2, string string_3)
			: base(string_2, string_3)
		{
			((BaseConnection)this).Boolean_0 = true;
		}

		public override void Connect(string string_2)
		{
			if (Boolean_1)
			{
				CloseConnect();
			}
			else
			{
				ReleaseConnect();
			}
			((BaseConnection)this).Boolean_0 = true;
			webSocket_0 = new WebSocket(new Uri(base.String_0));
			webSocket_0.Int32_0 = 5000;
			webSocket_0.Boolean_1 = true;
			webSocket_0.AddCustomHeader("PGUN-Client-Key", base.String_1);
			WebSocket webSocket = webSocket_0;
			webSocket.onWebSocketOpenDelegate_0 = (OnWebSocketOpenDelegate)Delegate.Combine(webSocket.onWebSocketOpenDelegate_0, new OnWebSocketOpenDelegate(OnOpen));
			WebSocket webSocket2 = webSocket_0;
			webSocket2.onWebSocketMessageDelegate_0 = (OnWebSocketMessageDelegate)Delegate.Combine(webSocket2.onWebSocketMessageDelegate_0, new OnWebSocketMessageDelegate(OnMessage));
			WebSocket webSocket3 = webSocket_0;
			webSocket3.onWebSocketBinaryDelegate_0 = (OnWebSocketBinaryDelegate)Delegate.Combine(webSocket3.onWebSocketBinaryDelegate_0, new OnWebSocketBinaryDelegate(OnBinary));
			WebSocket webSocket4 = webSocket_0;
			webSocket4.onWebSocketClosedDelegate_0 = (OnWebSocketClosedDelegate)Delegate.Combine(webSocket4.onWebSocketClosedDelegate_0, new OnWebSocketClosedDelegate(OnClosed));
			WebSocket webSocket5 = webSocket_0;
			webSocket5.onWebSocketErrorDelegate_0 = (OnWebSocketErrorDelegate)Delegate.Combine(webSocket5.onWebSocketErrorDelegate_0, new OnWebSocketErrorDelegate(OnError));
			HTTPManager.TimeSpan_1 = TimeSpan.FromSeconds(7.0);
			webSocket_0.Open();
		}

		public override void CloseConnect()
		{
			if (!Boolean_1)
			{
				Log.AddLine("[WebSocketConnection::CloseConnect. Websocket connection already close!]", Log.LogLevel.WARNING);
				return;
			}
			Log.AddLine("[WebSocketConnection::CloseConnect. Websocket connection close!]");
			webSocket_0.Close();
			ReleaseConnect();
		}

		public override void Send(byte[] byte_0, AbstractNetworkCommand abstractNetworkCommand_0)
		{
			if (Boolean_1)
			{
				base.Send(byte_0, abstractNetworkCommand_0);
				string text = string.Format("{0}\n", Convert.ToBase64String(byte_0));
				webSocket_0.Send(text);
			}
		}

		private void ReleaseConnect()
		{
			if (webSocket_0 != null)
			{
				WebSocket webSocket = webSocket_0;
				webSocket.onWebSocketOpenDelegate_0 = (OnWebSocketOpenDelegate)Delegate.Remove(webSocket.onWebSocketOpenDelegate_0, new OnWebSocketOpenDelegate(OnOpen));
				WebSocket webSocket2 = webSocket_0;
				webSocket2.onWebSocketMessageDelegate_0 = (OnWebSocketMessageDelegate)Delegate.Remove(webSocket2.onWebSocketMessageDelegate_0, new OnWebSocketMessageDelegate(OnMessage));
				WebSocket webSocket3 = webSocket_0;
				webSocket3.onWebSocketBinaryDelegate_0 = (OnWebSocketBinaryDelegate)Delegate.Remove(webSocket3.onWebSocketBinaryDelegate_0, new OnWebSocketBinaryDelegate(OnBinary));
				WebSocket webSocket4 = webSocket_0;
				webSocket4.onWebSocketClosedDelegate_0 = (OnWebSocketClosedDelegate)Delegate.Remove(webSocket4.onWebSocketClosedDelegate_0, new OnWebSocketClosedDelegate(OnClosed));
				WebSocket webSocket5 = webSocket_0;
				webSocket5.onWebSocketErrorDelegate_0 = (OnWebSocketErrorDelegate)Delegate.Remove(webSocket5.onWebSocketErrorDelegate_0, new OnWebSocketErrorDelegate(OnError));
				webSocket_0 = null;
			}
		}

		private void Reconnect()
		{
			if (Boolean_0)
			{
				float_1 = Time.time + float_0;
				if (!DependSceneEvent<MainUpdate>.Contains(Update))
				{
					DependSceneEvent<MainUpdate>.GlobalSubscribe(Update);
				}
			}
		}

		private void Update()
		{
			if (!(float_1 > Time.time))
			{
				if (DependSceneEvent<MainUpdate>.Contains(Update))
				{
					DependSceneEvent<MainUpdate>.GlobalUnsubscribe(Update);
				}
				if (!Boolean_0)
				{
					int_1 = 0;
				}
				else if (++int_1 > int_0)
				{
					int_1 = 0;
					((BaseConnection)this).Boolean_0 = false;
					EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>().Dispatch(new ConnectionStatusEventArg
					{
						string_0 = "[WebSocketConnection. Reconnect failure!]"
					}, ConnectionStatus.CONNECT_FAILURE);
				}
				else
				{
					Connect(string.Empty);
				}
			}
		}

		private void OnOpen(WebSocket webSocket_1)
		{
			OnOpen();
		}

		protected override void OnOpen()
		{
			int_1 = 0;
			Log.AddLine(string.Format("[WebSocketConnection::OnOpen. Websocket connection open!]: server url: {0}, auth key: {1}", base.String_0, base.String_1));
			base.OnOpen();
		}

		private void OnMessage(WebSocket webSocket_1, string string_2)
		{
			OnMessage(string_2);
		}

		private void OnBinary(WebSocket webSocket_1, byte[] byte_0)
		{
			OnBinary(byte_0);
		}

		private void OnClosed(WebSocket webSocket_1, ushort ushort_0, string string_2)
		{
			OnClosed(string.Format("Websocket connection close! Code {0}, Message: {1}", ushort_0, string_2));
		}

		protected override void OnClosed(string string_2)
		{
			Log.AddLine(string.Format("[WebSocketConnection::OnClosed. Websocket connection closed!]: message: {0}", string_2));
			ReleaseConnect();
			base.OnClosed(string_2);
			Reconnect();
		}

		private void OnError(WebSocket webSocket_1, Exception exception_0)
		{
			string text = string.Empty;
			if (webSocket_1.HTTPRequest_0.HTTPResponse_0 != null)
			{
				text = string.Format("Status Code from Server: {0} and Message: {1}", webSocket_1.HTTPRequest_0.HTTPResponse_0.Int32_2, webSocket_1.HTTPRequest_0.HTTPResponse_0.String_0);
			}
			text = "An error occured: Server status message: " + text + ((exception_0 == null) ? string.Empty : ("Exception error: " + exception_0.Message));
			OnError(text);
		}

		protected override void OnError(string string_2)
		{
			Log.AddLine(string.Format("[WebSocketConnection::OnError. Websocket connection error!]: message: {0}", string_2), Log.LogLevel.WARNING);
			ReleaseConnect();
			base.OnError(string_2);
			Reconnect();
		}
	}
}
