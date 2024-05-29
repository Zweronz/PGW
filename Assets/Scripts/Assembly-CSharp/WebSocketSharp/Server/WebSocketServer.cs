using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	public class WebSocketServer
	{
		private IPAddress ipaddress_0;

		private WebSocketSharp.Net.AuthenticationSchemes authenticationSchemes_0;

		private Func<IIdentity, WebSocketSharp.Net.NetworkCredential> func_0;

		private bool bool_0;

		private string string_0;

		private TcpListener tcpListener_0;

		private Logger logger_0;

		private int int_0;

		private string string_1;

		private Thread thread_0;

		private bool bool_1;

		private bool bool_2;

		private WebSocketServiceManager webSocketServiceManager_0;

		private ServerSslConfiguration serverSslConfiguration_0;

		private volatile ServerState serverState_0;

		private object object_0;

		[CompilerGenerated]
		private static Func<IIdentity, WebSocketSharp.Net.NetworkCredential> func_1;

		public IPAddress IPAddress_0
		{
			get
			{
				return ipaddress_0;
			}
		}

		public WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes_0
		{
			get
			{
				return authenticationSchemes_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					authenticationSchemes_0 = value;
				}
			}
		}

		public bool Boolean_0
		{
			get
			{
				return serverState_0 == ServerState.Start;
			}
		}

		public bool Boolean_1
		{
			get
			{
				return bool_2;
			}
		}

		public bool Boolean_2
		{
			get
			{
				return webSocketServiceManager_0.Boolean_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					webSocketServiceManager_0.Boolean_0 = value;
				}
			}
		}

		public Logger Logger_0
		{
			get
			{
				return logger_0;
			}
		}

		public int Int32_0
		{
			get
			{
				return int_0;
			}
		}

		public string String_0
		{
			get
			{
				return string_1 ?? (string_1 = "SECRET AREA");
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					string_1 = value;
				}
			}
		}

		public bool Boolean_3
		{
			get
			{
				return bool_1;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					bool_1 = value;
				}
			}
		}

		public ServerSslConfiguration ServerSslConfiguration_0
		{
			get
			{
				return serverSslConfiguration_0 ?? (serverSslConfiguration_0 = new ServerSslConfiguration(null));
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					serverSslConfiguration_0 = value;
				}
			}
		}

		public Func<IIdentity, WebSocketSharp.Net.NetworkCredential> Func_0
		{
			get
			{
				return (IIdentity iidentity_0) => null;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false);
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					func_0 = value;
				}
			}
		}

		public TimeSpan TimeSpan_0
		{
			get
			{
				return webSocketServiceManager_0.TimeSpan_0;
			}
			set
			{
				string text = serverState_0.CheckIfAvailable(true, false, false) ?? value.CheckIfValidWaitTime();
				if (text != null)
				{
					logger_0.Error(text);
				}
				else
				{
					webSocketServiceManager_0.TimeSpan_0 = value;
				}
			}
		}

		public WebSocketServiceManager WebSocketServiceManager_0
		{
			get
			{
				return webSocketServiceManager_0;
			}
		}

		public WebSocketServer()
		{
			init(null, IPAddress.Any, 80, false);
		}

		public WebSocketServer(int int_1)
			: this(int_1, int_1 == 443)
		{
		}

		public WebSocketServer(string string_2)
		{
			if (string_2 == null)
			{
				throw new ArgumentNullException("url");
			}
			if (string_2.Length == 0)
			{
				throw new ArgumentException("An empty string.", "url");
			}
			Uri uri_;
			string string_3;
			if (!tryCreateUri(string_2, out uri_, out string_3))
			{
				throw new ArgumentException(string_3, "url");
			}
			string dnsSafeHost = uri_.DnsSafeHost;
			IPAddress ipaddress_ = dnsSafeHost.ToIPAddress();
			if (!ipaddress_.IsLocal())
			{
				throw new ArgumentException("The host part isn't a local host name: " + string_2, "url");
			}
			init(dnsSafeHost, ipaddress_, uri_.Port, uri_.Scheme == "wss");
		}

		public WebSocketServer(int int_1, bool bool_3)
		{
			if (!int_1.IsPortNumber())
			{
				throw new ArgumentOutOfRangeException("port", "Not between 1 and 65535 inclusive: " + int_1);
			}
			init(null, IPAddress.Any, int_1, bool_3);
		}

		public WebSocketServer(IPAddress ipaddress_1, int int_1)
			: this(ipaddress_1, int_1, int_1 == 443)
		{
		}

		public WebSocketServer(IPAddress ipaddress_1, int int_1, bool bool_3)
		{
			if (ipaddress_1 == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!ipaddress_1.IsLocal())
			{
				throw new ArgumentException("Not a local IP address: " + ipaddress_1, "address");
			}
			if (!int_1.IsPortNumber())
			{
				throw new ArgumentOutOfRangeException("port", "Not between 1 and 65535 inclusive: " + int_1);
			}
			init(null, ipaddress_1, int_1, bool_3);
		}

		private void abort()
		{
			lock (object_0)
			{
				if (!Boolean_0)
				{
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			tcpListener_0.Stop();
			webSocketServiceManager_0.Stop(new CloseEventArgs(CloseStatusCode.ServerError), true, false);
			serverState_0 = ServerState.Stop;
		}

		private static bool authenticate(TcpListenerWebSocketContext tcpListenerWebSocketContext_0, WebSocketSharp.Net.AuthenticationSchemes authenticationSchemes_1, string string_2, Func<IIdentity, WebSocketSharp.Net.NetworkCredential> func_2)
		{
			string string_3 = ((authenticationSchemes_1 == WebSocketSharp.Net.AuthenticationSchemes.Basic) ? AuthenticationChallenge.CreateBasicChallenge(string_2).ToBasicString() : ((authenticationSchemes_1 != WebSocketSharp.Net.AuthenticationSchemes.Digest) ? null : AuthenticationChallenge.CreateDigestChallenge(string_2).ToDigestString()));
			if (string_3 == null)
			{
				tcpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.Forbidden);
				return false;
			}
			int int_0 = -1;
			Func<bool> func_3 = null;
			func_3 = delegate
			{
				int_0++;
				if (int_0 > 99)
				{
					tcpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.Forbidden);
					return false;
				}
				IPrincipal principal = HttpUtility.CreateUser(tcpListenerWebSocketContext_0.NameValueCollection_0["Authorization"], authenticationSchemes_1, string_2, tcpListenerWebSocketContext_0.String_0, func_2);
				if (principal != null && principal.Identity.IsAuthenticated)
				{
					tcpListenerWebSocketContext_0.SetUser(principal);
					return true;
				}
				tcpListenerWebSocketContext_0.SendAuthenticationChallenge(string_3);
				return func_3();
			};
			return func_3();
		}

		private string checkIfCertificateExists()
		{
			return (!bool_2 || (serverSslConfiguration_0 != null && serverSslConfiguration_0.X509Certificate2_0 != null)) ? null : "The secure connection requires a server certificate.";
		}

		private void init(string string_2, IPAddress ipaddress_1, int int_1, bool bool_3)
		{
			string_0 = string_2 ?? ipaddress_1.ToString();
			ipaddress_0 = ipaddress_1;
			int_0 = int_1;
			bool_2 = bool_3;
			authenticationSchemes_0 = WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			bool_0 = Uri.CheckHostName(string_2) == UriHostNameType.Dns;
			tcpListener_0 = new TcpListener(ipaddress_1, int_1);
			logger_0 = new Logger();
			webSocketServiceManager_0 = new WebSocketServiceManager(logger_0);
			object_0 = new object();
		}

		private void processRequest(TcpListenerWebSocketContext tcpListenerWebSocketContext_0)
		{
			Uri uri_ = tcpListenerWebSocketContext_0.Uri_0;
			if (!(uri_ == null) && uri_.Port == int_0)
			{
				if (bool_0)
				{
					string dnsSafeHost = uri_.DnsSafeHost;
					if (Uri.CheckHostName(dnsSafeHost) == UriHostNameType.Dns && dnsSafeHost != string_0)
					{
						tcpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.NotFound);
						return;
					}
				}
				WebSocketServiceHost webSocketServiceHost_;
				if (!webSocketServiceManager_0.InternalTryGetServiceHost(uri_.AbsolutePath, out webSocketServiceHost_))
				{
					tcpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.NotImplemented);
				}
				else
				{
					webSocketServiceHost_.StartSession(tcpListenerWebSocketContext_0);
				}
			}
			else
			{
				tcpListenerWebSocketContext_0.Close(WebSocketSharp.Net.HttpStatusCode.BadRequest);
			}
		}

		private void receiveRequest()
		{
			while (true)
			{
				try
				{
					TcpClient tcpClient_0 = tcpListener_0.AcceptTcpClient();
					ThreadPool.QueueUserWorkItem(delegate
					{
						try
						{
							TcpListenerWebSocketContext webSocketContext = tcpClient_0.GetWebSocketContext(null, bool_2, serverSslConfiguration_0, logger_0);
							if (authenticationSchemes_0 == WebSocketSharp.Net.AuthenticationSchemes.Anonymous || authenticate(webSocketContext, authenticationSchemes_0, String_0, Func_0))
							{
								processRequest(webSocketContext);
							}
						}
						catch (Exception ex3)
						{
							logger_0.Fatal(ex3.ToString());
							tcpClient_0.Close();
						}
					});
				}
				catch (SocketException ex)
				{
					logger_0.Warn("Receiving has been stopped.\n  reason: " + ex.Message);
					break;
				}
				catch (Exception ex2)
				{
					logger_0.Fatal(ex2.ToString());
					break;
				}
			}
			if (Boolean_0)
			{
				abort();
			}
		}

		private void startReceiving()
		{
			if (bool_1)
			{
				tcpListener_0.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			tcpListener_0.Start();
			thread_0 = new Thread(receiveRequest);
			thread_0.IsBackground = true;
			thread_0.Start();
		}

		private void stopReceiving(int int_1)
		{
			tcpListener_0.Stop();
			thread_0.Join(int_1);
		}

		private static bool tryCreateUri(string string_2, out Uri uri_0, out string string_3)
		{
			if (!string_2.TryCreateWebSocketUri(out uri_0, out string_3))
			{
				return false;
			}
			if (uri_0.PathAndQuery != "/")
			{
				uri_0 = null;
				string_3 = "Includes the path or query component: " + string_2;
				return false;
			}
			return true;
		}

		public void AddWebSocketService<TBehavior>(string string_2, Func<TBehavior> func_2) where TBehavior : WebSocketBehavior
		{
			string text = string_2.CheckIfValidServicePath() ?? ((func_2 != null) ? null : "'initializer' is null.");
			if (text != null)
			{
				logger_0.Error(text);
			}
			else
			{
				webSocketServiceManager_0.Add(string_2, func_2);
			}
		}

		public void AddWebSocketService<TBehaviorWithNew>(string string_2) where TBehaviorWithNew : WebSocketBehavior, new()
		{
			AddWebSocketService(string_2, () => new TBehaviorWithNew());
		}

		public bool RemoveWebSocketService(string string_2)
		{
			string text = string_2.CheckIfValidServicePath();
			if (text != null)
			{
				logger_0.Error(text);
				return false;
			}
			return webSocketServiceManager_0.Remove(string_2);
		}

		public void Start()
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(true, false, false) ?? checkIfCertificateExists();
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				webSocketServiceManager_0.Start();
				startReceiving();
				serverState_0 = ServerState.Start;
			}
		}

		public void Stop()
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			stopReceiving(5000);
			webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			serverState_0 = ServerState.Stop;
		}

		public void Stop(ushort ushort_0, string string_2)
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false) ?? WebSocket.CheckCloseParameters(ushort_0, string_2, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			stopReceiving(5000);
			if (ushort_0 == 1005)
			{
				webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !ushort_0.IsReserved();
				webSocketServiceManager_0.Stop(new CloseEventArgs(ushort_0, string_2), flag, flag);
			}
			serverState_0 = ServerState.Stop;
		}

		public void Stop(CloseStatusCode closeStatusCode_0, string string_2)
		{
			lock (object_0)
			{
				string text = serverState_0.CheckIfAvailable(false, true, false) ?? WebSocket.CheckCloseParameters(closeStatusCode_0, string_2, false);
				if (text != null)
				{
					logger_0.Error(text);
					return;
				}
				serverState_0 = ServerState.ShuttingDown;
			}
			stopReceiving(5000);
			if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				webSocketServiceManager_0.Stop(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !closeStatusCode_0.IsReserved();
				webSocketServiceManager_0.Stop(new CloseEventArgs(closeStatusCode_0, string_2), flag, flag);
			}
			serverState_0 = ServerState.Stop;
		}
	}
}
