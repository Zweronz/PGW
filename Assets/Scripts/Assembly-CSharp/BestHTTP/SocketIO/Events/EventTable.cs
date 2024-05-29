using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BestHTTP.Logger;

namespace BestHTTP.SocketIO.Events
{
	internal sealed class EventTable
	{
		private Dictionary<string, List<EventDescriptor>> dictionary_0 = new Dictionary<string, List<EventDescriptor>>();

		[CompilerGenerated]
		private Socket socket_0;

		private Socket Socket_0
		{
			[CompilerGenerated]
			get
			{
				return socket_0;
			}
			[CompilerGenerated]
			set
			{
				socket_0 = value;
			}
		}

		public EventTable(Socket socket_1)
		{
			Socket_0 = socket_1;
		}

		public void Register(string string_0, SocketIOCallback socketIOCallback_0, bool bool_0, bool bool_1)
		{
			List<EventDescriptor> value;
			if (!dictionary_0.TryGetValue(string_0, out value))
			{
				dictionary_0.Add(string_0, value = new List<EventDescriptor>(1));
			}
			EventDescriptor eventDescriptor = value.Find((EventDescriptor eventDescriptor_0) => eventDescriptor_0.Boolean_0 == bool_0 && eventDescriptor_0.Boolean_1 == bool_1);
			if (eventDescriptor == null)
			{
				value.Add(new EventDescriptor(bool_0, bool_1, socketIOCallback_0));
			}
			else
			{
				eventDescriptor.List_0.Add(socketIOCallback_0);
			}
		}

		public void Unregister(string string_0)
		{
			dictionary_0.Remove(string_0);
		}

		public void Unregister(string string_0, SocketIOCallback socketIOCallback_0)
		{
			List<EventDescriptor> value;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					value[i].List_0.Remove(socketIOCallback_0);
				}
			}
		}

		public void Call(string string_0, Packet packet_0, params object[] object_0)
		{
			if (HTTPManager.ILogger_0.Loglevels_0 <= Loglevels.All)
			{
				HTTPManager.ILogger_0.Verbose("EventTable", "Call - " + string_0);
			}
			List<EventDescriptor> value;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					value[i].Call(Socket_0, packet_0, object_0);
				}
			}
		}

		public void Call(Packet packet_0)
		{
			string text = packet_0.DecodeEventName();
			string text2 = ((packet_0.SocketIOEventTypes_0 == SocketIOEventTypes.Unknown) ? EventNames.GetNameFor(packet_0.TransportEventTypes_0) : EventNames.GetNameFor(packet_0.SocketIOEventTypes_0));
			object[] object_ = null;
			if (HasSubsciber(text) || HasSubsciber(text2))
			{
				if (packet_0.TransportEventTypes_0 == TransportEventTypes.Message && (packet_0.SocketIOEventTypes_0 == SocketIOEventTypes.Event || packet_0.SocketIOEventTypes_0 == SocketIOEventTypes.BinaryEvent) && ShouldDecodePayload(text))
				{
					object_ = packet_0.Decode(Socket_0.SocketManager_0.IJsonEncoder_0);
				}
				if (!string.IsNullOrEmpty(text))
				{
					Call(text, packet_0, object_);
				}
				if (!packet_0.Boolean_1 && ShouldDecodePayload(text2))
				{
					object_ = packet_0.Decode(Socket_0.SocketManager_0.IJsonEncoder_0);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					Call(text2, packet_0, object_);
				}
			}
		}

		public void Clear()
		{
			dictionary_0.Clear();
		}

		private bool ShouldDecodePayload(string string_0)
		{
			List<EventDescriptor> value;
			if (dictionary_0.TryGetValue(string_0, out value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].Boolean_1 && value[i].List_0.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool HasSubsciber(string string_0)
		{
			return dictionary_0.ContainsKey(string_0);
		}
	}
}
