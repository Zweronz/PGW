using System;
using UnityEngine;
using WebSocketSharp;
using engine.events;
using engine.helpers;

namespace engine.network
{
	public class WebSocketSharpConnection : BaseConnection
	{
		private WebSocket webSocket_0;

		private int int_0 = 3;

		private int int_1;

		private float float_0 = 5f;

		private float float_1;

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
				return webSocket_0 != null && webSocket_0.Boolean_1;
			}
		}

		private new bool Boolean_0
		{
			get
			{
				if (!((BaseConnection)this).Boolean_0)
				{
					return false;
				}
				if (Boolean_1)
				{
					return false;
				}
				return true;
			}
		}

		public WebSocketSharpConnection(string string_2, string string_3)
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
			base.String_0 = (string.IsNullOrEmpty(string_2) ? base.String_0 : string_2);
			if (string.IsNullOrEmpty(base.String_0))
			{
				Log.AddLineError("WebSocketSharpConnection:Ctr. serverUrl is empty??!!");
				return;
			}
			webSocket_0 = new WebSocket(base.String_0);
			webSocket_0.Logger_0.Action_0 = WebsocketLog;
			webSocket_0.customHeaders.Add("PGUN-Client-Key", base.String_1);
			webSocket_0.OnOpen += OnOpen;
			webSocket_0.OnMessage += OnMessage;
			webSocket_0.OnClose += OnClosed;
			webSocket_0.OnError += OnError;
			webSocket_0.Connect();
		}

		public override void CloseConnect()
		{
			((BaseConnection)this).Boolean_0 = false;
			if (!Boolean_1)
			{
				Log.AddLine("[WebSocketConnectionSta::CloseConnect. Websocket connection already close!]", Log.LogLevel.WARNING);
				return;
			}
			Log.AddLine("[WebSocketConnectionSta::CloseConnect. Websocket connection close!]");
			ReleaseConnect();
		}

		public override void Send(byte[] byte_0, AbstractNetworkCommand abstractNetworkCommand_0)
		{
			if (Boolean_1)
			{
				base.Send(byte_0, abstractNetworkCommand_0);
				string string_ = string.Format("{0}\n", Convert.ToBase64String(byte_0));
				webSocket_0.Send(string_);
			}
		}

		private void ReleaseConnect()
		{
			if (webSocket_0 != null)
			{
				webSocket_0.OnOpen -= OnOpen;
				webSocket_0.OnMessage -= OnMessage;
				webSocket_0.OnClose -= OnClosed;
				webSocket_0.OnError -= OnError;
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
						string_0 = "[WebSocketSharpConnection. Reconnect failure!]"
					}, ConnectionStatus.CONNECT_FAILURE);
				}
				else
				{
					Connect(string.Empty);
				}
			}
		}

		private void WebsocketLog(LogData logData_0, string string_2)
		{
			Log.LogLevel logLevel_ = Log.LogLevel.INFO;
			if (logData_0 != null)
			{
				switch (logData_0.LogLevel_0)
				{
				case LogLevel.Warn:
					logLevel_ = Log.LogLevel.WARNING;
					break;
				case LogLevel.Error:
					logLevel_ = Log.LogLevel.ERROR;
					break;
				case LogLevel.Fatal:
					logLevel_ = Log.LogLevel.FATAL;
					break;
				}
				Log.AddLine("[WebSocketSharpConnection::WebsocketLog. logData]: ", logData_0, logLevel_);
			}
			if (!string.IsNullOrEmpty(string_2))
			{
				Log.AddLine("[WebSocketSharpConnection::WebsocketLog. message]: " + string_2, logLevel_);
			}
		}

		private void OnOpen(object sender, EventArgs e)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				OnOpen();
			});
		}

		private void OnClosed(object sender, CloseEventArgs e)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				OnClosed(string.Format("Websocket connection close! Code {0}, Message: {1}", e.UInt16_0, e.String_0));
			});
		}

		private void OnError(object sender, ErrorEventArgs e)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				string string_ = string.Format("An error occured! Server status message: {0} {1}", e.String_0, (e.Exception_0 == null) ? string.Empty : ("Exception: " + e.Exception_0.Message));
				OnError(string_);
			});
		}

		private void OnMessage(object sender, MessageEventArgs e)
		{
			UnityThreadHelper.Dispatcher_0.Dispatch(delegate
			{
				if (e.Opcode_0 == Opcode.Binary)
				{
					OnBinary(e.Byte_0);
				}
				else if (e.Opcode_0 == Opcode.Text)
				{
					OnMessage(e.String_0);
				}
			});
		}

		protected override void OnOpen()
		{
			int_1 = 0;
			Log.AddLine(string.Format("[WebSocketSharpConnection::OnOpen. Websocket connection open!]: server url: {0}, auth key: {1}", base.String_0, base.String_1));
			base.OnOpen();
		}

		protected override void OnClosed(string string_2)
		{
			Log.AddLine(string.Format("[WebSocketSharpConnection::OnClosed. Websocket connection closed!]: message: {0}", string_2));
			ReleaseConnect();
			base.OnClosed(string_2);
			Reconnect();
		}

		protected override void OnError(string string_2)
		{
			Log.AddLine(string.Format("[WebSocketSharpConnection::OnError. Websocket connection error!]: message: {0}", string_2), Log.LogLevel.WARNING);
			ReleaseConnect();
			base.OnError(string_2);
			Reconnect();
		}
	}
}
