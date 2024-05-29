using System;
using System.Collections.Generic;
using engine.events;
using engine.helpers;
using engine.network;
using engine.operations;

public class UserNickController : BaseEvent<UserNickEventParams>
{
	public enum Event
	{
		USER_SET_NICK_SUCCESS = 0,
		USER_SET_NICK_FAILED = 1
	}

	private static UserNickController userNickController_0;

	private Action<int> action_0;

	public static readonly Dictionary<char, char> dictionary_1 = new Dictionary<char, char>
	{
		{ 'A', 'Z' },
		{ 'a', 'z' },
		{ '0', '9' }
	};

	public static readonly HashSet<char> hashSet_0 = new HashSet<char>
	{
		'!', '@', '#', '$', '%', '^', '&', '*', '(', ')',
		'_', '+', '-', '=', '<', '>', '.', ',', '?', '\'',
		';', '\\', '|', '/'
	};

	public static UserNickController UserNickController_0
	{
		get
		{
			if (userNickController_0 == null)
			{
				userNickController_0 = new UserNickController();
			}
			return userNickController_0;
		}
	}

	private UserNickController()
	{
	}

	public bool CheckUserNick()
	{
		UserData userData_ = UsersData.UsersData_0.UserData_0;
		if (userData_ == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(userData_.user_0.string_0))
		{
			ShowRenameWindow(false);
			return false;
		}
		return true;
	}

	public void ShowRenameWindow(bool bool_0)
	{
		MessageWindowRename.Show(new MessageRenameWindowParams(LocalizationStorage.Get.Term((!bool_0) ? "window.msg.rename.text" : "window.msg.rename.text_price"), bool_0));
	}

	public void ProcessUserSetNick(string string_0)
	{
		MessageWindowRename.MessageWindowRename_0.Hide();
		//if (CheckUserNickAfterRename(string_0))
		//{
		//	UsersData.Subscribe(UsersData.EventType.USER_CHANGED, OnUserChanged);
		//	OperationsManager.OperationsManager_0.Add(delegate
		//	{
		//		AbstractNetworkCommand.Send(new UserSetNickNetworkCommand
		//		{
		//			string_0 = string_0
		//		});
		//	});
		//}
	}

	private bool CheckUserNickAfterRename(string string_0)
	{
		int num = string_0.IndexOf("admin", StringComparison.OrdinalIgnoreCase);
		if (num > -1)
		{
			MessageWindow.Show(new MessageWindowParams(string.Format(Localizer.Get("window.msg.wrong_name"), string_0.Substring(num, 5))));
			return false;
		}
		num = string_0.IndexOf("rilisoft", StringComparison.OrdinalIgnoreCase);
		if (num > -1)
		{
			MessageWindow.Show(new MessageWindowParams(string.Format(Localizer.Get("window.msg.wrong_name"), string_0.Substring(num, 8))));
			return false;
		}
		return true;
	}

	private void OnUserChanged(UsersData.EventData eventData_0)
	{
		UsersData.Unsubscribe(UsersData.EventType.USER_CHANGED, OnUserChanged);
		UserData userData_ = UsersData.UsersData_0.UserData_0;
		if (userData_ != null && MessageWindowRename.MessageWindowRename_0 != null)
		{
			MessageWindowRename.MessageWindowRename_0.Hide();
		}
	}

	public string GetErrorText(int int_0)
	{
		string result = "error";
		switch (int_0)
		{
		case 25:
			result = LocalizationStore.Get("window.msg.rename.error.3");
			Log.AddLine("UserNickController::GetErrorText > not enought money");
			break;
		case 3:
			result = LocalizationStore.Get("window.msg.rename.error.2");
			Log.AddLine("UserNickController::GetErrorText > nick is empty");
			break;
		case 2:
			result = LocalizationStore.Get("window.msg.rename.error.1");
			Log.AddLine("UserNickController::GetErrorText > nick exists");
			break;
		}
		return result;
	}

	public char OnInputValidate(string string_0, int int_0, char char_0)
	{
		foreach (KeyValuePair<char, char> item in dictionary_1)
		{
			if (char_0 >= item.Key && char_0 <= item.Value)
			{
				return char_0;
			}
		}
		if (hashSet_0.Contains(char_0))
		{
			return char_0;
		}
		return '\0';
	}

	public bool HasInvalidSymbol(string string_0)
	{
		char[] array = string_0.ToCharArray();
		char[] array2 = array;
		int num = 0;
		while (true)
		{
			if (num < array2.Length)
			{
				char char_ = array2[num];
				if (IsInvalidSymbol(char_))
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		return true;
	}

	private bool IsInvalidSymbol(char char_0)
	{
		bool flag = true;
		foreach (KeyValuePair<char, char> item in dictionary_1)
		{
			if (char_0 >= item.Key && char_0 <= item.Value)
			{
				flag = false;
				break;
			}
		}
		if (flag && hashSet_0.Contains(char_0))
		{
			flag = false;
		}
		return flag;
	}
}
