using System;
using System.Runtime.CompilerServices;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket
{
	public sealed class WebSocket
	{
		public OnWebSocketOpenDelegate onWebSocketOpenDelegate_0;

		public OnWebSocketMessageDelegate onWebSocketMessageDelegate_0;

		public OnWebSocketBinaryDelegate onWebSocketBinaryDelegate_0;

		public OnWebSocketClosedDelegate onWebSocketClosedDelegate_0;

		public OnWebSocketErrorDelegate onWebSocketErrorDelegate_0;

		public OnWebSocketErrorDescriptionDelegate onWebSocketErrorDescriptionDelegate_0;

		public OnWebSocketIncompleteFrameDelegate onWebSocketIncompleteFrameDelegate_0;

		private bool bool_0;

		private WebSocketResponse webSocketResponse_0;

		[CompilerGenerated]
		private HTTPRequest httprequest_0;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private int int_0;

		public HTTPRequest HTTPRequest_0
		{
			[CompilerGenerated]
			get
			{
				return httprequest_0;
			}
			[CompilerGenerated]
			private set
			{
				httprequest_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return webSocketResponse_0 != null && !webSocketResponse_0.Boolean_6;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			set
			{
				bool_1 = value;
			}
		}

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public WebSocket(Uri uri_0)
			: this(uri_0, string.Empty, string.Empty)
		{
		}

		public WebSocket(Uri uri_0, string string_0, string string_1 = "")
		{
			Int32_0 = 1000;
			if (uri_0.Port == -1)
			{
				uri_0 = new Uri(uri_0.Scheme + "://" + uri_0.Host + ":" + ((!uri_0.Scheme.Equals("wss", StringComparison.OrdinalIgnoreCase)) ? "80" : "443") + uri_0.PathAndQuery);
			}
			HTTPRequest_0 = new HTTPRequest(uri_0, OnInternalRequestCallback);
			HTTPRequest_0.onRequestFinishedDelegate_0 = OnInternalRequestUpgraded;
			HTTPRequest_0.SetHeader("Host", uri_0.Host + ":" + uri_0.Port);
			HTTPRequest_0.SetHeader("Upgrade", "websocket");
			HTTPRequest_0.SetHeader("Connection", "keep-alive, Upgrade");
			HTTPRequest_0.SetHeader("Sec-WebSocket-Key", GetSecKey(new object[4]
			{
				this,
				HTTPRequest_0,
				uri_0,
				new object()
			}));
			if (!string.IsNullOrEmpty(string_0))
			{
				HTTPRequest_0.SetHeader("Origin", string_0);
			}
			HTTPRequest_0.SetHeader("Sec-WebSocket-Version", "13");
			if (!string.IsNullOrEmpty(string_1))
			{
				HTTPRequest_0.SetHeader("Sec-WebSocket-Protocol", string_1);
			}
			HTTPRequest_0.SetHeader("Cache-Control", "no-cache");
			HTTPRequest_0.SetHeader("Pragma", "no-cache");
			HTTPRequest_0.Boolean_3 = true;
			if (HTTPManager.HTTPProxy_0 != null)
			{
				HTTPRequest_0.HTTPProxy_0 = new HTTPProxy(HTTPManager.HTTPProxy_0.Uri_0, HTTPManager.HTTPProxy_0.Credentials_0, false, false, HTTPManager.HTTPProxy_0.Boolean_2);
			}
		}

		private void OnInternalRequestCallback(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			string empty = string.Empty;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			default:
				return;
			case HTTPRequestStates.Finished:
				if (!httpresponse_0.Boolean_0 && httpresponse_0.Int32_2 != 101)
				{
					empty = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
					break;
				}
				HTTPManager.ILogger_0.Information("WebSocket", string.Format("Request finished. Status Code: {0} Message: {1}", httpresponse_0.Int32_2.ToString(), httpresponse_0.String_0));
				return;
			case HTTPRequestStates.Error:
				empty = "Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? string.Empty : ("Exception: " + httprequest_1.Exception_0.Message + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.Aborted:
				empty = "Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				empty = "Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				empty = "Processing the request Timed Out!";
				break;
			}
			if (onWebSocketErrorDelegate_0 != null)
			{
				onWebSocketErrorDelegate_0(this, httprequest_1.Exception_0);
			}
			if (onWebSocketErrorDescriptionDelegate_0 != null)
			{
				onWebSocketErrorDescriptionDelegate_0(this, empty);
			}
			if (onWebSocketErrorDelegate_0 == null && onWebSocketErrorDescriptionDelegate_0 == null)
			{
				HTTPManager.ILogger_0.Error("WebSocket", empty);
			}
		}

		private void OnInternalRequestUpgraded(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			webSocketResponse_0 = httpresponse_0 as WebSocketResponse;
			if (webSocketResponse_0 == null)
			{
				if (onWebSocketErrorDelegate_0 != null)
				{
					onWebSocketErrorDelegate_0(this, httprequest_1.Exception_0);
				}
				if (onWebSocketErrorDescriptionDelegate_0 != null)
				{
					string string_ = string.Empty;
					if (httprequest_1.Exception_0 != null)
					{
						string_ = httprequest_1.Exception_0.Message + " " + httprequest_1.Exception_0.StackTrace;
					}
					onWebSocketErrorDescriptionDelegate_0(this, string_);
				}
				return;
			}
			if (onWebSocketOpenDelegate_0 != null)
			{
				try
				{
					onWebSocketOpenDelegate_0(this);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("WebSocket", "OnOpen", ex);
				}
			}
			webSocketResponse_0.action_0 = delegate(WebSocketResponse webSocketResponse_1, string string_0)
			{
				if (onWebSocketMessageDelegate_0 != null)
				{
					onWebSocketMessageDelegate_0(this, string_0);
				}
			};
			webSocketResponse_0.action_1 = delegate(WebSocketResponse webSocketResponse_1, byte[] byte_0)
			{
				if (onWebSocketBinaryDelegate_0 != null)
				{
					onWebSocketBinaryDelegate_0(this, byte_0);
				}
			};
			webSocketResponse_0.action_3 = delegate(WebSocketResponse webSocketResponse_1, ushort ushort_0, string string_0)
			{
				if (onWebSocketClosedDelegate_0 != null)
				{
					onWebSocketClosedDelegate_0(this, ushort_0, string_0);
				}
			};
			if (onWebSocketIncompleteFrameDelegate_0 != null)
			{
				webSocketResponse_0.action_2 = delegate(WebSocketResponse webSocketResponse_1, WebSocketFrameReader webSocketFrameReader_0)
				{
					if (onWebSocketIncompleteFrameDelegate_0 != null)
					{
						onWebSocketIncompleteFrameDelegate_0(this, webSocketFrameReader_0);
					}
				};
			}
			if (Boolean_1)
			{
				webSocketResponse_0.StartPinging(Math.Max(Int32_0, 100));
			}
			webSocketResponse_0.StartReceive();
		}

		public void AddCustomHeader(string string_0, string string_1)
		{
			if (!bool_0 && HTTPRequest_0 != null)
			{
				HTTPRequest_0.SetHeader(string_0, string_1);
			}
		}

		public void Open()
		{
			if (!bool_0 && HTTPRequest_0 != null)
			{
				HTTPRequest_0.Send();
				bool_0 = true;
			}
		}

		public void Send(string string_0)
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Send(string_0);
			}
		}

		public void Send(byte[] byte_0)
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Send(byte_0);
			}
		}

		public void Send(byte[] byte_0, ulong ulong_0, ulong ulong_1)
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Send(byte_0, ulong_0, ulong_1);
			}
		}

		public void Send(IWebSocketFrameWriter iwebSocketFrameWriter_0)
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Send(iwebSocketFrameWriter_0);
			}
		}

		public void Close()
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Close();
			}
		}

		public void Close(ushort ushort_0, string string_0)
		{
			if (Boolean_0)
			{
				webSocketResponse_0.Close(ushort_0, string_0);
			}
		}

		private string GetSecKey(object[] object_0)
		{
			byte[] array = new byte[16];
			int num = 0;
			for (int i = 0; i < object_0.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(object_0[i].GetHashCode());
				for (int j = 0; j < bytes.Length; j++)
				{
					if (num >= array.Length)
					{
						break;
					}
					array[num++] = bytes[j];
				}
			}
			return Convert.ToBase64String(array);
		}
	}
}
