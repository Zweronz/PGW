using Holoville.HOTween;
using UnityEngine;

public abstract class NotificationView : MonoBehaviour
{
	public NotificationType notifyType;

	public UILabel title;

	public NotificationViewData notificationViewData_0;

	public NotificationData notificationData_0;

	public bool bool_0;

	private UIWidget uiwidget_0;

	private Transform transform_0;

	private Vector3 vector3_0 = new Vector3(0f, -700f);

	private Vector3 vector3_1 = new Vector3(0f, -134f);

	private Vector3 vector3_2 = new Vector3(1500f, -134f);

	private Vector3 vector3_3 = new Vector3(0f, 0f);

	private Tweener tweener_0;

	private float float_0 = 0.5f;

	private float float_1 = 0.5f;

	private float float_2 = 0.2f;

	private float float_3 = 124f;

	private float float_4;

	public string String_0
	{
		get
		{
			return (notificationData_0 == null) ? string.Empty : Localizer.Get(notificationData_0.string_0);
		}
	}

	public void PreInit(NotificationViewData notificationViewData_1)
	{
		notificationViewData_0 = notificationViewData_1;
		notificationData_0 = NotificationController.NotificationController_0.GetNotificationData(notifyType);
		uiwidget_0 = base.transform.GetChild(0).gameObject.GetComponent<UIWidget>();
		transform_0 = uiwidget_0.transform;
		float_4 = 2f * float_3;
	}

	public abstract void Init();

	public void Close()
	{
		OnClose();
		Disappear();
	}

	public virtual void OnClose()
	{
	}

	private void Hide()
	{
		ResetIsTweening();
		NotificationController.NotificationController_0.HideNotification(this);
	}

	public void OnClickProcess()
	{
		if (!bool_0)
		{
			OnClick();
		}
	}

	public virtual void OnClick()
	{
	}

	public void OnCloseButtonClick()
	{
		if (!bool_0)
		{
			Close();
		}
	}

	private void ResetIsTweening()
	{
		bool_0 = false;
	}

	public virtual void Appear()
	{
		bool_0 = true;
		transform_0.localPosition = vector3_0;
		TweenParms tweenParms = new TweenParms();
		tweenParms.Prop("localPosition", vector3_1);
		tweenParms.Ease(EaseType.EaseInOutCubic);
		tweenParms.OnComplete(ResetIsTweening);
		HOTween.To(transform_0, float_0, tweenParms);
	}

	public virtual void Disappear(bool bool_1 = false)
	{
		bool_0 = true;
		vector3_2.y = transform_0.localPosition.y;
		TweenParms tweenParms = new TweenParms();
		tweenParms.Prop("localPosition", vector3_2);
		tweenParms.Ease(EaseType.EaseInOutCubic);
		tweenParms.OnComplete(Hide);
		tweener_0 = HOTween.To(transform_0, float_1, tweenParms);
		if (bool_1)
		{
			NotificationController.NotificationController_0.CloseSameNotifications(notificationData_0.notificationType_0, this);
		}
	}

	public virtual void Up()
	{
		float num = transform_0.localPosition.y + float_3;
		if (!(num > float_4))
		{
			vector3_3.y = transform_0.localPosition.y + float_3;
			TweenParms tweenParms = new TweenParms();
			tweenParms.Prop("localPosition", vector3_3);
			tweenParms.Ease(EaseType.EaseInOutCubic);
			if (tweener_0 != null)
			{
				tweener_0.Kill();
			}
			HOTween.To(transform_0, float_2, tweenParms);
		}
	}

	public virtual void Down()
	{
		vector3_3.y = transform_0.localPosition.y - float_3;
		TweenParms tweenParms = new TweenParms();
		tweenParms.Prop("localPosition", vector3_3);
		tweenParms.Ease(EaseType.EaseInOutCubic);
		HOTween.To(transform_0, float_2, tweenParms);
	}
}
