using UnityEngine;
using engine.events;
using engine.unity;

public class UpdateEventDispatcher : MonoSingleton<UpdateEventDispatcher>
{
	private float float_0;

	private void Update()
	{
		DependSceneEvent<MainUpdate>.GlobalDispatch();
		float_0 += Time.deltaTime;
		if (float_0 >= 1f)
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalDispatch();
			float_0 = 0f;
		}
	}

	private void OnLevelWasLoaded(int int_0)
	{
		DependSceneEvent<ChangeSceneUnityEvent>.GlobalDispatch();
	}

	private void OnApplicationQuit()
	{
		DependSceneEvent<ApplicationQuitUnityEvent>.GlobalDispatch();
	}

	private void OnApplicationPause(bool bool_0)
	{
		DependSceneEvent<ApplicationPauseUnityEvent, bool>.GlobalDispatch(bool_0);
	}

	private void OnApplicationFocus(bool bool_0)
	{
		DependSceneEvent<ApplicationFocusUnityEvent, bool>.GlobalDispatch(bool_0);
	}
}
