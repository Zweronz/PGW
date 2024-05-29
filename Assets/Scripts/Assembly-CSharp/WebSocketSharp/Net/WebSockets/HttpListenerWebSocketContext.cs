using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Principal;

namespace WebSocketSharp.Net.WebSockets
{
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		private HttpListenerContext httpListenerContext_0;

		private WebSocket webSocket_0;

		internal Logger Logger_0
		{
			get
			{
				return httpListenerContext_0.HttpListener_0.Logger_0;
			}
		}

		internal Stream Stream_0
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.Stream_0;
			}
		}

		public override CookieCollection CookieCollection_0
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.CookieCollection_0;
			}
		}

		public override NameValueCollection NameValueCollection_0
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0;
			}
		}

		public override string String_1
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0["Host"];
			}
		}

		public override bool Boolean_0
		{
			get
			{
				return httpListenerContext_0.IPrincipal_0 != null;
			}
		}

		public override bool Boolean_1
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.Boolean_2;
			}
		}

		public override bool Boolean_2
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.Boolean_1;
			}
		}

		public override bool Boolean_3
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.Boolean_4;
			}
		}

		public override string String_2
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0["Origin"];
			}
		}

		public override NameValueCollection NameValueCollection_1
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_1;
			}
		}

		public override Uri Uri_0
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.Uri_0;
			}
		}

		public override string String_3
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0["Sec-WebSocket-Key"];
			}
		}

		public override IEnumerable<string> Prop_0
		{
			get
			{
				string text = httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0["Sec-WebSocket-Protocol"];
				if (text != null)
				{
					string[] array = text.Split(',');
					foreach (string text2 in array)
					{
						yield return text2.Trim();
					}
				}
			}
		}

		public override string String_4
		{
			get
			{
				return httpListenerContext_0.HttpListenerRequest_0.NameValueCollection_0["Sec-WebSocket-Version"];
			}
		}

		public override IPEndPoint IPEndPoint_0
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.IPEndPoint_0;
			}
		}

		public override IPrincipal IPrincipal_0
		{
			get
			{
				return httpListenerContext_0.IPrincipal_0;
			}
		}

		public override IPEndPoint IPEndPoint_1
		{
			get
			{
				return httpListenerContext_0.HttpConnection_0.IPEndPoint_1;
			}
		}

		public override WebSocket WebSocket_0
		{
			get
			{
				return webSocket_0;
			}
		}

		internal HttpListenerWebSocketContext(HttpListenerContext httpListenerContext_1, string string_0)
		{
			httpListenerContext_0 = httpListenerContext_1;
			webSocket_0 = new WebSocket(this, string_0);
		}

		internal void Close()
		{
			httpListenerContext_0.HttpConnection_0.Close(true);
		}

		internal void Close(HttpStatusCode httpStatusCode_0)
		{
			httpListenerContext_0.HttpListenerResponse_0.Close(httpStatusCode_0);
		}

		public override string ToString()
		{
			return httpListenerContext_0.HttpListenerRequest_0.ToString();
		}
	}
}
