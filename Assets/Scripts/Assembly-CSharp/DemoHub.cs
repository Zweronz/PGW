using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using UnityEngine;

internal class DemoHub : Hub
{
	private float float_0;

	private string string_1 = "Not Started!";

	private string string_2 = string.Empty;

	private string string_3 = string.Empty;

	private string string_4 = string.Empty;

	private string string_5 = string.Empty;

	private string string_6 = string.Empty;

	private string string_7 = string.Empty;

	private string string_8 = string.Empty;

	private string string_9 = string.Empty;

	private string string_10 = string.Empty;

	private string string_11 = string.Empty;

	private string string_12 = string.Empty;

	private string string_13 = string.Empty;

	private string string_14 = string.Empty;

	private string string_15 = string.Empty;

	private string string_16 = string.Empty;

	private string string_17 = string.Empty;

	private GUIMessageList guimessageList_0 = new GUIMessageList();

	public DemoHub()
		: base("demo")
	{
		On("invoke", Invoke);
		On("signal", Signal);
		On("groupAdded", GroupAdded);
		On("fromArbitraryCode", FromArbitraryCode);
	}

	public void ReportProgress(string string_18)
	{
		Call("reportProgress", OnLongRunningJob_Done, null, OnLongRunningJob_Progress, string_18);
	}

	public void OnLongRunningJob_Progress(Hub hub_0, ClientMessage clientMessage_0, ProgressMessage progressMessage_0)
	{
		float_0 = (float)progressMessage_0.Double_0;
		string_1 = progressMessage_0.Double_0 + "%";
	}

	public void OnLongRunningJob_Done(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		string_1 = resultMessage_0.Object_0.ToString();
		MultipleCalls();
	}

	public void MultipleCalls()
	{
		Call("multipleCalls");
	}

	public void DynamicTask()
	{
		Call("dynamicTask", OnDynamicTask_Done, OnDynamicTask_Failed);
	}

	private void OnDynamicTask_Failed(Hub hub_0, ClientMessage clientMessage_0, FailureMessage failureMessage_0)
	{
		string_4 = string.Format("The dynamic task failed :( {0}", failureMessage_0.String_0);
	}

	private void OnDynamicTask_Done(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		string_4 = string.Format("The dynamic task! {0}", resultMessage_0.Object_0);
	}

	public void AddToGroups()
	{
		Call("addToGroups");
	}

	public void GetValue()
	{
		Call("getValue", delegate(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
		{
			string_5 = string.Format("The value is {0} after 5 seconds", resultMessage_0.Object_0);
		});
	}

	public void TaskWithException()
	{
		Call("taskWithException", null, delegate(Hub hub_0, ClientMessage clientMessage_0, FailureMessage failureMessage_0)
		{
			string_6 = string.Format("Error: {0}", failureMessage_0.String_0);
		});
	}

	public void GenericTaskWithException()
	{
		Call("genericTaskWithException", null, delegate(Hub hub_0, ClientMessage clientMessage_0, FailureMessage failureMessage_0)
		{
			string_7 = string.Format("Error: {0}", failureMessage_0.String_0);
		});
	}

	public void SynchronousException()
	{
		Call("synchronousException", null, delegate(Hub hub_0, ClientMessage clientMessage_0, FailureMessage failureMessage_0)
		{
			string_8 = string.Format("Error: {0}", failureMessage_0.String_0);
		});
	}

	public void PassingDynamicComplex(object object_0)
	{
		Call("passingDynamicComplex", delegate(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
		{
			string_9 = string.Format("The person's age is {0}", resultMessage_0.Object_0);
		}, object_0);
	}

	public void SimpleArray(int[] int_0)
	{
		Call("simpleArray", delegate
		{
			string_10 = "Simple array works!";
		}, int_0);
	}

	public void ComplexType(object object_0)
	{
		Call("complexType", delegate
		{
			string_11 = string.Format("Complex Type -> {0}", ((IHub)this).Connection.IJsonEncoder_0.Encode(base.Dictionary_0["person"]));
		}, object_0);
	}

	public void ComplexArray(object[] object_0)
	{
		Call("ComplexArray", delegate
		{
			string_12 = "Complex Array Works!";
		}, new object[1] { object_0 });
	}

	public void Overload()
	{
		Call("Overload", OnVoidOverload_Done);
	}

	private void OnVoidOverload_Done(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		string_13 = "Void Overload called";
		Overload(101);
	}

	public void Overload(int int_0)
	{
		Call("Overload", OnIntOverload_Done, int_0);
	}

	private void OnIntOverload_Done(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
	{
		string_14 = string.Format("Overload with return value called => {0}", resultMessage_0.Object_0.ToString());
	}

	public void ReadStateValue()
	{
		Call("readStateValue", delegate(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
		{
			string_15 = string.Format("Read some state! => {0}", resultMessage_0.Object_0);
		});
	}

	public void PlainTask()
	{
		Call("plainTask", delegate
		{
			string_16 = "Plain Task Result";
		});
	}

	public void GenericTaskWithContinueWith()
	{
		Call("genericTaskWithContinueWith", delegate(Hub hub_0, ClientMessage clientMessage_0, ResultMessage resultMessage_0)
		{
			string_17 = resultMessage_0.Object_0.ToString();
		});
	}

	private void FromArbitraryCode(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		string_2 = methodCallMessage_0.Object_0[0] as string;
	}

	private void GroupAdded(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		if (!string.IsNullOrEmpty(string_3))
		{
			string_3 = "Group Already Added!";
		}
		else
		{
			string_3 = "Group Added!";
		}
	}

	private void Signal(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		string_4 = string.Format("The dynamic task! {0}", methodCallMessage_0.Object_0[0]);
	}

	private void Invoke(Hub hub_0, MethodCallMessage methodCallMessage_0)
	{
		guimessageList_0.Add(string.Format("{0} client state index -> {1}", methodCallMessage_0.Object_0[0], base.Dictionary_0["index"]));
	}

	public void Draw()
	{
		GUILayout.Label("Arbitrary Code");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string.Format("Sending {0} from arbitrary code without the hub itself!", string_2));
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Group Added");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_3);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Dynamic Task");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_4);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Report Progress");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginVertical();
		GUILayout.Label(string_1);
		GUILayout.HorizontalSlider(float_0, 0f, 100f);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Generic Task");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_5);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Task With Exception");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_6);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Generic Task With Exception");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_7);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Synchronous Exception");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_8);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Invoking hub method with dynamic");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_9);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Simple Array");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_10);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Complex Type");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_11);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Complex Array");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_12);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Overloads");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginVertical();
		GUILayout.Label(string_13);
		GUILayout.Label(string_14);
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Read State Value");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_15);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Plain Task");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_16);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Generic Task With ContinueWith");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		GUILayout.Label(string_17);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.Label("Message Pump");
		GUILayout.BeginHorizontal();
		GUILayout.Space(20f);
		guimessageList_0.Draw(Screen.width - 40, 270f);
		GUILayout.EndHorizontal();
		GUILayout.Space(10f);
	}
}
