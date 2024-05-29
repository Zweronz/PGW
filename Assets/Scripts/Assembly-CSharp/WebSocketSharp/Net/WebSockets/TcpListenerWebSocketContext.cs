using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;

namespace WebSocketSharp.Net.WebSockets
{
	internal class TcpListenerWebSocketContext : WebSocketContext
	{
		private CookieCollection cookieCollection_0;

		private Logger logger_0;

		private NameValueCollection nameValueCollection_0;

		private HttpRequest httpRequest_0;

		private bool bool_0;

		private Stream stream_0;

		private TcpClient tcpClient_0;

		private Uri uri_0;

		private IPrincipal iprincipal_0;

		private WebSocket webSocket_0;

		internal string String_0
		{
			get
			{
				return httpRequest_0.String_1;
			}
		}

		internal Logger Logger_0
		{
			get
			{
				return logger_0;
			}
		}

		internal Stream Stream_0
		{
			get
			{
				return stream_0;
			}
		}

		public override CookieCollection CookieCollection_0
		{
			get
			{
				return cookieCollection_0 ?? (cookieCollection_0 = httpRequest_0.CookieCollection_0);
			}
		}

		public override NameValueCollection NameValueCollection_0
		{
			get
			{
				return httpRequest_0.NameValueCollection_0;
			}
		}

		public override string String_1
		{
			get
			{
				return httpRequest_0.NameValueCollection_0["Host"];
			}
		}

		public override bool Boolean_0
		{
			get
			{
				return iprincipal_0 != null;
			}
		}

		public override bool Boolean_1
		{
			get
			{
				return IPEndPoint_1.Address.IsLocal();
			}
		}

		public override bool Boolean_2
		{
			get
			{
				return bool_0;
			}
		}

		public override bool Boolean_3
		{
			get
			{
				return httpRequest_0.Boolean_0;
			}
		}

		public override string String_2
		{
			get
			{
				return httpRequest_0.NameValueCollection_0["Origin"];
			}
		}

		public override NameValueCollection NameValueCollection_1
		{
			get
			{
				return nameValueCollection_0 ?? (nameValueCollection_0 = HttpUtility.InternalParseQueryString((!(uri_0 != null)) ? null : uri_0.Query, Encoding.UTF8));
			}
		}

		public override Uri Uri_0
		{
			get
			{
				return uri_0;
			}
		}

		public override string String_3
		{
			get
			{
				return httpRequest_0.NameValueCollection_0["Sec-WebSocket-Key"];
			}
		}

		public override IEnumerable<string> Prop_0
		{
			get
			{
				string text = httpRequest_0.NameValueCollection_0["Sec-WebSocket-Protocol"];
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
				return httpRequest_0.NameValueCollection_0["Sec-WebSocket-Version"];
			}
		}

		public override IPEndPoint IPEndPoint_0
		{
			get
			{
				return (IPEndPoint)tcpClient_0.Client.LocalEndPoint;
			}
		}

		public override IPrincipal IPrincipal_0
		{
			get
			{
				return iprincipal_0;
			}
		}

		public override IPEndPoint IPEndPoint_1
		{
			get
			{
				return (IPEndPoint)tcpClient_0.Client.RemoteEndPoint;
			}
		}

		public override WebSocket WebSocket_0
		{
			get
			{
				return webSocket_0;
			}
		}

		internal TcpListenerWebSocketContext(TcpClient tcpClient_1, string string_0, bool bool_1, ServerSslConfiguration serverSslConfiguration_0, Logger logger_1)
		{
			tcpClient_0 = tcpClient_1;
			bool_0 = bool_1;
			logger_0 = logger_1;
			NetworkStream stream = tcpClient_1.GetStream();
			if (bool_1)
			{
				SslStream sslStream = new SslStream(stream, false, serverSslConfiguration_0.RemoteCertificateValidationCallback_1);
				sslStream.AuthenticateAsServer(serverSslConfiguration_0.X509Certificate2_0, serverSslConfiguration_0.Boolean_1, serverSslConfiguration_0.SslProtocols_0, serverSslConfiguration_0.Boolean_0);
				stream_0 = sslStream;
			}
			else
			{
				stream_0 = stream;
			}
			httpRequest_0 = HttpRequest.Read(stream_0, 90000);
			uri_0 = HttpUtility.CreateRequestUrl(httpRequest_0.String_2, httpRequest_0.NameValueCollection_0["Host"], httpRequest_0.Boolean_0, bool_1);
			webSocket_0 = new WebSocket(this, string_0);
		}

		internal void Close()
		{
			stream_0.Close();
			tcpClient_0.Close();
		}

		internal void Close(HttpStatusCode httpStatusCode_0)
		{
			webSocket_0.Close(HttpResponse.CreateCloseResponse(httpStatusCode_0));
		}

		internal void SendAuthenticationChallenge(string string_0)
		{
			byte[] array = HttpResponse.CreateUnauthorizedResponse(string_0).ToByteArray();
			stream_0.Write(array, 0, array.Length);
			httpRequest_0 = HttpRequest.Read(stream_0, 15000);
		}

		internal void SetUser(IPrincipal iprincipal_1)
		{
			iprincipal_0 = iprincipal_1;
		}

		public override string ToString()
		{
			return httpRequest_0.ToString();
		}
	}
}
