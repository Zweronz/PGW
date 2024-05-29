using UnityEngine;
using engine.events;
using engine.network;
using engine.operations;

public sealed class ProcessWebSocketConnect
{
	private static ProcessWebSocketConnect processWebSocketConnect_0;

	public static ProcessWebSocketConnect ProcessWebSocketConnect_0
	{
		get
		{
			return processWebSocketConnect_0 ?? (processWebSocketConnect_0 = new ProcessWebSocketConnect());
		}
	}

	public void ProcessConnect(string string_0, string string_1)
	{
		ConnectionStatusEvent @event = EventManager.EventManager_0.GetEvent<ConnectionStatusEvent>();
		if (!@event.Contains(OnNetworkFailure))
		{
			@event.Subscribe(OnNetworkFailure, BaseConnection.ConnectionStatus.CONNECT_FAILURE);
		}
		ConnectionResponseEvent event2 = EventManager.EventManager_0.GetEvent<ConnectionResponseEvent>();
		if (!event2.Contains(OnNetworkFailure))
		{
			event2.Subscribe(OnNetworkFailure, BaseConnection.ConnectionStatus.ERROR);
		}
		ConnectOperation connectOperation = new ConnectOperation(string_0, string_1, BaseConnection.ConnectionType.WEB_SOCKET);
		connectOperation.Subscribe(OnConnectComplete, Operation.StatusEvent.Complete);
		OperationsManager.OperationsManager_0.Add(connectOperation);
	}

	private void OnNetworkFailure(ConnectionStatusEventArg connectionStatusEventArg_0)
	{
		Debug.Log("[AppControllerTest::OnNetworkFailure] Network websocket connectopn error, need cloese app! Error: " + connectionStatusEventArg_0.string_0);
		AppControllerTest.AppControllerTest_0.NeedStopApp(AppControllerTest.AppStopReason.WEBSOCKET_CONNECTION_BROKEN);
	}

	private void OnConnectComplete(Operation operation_0)
	{
		operation_0.UnsubscribeAll();
		ConnectOperation connectOperation = operation_0 as ConnectOperation;
		if (connectOperation.Boolean_1)
		{
			Debug.Log("[ProcessWebSocketConnect::OnConnectComplete] Connect operation failed!");
			AppControllerTest.AppControllerTest_0.NeedStopApp(AppControllerTest.AppStopReason.WEBSOCKET_CONNECTION_CMD_ERROR);
		}
		else
		{
			Debug.Log("[ProcessWebSocketConnect::OnConnectComplete] Connect operation success!");
		}
		AbstractNetworkCommand.InitTest();
	}
}
