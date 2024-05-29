using System;
using engine.events;
using engine.helpers;
using engine.network;
using engine.unity;

namespace engine.operations
{
	public sealed class ConnectOperation : Operation
	{
		private BaseConnection baseConnection_0;

		private string string_1;

		private string string_2;

		private BaseConnection.ConnectionType connectionType_0;

		public ConnectOperation(string string_3, string string_4, BaseConnection.ConnectionType connectionType_1, float float_0 = 2f)
		{
			string_1 = string_3;
			connectionType_0 = connectionType_1;
			string_2 = string_4;
		}

		protected override void Execute()
		{
			try
			{
				PrepareConnection();
			}
			catch (Exception ex)
			{
				MonoSingleton<Log>.Prop_0.DumpError(ex);
				ErrorConnectOperation(string.Format("Create connection error:  connetion type: {0}, error: {1}", connectionType_0, ex.Message));
			}
		}

		private void PrepareConnection()
		{
			switch (connectionType_0)
			{
			default:
				Log.AddLine(string.Format("[ConnectOperation::PrepareConnection]: Connection type {0} not implemented!", connectionType_0));
				break;
			case BaseConnection.ConnectionType.WEB_SOCKET:
				CreateWebsocketConnect();
				break;
			}
		}

		private void CreateWebsocketConnect()
		{
			if (BaseConnection.BaseConnection_0 != null && BaseConnection.BaseConnection_0.Boolean_1)
			{
				CompleteConnectOperation(string.Format("Websocket already connection! Working using it: server URL: {0}, auth key: {1}", string_1, string_2));
				return;
			}
			Log.AddLineFormat("[ConnectOperation::CreateWebsocketConnect. Create websocket connection]: server URL: {0}, auth key: {1}", string_1, string_2);
			Subscribe();
			WebSocketSharpConnection webSocketSharpConnection = new WebSocketSharpConnection(string_1, string_2);
			webSocketSharpConnection.Connect(string.Empty);
		}

		public void OnOpen(ConnectionStatusEventArg connectionStatusEventArg_0)
		{
			Unsubscribe();
			CompleteConnectOperation("Websocket connected: " + ((connectionStatusEventArg_0 == null) ? string.Empty : connectionStatusEventArg_0.string_0));
		}

		public void OnError(ConnectionStatusEventArg connectionStatusEventArg_0)
		{
			Unsubscribe();
			ErrorConnectOperation("Websocket error: " + ((connectionStatusEventArg_0 == null) ? string.Empty : connectionStatusEventArg_0.string_0));
		}

		public void OnClose(ConnectionStatusEventArg connectionStatusEventArg_0)
		{
			Unsubscribe();
			ErrorConnectOperation("Websocket closed: " + ((connectionStatusEventArg_0 == null) ? string.Empty : connectionStatusEventArg_0.string_0));
		}

		private void Subscribe()
		{
			ConnectionStatusEvent @event = EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>();
			if (!@event.Contains(OnOpen, BaseConnection.ConnectionStatus.OPEN))
			{
				@event.Subscribe(OnOpen, BaseConnection.ConnectionStatus.OPEN);
			}
			if (!@event.Contains(OnError, BaseConnection.ConnectionStatus.ERROR))
			{
				@event.Subscribe(OnError, BaseConnection.ConnectionStatus.ERROR);
			}
			if (!@event.Contains(OnClose, BaseConnection.ConnectionStatus.CLOSE))
			{
				@event.Subscribe(OnClose, BaseConnection.ConnectionStatus.CLOSE);
			}
		}

		private void Unsubscribe()
		{
			ConnectionStatusEvent @event = EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>();
			if (@event.Contains(OnOpen, BaseConnection.ConnectionStatus.OPEN))
			{
				@event.Unsubscribe(OnOpen, BaseConnection.ConnectionStatus.OPEN);
			}
			if (@event.Contains(OnError, BaseConnection.ConnectionStatus.ERROR))
			{
				@event.Unsubscribe(OnError, BaseConnection.ConnectionStatus.ERROR);
			}
			if (@event.Contains(OnClose, BaseConnection.ConnectionStatus.CLOSE))
			{
				@event.Unsubscribe(OnClose, BaseConnection.ConnectionStatus.CLOSE);
			}
		}

		private void CompleteConnectOperation(string string_3)
		{
			Log.AddLineFormat("[ConnectOperation::CompleteConnectOperation]: {0}", string_3);
			Complete();
		}

		private void ErrorConnectOperation(string string_3)
		{
			Log.AddLineError("[ConnectOperation::ErrorConnectOperation]: {0}", string_3);
			Error();
		}
	}
}
