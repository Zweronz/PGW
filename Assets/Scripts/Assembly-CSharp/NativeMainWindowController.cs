using UnityEngine;
using engine.events;
using engine.unity;

public class NativeMainWindowController : MonoSingleton<NativeMainWindowController>
{
	private BaseWindowHandler baseWindowHandler_0;

	private Vector2 vector2_0;

	private Vector2 vector2_1 = new Vector2(10f, -10f);

	private bool bool_0;

	public void SetBorderlessStyle()
	{
		if (baseWindowHandler_0 != null)
		{
			baseWindowHandler_0.SetBorderlessStyle();
		}
	}

	public void MinimizeWindwow()
	{
		if (baseWindowHandler_0 != null)
		{
			baseWindowHandler_0.MinimizeWindwow();
		}
	}

	public void MaximizeWindwow()
	{
		if (baseWindowHandler_0 != null)
		{
			baseWindowHandler_0.MaximizeWindwow();
		}
	}

	protected override void Init()
	{
		baseWindowHandler_0 = new WinWindowHandler("PixelGun 3D Launcher");
		EventManager.EventManager_0.GetEvent<MainWindowDragEvent>().Subscribe(OnMainWindowStartDrag, MainWindowDragEvent.EventType.OnStartDrag);
		EventManager.EventManager_0.GetEvent<MainWindowDragEvent>().Subscribe(OnMainWindowDrag, MainWindowDragEvent.EventType.OnDrag);
		EventManager.EventManager_0.GetEvent<MainWindowDragEvent>().Subscribe(OnMainWindowStopDrag, MainWindowDragEvent.EventType.OnStopDrag);
	}

	private void OnMainWindowStartDrag()
	{
		if (baseWindowHandler_0 != null)
		{
		}
	}

	private void OnMainWindowDrag()
	{
		if (baseWindowHandler_0 != null)
		{
			vector2_0.x = Input.GetAxis("Mouse X");
			vector2_0.y = Input.GetAxis("Mouse Y");
			vector2_0 = Vector2.Scale(vector2_0, vector2_1);
			baseWindowHandler_0.SetWindowPosition(baseWindowHandler_0.Vector2_0 + vector2_0);
		}
	}

	private void OnMainWindowStopDrag()
	{
		if (baseWindowHandler_0 != null)
		{
		}
	}
}
