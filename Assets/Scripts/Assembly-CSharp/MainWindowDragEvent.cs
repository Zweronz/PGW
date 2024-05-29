using engine.events;

public sealed class MainWindowDragEvent : BaseEvent
{
	public enum EventType
	{
		OnStartDrag = 0,
		OnDrag = 1,
		OnStopDrag = 2
	}
}
