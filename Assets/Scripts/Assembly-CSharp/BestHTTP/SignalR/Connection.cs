using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BestHTTP.Extensions;
using BestHTTP.JSON;
using BestHTTP.SignalR.Authentication;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;
using BestHTTP.SignalR.Transports;

namespace BestHTTP.SignalR
{
	public sealed class Connection : IHeartbeat, IConnection
	{
		public static IJsonEncoder ijsonEncoder_0 = new DefaultJsonEncoder();

		private ConnectionStates connectionStates_0;

		internal object object_0 = new object();

		private readonly string string_0 = "1.5";

		private ulong ulong_0;

		private MultiMessage multiMessage_0;

		private string string_1;

		private List<IServerMessage> list_0;

		private DateTime dateTime_0;

		private DateTime? nullable_0;

		private DateTime dateTime_1;

		private TimeSpan timeSpan_0;

		private HTTPRequest httprequest_0;

		private DateTime? nullable_1;

		private StringBuilder stringBuilder_0 = new StringBuilder();

		private string string_2;

		private string string_3;

		private SupportedProtocols supportedProtocols_0;

		private OnConnectedDelegate onConnectedDelegate_0;

		private OnClosedDelegate onClosedDelegate_0;

		private OnErrorDelegate onErrorDelegate_0;

		private OnConnectedDelegate onConnectedDelegate_1;

		private OnConnectedDelegate onConnectedDelegate_2;

		private OnStateChanged onStateChanged_0;

		private OnNonHubMessageDelegate onNonHubMessageDelegate_0;

		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private NegotiationData negotiationData_0;

		[CompilerGenerated]
		private Hub[] hub_0;

		[CompilerGenerated]
		private TransportBase transportBase_0;

		[CompilerGenerated]
		private Dictionary<string, string> dictionary_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private IJsonEncoder ijsonEncoder_1;

		[CompilerGenerated]
		private IAuthenticationProvider iauthenticationProvider_0;

		[CompilerGenerated]
		private OnPrepareRequestDelegate onPrepareRequestDelegate_0;

		[CompilerGenerated]
		private ulong ulong_1;

		public Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			private set
			{
				uri_0 = value;
			}
		}

		public ConnectionStates ConnectionStates_0
		{
			get
			{
				return connectionStates_0;
			}
			private set
			{
				ConnectionStates connectionStates = connectionStates_0;
				connectionStates_0 = value;
				if (onStateChanged_0 != null)
				{
					onStateChanged_0(this, connectionStates, connectionStates_0);
				}
			}
		}

		public NegotiationData NegotiationData_0
		{
			[CompilerGenerated]
			get
			{
				return negotiationData_0;
			}
			[CompilerGenerated]
			private set
			{
				negotiationData_0 = value;
			}
		}

		public Hub[] Hub_0
		{
			[CompilerGenerated]
			get
			{
				return hub_0;
			}
			[CompilerGenerated]
			private set
			{
				hub_0 = value;
			}
		}

		public TransportBase TransportBase_0
		{
			[CompilerGenerated]
			get
			{
				return transportBase_0;
			}
			[CompilerGenerated]
			private set
			{
				transportBase_0 = value;
			}
		}

		public Dictionary<string, string> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			set
			{
				dictionary_0 = value;
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
			set
			{
				bool_0 = value;
			}
		}

		public IJsonEncoder IJsonEncoder_0
		{
			[CompilerGenerated]
			get
			{
				return ijsonEncoder_1;
			}
			[CompilerGenerated]
			set
			{
				ijsonEncoder_1 = value;
			}
		}

		public IAuthenticationProvider IAuthenticationProvider_0
		{
			[CompilerGenerated]
			get
			{
				return iauthenticationProvider_0;
			}
			[CompilerGenerated]
			set
			{
				iauthenticationProvider_0 = value;
			}
		}

		public OnPrepareRequestDelegate OnPrepareRequestDelegate_0
		{
			[CompilerGenerated]
			get
			{
				return onPrepareRequestDelegate_0;
			}
			[CompilerGenerated]
			set
			{
				onPrepareRequestDelegate_0 = value;
			}
		}

