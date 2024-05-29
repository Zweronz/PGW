using System;
using BestHTTP.SignalR;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;
using UnityEngine;

internal class DemoHubSample : MonoBehaviour
{
	private readonly Uri uri_0 = new Uri("http://besthttpsignalr.azurewebsites.net/signalr");

	private Connection connection_0;

	private DemoHub demoHub_0;

	private TypedDemoHub typedDemoHub_0;

	private Hub hub_0;

	private string string_0 = string.Empty;

	private Vector2 vector2_0;

	private void Start()
	{
		demoHub_0 = new DemoHub();
		typedDemoHub_0 = new TypedDemoHub();
		hub_0 = new Hub("vbdemo");
		connection_0 = new Connection(uri_0, demoHub_0, typedDemoHub_0, hub_0);
		connection_0.IJsonEncoder_0 = new LitJsonEncoder();
		connection_0.OnConnected += delegate
		{
			var anon = new
			{
				gparam_0 = "Foo",
				gparam_1 = 20,
				gparam_2 = new
				{
					gparam_0 = "One Microsoft Way",
					gparam_1 = "98052"
				}
			};
			demoHub_0.ReportProgress("Long running job!");
			demoHub_0.AddToGroups();
			demoHub_0.GetValue();
			demoHub_0.TaskWithException();
			demoHub_0.GenericTaskWithException();
			demoHub_0.SynchronousException();
			demoHub_0.DynamicTask();
			demoHub_0.PassingDynamicComplex(anon);
			demoHub_0.SimpleArray(new int[3] { 5, 5, 6 });
			demoHub_0.ComplexType(anon);
			demoHub_0.ComplexArray(new object[3] { anon, anon, anon });
			demoHub_0.Overload();
			demoHub_0.Dictionary_0["name"] = "Testing state!";
			demoHub_0.ReadStateValue();
			demoHub_0.PlainTask();
			demoHub_0.GenericTaskWithContinueWith();
			typedDemoHub_0.Echo("Typed echo callback");
			hub_0.Call("readStateValue", delegate(Hub hub_1, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
			{
				string_0 = string.Format("Read some state from VB.NET! => {0}", (resultMessage_0.Object_0 != null) ? resultMessage_0.Object_0.ToString() : "undefined");
			});
		};
		connection_0.Open();
	}

	private void OnDestroy()
	{
		connection_0.Close();
	}

	private void OnGUI()
	{
		GUIHelper.DrawArea(GUIHelper.rect_0, true, delegate
		{
			vector2_0 = GUILayout.BeginScrollView(vector2_0, false, false);
			GUILayout.BeginVertical();
			demoHub_0.Draw();
			typedDemoHub_0.Draw();
			GUILayout.Label("Read State Value");
			GUILayout.BeginHorizontal();
			GUILayout.Space(20f);
			GUILayout.Label(string_0);
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.EndVertical();
			GUILayout.EndScrollView();
		});
	}
}
