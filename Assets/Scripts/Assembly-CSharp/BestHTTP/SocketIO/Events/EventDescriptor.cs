using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SocketIO.Events
{
	internal sealed class EventDescriptor
	{
		private SocketIOCallback[] socketIOCallback_0;

		[CompilerGenerated]
		private List<SocketIOCallback> list_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		public List<SocketIOCallback> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			private set
			{
				list_0 = value;
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
			private set
			{
				bool_1 = value;
			}
		}

		public EventDescriptor(bool bool_2, bool bool_3, SocketIOCallback socketIOCallback_1)
		{
			Boolean_0 = bool_2;
			Boolean_1 = bool_3;
			List_0 = new List<SocketIOCallback>(1);
			if (socketIOCallback_1 != null)
			{
				List_0.Add(socketIOCallback_1);
			}
		}

		public void Call(Socket socket_0, Packet packet_0, params object[] object_0)
		{
			if (socketIOCallback_0 == null || socketIOCallback_0.Length < List_0.Count)
			{
				Array.Resize(ref socketIOCallback_0, List_0.Count);
			}
			List_0.CopyTo(socketIOCallback_0);
			for (int i = 0; i < socketIOCallback_0.Length; i++)
			{
				try
				{
					socketIOCallback_0[i](socket_0, packet_0, object_0);
				}
				catch (Exception ex)
				{
					((ISocket)socket_0).EmitError(SocketIOErrors.User, ex.Message + " " + ex.StackTrace);
					HTTPManager.ILogger_0.Exception("EventDescriptor", "Call", ex);
				}
				if (Boolean_0)
				{
					List_0.Remove(socketIOCallback_0[i]);
				}
				socketIOCallback_0[i] = null;
			}
		}
	}
}
