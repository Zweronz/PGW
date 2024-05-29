using System;
using System.IO;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public abstract class WebSocketBehavior : IWebSocketSession
	{
		private WebSocketContext webSocketContext_0;

		private Func<CookieCollection, CookieCollection, bool> func_0;

		private bool bool_0;

		private string string_0;

		private bool bool_1;

		private Func<string, bool> func_1;

		private string string_1;

		private WebSocketSessionManager webSocketSessionManager_0;

		private DateTime dateTime_0;

		private WebSocket webSocket_0;

		protected Logger Logger_0
		{
			get
			{
				return (webSocket_0 == null) ? null : webSocket_0.Logger_0;
			}
		}

		protected WebSocketSessionManager WebSocketSessionManager_0
		{
			get
			{
				return webSocketSessionManager_0;
			}
		}

		public WebSocketContext WebSocketContext_0
		{
			get
			{
				return webSocketContext_0;
			}
		}

		public Func<CookieCollection, CookieCollection, bool> Func_0
		{
			get
			{
				return func_0;
			}
			set
			{
				func_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return (webSocket_0 == null) ? bool_0 : webSocket_0.Boolean_2;
			}
			set
			{
				if (webSocket_0 != null)
				{
					webSocket_0.Boolean_2 = value;
				}
				else
				{
					bool_0 = value;
				}
			}
		}

		public string String_0
		{
			get
			{
				return string_0;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_1;
			}
			set
			{
				bool_1 = value;
			}
		}

		public Func<string, bool> Func_1
		{
			get
			{
				return func_1;
			}
			set
			{
				func_1 = value;
			}
		}

		public string String_1
		{
			get
			{
				return (webSocket_0 == null) ? (string_1 ?? string.Empty) : webSocket_0.String_2;
			}
			set
			{
				if (WebSocketState_0 == WebSocketState.Connecting && (value == null || (value.Length != 0 && value.IsToken())))
				{
					string_1 = value;
				}
			}
		}

		public DateTime DateTime_0
		{
			get
			{
				return dateTime_0;
			}
		}

		public WebSocketState WebSocketState_0
		{
			get
			{
				return (webSocket_0 != null) ? webSocket_0.WebSocketState_0 : WebSocketState.Connecting;
			}
		}

		protected WebSocketBehavior()
		{
			dateTime_0 = DateTime.MaxValue;
		}

		private string checkIfValidConnectionRequest(WebSocketContext webSocketContext_1)
		{
			return (func_1 != null && !func_1(webSocketContext_1.String_2)) ? "Invalid Origin header." : ((func_0 == null || func_0(webSocketContext_1.CookieCollection_0, webSocketContext_1.WebSocket_0.CookieCollection_0)) ? null : "Invalid Cookies.");
		}

		private void onClose(object sender, CloseEventArgs e)
		{
			if (string_0 != null)
			{
				webSocketSessionManager_0.Remove(string_0);
				OnClose(e);
			}
		}

		private void onError(object sender, ErrorEventArgs e)
		{
			OnError(e);
		}

		private void onMessage(object sender, MessageEventArgs e)
		{
			OnMessage(e);
		}

		private void onOpen(object sender, EventArgs e)
		{
			string_0 = webSocketSessionManager_0.Add(this);
			if (string_0 == null)
			{
				webSocket_0.Close(CloseStatusCode.Away);
				return;
			}
			dateTime_0 = DateTime.Now;
			OnOpen();
		}

		internal void Start(WebSocketContext webSocketContext_1, WebSocketSessionManager webSocketSessionManager_1)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.Logger_0.Error("This session has already been started.");
				webSocketContext_1.WebSocket_0.Close(HttpStatusCode.ServiceUnavailable);
				return;
			}
			webSocketContext_0 = webSocketContext_1;
			webSocketSessionManager_0 = webSocketSessionManager_1;
			webSocket_0 = webSocketContext_1.WebSocket_0;
			webSocket_0.Func_0 = checkIfValidConnectionRequest;
			webSocket_0.Boolean_2 = bool_0;
			webSocket_0.Boolean_0 = bool_1;
			webSocket_0.String_2 = string_1;
			TimeSpan timeSpan_ = webSocketSessionManager_1.TimeSpan_0;
			if (timeSpan_ != webSocket_0.TimeSpan_0)
			{
				webSocket_0.TimeSpan_0 = timeSpan_;
			}
			webSocket_0.OnOpen += onOpen;
			webSocket_0.OnMessage += onMessage;
			webSocket_0.OnError += onError;
			webSocket_0.OnClose += onClose;
			webSocket_0.InternalAccept();
		}

		protected void Error(string string_2, Exception exception_0)
		{
			if (string_2 != null && string_2.Length > 0)
			{
				OnError(new ErrorEventArgs(string_2, exception_0));
			}
		}

		protected virtual void OnClose(CloseEventArgs closeEventArgs_0)
		{
		}

		protected virtual void OnError(ErrorEventArgs errorEventArgs_0)
		{
		}

		protected virtual void OnMessage(MessageEventArgs messageEventArgs_0)
		{
		}

		protected virtual void OnOpen()
		{
		}

		protected void Send(byte[] byte_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.Send(byte_0);
			}
		}

		protected void Send(FileInfo fileInfo_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.Send(fileInfo_0);
			}
		}

		protected void Send(string string_2)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.Send(string_2);
			}
		}

		protected void SendAsync(byte[] byte_0, Action<bool> action_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.SendAsync(byte_0, action_0);
			}
		}

		protected void SendAsync(FileInfo fileInfo_0, Action<bool> action_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.SendAsync(fileInfo_0, action_0);
			}
		}

		protected void SendAsync(string string_2, Action<bool> action_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.SendAsync(string_2, action_0);
			}
		}

		protected void SendAsync(Stream stream_0, int int_0, Action<bool> action_0)
		{
			if (webSocket_0 != null)
			{
				webSocket_0.SendAsync(stream_0, int_0, action_0);
			}
		}
	}
}
