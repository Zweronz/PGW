using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.JSON;
using BestHTTP.SocketIO.Events;

namespace BestHTTP.SocketIO
{
	public sealed class Socket : ISocket
	{
		private Dictionary<int, SocketIOAckCallback> dictionary_0;

		private EventTable eventTable_0;

		private List<object> list_0 = new List<object>();

		[CompilerGenerated]
		private SocketManager socketManager_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		public SocketManager SocketManager_0
		{
			[CompilerGenerated]
			get
			{
				return socketManager_0;
			}
			[CompilerGenerated]
			private set
			{
				socketManager_0 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
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
			private set
			{
				bool_0 = value;
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

		internal Socket(string string_1, SocketManager socketManager_1)
		{
			String_0 = string_1;
			SocketManager_0 = socketManager_1;
			Boolean_0 = false;
			Boolean_1 = true;
			eventTable_0 = new EventTable(this);
		}

		void ISocket.Open()
		{
			if (SocketManager_0.States_0 == SocketManager.States.Open)
			{
				OnTransportOpen(SocketManager_0.Socket_0, null);
				return;
			}
			SocketManager_0.Socket_0.Off("connect", OnTransportOpen);
			SocketManager_0.Socket_0.On("connect", OnTransportOpen);
			if (SocketManager_0.SocketOptions_0.Boolean_1 && SocketManager_0.States_0 == SocketManager.States.Initial)
			{
				SocketManager_0.Open();
			}
		}

		void ISocket.Disconnect(bool remove)
		{
			if (Boolean_0)
			{
				Packet packet = new Packet(TransportEventTypes.Message, SocketIOEventTypes.Disconnect, String_0, string.Empty);
				((IManager)SocketManager_0).SendPacket(packet);
				Boolean_0 = false;
				((ISocket)this).OnPacket(packet);
			}
			if (dictionary_0 != null)
			{
				dictionary_0.Clear();
			}
			if (remove)
			{
				eventTable_0.Clear();
				((IManager)SocketManager_0).Remove(this);
			}
		}

		void ISocket.OnPacket(Packet packet)
		{
			switch (packet.SocketIOEventTypes_0)
			{
			case SocketIOEventTypes.Disconnect:
				if (Boolean_0)
				{
					Boolean_0 = false;
					Disconnect();
				}
				break;
			case SocketIOEventTypes.Error:
			{
				bool flag = false;
				Dictionary<string, object> dictionary = Json.Decode(packet.String_1, ref flag) as Dictionary<string, object>;
				if (flag)
				{
					Error error = new Error((SocketIOErrors)Convert.ToInt32(dictionary["code"]), dictionary["message"] as string);
					eventTable_0.Call(EventNames.GetNameFor(SocketIOEventTypes.Error), packet, error);
					return;
				}
				break;
			}
			}
			eventTable_0.Call(packet);
			if ((packet.SocketIOEventTypes_0 != SocketIOEventTypes.Ack && packet.SocketIOEventTypes_0 != SocketIOEventTypes.BinaryAck) || dictionary_0 == null)
			{
				return;
			}
			SocketIOAckCallback value = null;
			if (dictionary_0.TryGetValue(packet.Int32_1, out value) && value != null)
			{
				try
				{
					value(this, packet, packet.Decode(SocketManager_0.IJsonEncoder_0));
				}
				catch (Exception ex)
				{
					HTTPManager.ILogger_0.Exception("Socket", "ackCallback", ex);
				}
			}
			dictionary_0.Remove(packet.Int32_1);
		}

		void ISocket.EmitEvent(SocketIOEventTypes type, params object[] args)
		{
			((ISocket)this).EmitEvent(EventNames.GetNameFor(type), args);
		}

		void ISocket.EmitEvent(string eventName, params object[] args)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				eventTable_0.Call(eventName, null, args);
			}
		}

		void ISocket.EmitError(SocketIOErrors errCode, string msg)
		{
			((ISocket)this).EmitEvent(SocketIOEventTypes.Error, new object[1]
			{
				new Error(errCode, msg)
			});
		}

		public void Disconnect()
		{
			((ISocket)this).Disconnect(true);
		}

		public Socket Emit(string string_1, params object[] object_0)
		{
			return Emit(string_1, null, object_0);
		}

		public Socket Emit(string string_1, SocketIOAckCallback socketIOAckCallback_0, params object[] object_0)
		{
			if (EventNames.IsBlacklisted(string_1))
			{
				throw new ArgumentException("Blacklisted event: " + string_1);
			}
			list_0.Clear();
			list_0.Add(string_1);
			List<byte[]> list = null;
			if (object_0 != null && object_0.Length > 0)
			{
				int num = 0;
				for (int i = 0; i < object_0.Length; i++)
				{
					byte[] array = object_0[i] as byte[];
					if (array != null)
					{
						if (list == null)
						{
							list = new List<byte[]>();
						}
						list_0.Add(string.Format("{{\"_placeholder\":true,\"num\":{0}}}", num++.ToString()));
						list.Add(array);
					}
					else
					{
						list_0.Add(object_0[i]);
					}
				}
			}
			string text = null;
			try
			{
				text = SocketManager_0.IJsonEncoder_0.Encode(list_0);
			}
			catch (Exception ex)
			{
				((ISocket)this).EmitError(SocketIOErrors.Internal, "Error while encoding payload: " + ex.Message + " " + ex.StackTrace);
				return this;
			}
			list_0.Clear();
			if (text == null)
			{
				throw new ArgumentException("Encoding the arguments to JSON failed!");
			}
			int num2 = 0;
			if (socketIOAckCallback_0 != null)
			{
				num2 = SocketManager_0.Int32_1;
				if (dictionary_0 == null)
				{
					dictionary_0 = new Dictionary<int, SocketIOAckCallback>();
				}
				dictionary_0[num2] = socketIOAckCallback_0;
			}
			Packet packet = new Packet(TransportEventTypes.Message, (list != null) ? SocketIOEventTypes.BinaryEvent : SocketIOEventTypes.Event, String_0, text, 0, num2);
			if (list != null)
			{
				packet.List_0 = list;
			}
			((IManager)SocketManager_0).SendPacket(packet);
			return this;
		}

		public Socket EmitAck(Packet packet_0, params object[] object_0)
		{
			if (packet_0 == null)
			{
				throw new ArgumentNullException("originalPacket == null!");
			}
			if (packet_0.SocketIOEventTypes_0 != SocketIOEventTypes.Event && packet_0.SocketIOEventTypes_0 != SocketIOEventTypes.BinaryEvent)
			{
				throw new ArgumentException("Wrong packet - you can't send an Ack for a packet with id == 0 and SocketIOEvent != Event or SocketIOEvent != BinaryEvent!");
			}
			list_0.Clear();
			if (object_0 != null && object_0.Length > 0)
			{
				list_0.AddRange(object_0);
			}
			string text = null;
			try
			{
				text = SocketManager_0.IJsonEncoder_0.Encode(list_0);
			}
			catch (Exception ex)
			{
				((ISocket)this).EmitError(SocketIOErrors.Internal, "Error while encoding payload: " + ex.Message + " " + ex.StackTrace);
				return this;
			}
			if (text == null)
			{
				throw new ArgumentException("Encoding the arguments to JSON failed!");
			}
			Packet packet = new Packet(TransportEventTypes.Message, (packet_0.SocketIOEventTypes_0 != SocketIOEventTypes.Event) ? SocketIOEventTypes.BinaryAck : SocketIOEventTypes.Ack, String_0, text, 0, packet_0.Int32_1);
			((IManager)SocketManager_0).SendPacket(packet);
			return this;
		}

		public void On(string string_1, SocketIOCallback socketIOCallback_0)
		{
			eventTable_0.Register(string_1, socketIOCallback_0, false, Boolean_1);
		}

		public void On(SocketIOEventTypes socketIOEventTypes_0, SocketIOCallback socketIOCallback_0)
		{
			string nameFor = EventNames.GetNameFor(socketIOEventTypes_0);
			eventTable_0.Register(nameFor, socketIOCallback_0, false, Boolean_1);
		}

		public void On(string string_1, SocketIOCallback socketIOCallback_0, bool bool_2)
		{
			eventTable_0.Register(string_1, socketIOCallback_0, false, bool_2);
		}

		public void On(SocketIOEventTypes socketIOEventTypes_0, SocketIOCallback socketIOCallback_0, bool bool_2)
		{
			string nameFor = EventNames.GetNameFor(socketIOEventTypes_0);
			eventTable_0.Register(nameFor, socketIOCallback_0, false, bool_2);
		}

		public void Once(string string_1, SocketIOCallback socketIOCallback_0)
		{
			eventTable_0.Register(string_1, socketIOCallback_0, true, Boolean_1);
		}

		public void Once(SocketIOEventTypes socketIOEventTypes_0, SocketIOCallback socketIOCallback_0)
		{
			eventTable_0.Register(EventNames.GetNameFor(socketIOEventTypes_0), socketIOCallback_0, true, Boolean_1);
		}

		public void Once(string string_1, SocketIOCallback socketIOCallback_0, bool bool_2)
		{
			eventTable_0.Register(string_1, socketIOCallback_0, true, bool_2);
		}

		public void Once(SocketIOEventTypes socketIOEventTypes_0, SocketIOCallback socketIOCallback_0, bool bool_2)
		{
			eventTable_0.Register(EventNames.GetNameFor(socketIOEventTypes_0), socketIOCallback_0, true, bool_2);
		}

		public void Off()
		{
			eventTable_0.Clear();
		}

		public void Off(string string_1)
		{
			eventTable_0.Unregister(string_1);
		}

		public void Off(SocketIOEventTypes socketIOEventTypes_0)
		{
			Off(EventNames.GetNameFor(socketIOEventTypes_0));
		}

		public void Off(string string_1, SocketIOCallback socketIOCallback_0)
		{
			eventTable_0.Unregister(string_1, socketIOCallback_0);
		}

		public void Off(SocketIOEventTypes socketIOEventTypes_0, SocketIOCallback socketIOCallback_0)
		{
			eventTable_0.Unregister(EventNames.GetNameFor(socketIOEventTypes_0), socketIOCallback_0);
		}

		private void OnTransportOpen(Socket socket_0, Packet packet_0, params object[] object_0)
		{
			if (String_0 != "/")
			{
				((IManager)SocketManager_0).SendPacket(new Packet(TransportEventTypes.Message, SocketIOEventTypes.Connect, String_0, string.Empty));
			}
			Boolean_0 = true;
		}
	}
}
