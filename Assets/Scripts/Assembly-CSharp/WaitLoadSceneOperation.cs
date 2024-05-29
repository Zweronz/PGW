using UnityEngine;
using engine.events;
using engine.operations;

public sealed class WaitLoadSceneOperation : Operation
{
	private string string_1;

	public WaitLoadSceneOperation(string string_2)
	{
		string_1 = string_2;
	}

	protected override void Execute()
	{
		if (string.IsNullOrEmpty(string_1))
		{
			Complete();
		}
		else if (Application.loadedLevelName == string_1)
		{
			Complete();
		}
		else if (!DependSceneEvent<ChangeSceneUnityEvent>.Contains(LoadingComplete))
		{
			DependSceneEvent<ChangeSceneUnityEvent>.GlobalSubscribe(LoadingComplete);
		}
	}

	private void LoadingComplete()
	{
		if (string.IsNullOrEmpty(string_1))
		{
			Complete();
		}
		else if (!(Application.loadedLevelName != string_1))
		{
			if (DependSceneEvent<ChangeSceneUnityEvent>.Contains(LoadingComplete))
			{
				DependSceneEvent<ChangeSceneUnityEvent>.GlobalUnsubscribe(LoadingComplete);
			}
			Complete();
		}
	}
}
