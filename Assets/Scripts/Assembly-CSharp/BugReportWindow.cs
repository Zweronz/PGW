using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.BugReport)]
public sealed class BugReportWindow : BaseGameWindow
{
	private static BugReportWindow bugReportWindow_0 = null;

	public UILabel uilabel_0;

	public UIPopupList uipopupList_0;

	public UILabel uilabel_1;

	public UIButton uibutton_0;

	private static List<string> list_0 = new List<string>();

	private bool bool_1;

	public static BugReportWindow BugReportWindow_0
	{
		get
		{
			return bugReportWindow_0;
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return BugReportWindow_0 != null && BugReportWindow_0.Boolean_0;
		}
	}

	public static void Show()
	{
		if (!(bugReportWindow_0 != null))
		{
			bugReportWindow_0 = BaseWindow.Load("BugReportWindow") as BugReportWindow;
			bugReportWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			bugReportWindow_0.Parameters_0.bool_5 = true;
			bugReportWindow_0.Parameters_0.bool_0 = false;
			bugReportWindow_0.Parameters_0.bool_6 = false;
			bugReportWindow_0.InternalShow();
		}
	}

	public override void OnShow()
	{
		if (uilabel_0 != null)
		{
			uilabel_0.Pivot_1 = UIWidget.Pivot.TopLeft;
			uilabel_0.SetCurrentSelection();
			uilabel_0.GetComponent<UIInput>().Boolean_2 = true;
		}
		AddInputKey(KeyCode.Escape, OnExitButton);
		FillIssueTypes();
		for (int i = 0; i < list_0.Count; i++)
		{
			uipopupList_0.items.Add(list_0[i]);
		}
		uipopupList_0.String_0 = string.Empty;
		bool_1 = true;
		uipopupList_0.gameObject.SendMessage("OnClick");
		uibutton_0.Boolean_0 = false;
		base.OnShow();
	}

	public void OnBtnListClick()
	{
		uibutton_0.Boolean_0 = true;
		uipopupList_0.gameObject.SendMessage("OnClick");
	}

	public void OnPopupListClick()
	{
		uibutton_0.Boolean_0 = true;
	}

	public override void OnHide()
	{
		base.OnHide();
		bugReportWindow_0 = null;
	}

	private void FillIssueTypes()
	{
		list_0.Clear();
		list_0.Add(Localizer.Get("ui.bug_report.issue_1"));
		list_0.Add(Localizer.Get("ui.bug_report.issue_2"));
		list_0.Add(Localizer.Get("ui.bug_report.issue_3"));
		list_0.Add(Localizer.Get("ui.bug_report.issue_4"));
		list_0.Add(Localizer.Get("ui.bug_report.issue_5"));
		list_0.Add(Localizer.Get("ui.bug_report.issue_6"));
	}

	public void OnModePopupValueChange()
	{
		string string_ = uipopupList_0.String_0;
		uilabel_1.String_0 = string_;
		if (!bool_1)
		{
			uibutton_0.Boolean_0 = true;
		}
		if (bool_1)
		{
			bool_1 = !bool_1;
		}
	}

	public void OnExitButton()
	{
		BugReportController.BugReportController_0.SwitchWindow();
	}

	public void OnSendButton()
	{
		if (!(uilabel_0 == null) && !string.IsNullOrEmpty(uilabel_0.String_0) && !(uilabel_1 == null) && !string.IsNullOrEmpty(uilabel_1.String_0))
		{
			string string_ = uipopupList_0.String_0;
			int num = 0;
			for (int i = 0; i < list_0.Count; i++)
			{
				if (string.Equals(string_, list_0[i]))
				{
					num = i;
					break;
				}
			}
			BugReportController.BugReportController_0.OnSendReport(num + 1, uilabel_0.String_0);
		}
		else
		{
			BugReportController.BugReportController_0.SwitchWindow();
		}
	}

	protected override void Update()
	{
		base.Update();
		if (bugReportWindow_0 != null && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return))
		{
			OnSendButton();
		}
		if (bugReportWindow_0 != null && Input.GetKey(KeyCode.Escape))
		{
			Hide();
		}
	}
}
