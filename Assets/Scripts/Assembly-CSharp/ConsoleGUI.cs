using System.Collections.Generic;
using UnityEngine;

public class ConsoleGUI : MonoBehaviour
{
	private const int int_0 = 50;

	public const int int_1 = 100000;

	public ConsoleAction escapeAction;

	public ConsoleAction submitAction;

	public string string_0 = string.Empty;

	private ConsoleLog consoleLog_0;

	private Rect rect_0;

	private bool bool_0;

	private int int_2;

	private ConsoleCommandsRepository consoleCommandsRepository_0;

	public int maxConsoleHistorySize = 100;

	private int int_3;

	private List<string> list_0 = new List<string>();

	private bool bool_1;

	private void Awake()
	{
		rect_0 = new Rect(0f, 0f, Screen.width, Mathf.Min(300, Screen.height));
		consoleLog_0 = ConsoleLog.ConsoleLog_0;
		consoleCommandsRepository_0 = ConsoleCommandsRepository.ConsoleCommandsRepository_0;
	}

	private void OnEnable()
	{
		bool_0 = true;
	}

	private void OnDisable()
	{
		bool_0 = true;
	}

	public void OnGUI()
	{
		GUILayout.Window(50, rect_0, RenderWindow, "Console");
	}

	private void RenderWindow(int int_4)
	{
		if (bool_1)
		{
			MoveCursorToPos(string_0.Length);
			bool_1 = false;
		}
		if (consoleLog_0 != null)
		{
			HandleSubmit();
			HandleEscape();
			HandleTab();
			HandleUp();
			HandleDown();
			int_2 = (int)GUILayout.BeginScrollView(new Vector2(0f, int_2), false, true).y;
			GUILayout.Label(consoleLog_0.string_0);
			GUILayout.EndScrollView();
			GUI.SetNextControlName("input");
			string_0 = GUILayout.TextField(string_0);
			if (bool_0)
			{
				GUI.FocusControl("input");
				bool_0 = false;
			}
		}
	}

	public void Update()
	{
		if (string_0 == "`")
		{
			string_0 = string.Empty;
		}
		if (consoleLog_0.Boolean_0)
		{
			int_2 = 100000;
		}
	}

	private void HandleSubmit()
	{
		if (!KeyDown("[enter]") && !KeyDown("return"))
		{
			return;
		}
		int_3 = -1;
		if (submitAction != null)
		{
			submitAction.Activate();
			list_0.Insert(0, string_0);
			if (list_0.Count > maxConsoleHistorySize)
			{
				list_0.RemoveAt(list_0.Count - 1);
			}
		}
		string_0 = string.Empty;
	}

	private void HandleEscape()
	{
		if (KeyDown("escape") || KeyDown("`"))
		{
			escapeAction.Activate();
			string_0 = string.Empty;
		}
	}

	private bool KeyDown(string string_1)
	{
		return Event.current.Equals(Event.KeyboardEvent(string_1));
	}

	private string LargestSubString(string string_1, string string_2)
	{
		string text = string.Empty;
		int num = Mathf.Min(string_1.Length, string_2.Length);
		int num2 = 0;
		while (true)
		{
			if (num2 < num)
			{
				if (string_1[num2] != string_2[num2])
				{
					break;
				}
				text += string_1[num2];
				num2++;
				continue;
			}
			return text;
		}
		return text;
	}

	private void MoveCursorToPos(int int_4)
	{
		TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
		//textEditor.selectPos = int_4;
		//textEditor.pos = int_4;
	}

	private void HandleTab()
	{
		if (!KeyDown("tab"))
		{
			return;
		}
		if (string_0 == string.Empty)
		{
			consoleCommandsRepository_0.ListCommands();
			return;
		}
		List<string> list = consoleCommandsRepository_0.SearchCommands(string_0);
		if (list.Count == 0)
		{
			consoleLog_0.Log(string.Format("No command start with \"{0}\".", string_0));
			string_0 = string.Empty;
			return;
		}
		if (list.Count == 1)
		{
			string_0 = list[0] + " ";
			MoveCursorToPos(string_0.Length);
			return;
		}
		consoleLog_0.Log("Commands starting with \"" + string_0 + "\":");
		string string_ = list[0];
		foreach (string item in list)
		{
			consoleLog_0.Log(item);
			string_ = LargestSubString(string_, item);
		}
		string_0 = string_;
		MoveCursorToPos(string_0.Length);
	}

	private void HandleUp()
	{
		if (KeyDown("up"))
		{
			int_3++;
			if (int_3 > list_0.Count - 1)
			{
				int_3 = list_0.Count - 1;
			}
			if (list_0.Count != 0)
			{
				string_0 = list_0[int_3];
				bool_1 = true;
			}
		}
	}

	private void HandleDown()
	{
		if (KeyDown("down"))
		{
			int_3--;
			if (int_3 < 0)
			{
				int_3 = -1;
				string_0 = string.Empty;
			}
			else
			{
				string_0 = list_0[int_3];
			}
			MoveCursorToPos(string_0.Length);
		}
	}
}
