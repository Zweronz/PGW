using System.Collections.Generic;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

internal class BaseHub : Hub
{
	private string string_1;

	private GUIMessageList guimessageList_0 = new GUIMessageList();

	public BaseHub(string string_2, string string_3)
		: base(string_2)
	{
		string_1 = string_3;
		On("joined", Joined);
		On("rejoined", Rejoined);
		On("left", Left);
		On("invoked", Invoked);
	}

	private void Joined(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		Dictionary<string, object> dictionary = methodCallMessage_0.Object_0[2] as Dictionary<string, object>;
		guimessageList_0.Add(string.Format("{0} joined at {1}\n\tIsAuthenticated: {2} IsAdmin: {3} UserName: {4}", methodCallMessage_0.Object_0[0], methodCallMessage_0.Object_0[1], dictionary["IsAuthenticated"], dictionary["IsAdmin"], dictionary["UserName"]));
	}

	private void Rejoined(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		guimessageList_0.Add(string.Format("{0} reconnected at {1}", methodCallMessage_0.Object_0[0], methodCallMessage_0.Object_0[1]));
	}

	private void Left(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		guimessageList_0.Add(string.Format("{0} left at {1}", methodCallMessage_0.Object_0[0], methodCallMessage_0.Object_0[1]));
	}

	private void Invoked(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		guimessageList_0.Add(string.Format("{0} invoked hub method at {1}", methodCallMessage_0.Object_0[0], methodCallMessage_0.Object_0[1]));
	}

	public void InvokedFromClient()
	{
		Call("invokedFromClient", OnInvoked, OnInvokeFailed);
	}

	private void OnInvoked(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		Debug.Log(hub_0.String_0 + " invokedFromClient success!");
	}

	private void OnInvokeFailed(Hub hub_0, ClientMessage clientMessage_0, FailureMessage failureMessage_0)
	{
		Debug.LogWarning(hub_0.String_0 + " " + failureMessage_0.String_0);
	}

	public void Draw()
	{
		GUILayout.Label(string_1);
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		guimessageList_0.Draw(Screen.width - 20, 100f);
		GUILayout.EndHorizontal();
	}
}
