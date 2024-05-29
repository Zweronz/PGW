using UnityEngine;
using engine.unity;

public class NotificationWindow : MonoBehaviour
{
	private static NotificationWindow notificationWindow_0;

	public NotificationView[] views;

	public static NotificationWindow NotificationWindow_0
	{
		get
		{
			if (notificationWindow_0 == null)
			{
				notificationWindow_0 = ScreenController.ScreenController_0.LoadUI("NotificationWindow").GetComponent<NotificationWindow>();
				notificationWindow_0.Init();
			}
			return notificationWindow_0;
		}
	}

	private void Init()
	{
		NotificationView[] array = views;
		foreach (NotificationView notificationView in array)
		{
			if (!(notificationView == null))
			{
				NGUITools.SetActive(notificationView.gameObject, false);
			}
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}
}
