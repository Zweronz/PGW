using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

internal class TypedDemoHub : Hub
{
	private string string_1 = string.Empty;

	private string string_2 = string.Empty;

	public TypedDemoHub()
		: base("typeddemohub")
	{
		On("Echo", Echo);
	}

	private void Echo(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		string_2 = string.Format("{0} #{1} triggered!", methodCallMessage_0.Object_0[0], methodCallMessage_0.Object_0[1]);
	}

	public void Echo(string string_3)
	{
		Call("echo", OnEcho_Done, string_3);
	}

	private void OnEcho_Done(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		string_1 = "TypedDemoHub.Echo(string message) invoked!";
	}

	public void Draw()
	{
		GUILayout.Label("Typed callback");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginVertical();
		GUILayout.Label(string_1);
		GUILayout.Label(string_2);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
	}
}
