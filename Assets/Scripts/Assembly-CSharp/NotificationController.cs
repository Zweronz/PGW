using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using engine.controllers;
using engine.data;
using engine.events;
using engine.helpers;
using engine.unity;

public sealed class NotificationController
{
	public const int int_0 = 3;

	public const float float_0 = 0.7f;

	private static NotificationController notificationController_0;

	private Dictionary<NotificationType, Queue<NotificationViewData>> dictionary_0 = new Dictionary<NotificationType, Queue<NotificationViewData>>();

	private List<NotificationView> list_0 = new List<NotificationView>();

	private List<NotificationData> list_1 = new List<NotificationData>();

	private Vector3 vector3_0 = new Vector3(0.7f, 0.7f);

	private int int_1;

	private Dictionary<NotificationType, List<NotificationType>> dictionary_1 = new Dictionary<NotificationType, List<NotificationType>> { 
	{
		NotificationType.NOTIFICATION_CLAN_ENTER,
		new List<NotificationType>
		{
			NotificationType.NOTIFICATION_CLAN_INVITE,
			NotificationType.NOTIFICATION_4LEVEL
		}
	} };

	[CompilerGenerated]
	private static Comparison<NotificationData> comparison_0;

	public static NotificationController NotificationController_0
	{
		get
		{
			if (notificationController_0 == null)
			{
				notificationController_0 = new NotificationController();
			}
			return notificationController_0;
		}
	}

	private NotificationController()
	{
	}

	public void Init()
	{
		if (!DependSceneEvent<MainUpdateOneSecond>.Contains(OnTickTimer))
		{
			DependSceneEvent<MainUpdateOneSecond>.GlobalSubscribe(OnTickTimer);
		}
		if (!StorageManager.StorageManager_0.Contains(CalculatePriority))
		{
			StorageManager.StorageManager_0.Subscribe(CalculatePriority, StorageManager.StatusEvent.LOADING_COMPLETE);
		}
		if (!DependSceneEvent<EventApplyBonus, ApplyBonusNetworkCommand>.Contains(OnMailValidated))
		{
			DependSceneEvent<EventApplyBonus, ApplyBonusNetworkCommand>.GlobalSubscribe(OnMailValidated);
		}
		BaseGameWindow.Subscribe(GameWindowType.None, OnShowAnyWindow);
	}

	public void Push(NotificationType notificationType_0, NotificationViewData notificationViewData_0)
	{
		if (!dictionary_0.ContainsKey(notificationType_0))
		{
			dictionary_0.Add(notificationType_0, new Queue<NotificationViewData>());
		}
		if (notificationViewData_0 == null)
		{
			notificationViewData_0 = new NotificationViewData();
		}
		NotificationData notificationData = GetNotificationData(notificationType_0);
		if (notificationData == null)
		{
			Log.AddLineFormat("NotificationController::Push > Failed to push notification {0} type. There is no data.", notificationType_0);
			return;
		}
		if (notificationData.int_1 > 0 && notificationData.bool_0)
		{
			notificationViewData_0.Int32_0 = (int)Utility.Double_0 + notificationData.int_1;
		}
		dictionary_0[notificationType_0].Enqueue(notificationViewData_0);
		FilterQueue();
	}

	public NotificationData GetNotificationData(NotificationType notificationType_0)
	{
		return NotificationStorage.Get.Storage.GetObjectByKey(notificationType_0);
	}

	public bool HasAnyNotifiaction(NotificationType notificationType_0)
	{
		foreach (NotificationView item in list_0)
		{
			if (item.notificationData_0.notificationType_0 == notificationType_0)
			{
				return true;
			}
		}
		if (dictionary_0.ContainsKey(notificationType_0) && dictionary_0[notificationType_0].Count > 0)
		{
			return true;
		}
		return false;
	}

	public void CloseSameNotifications(NotificationType notificationType_0, NotificationView notificationView_0)
	{
		List<NotificationView> list = new List<NotificationView>();
		foreach (NotificationView item in list_0)
		{
			if (item.notificationData_0.notificationType_0 == notificationType_0 && !item.Equals(notificationView_0))
			{
				list.Add(item);
			}
		}
		foreach (NotificationView item2 in list)
		{
			item2.Close();
		}
	}

	private void OnShowAnyWindow(GameWindowEventArg gameWindowEventArg_0)
	{
		int_1 = 1;
		HideAllNotifications(true);
	}

	private void FilterQueue()
	{
		List<NotificationType> list = new List<NotificationType>();
		foreach (KeyValuePair<NotificationType, Queue<NotificationViewData>> item in dictionary_0)
		{
			if (dictionary_1.ContainsKey(item.Key))
			{
				list.AddRange(dictionary_1[item.Key]);
			}
		}
		foreach (NotificationType item2 in list)
		{
			if (dictionary_0.ContainsKey(item2))
			{
				dictionary_0[item2].Clear();
				dictionary_0.Remove(item2);
			}
		}
	}

	private void CalculatePriority()
	{
		list_1.Clear();
		foreach (KeyValuePair<NotificationType, NotificationData> item in (IEnumerable<KeyValuePair<NotificationType, NotificationData>>)NotificationStorage.Get.Storage)
		{
			list_1.Add(item.Value);
		}
		list_1.Sort(delegate(NotificationData notificationData_0, NotificationData notificationData_1)
		{
			if (notificationData_0.int_0 == 0 && notificationData_1.int_0 == 0)
			{
				return 0;
			}
			if (notificationData_0.int_0 == 0 && notificationData_1.int_0 > 0)
			{
				return 1;
			}
			return (notificationData_1.int_0 == 0 && notificationData_0.int_0 > 0) ? (-1) : notificationData_0.int_0.CompareTo(notificationData_1.int_0);
		});
	}