		public Hub this[int idx]
		{
			get
			{
				return Hub_0[idx];
			}
		}

		public Hub this[string hubName]
		{
			get
			{
				int num = 0;
				Hub hub;
				while (true)
				{
					if (num < Hub_0.Length)
					{
						hub = Hub_0[num];
						if (hub.String_0.Equals(hubName, StringComparison.OrdinalIgnoreCase))
						{
							break;
						}
						num++;
						continue;
					}
					return null;
				}
				return hub;
			}
		}

		internal ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_1;
			}
			[CompilerGenerated]
			set
			{
				ulong_1 = value;
			}
		}

		private uint UInt32_0
		{
			get
			{
				return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).Ticks;
			}
		}

		private string String_0
		{
			get
			{
				if (!string.IsNullOrEmpty(string_2))
				{
					return string_2;
				}
				StringBuilder stringBuilder = new StringBuilder("[", Hub_0.Length * 4);
				if (Hub_0 != null)
				{
					for (int i = 0; i < Hub_0.Length; i++)
					{
						stringBuilder.Append("{\"Name\":\"");
						stringBuilder.Append(Hub_0[i].String_0);
						stringBuilder.Append("\"}");
						if (i < Hub_0.Length - 1)
						{
							stringBuilder.Append(",");
						}
					}
				}
				stringBuilder.Append("]");
				return string_2 = Uri.EscapeUriString(stringBuilder.ToString());
			}
		}

		private string String_1
		{
			get
			{
				if (Dictionary_0 != null && Dictionary_0.Count != 0)
				{
					if (!string.IsNullOrEmpty(string_3))
					{
						return string_3;
					}
					StringBuilder stringBuilder = new StringBuilder(Dictionary_0.Count * 4);
					foreach (KeyValuePair<string, string> item in Dictionary_0)
					{
						stringBuilder.Append("&");
						stringBuilder.Append(item.Key);
						if (!string.IsNullOrEmpty(item.Value))
						{
							stringBuilder.Append("=");
							stringBuilder.Append(Uri.EscapeDataString(item.Value));
						}
					}
					return string_3 = stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		public event OnConnectedDelegate OnConnected
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onConnectedDelegate_0 = (OnConnectedDelegate)Delegate.Combine(onConnectedDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onConnectedDelegate_0 = (OnConnectedDelegate)Delegate.Remove(onConnectedDelegate_0, value);
			}
		}

		public event OnClosedDelegate OnClosed
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onClosedDelegate_0 = (OnClosedDelegate)Delegate.Combine(onClosedDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onClosedDelegate_0 = (OnClosedDelegate)Delegate.Remove(onClosedDelegate_0, value);
			}
		}

		public event OnErrorDelegate OnError
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onErrorDelegate_0 = (OnErrorDelegate)Delegate.Combine(onErrorDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onErrorDelegate_0 = (OnErrorDelegate)Delegate.Remove(onErrorDelegate_0, value);
			}
		}

		public event OnConnectedDelegate OnReconnecting
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onConnectedDelegate_1 = (OnConnectedDelegate)Delegate.Combine(onConnectedDelegate_1, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onConnectedDelegate_1 = (OnConnectedDelegate)Delegate.Remove(onConnectedDelegate_1, value);
			}
		}

		public event OnConnectedDelegate OnReconnected
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onConnectedDelegate_2 = (OnConnectedDelegate)Delegate.Combine(onConnectedDelegate_2, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onConnectedDelegate_2 = (OnConnectedDelegate)Delegate.Remove(onConnectedDelegate_2, value);
			}
		}

		public event OnStateChanged OnStateChanged
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onStateChanged_0 = (OnStateChanged)Delegate.Combine(onStateChanged_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onStateChanged_0 = (OnStateChanged)Delegate.Remove(onStateChanged_0, value);
			}
		}

		public event OnNonHubMessageDelegate OnNonHubMessage
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				onNonHubMessageDelegate_0 = (OnNonHubMessageDelegate)Delegate.Combine(onNonHubMessageDelegate_0, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				onNonHubMessageDelegate_0 = (OnNonHubMessageDelegate)Delegate.Remove(onNonHubMessageDelegate_0, value);
			}
		}

		public Connection(Uri uri_1, params string[] string_4)
			: this(uri_1)
		{
			if (string_4 != null && string_4.Length > 0)
			{
				Hub_0 = new Hub[string_4.Length];
				for (int i = 0; i < string_4.Length; i++)
				{
					Hub_0[i] = new Hub(string_4[i], this);
				}
			}
		}

		public Connection(Uri uri_1, params Hub[] hub_1)
			: this(uri_1)
		{
			Hub_0 = hub_1;
			if (hub_1 != null)
			{
				for (int i = 0; i < hub_1.Length; i++)
				{
					((IHub)hub_1[i]).Connection = this;
				}
			}
		}

		public Connection(Uri uri_1)
		{
			ConnectionStates_0 = ConnectionStates.Initial;
			Uri_0 = uri_1;
			IJsonEncoder_0 = ijsonEncoder_0;
			timeSpan_0 = TimeSpan.FromMinutes(5.0);
		}

		void IConnection.OnMessage(IServerMessage msg)
		{
			if (ConnectionStates_0 == ConnectionStates.Closed)
			{
				return;
			}
			if (ConnectionStates_0 == ConnectionStates.Connecting)
			{
				if (list_0 == null)
				{
					list_0 = new List<IServerMessage>();
				}
				list_0.Add(msg);
				return;
			}
			dateTime_0 = DateTime.UtcNow;
			switch (msg.Type)
			{
			default:
				HTTPManager.ILogger_0.Warning("SignalR Connection", "Unknown message type received: " + msg.Type);
				break;
			case MessageTypes.Data:
				if (onNonHubMessageDelegate_0 != null)
				{
					onNonHubMessageDelegate_0(this, (msg as DataMessage).Object_0);
				}
				break;
			case MessageTypes.Multiple:
				multiMessage_0 = msg as MultiMessage;
				if (multiMessage_0.Boolean_0)
				{
					HTTPManager.ILogger_0.Information("SignalR Connection", "OnMessage - Init");
				}
				if (multiMessage_0.String_1 != null)
				{
					string_1 = multiMessage_0.String_1;
				}
				if (multiMessage_0.Boolean_1)
				{
					HTTPManager.ILogger_0.Information("SignalR Connection", "OnMessage - Should Reconnect");
					Reconnect();
				}
				if (multiMessage_0.List_0 != null)
				{
					for (int i = 0; i < multiMessage_0.List_0.Count; i++)
					{
						((IConnection)this).OnMessage(multiMessage_0.List_0[i]);
					}
				}
				break;
			case MessageTypes.MethodCall:
			{
				MethodCallMessage methodCallMessage = msg as MethodCallMessage;
				Hub hub = this[methodCallMessage.String_0];
				if (hub != null)
				{
					((IHub)hub).OnMethod(methodCallMessage);
				}
				else
				{
					HTTPManager.ILogger_0.Warning("SignalR Connection", string.Format("Hub \"{0}\" not found!", methodCallMessage.String_0));
				}
				break;
			}
			case MessageTypes.Result:
			case MessageTypes.Failure:
			case MessageTypes.Progress:
			{
				ulong uInt64_ = (msg as IHubMessage).UInt64_0;
				Hub hub = FindHub(uInt64_);
				if (hub != null)
				{
					((IHub)hub).OnMessage(msg);
				}
				else
				{
					HTTPManager.ILogger_0.Warning("SignalR Connection", string.Format("No Hub found for Progress message! Id: {0}", uInt64_.ToString()));
				}
				break;
			}
			case MessageTypes.KeepAlive:
				break;
			}
		}

		void IConnection.TransportStarted()
		{
			if (ConnectionStates_0 != ConnectionStates.Connecting)
			{
				return;
			}
			InitOnStart();
			if (onConnectedDelegate_0 != null)
			{
				try
				{
					onConnectedDelegate_0(this);
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("SignalR Connection", "OnOpened", ex);
				}
			}
			if (list_0 != null)
			{
				for (int i = 0; i < list_0.Count; i++)
				{
					((IConnection)this).OnMessage(list_0[i]);
				}
				list_0.Clear();
				list_0 = null;
			}
		}

		void IConnection.TransportReconnected()
		{
			if (ConnectionStates_0 != ConnectionStates.Reconnecting)
			{
				return;
			}
			HTTPManager.ILogger_0.Information("SignalR Connection", "Transport Reconnected");
			InitOnStart();
			if (onConnectedDelegate_2 == null)
			{
				return;
			}
			try
			{
				onConnectedDelegate_2(this);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("SignalR Connection", "OnReconnected", ex);
			}
		}

		void IConnection.TransportAborted()
		{
			Close();
		}

		void IConnection.Error(string reason)
		{
			if (ConnectionStates_0 == ConnectionStates.Closed)
			{
				return;
			}
			HTTPManager.ILogger_0.Error("SignalR Connection", reason);
			if (onErrorDelegate_0 != null)
			{
				onErrorDelegate_0(this, reason);
			}
			if (ConnectionStates_0 != ConnectionStates.Connected && ConnectionStates_0 != ConnectionStates.Reconnecting)
			{
				if (ConnectionStates_0 != ConnectionStates.Connecting || !TryFallbackTransport())
				{
					Close();
				}
			}
			else
			{
				Reconnect();
			}
		}

		Uri IConnection.BuildUri(RequestTypes type)
		{
			return ((IConnection)this).BuildUri(type, (TransportBase)null);
		}

		Uri IConnection.BuildUri(RequestTypes type, TransportBase transport)
		{
			lock (object_0)
			{
				stringBuilder_0.Length = 0;
				UriBuilder uriBuilder = new UriBuilder(Uri_0);
				if (!uriBuilder.Path.EndsWith("/"))
				{
					uriBuilder.Path += "/";
				}
				ulong_0 %= ulong.MaxValue;
				switch (type)
				{
				case RequestTypes.Negotiate:
					uriBuilder.Path += "negotiate";
					goto default;
				case RequestTypes.Connect:
					if (transport != null && transport.TransportTypes_0 == TransportTypes.WebSocket)
					{
						uriBuilder.Scheme = ((!HTTPProtocolFactory.IsSecureProtocol(Uri_0)) ? "ws" : "wss");
					}
					uriBuilder.Path += "connect";
					goto default;
				case RequestTypes.Start:
					uriBuilder.Path += "start";
					goto default;
				case RequestTypes.Poll:
					uriBuilder.Path += "poll";
					if (multiMessage_0 != null)
					{
						stringBuilder_0.Append("messageId=");
						stringBuilder_0.Append(multiMessage_0.String_0);
					}
					goto default;
				case RequestTypes.Send:
					uriBuilder.Path += "send";
					goto default;
				case RequestTypes.Reconnect:
					if (transport != null && transport.TransportTypes_0 == TransportTypes.WebSocket)
					{
						uriBuilder.Scheme = ((!HTTPProtocolFactory.IsSecureProtocol(Uri_0)) ? "ws" : "wss");
					}
					uriBuilder.Path += "reconnect";
					if (multiMessage_0 != null)
					{
						stringBuilder_0.Append("messageId=");
						stringBuilder_0.Append(multiMessage_0.String_0);
					}
					if (!string.IsNullOrEmpty(string_1))
					{
						if (stringBuilder_0.Length > 0)
						{
							stringBuilder_0.Append("&");
						}
						stringBuilder_0.Append("groupsToken=");
						stringBuilder_0.Append(string_1);
					}
					goto default;
				case RequestTypes.Abort:
					uriBuilder.Path += "abort";
					goto default;
				default:
					if (stringBuilder_0.Length > 0)
					{
						stringBuilder_0.Append("&");
					}
					stringBuilder_0.Append("tid=");
					stringBuilder_0.Append(ulong_0++.ToString());
					stringBuilder_0.Append("&_=");
					stringBuilder_0.Append(UInt32_0.ToString());
					if (transport != null)
					{
						stringBuilder_0.Append("&transport=");
						stringBuilder_0.Append(transport.String_0);
					}
					stringBuilder_0.Append("&clientProtocol=");
					stringBuilder_0.Append(string_0);
					if (NegotiationData_0 != null && !string.IsNullOrEmpty(NegotiationData_0.String_2))
					{
						stringBuilder_0.Append("&connectionToken=");
						stringBuilder_0.Append(NegotiationData_0.String_2);
					}
					if (Hub_0 != null && Hub_0.Length > 0)
					{
						stringBuilder_0.Append("&connectionData=");
						stringBuilder_0.Append(String_0);
					}
					break;
				case RequestTypes.Ping:
					uriBuilder.Path += "ping";
					stringBuilder_0.Append("&tid=");
					stringBuilder_0.Append(ulong_0++.ToString());
					stringBuilder_0.Append("&_=");
					stringBuilder_0.Append(UInt32_0.ToString());
					break;
				}
				if (Dictionary_0 != null && Dictionary_0.Count > 0)
				{
					stringBuilder_0.Append(String_1);
				}
				uriBuilder.Query = stringBuilder_0.ToString();
				stringBuilder_0.Length = 0;
				return uriBuilder.Uri;
			}
		}

		HTTPRequest IConnection.PrepareRequest(HTTPRequest req, RequestTypes type)
		{
			if (req != null && IAuthenticationProvider_0 != null)
			{
				IAuthenticationProvider_0.PrepareRequest(req, type);
			}
			if (OnPrepareRequestDelegate_0 != null)
			{
				OnPrepareRequestDelegate_0(this, req, type);
			}
			return req;
		}

		string IConnection.ParseResponse(string responseStr)
		{
			Dictionary<string, object> dictionary = Json.Decode(responseStr) as Dictionary<string, object>;
			if (dictionary == null)
			{
				((IConnection)this).Error("Failed to parse Start response: " + responseStr);
				return string.Empty;
			}
			object value;
			if (dictionary.TryGetValue("Response", out value) && value != null)
			{
				return value.ToString();
			}
			((IConnection)this).Error("No 'Response' key found in response: " + responseStr);
			return string.Empty;
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			ConnectionStates connectionStates = ConnectionStates_0;
			if (connectionStates != ConnectionStates.Connected)
			{
				DateTime? dateTime = nullable_1;
				if (dateTime.HasValue)
				{
					DateTime? dateTime2 = nullable_1;
					TimeSpan? timeSpan = ((!dateTime2.HasValue) ? null : new TimeSpan?(DateTime.UtcNow - dateTime2.Value));
					if (timeSpan.HasValue && timeSpan.Value >= NegotiationData_0.TimeSpan_2)
					{
						HTTPManager.ILogger_0.Warning("SignalR Connection", "OnHeartbeatUpdate - Transport failed to connect in the given time!");
						((IConnection)this).Error("Transport failed to connect in the given time!");
					}
				}
				DateTime? dateTime3 = nullable_0;
				if (dateTime3.HasValue)
				{
					DateTime? dateTime4 = nullable_0;
					TimeSpan? timeSpan2 = ((!dateTime4.HasValue) ? null : new TimeSpan?(DateTime.UtcNow - dateTime4.Value));
					if (timeSpan2.HasValue && timeSpan2.Value >= NegotiationData_0.TimeSpan_0)
					{
						HTTPManager.ILogger_0.Warning("SignalR Connection", "OnHeartbeatUpdate - Failed to reconnect in the given time!");
						Close();
					}
				}
				return;
			}
			if (TransportBase_0.Boolean_0 && NegotiationData_0.Nullable_0.HasValue)
			{
				TimeSpan? timeSpan3 = NegotiationData_0.Nullable_0;
				if (timeSpan3.HasValue && DateTime.UtcNow - dateTime_0 >= timeSpan3.Value)
				{
					Reconnect();
				}
			}
			if (httprequest_0 == null && DateTime.UtcNow - dateTime_1 >= timeSpan_0)
			{
				Ping();
			}
		}

		public void Open()
		{
			if (ConnectionStates_0 == ConnectionStates.Initial || ConnectionStates_0 == ConnectionStates.Closed)
			{
				if (IAuthenticationProvider_0 != null && IAuthenticationProvider_0.Boolean_0)
				{
					ConnectionStates_0 = ConnectionStates.Authenticating;
					IAuthenticationProvider_0.OnAuthenticationSucceded += OnAuthenticationSucceded;
					IAuthenticationProvider_0.OnAuthenticationFailed += OnAuthenticationFailed;
					IAuthenticationProvider_0.StartAuthentication();
				}
				else
				{
					StartImpl();
				}
			}
		}

		private void OnAuthenticationSucceded(IAuthenticationProvider iauthenticationProvider_1)
		{
			iauthenticationProvider_1.OnAuthenticationSucceded -= OnAuthenticationSucceded;
			StartImpl();
		}

		private void OnAuthenticationFailed(IAuthenticationProvider iauthenticationProvider_1, string string_4)
		{
			iauthenticationProvider_1.OnAuthenticationFailed -= OnAuthenticationFailed;
			((IConnection)this).Error(string_4);
		}

		private void StartImpl()
		{
			ConnectionStates_0 = ConnectionStates.Negotiating;
			NegotiationData_0 = new NegotiationData(this);
			NegotiationData_0.action_0 = OnNegotiationDataReceived;
			NegotiationData_0.action_1 = OnNegotiationError;
			NegotiationData_0.Start();
		}

		private void OnNegotiationDataReceived(NegotiationData negotiationData_1)
		{
			if (negotiationData_1.Boolean_0)
			{
				TransportBase_0 = new WebSocketTransport(this);
				supportedProtocols_0 = SupportedProtocols.ServerSentEvents;
			}
			else
			{
				TransportBase_0 = new ServerSentEventsTransport(this);
				supportedProtocols_0 = SupportedProtocols.HTTP;
			}
			ConnectionStates_0 = ConnectionStates.Connecting;
			nullable_1 = DateTime.UtcNow;
			TransportBase_0.Connect();
		}

		private void OnNegotiationError(NegotiationData negotiationData_1, string string_4)
		{
			((IConnection)this).Error(string_4);
		}

		public void Close()
		{
			if (ConnectionStates_0 == ConnectionStates.Closed)
			{
				return;
			}
			ConnectionStates_0 = ConnectionStates.Closed;
			nullable_0 = null;
			nullable_1 = null;
			if (TransportBase_0 != null)
			{
				TransportBase_0.Abort();
				TransportBase_0 = null;
			}
			NegotiationData_0 = null;
			HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			multiMessage_0 = null;
			if (Hub_0 != null)
			{
				for (int i = 0; i < Hub_0.Length; i++)
				{
					((IHub)Hub_0[i]).Close();
				}
			}
			if (list_0 != null)
			{
				list_0.Clear();
				list_0 = null;
			}
			if (onClosedDelegate_0 == null)
			{
				return;
			}
			try
			{
				onClosedDelegate_0(this);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("SignalR Connection", "OnClosed", ex);
			}
		}

		public void Reconnect()
		{
			DateTime? dateTime = nullable_0;
			if (dateTime.HasValue)
			{
				return;
			}
			HTTPManager.ILogger_0.Warning("SignalR Connection", "Reconnecting");
			ConnectionStates_0 = ConnectionStates.Reconnecting;
			nullable_0 = DateTime.UtcNow;
			TransportBase_0.Reconnect();
			if (httprequest_0 != null)
			{
				httprequest_0.Abort();
			}
			if (onConnectedDelegate_1 == null)
			{
				return;
			}
			try
			{
				onConnectedDelegate_1(this);
			}
			catch (Exception ex)
			{
				HTTPManager.ILogger_0.Exception("SignalR Connection", "OnReconnecting", ex);
			}
		}

		public void Send(object object_1)
		{
			if (object_1 == null)
			{
				throw new ArgumentNullException("arg");
			}
			lock (object_0)
			{
				if (ConnectionStates_0 == ConnectionStates.Connected)
				{
					string text = IJsonEncoder_0.Encode(object_1);
					TransportBase_0.Send(text);
				}
			}
		}

		public void SendJson(string string_4)
		{
			if (string_4 == null)
			{
				throw new ArgumentNullException("json");
			}
			lock (object_0)
			{
				if (ConnectionStates_0 == ConnectionStates.Connected)
				{
					TransportBase_0.Send(string_4);
				}
			}
		}

		private void InitOnStart()
		{
			ConnectionStates_0 = ConnectionStates.Connected;
			nullable_0 = null;
			nullable_1 = null;
			dateTime_1 = DateTime.UtcNow;
			dateTime_0 = DateTime.UtcNow;
			HTTPManager.HeartbeatManager_0.Subscribe(this);
		}

		private Hub FindHub(ulong ulong_2)
		{
			if (Hub_0 != null)
			{
				for (int i = 0; i < Hub_0.Length; i++)
				{
					if (((IHub)Hub_0[i]).HasSentMessageId(ulong_2))
					{
						return Hub_0[i];
					}
				}
			}
			return null;
		}

		private bool TryFallbackTransport()
		{
			if (ConnectionStates_0 == ConnectionStates.Connecting)
			{
				if (list_0 != null)
				{
					list_0.Clear();
				}
				TransportBase_0.Stop();
				TransportBase_0 = null;
				switch (supportedProtocols_0)
				{
				case SupportedProtocols.Unknown:
					return false;
				case SupportedProtocols.HTTP:
					TransportBase_0 = new PollingTransport(this);
					supportedProtocols_0 = SupportedProtocols.Unknown;
					break;
				case SupportedProtocols.ServerSentEvents:
					TransportBase_0 = new ServerSentEventsTransport(this);
					supportedProtocols_0 = SupportedProtocols.HTTP;
					break;
				}
				nullable_1 = DateTime.UtcNow;
				TransportBase_0.Connect();
				if (httprequest_0 != null)
				{
					httprequest_0.Abort();
				}
				return true;
			}
			return false;
		}

		private void Ping()
		{
			HTTPManager.ILogger_0.Information("SignalR Connection", "Sending Ping request.");
			httprequest_0 = new HTTPRequest(((IConnection)this).BuildUri(RequestTypes.Ping), OnPingRequestFinished);
			httprequest_0.TimeSpan_0 = timeSpan_0;
			httprequest_0.Send();
			dateTime_1 = DateTime.UtcNow;
		}

		private void OnPingRequestFinished(HTTPRequest httprequest_1, HTTPResponse httpresponse_0)
		{
			httprequest_0 = null;
			string text = string.Empty;
			switch (httprequest_1.HTTPRequestStates_0)
			{
			case HTTPRequestStates.Finished:
				if (httpresponse_0.Boolean_0)
				{
					string text2 = ((IConnection)this).ParseResponse(httpresponse_0.String_1);
					if (text2 != "pong")
					{
						text = "Wrong answer for ping request: " + text2;
					}
					else
					{
						HTTPManager.ILogger_0.Information("SignalR Connection", "Pong received.");
					}
				}
				else
				{
					text = string.Format("Ping - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", httpresponse_0.Int32_2, httpresponse_0.String_0, httpresponse_0.String_1);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Ping - Request Finished with Error! " + ((httprequest_1.Exception_0 == null) ? "No Exception" : (httprequest_1.Exception_0.Message + "\n" + httprequest_1.Exception_0.StackTrace));
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Ping - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Ping - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IConnection)this).Error(text);
			}
		}
	}
}
