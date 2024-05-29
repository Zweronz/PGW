using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp
{
	public class WebSocket : IDisposable
	{
		private const string string_0 = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private const string string_1 = "13";

		internal const int int_0 = 2147483633;

		public Dictionary<string, string> customHeaders = new Dictionary<string, string>();

		private AuthenticationChallenge authenticationChallenge_0;

		private string string_2;

		private bool bool_0;

		private Action action_0;

		private CompressionMethod compressionMethod_0;

		private WebSocketContext webSocketContext_0;

		private CookieCollection cookieCollection_0;

		private NetworkCredential networkCredential_0;

		private bool bool_1;

		private bool bool_2;

		private string string_3;

		private AutoResetEvent autoResetEvent_0;

		private Opcode opcode_0;

		private object object_0;

		private object object_1;

		private object object_2;

		private object object_3;

		private MemoryStream memoryStream_0;

		private Func<WebSocketContext, string> func_0;

		private bool bool_3;

		private bool bool_4;

		private volatile Logger logger_0;

		private Queue<MessageEventArgs> queue_0;

		private uint uint_0;

		private string string_4;

		private bool bool_5;

		private string string_5;

		private string[] string_6;

		private NetworkCredential networkCredential_1;

		private Uri uri_0;

		private volatile WebSocketState webSocketState_0;

		private AutoResetEvent autoResetEvent_1;

		private bool bool_6;

		private ClientSslConfiguration clientSslConfiguration_0;

		private Stream stream_0;

		private TcpClient tcpClient_0;

		private Uri uri_1;

		private TimeSpan timeSpan_0;

		internal static readonly RandomNumberGenerator randomNumberGenerator_0 = new RNGCryptoServiceProvider();

		private EventHandler<CloseEventArgs> eventHandler_0;

		private EventHandler<ErrorEventArgs> eventHandler_1;

		private EventHandler<MessageEventArgs> eventHandler_2;

		private EventHandler eventHandler_3;

		[CompilerGenerated]
		private static Func<WebSocketContext, string> func_1;

		internal CookieCollection CookieCollection_0
		{
			get
			{
				return cookieCollection_0;
			}
		}

		internal Func<WebSocketContext, string> Func_0
		{
			get
			{
				return func_0 ?? ((Func<WebSocketContext, string>)((WebSocketContext webSocketContext_1) => null));
			}
			set
			{
				func_0 = value;
			}
		}

		internal bool Boolean_0
		{
			get
			{
				return bool_3;
			}
			set
			{
				bool_3 = value;
			}
		}

		internal bool Boolean_1
		{
			get
			{
				return webSocketState_0 == WebSocketState.Open || webSocketState_0 == WebSocketState.Closing;
			}
		}

		public CompressionMethod CompressionMethod_0
		{
			get
			{
				return compressionMethod_0;
			}
			set
			{
				lock (object_0)
				{
					string text = checkIfAvailable(false, false);
					if (text != null)
					{
						logger_0.Error(text);
						error("An error has occurred in setting the compression.", null);
					}
					else
					{
						compressionMethod_0 = value;
					}
				}
			}
		}

		public IEnumerable<Cookie> IEnumerable_0
		{
			get
			{
				object syncRoot = cookieCollection_0.SyncRoot;
				Monitor.Enter(syncRoot);
				/*Error near IL_0047: Could not find block for branch target IL_0055*/;
				yield break;
			}
		}

		public NetworkCredential NetworkCredential_0
		{
			get
			{
				return networkCredential_0;
			}
		}

		public bool Boolean_2
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

		public bool Boolean_3
		{
			get
			{
				return bool_2;
			}
			set
			{
				lock (object_0)
				{
					string text = checkIfAvailable(false, false);
					if (text != null)
					{
						logger_0.Error(text);
						error("An error has occurred in setting the enable redirection.", null);
					}
					else
					{
						bool_2 = value;
					}
				}
			}
		}

		public string String_0
		{
			get
			{
				return string_3 ?? string.Empty;
			}
		}

		public bool Boolean_4
		{
			get
			{
				return Ping();
			}
		}

		public bool Boolean_5
		{
			get
			{
				return bool_6;
			}
		}

		public Logger Logger_0
		{
			get
			{
				return logger_0;
			}
			internal set
			{
				logger_0 = value;
			}
		}

		public string String_1
		{
			get
			{
				return string_4;
			}
			set
			{
				lock (object_0)
				{
					string text = checkIfAvailable(false, false);
					if (text == null)
					{
						if (value.IsNullOrEmpty())
						{
							string_4 = value;
							return;
						}
						Uri result;
						if (!Uri.TryCreate(value, UriKind.Absolute, out result) || result.Segments.Length > 1)
						{
							text = "The syntax of the origin must be '<scheme>://<host>[:<port>]'.";
						}
					}
					if (text != null)
					{
						logger_0.Error(text);
						error("An error has occurred in setting the origin.", null);
					}
					else
					{
						string_4 = value.TrimEnd('/');
					}
				}
			}
		}

		public string String_2
		{
			get
			{
				return string_5 ?? string.Empty;
			}
			internal set
			{
				string_5 = value;
			}
		}

		public WebSocketState WebSocketState_0
		{
			get
			{
				return webSocketState_0;
			}
		}

		public ClientSslConfiguration ClientSslConfiguration_0
		{
			get
			{
				return (!bool_0) ? null : (clientSslConfiguration_0 ?? (clientSslConfiguration_0 = new ClientSslConfiguration(uri_1.DnsSafeHost)));
			}
			set
			{
				lock (object_0)
				{
					string text = checkIfAvailable(false, false);
					if (text != null)
					{
						logger_0.Error(text);
						error("An error has occurred in setting the ssl configuration.", null);
					}
					else
					{
						clientSslConfiguration_0 = value;
					}
				}
			}
		}

		public Uri Uri_0
		{
			get
			{
				return (!bool_0) ? webSocketContext_0.Uri_0 : uri_1;
			}
		}

		public TimeSpan TimeSpan_0
		{
			get
			{
				return timeSpan_0;
			}
			set
			{
				lock (object_0)
				{
					string text = checkIfAvailable(true, false) ?? value.CheckIfValidWaitTime();
					if (text != null)
					{
						logger_0.Error(text);
						error("An error has occurred in setting the wait time.", null);
					}
					else
					{
						timeSpan_0 = value;
					}
				}
			}
		}

		public event EventHandler<CloseEventArgs> OnClose
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_0 = (EventHandler<CloseEventArgs>)Delegate.Combine(eventHandler_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_0 = (EventHandler<CloseEventArgs>)Delegate.Remove(eventHandler_0, value);
			}
		}

		public event EventHandler<ErrorEventArgs> OnError
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_1 = (EventHandler<ErrorEventArgs>)Delegate.Combine(eventHandler_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_1 = (EventHandler<ErrorEventArgs>)Delegate.Remove(eventHandler_1, value);
			}
		}

		public event EventHandler<MessageEventArgs> OnMessage
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_2 = (EventHandler<MessageEventArgs>)Delegate.Combine(eventHandler_2, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_2 = (EventHandler<MessageEventArgs>)Delegate.Remove(eventHandler_2, value);
			}
		}

		public event EventHandler OnOpen
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				eventHandler_3 = (EventHandler)Delegate.Combine(eventHandler_3, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				eventHandler_3 = (EventHandler)Delegate.Remove(eventHandler_3, value);
			}
		}

		internal WebSocket(HttpListenerWebSocketContext httpListenerWebSocketContext_0, string string_7)
		{
			webSocketContext_0 = httpListenerWebSocketContext_0;
			string_5 = string_7;
			action_0 = httpListenerWebSocketContext_0.Close;
			logger_0 = httpListenerWebSocketContext_0.Logger_0;
			bool_6 = httpListenerWebSocketContext_0.Boolean_2;
			stream_0 = httpListenerWebSocketContext_0.Stream_0;
			timeSpan_0 = TimeSpan.FromSeconds(1.0);
			init();
		}

		internal WebSocket(TcpListenerWebSocketContext tcpListenerWebSocketContext_0, string string_7)
		{
			webSocketContext_0 = tcpListenerWebSocketContext_0;
			string_5 = string_7;
			action_0 = tcpListenerWebSocketContext_0.Close;
			logger_0 = tcpListenerWebSocketContext_0.Logger_0;
			bool_6 = tcpListenerWebSocketContext_0.Boolean_2;
			stream_0 = tcpListenerWebSocketContext_0.Stream_0;
			timeSpan_0 = TimeSpan.FromSeconds(1.0);
			init();
		}

		public WebSocket(string string_7, params string[] string_8)
		{
			if (string_7 == null)
			{
				throw new ArgumentNullException("url");
			}
			if (string_7.Length == 0)
			{
				throw new ArgumentException("An empty string.", "url");
			}
			string message;
			if (!string_7.TryCreateWebSocketUri(out uri_1, out message))
			{
				throw new ArgumentException(message, "url");
			}
			if (string_8 != null && string_8.Length > 0)
			{
				message = string_8.CheckIfValidProtocols();
				if (message != null)
				{
					throw new ArgumentException(message, "protocols");
				}
				string_6 = string_8;
			}
			string_2 = CreateBase64Key();
			bool_0 = true;
			logger_0 = new Logger();
			bool_6 = uri_1.Scheme == "wss";
			timeSpan_0 = TimeSpan.FromSeconds(5.0);
			init();
		}

		void IDisposable.Dispose()
		{
			close(new CloseEventArgs(CloseStatusCode.Away), true, true);
		}

		private bool accept()
		{
			lock (object_0)
			{
				string text = webSocketState_0.CheckIfAvailable(true, false, false, false);
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in accepting.", null);
					return false;
				}
				try
				{
					if (acceptHandshake())
					{
						webSocketState_0 = WebSocketState.Open;
						return true;
					}
				}
				catch (Exception exception_)
				{
					processException(exception_, "An exception has occurred while accepting.");
				}
				return false;
			}
		}

		private bool acceptHandshake()
		{
			logger_0.Debug(string.Format("A connection request from {0}:\n{1}", webSocketContext_0.IPEndPoint_1, webSocketContext_0));
			string text = checkIfValidHandshakeRequest(webSocketContext_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred while accepting.", null);
				Close(HttpStatusCode.BadRequest);
				return false;
			}
			if (string_5 != null && !webSocketContext_0.Prop_0.Contains((string string_7) => string_7 == string_5))
			{
				string_5 = null;
			}
			if (!bool_3)
			{
				string text2 = webSocketContext_0.NameValueCollection_0["Sec-WebSocket-Extensions"];
				if (text2 != null && text2.Length > 0)
				{
					processSecWebSocketExtensionsHeader(text2);
				}
			}
			return sendHttpResponse(createHandshakeResponse());
		}

		private string checkIfAvailable(bool bool_7, bool bool_8)
		{
			return (!bool_7 && !bool_0) ? "This operation isn't available in the server." : ((bool_8 || !Boolean_1) ? null : ("This operation isn't available in: " + webSocketState_0));
		}

		private string checkIfCanAccept()
		{
			return (!bool_0) ? webSocketState_0.CheckIfAvailable(true, false, false, false) : "This operation isn't available in the client.";
		}

		private string checkIfCanConnect()
		{
			return bool_0 ? webSocketState_0.CheckIfAvailable(true, false, false, true) : "This operation isn't available in the server.";
		}

		private string checkIfValidHandshakeRequest(WebSocketContext webSocketContext_1)
		{
			NameValueCollection nameValueCollection_ = webSocketContext_1.NameValueCollection_0;
			return (webSocketContext_1.Uri_0 == null) ? "Specifies an invalid Request-URI." : ((!webSocketContext_1.Boolean_3) ? "Not a WebSocket connection request." : ((!validateSecWebSocketKeyHeader(nameValueCollection_["Sec-WebSocket-Key"])) ? "Includes an invalid Sec-WebSocket-Key header." : (validateSecWebSocketVersionClientHeader(nameValueCollection_["Sec-WebSocket-Version"]) ? Func_0(webSocketContext_1) : "Includes an invalid Sec-WebSocket-Version header.")));
		}

		private string checkIfValidHandshakeResponse(HttpResponse httpResponse_0)
		{
			NameValueCollection nameValueCollection_ = httpResponse_0.NameValueCollection_0;
			return httpResponse_0.Boolean_2 ? "Indicates the redirection." : (httpResponse_0.Boolean_3 ? "Requires the authentication." : ((!httpResponse_0.Boolean_4) ? "Not a WebSocket connection response." : ((!validateSecWebSocketAcceptHeader(nameValueCollection_["Sec-WebSocket-Accept"])) ? "Includes an invalid Sec-WebSocket-Accept header." : ((!validateSecWebSocketProtocolHeader(nameValueCollection_["Sec-WebSocket-Protocol"])) ? "Includes an invalid Sec-WebSocket-Protocol header." : ((!validateSecWebSocketExtensionsHeader(nameValueCollection_["Sec-WebSocket-Extensions"])) ? "Includes an invalid Sec-WebSocket-Extensions header." : (validateSecWebSocketVersionServerHeader(nameValueCollection_["Sec-WebSocket-Version"]) ? null : "Includes an invalid Sec-WebSocket-Version header."))))));
		}

		private string checkIfValidReceivedFrame(WebSocketFrame webSocketFrame_0)
		{
			bool boolean_ = webSocketFrame_0.Boolean_8;
			return (bool_0 && boolean_) ? "A frame from the server is masked." : ((!bool_0 && !boolean_) ? "A frame from a client isn't masked." : ((bool_4 && webSocketFrame_0.Boolean_5) ? "A data frame has been received while receiving the fragmented data." : ((!webSocketFrame_0.Boolean_2 || compressionMethod_0 != 0) ? null : "A compressed frame is without the available decompression method.")));
		}

		private void close(CloseEventArgs closeEventArgs_0, bool bool_7, bool bool_8)
		{
			lock (object_0)
			{
				if (webSocketState_0 == WebSocketState.Closing)
				{
					logger_0.Info("The closing is already in progress.");
					return;
				}
				if (webSocketState_0 == WebSocketState.Closed)
				{
					logger_0.Info("The connection has been closed.");
					return;
				}
				bool_7 = bool_7 && webSocketState_0 == WebSocketState.Open;
				bool_8 = bool_8 && bool_7;
				webSocketState_0 = WebSocketState.Closing;
			}
			logger_0.Trace("Begin closing the connection.");
			closeEventArgs_0.Boolean_0 = closeHandshake((!bool_7) ? null : WebSocketFrame.CreateCloseFrame(closeEventArgs_0.PayloadData_0, bool_0).ToByteArray(), (!bool_8) ? TimeSpan.Zero : timeSpan_0, (!bool_0) ? new Action(releaseServerResources) : new Action(releaseClientResources));
			logger_0.Trace("End closing the connection.");
			webSocketState_0 = WebSocketState.Closed;
			try
			{
				eventHandler_0.Emit(this, closeEventArgs_0);
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
				error("An exception has occurred during an OnClose event.", ex);
			}
		}

		private void closeAsync(CloseEventArgs closeEventArgs_0, bool bool_7, bool bool_8)
		{
			Action<CloseEventArgs, bool, bool> action_0 = close;
			action_0.BeginInvoke(closeEventArgs_0, bool_7, bool_8, delegate(IAsyncResult iasyncResult_0)
			{
				action_0.EndInvoke(iasyncResult_0);
			}, null);
		}

		private bool closeHandshake(byte[] byte_0, TimeSpan timeSpan_1, Action action_1)
		{
			bool flag = byte_0 != null && sendBytes(byte_0);
			bool flag2 = timeSpan_1 == TimeSpan.Zero || (flag && autoResetEvent_0 != null && autoResetEvent_0.WaitOne(timeSpan_1));
			action_1();
			if (memoryStream_0 != null)
			{
				memoryStream_0.Dispose();
				memoryStream_0 = null;
			}
			if (autoResetEvent_1 != null)
			{
				autoResetEvent_1.Close();
				autoResetEvent_1 = null;
			}
			if (autoResetEvent_0 != null)
			{
				autoResetEvent_0.Close();
				autoResetEvent_0 = null;
			}
			bool flag3 = flag && flag2;
			logger_0.Debug(string.Format("Was clean?: {0}\n  sent: {1}\n  received: {2}", flag3, flag, flag2));
			return flag3;
		}

		private bool connect()
		{
			lock (object_0)
			{
				string text = webSocketState_0.CheckIfAvailable(true, false, false, true);
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in connecting.", null);
					return false;
				}
				try
				{
					webSocketState_0 = WebSocketState.Connecting;
					if (doHandshake())
					{
						webSocketState_0 = WebSocketState.Open;
						return true;
					}
				}
				catch (Exception exception_)
				{
					processException(exception_, "An exception has occurred while connecting.");
				}
				return false;
			}
		}

		private string createExtensions()
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			if (compressionMethod_0 != 0)
			{
				string arg = compressionMethod_0.ToExtensionString("server_no_context_takeover", "client_no_context_takeover");
				stringBuilder.AppendFormat("{0}, ", arg);
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				return stringBuilder.ToString();
			}
			return null;
		}

		private HttpResponse createHandshakeCloseResponse(HttpStatusCode httpStatusCode_0)
		{
			HttpResponse httpResponse = HttpResponse.CreateCloseResponse(httpStatusCode_0);
			httpResponse.NameValueCollection_0["Sec-WebSocket-Version"] = "13";
			return httpResponse;
		}

		private HttpRequest createHandshakeRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateWebSocketRequest(uri_1);
			NameValueCollection nameValueCollection_ = httpRequest.NameValueCollection_0;
			if (!string_4.IsNullOrEmpty())
			{
				nameValueCollection_["Origin"] = string_4;
			}
			nameValueCollection_["Sec-WebSocket-Key"] = string_2;
			if (string_6 != null)
			{
				nameValueCollection_["Sec-WebSocket-Protocol"] = string_6.ToString(", ");
			}
			string text = createExtensions();
			if (text != null)
			{
				nameValueCollection_["Sec-WebSocket-Extensions"] = text;
			}
			nameValueCollection_["Sec-WebSocket-Version"] = "13";
			AuthenticationResponse authenticationResponse = null;
			if (authenticationChallenge_0 != null && networkCredential_0 != null)
			{
				authenticationResponse = new AuthenticationResponse(authenticationChallenge_0, networkCredential_0, uint_0);
				uint_0 = authenticationResponse.UInt32_0;
			}
			else if (bool_5)
			{
				authenticationResponse = new AuthenticationResponse(networkCredential_0);
			}
			if (authenticationResponse != null)
			{
				nameValueCollection_["Authorization"] = authenticationResponse.ToString();
			}
			if (customHeaders.Count > 0)
			{
				foreach (KeyValuePair<string, string> customHeader in customHeaders)
				{
					nameValueCollection_.Add(customHeader.Key, customHeader.Value);
				}
			}
			if (cookieCollection_0.Count > 0)
			{
				httpRequest.SetCookies(cookieCollection_0);
			}
			return httpRequest;
		}

		private HttpResponse createHandshakeResponse()
		{
			HttpResponse httpResponse = HttpResponse.CreateWebSocketResponse();
			NameValueCollection nameValueCollection_ = httpResponse.NameValueCollection_0;
			nameValueCollection_["Sec-WebSocket-Accept"] = CreateResponseKey(string_2);
			if (string_5 != null)
			{
				nameValueCollection_["Sec-WebSocket-Protocol"] = string_5;
			}
			if (string_3 != null)
			{
				nameValueCollection_["Sec-WebSocket-Extensions"] = string_3;
			}
			if (cookieCollection_0.Count > 0)
			{
				httpResponse.SetCookies(cookieCollection_0);
			}
			return httpResponse;
		}

		private MessageEventArgs dequeueFromMessageEventQueue()
		{
			lock (object_2)
			{
				return (queue_0.Count <= 0) ? null : queue_0.Dequeue();
			}
		}

		private bool doHandshake()
		{
			setClientStream();
			HttpResponse httpResponse = sendHandshakeRequest();
			string text = checkIfValidHandshakeResponse(httpResponse);
			if (text != null)
			{
				logger_0.Error(text);
				text = "An error has occurred while connecting.";
				error(text, null);
				close(new CloseEventArgs(CloseStatusCode.Abnormal, text), false, false);
				return false;
			}
			CookieCollection cookieCollection = httpResponse.CookieCollection_0;
			if (cookieCollection.Count > 0)
			{
				cookieCollection_0.SetOrRemove(cookieCollection);
			}
			return true;
		}

		private void enqueueToMessageEventQueue(MessageEventArgs messageEventArgs_0)
		{
			lock (object_2)
			{
				queue_0.Enqueue(messageEventArgs_0);
			}
		}

		private void error(string string_7, Exception exception_0)
		{
			try
			{
				eventHandler_1.Emit(this, new ErrorEventArgs(string_7, exception_0));
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
			}
		}

		private void init()
		{
			compressionMethod_0 = CompressionMethod.None;
			cookieCollection_0 = new CookieCollection();
			object_0 = new object();
			object_1 = new object();
			object_3 = new object();
			queue_0 = new Queue<MessageEventArgs>();
			object_2 = ((ICollection)queue_0).SyncRoot;
			webSocketState_0 = WebSocketState.Connecting;
		}

		private void open()
		{
			try
			{
				startReceiving();
				lock (object_1)
				{
					try
					{
						eventHandler_3.Emit(this, EventArgs.Empty);
					}
					catch (Exception exception_)
					{
						processException(exception_, "An exception has occurred during an OnOpen event.");
					}
				}
			}
			catch (Exception exception_2)
			{
				processException(exception_2, "An exception has occurred while opening.");
			}
		}

		private bool processCloseFrame(WebSocketFrame webSocketFrame_0)
		{
			PayloadData payloadData_ = webSocketFrame_0.PayloadData_0;
			close(new CloseEventArgs(payloadData_), !payloadData_.Boolean_0, false);
			return false;
		}

		private bool processDataFrame(WebSocketFrame webSocketFrame_0)
		{
			enqueueToMessageEventQueue((!webSocketFrame_0.Boolean_2) ? new MessageEventArgs(webSocketFrame_0) : new MessageEventArgs(webSocketFrame_0.Opcode_0, webSocketFrame_0.PayloadData_0.Byte_0.Decompress(compressionMethod_0)));
			return true;
		}

		private void processException(Exception exception_0, string string_7)
		{
			CloseStatusCode closeStatusCode = CloseStatusCode.Abnormal;
			string text = string_7;
			if (exception_0 is WebSocketException)
			{
				WebSocketException ex = (WebSocketException)exception_0;
				closeStatusCode = ex.CloseStatusCode_0;
				text = ex.Message;
			}
			if (closeStatusCode != CloseStatusCode.Abnormal && closeStatusCode != CloseStatusCode.TlsHandshakeFailure)
			{
				logger_0.Error(text);
			}
			else
			{
				logger_0.Fatal(exception_0.ToString());
			}
			error(string_7 ?? closeStatusCode.GetMessage(), exception_0);
			if (!bool_0 && webSocketState_0 == WebSocketState.Connecting)
			{
				Close(HttpStatusCode.BadRequest);
			}
			else
			{
				close(new CloseEventArgs(closeStatusCode, text ?? closeStatusCode.GetMessage()), !closeStatusCode.IsReserved(), false);
			}
		}

		private bool processFragmentedFrame(WebSocketFrame webSocketFrame_0)
		{
			if (!bool_4)
			{
				if (webSocketFrame_0.Boolean_3)
				{
					return true;
				}
				opcode_0 = webSocketFrame_0.Opcode_0;
				memoryStream_0 = new MemoryStream();
				bool_4 = true;
			}
			memoryStream_0.WriteBytes(webSocketFrame_0.PayloadData_0.Byte_0);
			if (webSocketFrame_0.Boolean_6)
			{
				using (memoryStream_0)
				{
					byte[] byte_ = ((compressionMethod_0 == CompressionMethod.None) ? memoryStream_0.ToArray() : memoryStream_0.DecompressToArray(compressionMethod_0));
					enqueueToMessageEventQueue(new MessageEventArgs(opcode_0, byte_));
				}
				memoryStream_0 = null;
				bool_4 = false;
			}
			return true;
		}

		private bool processPingFrame(WebSocketFrame webSocketFrame_0)
		{
			if (send(new WebSocketFrame(Opcode.Pong, webSocketFrame_0.PayloadData_0, bool_0).ToByteArray()))
			{
				logger_0.Trace("Returned a Pong.");
			}
			if (bool_1)
			{
				enqueueToMessageEventQueue(new MessageEventArgs(webSocketFrame_0));
			}
			return true;
		}

		private bool processPongFrame(WebSocketFrame webSocketFrame_0)
		{
			autoResetEvent_1.Set();
			logger_0.Trace("Received a Pong.");
			return true;
		}

		private bool processReceivedFrame(WebSocketFrame webSocketFrame_0)
		{
			string text = checkIfValidReceivedFrame(webSocketFrame_0);
			if (text != null)
			{
				return processUnsupportedFrame(webSocketFrame_0, CloseStatusCode.ProtocolError, text);
			}
			webSocketFrame_0.Unmask();
			return webSocketFrame_0.Boolean_7 ? processFragmentedFrame(webSocketFrame_0) : (webSocketFrame_0.Boolean_5 ? processDataFrame(webSocketFrame_0) : (webSocketFrame_0.Boolean_10 ? processPingFrame(webSocketFrame_0) : (webSocketFrame_0.Boolean_11 ? processPongFrame(webSocketFrame_0) : ((!webSocketFrame_0.Boolean_1) ? processUnsupportedFrame(webSocketFrame_0, CloseStatusCode.UnsupportedData, null) : processCloseFrame(webSocketFrame_0)))));
		}

		private void processSecWebSocketExtensionsHeader(string string_7)
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			bool flag = false;
			foreach (string item in string_7.SplitHeaderValue(','))
			{
				string text = item.Trim();
				if (!flag && text.IsCompressionExtension(CompressionMethod.Deflate))
				{
					compressionMethod_0 = CompressionMethod.Deflate;
					string arg = compressionMethod_0.ToExtensionString("client_no_context_takeover", "server_no_context_takeover");
					stringBuilder.AppendFormat("{0}, ", arg);
					flag = true;
				}
			}
			int length = stringBuilder.Length;
			if (length > 2)
			{
				stringBuilder.Length = length - 2;
				string_3 = stringBuilder.ToString();
			}
		}

		private bool processUnsupportedFrame(WebSocketFrame webSocketFrame_0, CloseStatusCode closeStatusCode_0, string string_7)
		{
			logger_0.Debug("An unsupported frame:" + webSocketFrame_0.PrintToString(false));
			processException(new WebSocketException(closeStatusCode_0, string_7), null);
			return false;
		}

		private void releaseClientResources()
		{
			if (stream_0 != null)
			{
				stream_0.Dispose();
				stream_0 = null;
			}
			if (tcpClient_0 != null)
			{
				tcpClient_0.Close();
				tcpClient_0 = null;
			}
		}

		private void releaseServerResources()
		{
			if (action_0 != null)
			{
				action_0();
				action_0 = null;
				stream_0 = null;
				webSocketContext_0 = null;
			}
		}

		private bool send(byte[] byte_0)
		{
			lock (object_0)
			{
				if (webSocketState_0 != WebSocketState.Open)
				{
					logger_0.Error("The sending has been interrupted.");
					return false;
				}
				return sendBytes(byte_0);
			}
		}

		private bool send(Opcode opcode_1, Stream stream_1)
		{
			lock (object_3)
			{
				Stream stream = stream_1;
				bool flag = false;
				bool result = false;
				try
				{
					if (compressionMethod_0 != 0)
					{
						stream_1 = stream_1.Compress(compressionMethod_0);
						flag = true;
					}
					if (!(result = send(opcode_1, stream_1, flag)))
					{
						error("The sending has been interrupted.", null);
					}
				}
				catch (Exception ex)
				{
					logger_0.Fatal(ex.ToString());
					error("An exception has occurred while sending the data.", ex);
				}
				finally
				{
					if (flag)
					{
						stream_1.Dispose();
					}
					stream.Dispose();
				}
				return result;
			}
		}

		private bool send(Opcode opcode_1, Stream stream_1, bool bool_7)
		{
			long length = stream_1.Length;
			if (length == 0L)
			{
				return send(Fin.Final, opcode_1, new byte[0], bool_7);
			}
			long num = length / 2147483633L;
			int num2 = (int)(length % 2147483633L);
			byte[] array = null;
			if (num == 0L)
			{
				array = new byte[num2];
				return stream_1.Read(array, 0, num2) == num2 && send(Fin.Final, opcode_1, array, bool_7);
			}
			array = new byte[2147483633];
			if (num == 1L && num2 == 0)
			{
				return stream_1.Read(array, 0, 2147483633) == 2147483633 && send(Fin.Final, opcode_1, array, bool_7);
			}
			if (stream_1.Read(array, 0, 2147483633) == 2147483633 && send(Fin.More, opcode_1, array, bool_7))
			{
				long num3 = ((num2 != 0) ? (num - 1L) : (num - 2L));
				long num4 = 0L;
				while (true)
				{
					if (num4 < num3)
					{
						if (stream_1.Read(array, 0, 2147483633) != 2147483633 || !send(Fin.More, Opcode.Cont, array, bool_7))
						{
							break;
						}
						num4++;
						continue;
					}
					if (num2 == 0)
					{
						num2 = 2147483633;
					}
					else
					{
						array = new byte[num2];
					}
					return stream_1.Read(array, 0, num2) == num2 && send(Fin.Final, Opcode.Cont, array, bool_7);
				}
				return false;
			}
			return false;
		}

		private bool send(Fin fin_0, Opcode opcode_1, byte[] byte_0, bool bool_7)
		{
			lock (object_0)
			{
				if (webSocketState_0 != WebSocketState.Open)
				{
					logger_0.Error("The sending has been interrupted.");
					return false;
				}
				return sendBytes(new WebSocketFrame(fin_0, opcode_1, byte_0, bool_7, bool_0).ToByteArray());
			}
		}

		private void sendAsync(Opcode opcode_1, Stream stream_1, Action<bool> action_1)
		{
			Func<Opcode, Stream, bool> func_0 = send;
			func_0.BeginInvoke(opcode_1, stream_1, delegate(IAsyncResult iasyncResult_0)
			{
				try
				{
					bool obj = func_0.EndInvoke(iasyncResult_0);
					if (action_1 != null)
					{
						action_1(obj);
					}
				}
				catch (Exception ex)
				{
					logger_0.Fatal(ex.ToString());
					error("An exception has occurred during a send callback.", ex);
				}
			}, null);
		}

		private bool sendBytes(byte[] byte_0)
		{
			try
			{
				stream_0.Write(byte_0, 0, byte_0.Length);
				return true;
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
				return false;
			}
		}

		private HttpResponse sendHandshakeRequest()
		{
			HttpRequest httpRequest = createHandshakeRequest();
			HttpResponse httpResponse = sendHttpRequest(httpRequest, 90000);
			if (httpResponse.Boolean_3)
			{
				string arg = httpResponse.NameValueCollection_0["WWW-Authenticate"];
				logger_0.Warn(string.Format("Received an authentication requirement for '{0}'.", arg));
				if (arg.IsNullOrEmpty())
				{
					logger_0.Error("No authentication challenge is specified.");
					return httpResponse;
				}
				authenticationChallenge_0 = AuthenticationChallenge.Parse(arg);
				if (authenticationChallenge_0 == null)
				{
					logger_0.Error("An invalid authentication challenge is specified.");
					return httpResponse;
				}
				if (networkCredential_0 != null && (!bool_5 || authenticationChallenge_0.AuthenticationSchemes_0 == AuthenticationSchemes.Digest))
				{
					if (httpResponse.Boolean_0)
					{
						releaseClientResources();
						setClientStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(authenticationChallenge_0, networkCredential_0, uint_0);
					uint_0 = authenticationResponse.UInt32_0;
					httpRequest.NameValueCollection_0["Authorization"] = authenticationResponse.ToString();
					httpResponse = sendHttpRequest(httpRequest, 15000);
				}
			}
			if (httpResponse.Boolean_2)
			{
				string arg2 = httpResponse.NameValueCollection_0["Location"];
				logger_0.Warn(string.Format("Received a redirection to '{0}'.", arg2));
				if (bool_2)
				{
					if (arg2.IsNullOrEmpty())
					{
						logger_0.Error("No url to redirect is located.");
						return httpResponse;
					}
					Uri uri;
					string text;
					if (!arg2.TryCreateWebSocketUri(out uri, out text))
					{
						logger_0.Error("An invalid url to redirect is located: " + text);
						return httpResponse;
					}
					releaseClientResources();
					uri_1 = uri;
					bool_6 = uri.Scheme == "wss";
					setClientStream();
					return sendHandshakeRequest();
				}
			}
			return httpResponse;
		}

		private HttpResponse sendHttpRequest(HttpRequest httpRequest_0, int int_1)
		{
			logger_0.Debug("A request to the server:\n" + httpRequest_0.ToString());
			HttpResponse response = httpRequest_0.GetResponse(stream_0, int_1);
			logger_0.Debug("A response to this request:\n" + response.ToString());
			return response;
		}

		private bool sendHttpResponse(HttpResponse httpResponse_0)
		{
			logger_0.Debug("A response to this request:\n" + httpResponse_0.ToString());
			return sendBytes(httpResponse_0.ToByteArray());
		}

		private void sendProxyConnectRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateConnectRequest(uri_1);
			HttpResponse httpResponse = sendHttpRequest(httpRequest, 90000);
			if (httpResponse.Boolean_1)
			{
				string arg = httpResponse.NameValueCollection_0["Proxy-Authenticate"];
				logger_0.Warn(string.Format("Received a proxy authentication requirement for '{0}'.", arg));
				if (arg.IsNullOrEmpty())
				{
					throw new WebSocketException("No proxy authentication challenge is specified.");
				}
				AuthenticationChallenge authenticationChallenge = AuthenticationChallenge.Parse(arg);
				if (authenticationChallenge == null)
				{
					throw new WebSocketException("An invalid proxy authentication challenge is specified.");
				}
				if (networkCredential_1 != null)
				{
					if (httpResponse.Boolean_0)
					{
						releaseClientResources();
						tcpClient_0 = new TcpClient(uri_0.DnsSafeHost, uri_0.Port);
						stream_0 = tcpClient_0.GetStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(authenticationChallenge, networkCredential_1, 0u);
					httpRequest.NameValueCollection_0["Proxy-Authorization"] = authenticationResponse.ToString();
					httpResponse = sendHttpRequest(httpRequest, 15000);
				}
				if (httpResponse.Boolean_1)
				{
					throw new WebSocketException("A proxy authentication is required.");
				}
			}
			if (httpResponse.String_2[0] != '2')
			{
				throw new WebSocketException("The proxy has failed a connection to the requested host and port.");
			}
		}

		private void setClientStream()
		{
			if (uri_0 != null)
			{
				tcpClient_0 = new TcpClient(uri_0.DnsSafeHost, uri_0.Port);
				stream_0 = tcpClient_0.GetStream();
				sendProxyConnectRequest();
			}
			else
			{
				tcpClient_0 = new TcpClient(uri_1.DnsSafeHost, uri_1.Port);
				stream_0 = tcpClient_0.GetStream();
			}
			if (bool_6)
			{
				ClientSslConfiguration clientSslConfiguration = ClientSslConfiguration_0;
				string text = clientSslConfiguration.String_0;
				if (text != uri_1.DnsSafeHost)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, "An invalid host name is specified.");
				}
				try
				{
					SslStream sslStream = new SslStream(stream_0, false, clientSslConfiguration.RemoteCertificateValidationCallback_1, clientSslConfiguration.LocalCertificateSelectionCallback_1);
					sslStream.AuthenticateAsClient(text, clientSslConfiguration.X509CertificateCollection_0, clientSslConfiguration.SslProtocols_0, clientSslConfiguration.Boolean_0);
					stream_0 = sslStream;
				}
				catch (Exception exception_)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, exception_);
				}
			}
		}

		private void startReceiving()
		{
			if (queue_0.Count > 0)
			{
				queue_0.Clear();
			}
			autoResetEvent_0 = new AutoResetEvent(false);
			autoResetEvent_1 = new AutoResetEvent(false);
			Action action_0 = null;
			action_0 = delegate
			{
				WebSocketFrame.ReadAsync(stream_0, false, delegate(WebSocketFrame webSocketFrame_0)
				{
					if (processReceivedFrame(webSocketFrame_0) && webSocketState_0 != WebSocketState.Closed)
					{
						action_0();
						if ((!webSocketFrame_0.Boolean_4 || (webSocketFrame_0.Boolean_10 && bool_1)) && webSocketFrame_0.Boolean_6)
						{
							lock (object_1)
							{
								try
								{
									MessageEventArgs messageEventArgs = dequeueFromMessageEventQueue();
									if (messageEventArgs != null && webSocketState_0 == WebSocketState.Open)
									{
										eventHandler_2.Emit(this, messageEventArgs);
									}
								}
								catch (Exception exception_)
								{
									processException(exception_, "An exception has occurred during an OnMessage event.");
								}
							}
						}
					}
					else if (autoResetEvent_0 != null)
					{
						autoResetEvent_0.Set();
					}
				}, delegate(Exception exception_0)
				{
					processException(exception_0, "An exception has occurred while receiving a message.");
				});
			};
			action_0();
		}

		private bool validateSecWebSocketAcceptHeader(string string_7)
		{
			return string_7 != null && string_7 == CreateResponseKey(string_2);
		}

		private bool validateSecWebSocketExtensionsHeader(string string_7)
		{
			bool flag = compressionMethod_0 != CompressionMethod.None;
			if (string_7 != null && string_7.Length != 0)
			{
				if (!flag)
				{
					return false;
				}
				foreach (string item in string_7.SplitHeaderValue(','))
				{
					string text = item.Trim();
					if (text.IsCompressionExtension(compressionMethod_0))
					{
						if (text.Contains("server_no_context_takeover"))
						{
							if (!text.Contains("client_no_context_takeover"))
							{
								logger_0.Warn("The server hasn't sent back 'client_no_context_takeover'.");
							}
							string string_8 = compressionMethod_0.ToExtensionString();
							if (text.SplitHeaderValue(';').Contains(delegate(string string_1)
							{
								string_1 = string_1.Trim();
								return string_1 != string_8 && string_1 != "server_no_context_takeover" && string_1 != "client_no_context_takeover";
							}))
							{
								return false;
							}
							continue;
						}
						logger_0.Error("The server hasn't sent back 'server_no_context_takeover'.");
						return false;
					}
					return false;
				}
				string_3 = string_7;
				return true;
			}
			if (flag)
			{
				compressionMethod_0 = CompressionMethod.None;
			}
			return true;
		}

		private bool validateSecWebSocketKeyHeader(string string_7)
		{
			if (string_7 != null && string_7.Length != 0)
			{
				string_2 = string_7;
				return true;
			}
			return false;
		}

		private bool validateSecWebSocketProtocolHeader(string string_7)
		{
			if (string_7 == null)
			{
				return string_6 == null;
			}
			if (string_6 != null && string_6.Contains((string string_1) => string_1 == string_7))
			{
				string_5 = string_7;
				return true;
			}
			return false;
		}

		private bool validateSecWebSocketVersionClientHeader(string string_7)
		{
			return string_7 != null && string_7 == "13";
		}

		private bool validateSecWebSocketVersionServerHeader(string string_7)
		{
			return string_7 == null || string_7 == "13";
		}

		internal static string CheckCloseParameters(ushort ushort_0, string string_7, bool bool_7)
		{
			return (!ushort_0.IsCloseStatusCode()) ? "An invalid close status code." : ((ushort_0 == 1005) ? (string_7.IsNullOrEmpty() ? null : "NoStatus cannot have a reason.") : ((ushort_0 == 1010 && !bool_7) ? "MandatoryExtension cannot be used by the server." : ((ushort_0 == 1011 && bool_7) ? "ServerError cannot be used by the client." : ((string_7.IsNullOrEmpty() || Encoding.UTF8.GetBytes(string_7).Length <= 123) ? null : "A reason has greater than the allowable max size."))));
		}

		internal static string CheckCloseParameters(CloseStatusCode closeStatusCode_0, string string_7, bool bool_7)
		{
			return (closeStatusCode_0 == CloseStatusCode.NoStatus) ? (string_7.IsNullOrEmpty() ? null : "NoStatus cannot have a reason.") : ((closeStatusCode_0 == CloseStatusCode.MandatoryExtension && !bool_7) ? "MandatoryExtension cannot be used by the server." : ((closeStatusCode_0 == CloseStatusCode.ServerError && bool_7) ? "ServerError cannot be used by the client." : ((string_7.IsNullOrEmpty() || Encoding.UTF8.GetBytes(string_7).Length <= 123) ? null : "A reason has greater than the allowable max size.")));
		}

		internal static string CheckPingParameter(string string_7, out byte[] byte_0)
		{
			byte_0 = Encoding.UTF8.GetBytes(string_7);
			return (byte_0.Length <= 125) ? null : "A message has greater than the allowable max size.";
		}

		internal void Close(HttpResponse httpResponse_0)
		{
			webSocketState_0 = WebSocketState.Closing;
			sendHttpResponse(httpResponse_0);
			releaseServerResources();
			webSocketState_0 = WebSocketState.Closed;
		}

		internal void Close(HttpStatusCode httpStatusCode_0)
		{
			Close(createHandshakeCloseResponse(httpStatusCode_0));
		}

		internal void Close(CloseEventArgs closeEventArgs_0, byte[] byte_0, TimeSpan timeSpan_1)
		{
			lock (object_0)
			{
				if (webSocketState_0 == WebSocketState.Closing)
				{
					logger_0.Info("The closing is already in progress.");
					return;
				}
				if (webSocketState_0 == WebSocketState.Closed)
				{
					logger_0.Info("The connection has been closed.");
					return;
				}
				webSocketState_0 = WebSocketState.Closing;
			}
			closeEventArgs_0.Boolean_0 = closeHandshake(byte_0, timeSpan_1, releaseServerResources);
			webSocketState_0 = WebSocketState.Closed;
			try
			{
				eventHandler_0.Emit(this, closeEventArgs_0);
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
			}
		}

		internal static string CreateBase64Key()
		{
			byte[] array = new byte[16];
			randomNumberGenerator_0.GetBytes(array);
			return Convert.ToBase64String(array);
		}

		internal static string CreateResponseKey(string string_7)
		{
			StringBuilder stringBuilder = new StringBuilder(string_7, 64);
			stringBuilder.Append("258EAFA5-E914-47DA-95CA-C5AB0DC85B11");
			SHA1 sHA = new SHA1CryptoServiceProvider();
			byte[] inArray = sHA.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
			return Convert.ToBase64String(inArray);
		}

		internal void InternalAccept()
		{
			try
			{
				if (acceptHandshake())
				{
					webSocketState_0 = WebSocketState.Open;
					open();
				}
			}
			catch (Exception exception_)
			{
				processException(exception_, "An exception has occurred while accepting.");
			}
		}

		internal bool Ping(byte[] byte_0, TimeSpan timeSpan_1)
		{
			try
			{
				AutoResetEvent autoResetEvent;
				return webSocketState_0 == WebSocketState.Open && send(byte_0) && (autoResetEvent = autoResetEvent_1) != null && autoResetEvent.WaitOne(timeSpan_1);
			}
			catch (Exception ex)
			{
				logger_0.Fatal(ex.ToString());
				return false;
			}
		}

		internal void Send(Opcode opcode_1, byte[] byte_0, Dictionary<CompressionMethod, byte[]> dictionary_0)
		{
			lock (object_3)
			{
				lock (object_0)
				{
					if (webSocketState_0 != WebSocketState.Open)
					{
						logger_0.Error("The sending has been interrupted.");
						return;
					}
					try
					{
						byte[] value;
						if (!dictionary_0.TryGetValue(compressionMethod_0, out value))
						{
							value = new WebSocketFrame(Fin.Final, opcode_1, byte_0.Compress(compressionMethod_0), compressionMethod_0 != CompressionMethod.None, false).ToByteArray();
							dictionary_0.Add(compressionMethod_0, value);
						}
						sendBytes(value);
					}
					catch (Exception ex)
					{
						logger_0.Fatal(ex.ToString());
					}
				}
			}
		}

		internal void Send(Opcode opcode_1, Stream stream_1, Dictionary<CompressionMethod, Stream> dictionary_0)
		{
			lock (object_3)
			{
				try
				{
					Stream value;
					if (!dictionary_0.TryGetValue(compressionMethod_0, out value))
					{
						value = stream_1.Compress(compressionMethod_0);
						dictionary_0.Add(compressionMethod_0, value);
					}
					else
					{
						value.Position = 0L;
					}
					send(opcode_1, value, compressionMethod_0 != CompressionMethod.None);
				}
				catch (Exception ex)
				{
					logger_0.Fatal(ex.ToString());
				}
			}
		}

		public void Accept()
		{
			string text = checkIfCanAccept();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in accepting.", null);
			}
			else if (accept())
			{
				open();
			}
		}

		public void AcceptAsync()
		{
			string text = checkIfCanAccept();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in accepting.", null);
				return;
			}
			Func<bool> func_0 = accept;
			func_0.BeginInvoke(delegate(IAsyncResult iasyncResult_0)
			{
				if (func_0.EndInvoke(iasyncResult_0))
				{
					open();
				}
			}, null);
		}

		public void Close()
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else
			{
				close(new CloseEventArgs(), true, true);
			}
		}

		public void Close(ushort ushort_0)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(ushort_0, null, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (ushort_0 == 1005)
			{
				close(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !ushort_0.IsReserved();
				close(new CloseEventArgs(ushort_0), flag, flag);
			}
		}

		public void Close(CloseStatusCode closeStatusCode_0)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(closeStatusCode_0, null, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				close(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !closeStatusCode_0.IsReserved();
				close(new CloseEventArgs(closeStatusCode_0), flag, flag);
			}
		}

		public void Close(ushort ushort_0, string string_7)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(ushort_0, string_7, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (ushort_0 == 1005)
			{
				close(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !ushort_0.IsReserved();
				close(new CloseEventArgs(ushort_0, string_7), flag, flag);
			}
		}

		public void Close(CloseStatusCode closeStatusCode_0, string string_7)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(closeStatusCode_0, string_7, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				close(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !closeStatusCode_0.IsReserved();
				close(new CloseEventArgs(closeStatusCode_0, string_7), flag, flag);
			}
		}

		public void CloseAsync()
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else
			{
				closeAsync(new CloseEventArgs(), true, false);
			}
		}

		public void CloseAsync(ushort ushort_0)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(ushort_0, null, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (ushort_0 == 1005)
			{
				closeAsync(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !ushort_0.IsReserved();
				closeAsync(new CloseEventArgs(ushort_0), flag, flag);
			}
		}

		public void CloseAsync(CloseStatusCode closeStatusCode_0)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(closeStatusCode_0, null, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				closeAsync(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !closeStatusCode_0.IsReserved();
				closeAsync(new CloseEventArgs(closeStatusCode_0), flag, flag);
			}
		}

		public void CloseAsync(ushort ushort_0, string string_7)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(ushort_0, string_7, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (ushort_0 == 1005)
			{
				closeAsync(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !ushort_0.IsReserved();
				closeAsync(new CloseEventArgs(ushort_0, string_7), flag, flag);
			}
		}

		public void CloseAsync(CloseStatusCode closeStatusCode_0, string string_7)
		{
			string text = webSocketState_0.CheckIfAvailable(true, true, false, false) ?? CheckCloseParameters(closeStatusCode_0, string_7, bool_0);
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in closing the connection.", null);
			}
			else if (closeStatusCode_0 == CloseStatusCode.NoStatus)
			{
				closeAsync(new CloseEventArgs(), true, true);
			}
			else
			{
				bool flag = !closeStatusCode_0.IsReserved();
				closeAsync(new CloseEventArgs(closeStatusCode_0, string_7), flag, flag);
			}
		}

		public void Connect()
		{
			string text = checkIfCanConnect();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in connecting.", null);
			}
			else if (connect())
			{
				open();
			}
		}

		public void ConnectAsync()
		{
			string text = checkIfCanConnect();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in connecting.", null);
				return;
			}
			Func<bool> func_0 = connect;
			func_0.BeginInvoke(delegate(IAsyncResult iasyncResult_0)
			{
				if (func_0.EndInvoke(iasyncResult_0))
				{
					open();
				}
			}, null);
		}

		public bool Ping()
		{
			byte[] byte_ = ((!bool_0) ? WebSocketFrame.byte_3 : WebSocketFrame.CreatePingFrame(true).ToByteArray());
			return Ping(byte_, timeSpan_0);
		}

		public bool Ping(string string_7)
		{
			if (string_7 != null && string_7.Length != 0)
			{
				byte[] byte_;
				string text = CheckPingParameter(string_7, out byte_);
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in sending the ping.", null);
					return false;
				}
				return Ping(WebSocketFrame.CreatePingFrame(byte_, bool_0).ToByteArray(), timeSpan_0);
			}
			return Ping();
		}

		public void Send(byte[] byte_0)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? byte_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				send(Opcode.Binary, new MemoryStream(byte_0));
			}
		}

		public void Send(FileInfo fileInfo_0)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? fileInfo_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				send(Opcode.Binary, fileInfo_0.OpenRead());
			}
		}

		public void Send(string string_7)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? string_7.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				send(Opcode.Text, new MemoryStream(Encoding.UTF8.GetBytes(string_7)));
			}
		}

		public void SendAsync(byte[] byte_0, Action<bool> action_1)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? byte_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				sendAsync(Opcode.Binary, new MemoryStream(byte_0), action_1);
			}
		}

		public void SendAsync(FileInfo fileInfo_0, Action<bool> action_1)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? fileInfo_0.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				sendAsync(Opcode.Binary, fileInfo_0.OpenRead(), action_1);
			}
		}

		public void SendAsync(string string_7, Action<bool> action_1)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? string_7.CheckIfValidSendData();
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
			}
			else
			{
				sendAsync(Opcode.Text, new MemoryStream(Encoding.UTF8.GetBytes(string_7)), action_1);
			}
		}

		public void SendAsync(Stream stream_1, int int_1, Action<bool> action_1)
		{
			string text = webSocketState_0.CheckIfAvailable(false, true, false, false) ?? stream_1.CheckIfCanRead() ?? ((int_1 >= 1) ? null : "'length' is less than 1.");
			if (text != null)
			{
				logger_0.Error(text);
				error("An error has occurred in sending the data.", null);
				return;
			}
			stream_1.ReadBytesAsync(int_1, delegate(byte[] byte_0)
			{
				int num = byte_0.Length;
				if (num == 0)
				{
					logger_0.Error("The data cannot be read from 'stream'.");
					error("An error has occurred in sending the data.", null);
				}
				else
				{
					if (num < int_1)
					{
						logger_0.Warn(string.Format("The data with 'length' cannot be read from 'stream':\n  expected: {0}\n  actual: {1}", int_1, num));
					}
					bool obj = send(Opcode.Binary, new MemoryStream(byte_0));
					if (action_1 != null)
					{
						action_1(obj);
					}
				}
			}, delegate(Exception exception_0)
			{
				logger_0.Fatal(exception_0.ToString());
				error("An exception has occurred while sending the data.", exception_0);
			});
		}

		public void SetCookie(Cookie cookie_0)
		{
			lock (object_0)
			{
				string text = checkIfAvailable(false, false) ?? ((cookie_0 != null) ? null : "'cookie' is null.");
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in setting the cookie.", null);
					return;
				}
				lock (cookieCollection_0.SyncRoot)
				{
					cookieCollection_0.SetOrRemove(cookie_0);
				}
			}
		}

		public void SetCredentials(string string_7, string string_8, bool bool_7)
		{
			lock (object_0)
			{
				string text = checkIfAvailable(false, false);
				if (text == null)
				{
					if (string_7.IsNullOrEmpty())
					{
						networkCredential_0 = null;
						bool_5 = false;
						logger_0.Warn("The credentials were set back to the default.");
						return;
					}
					text = ((string_7.Contains(':') || !string_7.IsText()) ? "'username' contains an invalid character." : ((string_8.IsNullOrEmpty() || string_8.IsText()) ? null : "'password' contains an invalid character."));
				}
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in setting the credentials.", null);
				}
				else
				{
					networkCredential_0 = new NetworkCredential(string_7, string_8, uri_1.PathAndQuery);
					bool_5 = bool_7;
				}
			}
		}

		public void SetProxy(string string_7, string string_8, string string_9)
		{
			lock (object_0)
			{
				string text = checkIfAvailable(false, false);
				if (text == null)
				{
					if (string_7.IsNullOrEmpty())
					{
						uri_0 = null;
						networkCredential_1 = null;
						logger_0.Warn("The proxy url and credentials were set back to the default.");
						return;
					}
					Uri result;
					if (Uri.TryCreate(string_7, UriKind.Absolute, out result) && !(result.Scheme != "http") && result.Segments.Length <= 1)
					{
						uri_0 = result;
						if (string_8.IsNullOrEmpty())
						{
							networkCredential_1 = null;
							logger_0.Warn("The proxy credentials were set back to the default.");
							return;
						}
						text = ((string_8.Contains(':') || !string_8.IsText()) ? "'username' contains an invalid character." : ((string_9.IsNullOrEmpty() || string_9.IsText()) ? null : "'password' contains an invalid character."));
					}
					else
					{
						text = "The syntax of the proxy url must be 'http://<host>[:<port>]'.";
					}
				}
				if (text != null)
				{
					logger_0.Error(text);
					error("An error has occurred in setting the proxy.", null);
				}
				else
				{
					networkCredential_1 = new NetworkCredential(string_8, string_9, string.Format("{0}:{1}", uri_1.DnsSafeHost, uri_1.Port));
				}
			}
		}
	}
}
