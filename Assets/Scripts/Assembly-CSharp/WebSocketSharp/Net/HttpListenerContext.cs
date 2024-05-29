using System;
using System.Security.Principal;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Net
{
	public sealed class HttpListenerContext
	{
		private HttpConnection httpConnection_0;

		private string string_0;

		private int int_0;

		private HttpListener httpListener_0;

		private HttpListenerRequest httpListenerRequest_0;

		private HttpListenerResponse httpListenerResponse_0;

		private IPrincipal iprincipal_0;

		internal HttpConnection HttpConnection_0
		{
			get
			{
				return httpConnection_0;
			}
		}

		internal string String_0
		{
			get
			{
				return string_0;
			}
			set
			{
				string_0 = value;
			}
		}

		internal int Int32_0
		{
			get
			{
				return int_0;
			}
			set
			{
				int_0 = value;
			}
		}

		internal bool Boolean_0
		{
			get
			{
				return string_0 != null;
			}
		}

		internal HttpListener HttpListener_0
		{
			get
			{
				return httpListener_0;
			}
			set
			{
				httpListener_0 = value;
			}
		}

		public HttpListenerRequest HttpListenerRequest_0
		{
			get
			{
				return httpListenerRequest_0;
			}
		}

		public HttpListenerResponse HttpListenerResponse_0
		{
			get
			{
				return httpListenerResponse_0;
			}
		}

		public IPrincipal IPrincipal_0
		{
			get
			{
				return iprincipal_0;
			}
			internal set
			{
				iprincipal_0 = value;
			}
		}

		internal HttpListenerContext(HttpConnection httpConnection_1)
		{
			httpConnection_0 = httpConnection_1;
			int_0 = 400;
			httpListenerRequest_0 = new HttpListenerRequest(this);
			httpListenerResponse_0 = new HttpListenerResponse(this);
		}

		public HttpListenerWebSocketContext AcceptWebSocket(string string_1)
		{
			if (string_1 != null)
			{
				if (string_1.Length == 0)
				{
					throw new ArgumentException("An empty string.", "protocol");
				}
				if (!string_1.IsToken())
				{
					throw new ArgumentException("Contains an invalid character.", "protocol");
				}
			}
			return new HttpListenerWebSocketContext(this, string_1);
		}
	}
}