	private void OnTickTimer()
	{
		if (int_1-- <= 0)
		{
			CheckNotificationForHide();
			CheckNotificationsForShow();
		}
	}

	private void CheckNotificationsForShow()
	{
		if (list_0.Count >= 3)
		{
			return;
		}
		foreach (NotificationData item in list_1)
		{
			if (!dictionary_0.ContainsKey(item.notificationType_0) || !CanShowNotification(item))
			{
				continue;
			}
			Queue<NotificationViewData> queue = dictionary_0[item.notificationType_0];
			if (queue.Count == 0)
			{
				dictionary_0.Remove(item.notificationType_0);
				continue;
			}
			NotificationViewData notificationViewData = queue.Dequeue();
			if (notificationViewData != null && notificationViewData.Int32_0 > 0 && Utility.Double_0 >= (double)notificationViewData.Int32_0)
			{
				continue;
			}
			ShowNotification(item.notificationType_0, notificationViewData);
			break;
		}
	}

	private bool CanShowNotification(NotificationData notificationData_0)
	{
		if (AppStateController.AppStateController_0.States_0 != AppStateController.States.MAIN_MENU)
		{
			return false;
		}
		return true;
	}

	private void CheckNotificationForHide()
	{
		NotificationView notificationView = null;
		foreach (NotificationView item in list_0)
		{
			if (item.notificationViewData_0 != null && !(Utility.Double_0 < (double)item.notificationViewData_0.Int32_0))
			{
				notificationView = item;
				break;
			}
		}
		if (notificationView != null)
		{
			notificationView.Close();
		}
		if (AppStateController.AppStateController_0.States_0 != AppStateController.States.MAIN_MENU)
		{
			HideAllNotifications(true);
		}
	}

	private void ShowNotification(NotificationType notificationType_0, NotificationViewData notificationViewData_0)
	{
		int num = (int)(notificationType_0 - 1);
		if (num >= NotificationWindow.NotificationWindow_0.views.Length)
		{
			Log.AddLineFormat("NotificationController::ShowNotification > Failed to show notification {0} type in index {1}", notificationType_0, num);
			return;
		}
		NotificationView notificationView = NotificationWindow.NotificationWindow_0.views[num];
		if (notificationView == null)
		{
			Log.AddLineFormat("NotificationController::ShowNotification > Failed to show notification {0} type. View not found!", notificationType_0);
			return;
		}
		NotificationData notificationData = GetNotificationData(notificationType_0);
		if (notificationViewData_0 != null && notificationViewData_0.Int32_0 == 0 && notificationData.int_1 > 0)
		{
			notificationViewData_0.Int32_0 = (int)Utility.Double_0 + notificationData.int_1;
		}
		GameObject gameObject = NGUITools.AddChild(NotificationWindow.NotificationWindow_0.gameObject, notificationView.gameObject);
		gameObject.name = string.Format("{0}_{1}", notificationType_0.ToString(), list_0.Count + 1);
		gameObject.transform.localScale = vector3_0;
		NotificationView component = gameObject.GetComponent<NotificationView>();
		component.PreInit(notificationViewData_0);
		component.Init();
		NotificationWindow.NotificationWindow_0.Show();
		NGUITools.SetActive(gameObject, true);
		component.Appear();
		UpdateUpPositions();
		list_0.Add(component);
	}

	public void HideNotification(NotificationView notificationView_0)
	{
		NGUITools.SetActive(notificationView_0.gameObject, false);
		int num = list_0.FindIndex((NotificationView notificationView_1) => notificationView_1.Equals(notificationView_0));
		if (num > 0)
		{
			UpdateDownPositions(num);
		}
		list_0.RemoveAt(num);
		UnityEngine.Object.Destroy(notificationView_0.gameObject);
		if (list_0.Count == 0)
		{
			NotificationWindow.NotificationWindow_0.Hide();
		}
	}

	private void HideAllNotifications(bool bool_0 = false)
	{
		List<NotificationView> list = new List<NotificationView>(list_0);
		if (bool_0)
		{
			foreach (NotificationView item in list)
			{
				HideNotification(item);
			}
			return;
		}
		foreach (NotificationView item2 in list)
		{
			item2.Close();
		}
	}

	private void UpdateUpPositions()
	{
		foreach (NotificationView item in list_0)
		{
			item.Up();
		}
	}

	private void UpdateDownPositions(int int_2)
	{
		for (int i = 0; i < int_2; i++)
		{
			if (i < list_0.Count)
			{
				list_0[i].Down();
			}
		}
	}

	private void OnMailValidated(ApplyBonusNetworkCommand applyBonusNetworkCommand_0)
	{
		if (applyBonusNetworkCommand_0.bonusType_0 == ApplyBonusNetworkCommand.BonusType.MAIL_VALIDITY)
		{
			PaymentNotificationData paymentNotificationData = new PaymentNotificationData();
			paymentNotificationData.Int32_2 = applyBonusNetworkCommand_0.dictionary_0[MoneyType.MONEY_TYPE_COINS];
			NotificationController_0.Push(NotificationType.NOTIFICATION_EMAIL_CONFIRM, paymentNotificationData);
			UserOverrideContentGroupStorage.Dispatch(OverrideContentGroupEventType.UPDATE_ALL, new OverrideContentGroupEventData());
		}
	}
}
