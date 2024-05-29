using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.SocketIO.Events;
using BestHTTP.SocketIO.JsonEncoders;
using BestHTTP.SocketIO.Transports;

namespace BestHTTP.SocketIO
{
	public sealed class SocketManager : IHeartbeat, IManager
	{
		public enum States
		{
			Initial = 0,
			Closed = 1,
			Opening = 2,
			Open = 3,
			Reconnecting = 4
		}

		public const int int_0 = 4;

		public static IJsonEncoder ijsonEncoder_0 = new DefaultJSonEncoder();

		private States states_0;

		private int int_1;

		private Dictionary<string, Socket> dictionary_0 = new Dictionary<string, Socket>();

		private List<Socket> list_0 = new List<Socket>();

		private List<Packet> list_1;

		private DateTime dateTime_0 = DateTime.MinValue;

		private DateTime dateTime_1 = DateTime.MinValue;

		private DateTime dateTime_2;

		private DateTime dateTime_3;

		[CompilerGenerated]
		private SocketOptions socketOptions_0;

		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private HandshakeData handshakeData_0;

		[CompilerGenerated]
		private ITransport itransport_0;

		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private int int_2;

		[CompilerGenerated]
		private IJsonEncoder ijsonEncoder_1;

		[CompilerGenerated]
		private States states_1;

		public States States_0
		{
			get
			{
				return states_0;
			}
			private set
			{
				States_1 = states_0;
				states_0 = value;
			}
		}

		public SocketOptions SocketOptions_0
		{
			[CompilerGenerated]
			get
			{
				return socketOptions_0;
			}
			[CompilerGenerated]
			private set
			{
				socketOptions_0 = value;
			}
		}

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

		public HandshakeData HandshakeData_0
		{
			[CompilerGenerated]
			get
			{
				return handshakeData_0;
			}
			[CompilerGenerated]
			private set
			{
				handshakeData_0 = value;
			}
		}

		public ITransport ITransport_0
		{
			[CompilerGenerated]
			get
			{
				return itransport_0;
			}
			[CompilerGenerated]
			private set
			{
				itransport_0 = value;
			}
		}

