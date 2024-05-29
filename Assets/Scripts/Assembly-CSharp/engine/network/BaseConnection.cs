using System;
using System.Runtime.CompilerServices;
using engine.events;
using engine.helpers;
using engine.unity;

namespace engine.network
{
	public abstract class BaseConnection
	{
		public enum ConnectionStatus
		{
			OPEN = 0,
			CLOSE = 1,
			ERROR = 2,
			CONNECT_FAILURE = 3
		}

		public enum ConnectionType
		{
			SOCKET = 0,
			HTTP = 1,
			WEB_SOCKET = 2
		}

		private SequenceNetworkRequests sequenceNetworkRequests_0 = new SequenceNetworkRequests();

		[CompilerGenerated]
		private static BaseConnection baseConnection_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		public static BaseConnection BaseConnection_0
		{
			[CompilerGenerated]
			get
			{
				return baseConnection_0;
			}
			[CompilerGenerated]
			protected set
			{
				baseConnection_0 = value;
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
			protected set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			protected set
			{
				string_1 = value;
			}
		}

		public abstract ConnectionType ConnectionType_0 { get; }

		public virtual bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			protected set
			{
				bool_0 = value;
			}
		}

		public virtual bool Boolean_0
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

		public BaseConnection(string string_2, string string_3)
		{
			String_0 = string_2;
			String_1 = string_3;
			BaseConnection_0 = this;
			Boolean_0 = false;
			DependSceneEvent<ApplicationQuitUnityEvent>.GlobalSubscribe(OnApplicationQuit);
			AbstractNetworkCommand.Init();
		}

		public virtual void OnApplicationQuit()
		{
			CloseConnect();
			sequenceNetworkRequests_0.CheckHangingRequests();
		}

		public abstract void Connect(string string_2);

		public abstract void CloseConnect();

		public virtual void Send(byte[] byte_0, AbstractNetworkCommand abstractNetworkCommand_0)
		{
			sequenceNetworkRequests_0.Add(abstractNetworkCommand_0);
		}

		protected virtual void OnOpen()
		{
			EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>().Dispatch(new ConnectionStatusEventArg
			{
				string_0 = string.Empty
			}, ConnectionStatus.OPEN);
		}

		protected virtual void OnClosed(string string_2)
		{
			EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>().Dispatch(new ConnectionStatusEventArg
			{
				string_0 = string_2
			}, ConnectionStatus.CLOSE);
		}

		protected virtual void OnError(string string_2)
		{
			EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>().Dispatch(new ConnectionStatusEventArg
			{
				string_0 = string_2
			}, ConnectionStatus.ERROR);
		}

		protected virtual void OnMessage(string string_2)
		{
		}

		protected virtual void OnBinary(byte[] byte_0)
		{
			AbstractNetworkCommand abstractNetworkCommand = null;
			try
			{
				abstractNetworkCommand = NetworkCommandWrapper.FromByte(byte_0);
				if (abstractNetworkCommand.NetworkCommandInfo_0.int_0 == NetworkCommandResultCode.int_0)
				{
					abstractNetworkCommand.Run();
				}
				if (abstractNetworkCommand.NetworkCommandInfo_0.int_1 >= 0)
				{
					sequenceNetworkRequests_0.ProcessCommand(abstractNetworkCommand);
				}
			}
			catch (Exception ex)
			{
				MonoSingleton<Log>.Prop_0.DumpError(ex, true);
				string string_ = string.Format("Received server command. Command: {0}, Error: {1}", (abstractNetworkCommand != null) ? abstractNetworkCommand.GetType().Name : string.Empty, ex.Message);
				Log.AddLine(string_, Log.LogLevel.WARNING);
				EventManager.EventManager_0.GetEvent<ConnectionResponseEvent>().Dispatch(new ConnectionStatusEventArg
				{
					string_0 = string_
				}, ConnectionStatus.ERROR);
			}
		}
	}
}
