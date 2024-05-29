using System;
using System.Runtime.CompilerServices;
using WebSocketSharp;
using engine.unity;
using pixelgun.tutorial;

public class NotificationBankBuy : NotificationView
{
	[CompilerGenerated]
	private static Action action_0;

	public override void Init()
	{
		PaymentNotificationData paymentNotificationData = notificationViewData_0 as PaymentNotificationData;
		title.String_0 = string.Format(base.String_0, ":coin:", paymentNotificationData.Int32_2);
	}

	public override void OnClick()
	{
		if (UsersData.UsersData_0.UserData_0.user_0.string_0.IsNullOrEmpty() || TutorialController.TutorialController_0.Boolean_0)
		{
			return;
		}
		string urlAtPosition = title.GetUrlAtPosition(UICamera.vector3_0);
		if (string.IsNullOrEmpty(urlAtPosition) || RebuyArticulWindow.Boolean_1)
		{
			return;
		}
		WindowController.WindowController_0.ForceHideAllWindow();
		if (Lobby.Lobby_0 != null)
		{
			Lobby.Lobby_0.Hide();
		}
		if (ShopWindow.ShopWindow_0 == null)
		{
			ShopWindowParams shopWindowParams = new ShopWindowParams();
			shopWindowParams.action_0 = delegate
			{
				Lobby.Lobby_0.Show();
			};
			shopWindowParams.openStyle_0 = ShopWindow.OpenStyle.ANIMATED;
			ShopWindow.Show(shopWindowParams);
		}
		Close();
	}
}