		public ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_0;
			}
			[CompilerGenerated]
			internal set
			{
				ulong_0 = value;
			}
		}

		public Socket Socket_0
		{
			get
			{
				return GetSocket();
			}
		}

		public Socket this[string nsp]
		{
			get
			{
				return GetSocket(nsp);
			}
		}

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_2;
			}
			[CompilerGenerated]
			private set
			{
				int_2 = value;
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

		internal uint UInt32_0
		{
			get
			{
				return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
			}
		}

		internal int Int32_1
		{
			get
			{
				return Interlocked.Increment(ref int_1);
			}
		}

		internal States States_1
		{
			[CompilerGenerated]
			get
			{
				return states_1;
			}
			[CompilerGenerated]
			private set
			{
				states_1 = value;
			}
		}

		public SocketManager(Uri uri_1)
			: this(uri_1, new SocketOptions())
		{
		}

		public SocketManager(Uri uri_1, SocketOptions socketOptions_1)
		{
			Uri_0 = uri_1;
			SocketOptions_0 = socketOptions_1;
			States_0 = States.Initial;
			States_1 = States.Initial;
			IJsonEncoder_0 = ijsonEncoder_0;
		}

		void IManager.Remove(Socket socket)
		{
			dictionary_0.Remove(socket.String_0);
			list_0.Remove(socket);
		}

		void IManager.Close(bool removeSockets)
		{
			if (States_0 == States.Closed)
			{
				return;
			}
			HTTPManager.ILogger_0.Information("SocketManager", "Closing");
			HTTPManager.HeartbeatManager_0.Unsubscribe(this);
			if (removeSockets)
			{
				while (list_0.Count > 0)
				{
					((ISocket)list_0[list_0.Count - 1]).Disconnect(removeSockets);
				}
			}
			else
			{
				for (int i = 0; i < list_0.Count; i++)
				{
					((ISocket)list_0[i]).Disconnect(removeSockets);
				}
			}
			States_0 = States.Closed;
			dateTime_0 = DateTime.MinValue;
			if (list_1 != null)
			{
				list_1.Clear();
			}
			if (removeSockets)
			{
				dictionary_0.Clear();
			}
			if (HandshakeData_0 != null)
			{
				HandshakeData_0.Abort();
			}
			HandshakeData_0 = null;
			if (ITransport_0 != null)
			{
				ITransport_0.Close();
			}
			ITransport_0 = null;
		}

		void IManager.TryToReconnect()
		{
			if (States_0 == States.Reconnecting || States_0 == States.Closed)
			{
				return;
			}
			if (!SocketOptions_0.Boolean_0)
			{
				((IManager)this).EmitAll(EventNames.GetNameFor(SocketIOEventTypes.Disconnect), new object[0]);
				Close();
				return;
			}
			if (++Int32_0 >= SocketOptions_0.Int32_0)
			{
				((IManager)this).EmitEvent("reconnect_failed", new object[0]);
				Close();
				return;
			}
			Random random = new Random();
			int num = (int)SocketOptions_0.TimeSpan_0.TotalMilliseconds * Int32_0;
			dateTime_2 = DateTime.UtcNow + TimeSpan.FromMilliseconds(Math.Min(random.Next((int)((float)num - (float)num * SocketOptions_0.Single_0), (int)((float)num + (float)num * SocketOptions_0.Single_0)), (int)SocketOptions_0.TimeSpan_1.TotalMilliseconds));
			((IManager)this).Close(false);
			States_0 = States.Reconnecting;
			for (int i = 0; i < list_0.Count; i++)
			{
				((ISocket)list_0[i]).Open();
			}
			HTTPManager.HeartbeatManager_0.Subscribe(this);
			HTTPManager.ILogger_0.Information("SocketManager", "Reconnecting");
		}

		bool IManager.OnTransportConnected(ITransport transport)
		{
			if (States_0 != States.Opening)
			{
				return false;
			}
			if (States_1 == States.Reconnecting)
			{
				((IManager)this).EmitEvent("reconnect", new object[0]);
			}
			States_0 = States.Open;
			dateTime_1 = DateTime.UtcNow;
			Int32_0 = 0;
			SendOfflinePackets();
			HTTPManager.ILogger_0.Information("SocketManager", "Open");
			return true;
		}

		void IManager.OnTransportError(ITransport trans, string err)
		{
			((IManager)this).EmitError(SocketIOErrors.Internal, err);
			if (trans.TransportStates_0 != 0 && trans.TransportStates_0 != TransportStates.Opening)
			{
				trans.Close();
				((IManager)this).TryToReconnect();
			}
			else if (trans is WebSocketTransport)
			{
				trans.Close();
				ITransport_0 = new PollingTransport(this);
				ITransport_0.Open();
			}
			else
			{
				((IManager)this).TryToReconnect();
			}
		}

		void IManager.SendPacket(Packet packet)
		{
			ITransport transport = SelectTransport();
			if (transport != null)
			{
				try
				{
					transport.Send(packet);
					return;
				}
				catch (Exception ex)
				{
					((IManager)this).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
					return;
				}
			}
			if (list_1 == null)
			{
				list_1 = new List<Packet>();
			}
			list_1.Add(packet.Clone());
		}

		void IManager.OnPacket(Packet packet)
		{
			if (States_0 != States.Closed)
			{
				switch (packet.TransportEventTypes_0)
				{
				case TransportEventTypes.Pong:
					dateTime_1 = DateTime.UtcNow;
					break;
				case TransportEventTypes.Ping:
					((IManager)this).SendPacket(new Packet(TransportEventTypes.Pong, SocketIOEventTypes.Unknown, "/", string.Empty));
					break;
				}
				Socket value = null;
				if (dictionary_0.TryGetValue(packet.String_0, out value))
				{
					((ISocket)value).OnPacket(packet);
				}
				else
				{
					HTTPManager.ILogger_0.Warning("SocketManager", "Namespace \"" + packet.String_0 + "\" not found!");
				}
			}
		}

		void IManager.EmitEvent(string eventName, params object[] args)
		{
			Socket value = null;
			if (dictionary_0.TryGetValue("/", out value))
			{
				((ISocket)value).EmitEvent(eventName, args);
			}
		}

		void IManager.EmitEvent(SocketIOEventTypes type, params object[] args)
		{
			((IManager)this).EmitEvent(EventNames.GetNameFor(type), args);
		}

		void IManager.EmitError(SocketIOErrors errCode, string msg)
		{
			((IManager)this).EmitEvent(SocketIOEventTypes.Error, new object[1]
			{
				new Error(errCode, msg)
			});
		}

		void IManager.EmitAll(string eventName, params object[] args)
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				((ISocket)list_0[i]).EmitEvent(eventName, args);
			}
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			switch (States_0)
			{
			case States.Opening:
				if (DateTime.UtcNow - dateTime_3 >= SocketOptions_0.TimeSpan_2)
				{
					((IManager)this).EmitEvent("connect_error", new object[0]);
					((IManager)this).EmitEvent("connect_timeout", new object[0]);
					((IManager)this).TryToReconnect();
				}
				break;
			case States.Open:
			{
				ITransport transport = null;
				if (ITransport_0 != null && ITransport_0.TransportStates_0 == TransportStates.Open)
				{
					transport = ITransport_0;
				}
				if (transport == null || transport.TransportStates_0 != TransportStates.Open)
				{
					break;
				}
				transport.Poll();
				SendOfflinePackets();
				if (dateTime_0 == DateTime.MinValue)
				{
					dateTime_0 = DateTime.UtcNow;
					break;
				}
				if (DateTime.UtcNow - dateTime_0 > HandshakeData_0.TimeSpan_0)
				{
					((IManager)this).SendPacket(new Packet(TransportEventTypes.Ping, SocketIOEventTypes.Unknown, "/", string.Empty));
					dateTime_0 = DateTime.UtcNow;
				}
				if (DateTime.UtcNow - dateTime_1 > HandshakeData_0.TimeSpan_1)
				{
					((IManager)this).EmitAll(EventNames.GetNameFor(SocketIOEventTypes.Disconnect), new object[0]);
					((IManager)this).TryToReconnect();
				}
				break;
			}
			case States.Reconnecting:
				if (dateTime_2 != DateTime.MinValue && DateTime.UtcNow >= dateTime_2)
				{
					((IManager)this).EmitEvent("reconnect_attempt", new object[0]);
					((IManager)this).EmitEvent("reconnecting", new object[0]);
					Open();
				}
				break;
			}
		}

		public Socket GetSocket()
		{
			return GetSocket("/");
		}

		public Socket GetSocket(string string_0)
		{
			if (string.IsNullOrEmpty(string_0))
			{
				throw new ArgumentNullException("Namespace parameter is null or empty!");
			}
			Socket value = null;
			if (!dictionary_0.TryGetValue(string_0, out value))
			{
				value = new Socket(string_0, this);
				dictionary_0.Add(string_0, value);
				list_0.Add(value);
				((ISocket)value).Open();
			}
			return value;
		}

		public void Open()
		{
			if (States_0 == States.Initial || States_0 == States.Closed || States_0 == States.Reconnecting)
			{
				HTTPManager.ILogger_0.Information("SocketManager", "Opening");
				dateTime_2 = DateTime.MinValue;
				HandshakeData_0 = new HandshakeData(this);
				HandshakeData_0.action_0 = delegate
				{
					CreateTransports();
				};
				HandshakeData_0.action_1 = delegate(HandshakeData handshakeData_1, string string_0)
				{
					((IManager)this).EmitError(SocketIOErrors.Internal, string_0);
					((IManager)this).TryToReconnect();
				};
				HandshakeData_0.Start();
				((IManager)this).EmitEvent("connecting", new object[0]);
				States_0 = States.Opening;
				dateTime_3 = DateTime.UtcNow;
				HTTPManager.HeartbeatManager_0.Subscribe(this);
				GetSocket("/");
			}
		}

		public void Close()
		{
			((IManager)this).Close(true);
		}

		private void CreateTransports()
		{
			if (HandshakeData_0.List_0.Contains("websocket"))
			{
				ITransport_0 = new WebSocketTransport(this);
			}
			else
			{
				ITransport_0 = new PollingTransport(this);
			}
			ITransport_0.Open();
		}

		private ITransport SelectTransport()
		{
			if (States_0 != States.Open)
			{
				return null;
			}
			ITransport result;
			if (ITransport_0.Boolean_0)
			{
				ITransport transport = null;
				result = transport;
			}
			else
			{
				result = ITransport_0;
			}
			return result;
		}

		private void SendOfflinePackets()
		{
			ITransport transport = SelectTransport();
			if (list_1 != null && list_1.Count > 0 && transport != null)
			{
				transport.Send(list_1);
				list_1.Clear();
			}
		}

		public void EmitAll(string string_0, params object[] object_0)
		{
			for (int i = 0; i < list_0.Count; i++)
			{
				list_0[i].Emit(string_0, object_0);
			}
		}
	}
}
