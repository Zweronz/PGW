using UnityEngine;
using engine.events;

public class MainWindowDragDispatcher : MonoBehaviour
{
	private MainWindowDragEvent mainWindowDragEvent_0;

	private MainWindowDragEvent.EventType eventType_0 = MainWindowDragEvent.EventType.OnStopDrag;

	private void Awake()
	{
		mainWindowDragEvent_0 = EventManager.EventManager_0.GetEvent<MainWindowDragEvent>();
	}

	private void Update()
	{
		if (mainWindowDragEvent_0 != null && eventType_0 != MainWindowDragEvent.EventType.OnStopDrag)
		{
			mainWindowDragEvent_0.Dispatch(MainWindowDragEvent.EventType.OnDrag);
		}
	}

	private void OnPress(bool bool_0)
	{
		if (mainWindowDragEvent_0 != null)
		{
			eventType_0 = MainWindowDragEvent.EventType.OnStopDrag;
			if (bool_0)
			{
				eventType_0 = MainWindowDragEvent.EventType.OnStartDrag;
			}
			mainWindowDragEvent_0.Dispatch(eventType_0);
		}
	}
}
