using UnityEngine;

public class MailValidityButton : MonoBehaviour
{
	private string String_0
	{
		get
		{
			string string_ = AppController.AppController_0.ProcessArguments_0.String_1;
			return string.Format("{0}verifyemail?email={1}", AppController.AppController_0.String_2, string_);
		}
	}

	private void OnEnable()
	{
		UsersData.Subscribe(UsersData.EventType.USER_CHANGED, UpdateState);
		UpdateState();
	}

	private void OnDisable()
	{
		UsersData.Unsubscribe(UsersData.EventType.USER_CHANGED, UpdateState);
	}

	public void OpenSiteUrl()
	{
		Application.OpenURL(String_0);
	}

	private void UpdateState(UsersData.EventData eventData_0 = null)
	{
		base.gameObject.SetActive(!UserController.UserController_0.UserData_0.user_0.bool_5);
	}
}
