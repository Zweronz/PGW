using UnityEngine;

internal sealed class CoinsUpdater : MonoBehaviour
{
	private UILabel uilabel_0;

	private void Awake()
	{
		uilabel_0 = GetComponent<UILabel>();
	}

	private void OnEnable()
	{
		UpdateMoneyCount();
		UsersData.Subscribe(UsersData.EventType.USER_CHANGED, UpdateMoneyCount);
	}

	private void OnDisable()
	{
		UsersData.Unsubscribe(UsersData.EventType.USER_CHANGED, UpdateMoneyCount);
	}

	private void UpdateMoneyCount(UsersData.EventData eventData_0 = null)
	{
		if (!(uilabel_0 == null))
		{
			uilabel_0.String_0 = UserController.UserController_0.GetMoneyByType(MoneyType.MONEY_TYPE_COINS).ToString();
		}
	}
}
