using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Rilisoft;
using UnityEngine;

public class FeedbackMenuController : MonoBehaviour
{
	public enum State
	{
		FAQ = 0,
		TermsFuse = 1
	}

	private State state_0;

	public UIButton faqButton;

	public UIButton termsFuseButton;

	public UILabel textLabel;

	public UIButton sendFeedbackButton;

	public UIButton backButton;

	public GameObject SettingPanel;

	public GameObject textFAQScroll;

	public GameObject textTermsOfUse;

	[CompilerGenerated]
	private static bool bool_0;

	[CompilerGenerated]
	private static Func<UIButton, bool> func_0;

	public static bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public State State_0
	{
		get
		{
			return state_0;
		}
		set
		{
			if (faqButton != null)
			{
				faqButton.Boolean_0 = value != State.FAQ;
				Transform transform = faqButton.gameObject.transform.Find("SpriteLabel");
				if (transform != null)
				{
					transform.gameObject.SetActive(value != State.FAQ);
				}
				Transform transform2 = faqButton.gameObject.transform.Find("ChekmarkLabel");
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(value == State.FAQ);
				}
				textFAQScroll.SetActive(value == State.FAQ);
			}
			if (termsFuseButton != null)
			{
				termsFuseButton.Boolean_0 = value != State.TermsFuse;
				Transform transform3 = termsFuseButton.gameObject.transform.Find("SpriteLabel");
				if (transform3 != null)
				{
					transform3.gameObject.SetActive(value != State.TermsFuse);
				}
				else
				{
					Debug.Log("_spriteLabel=null");
				}
				Transform transform4 = termsFuseButton.gameObject.transform.Find("ChekmarkLabel");
				if (transform4 != null)
				{
					transform4.gameObject.SetActive(value == State.TermsFuse);
				}
				else
				{
					Debug.Log("_spriteLabel=null");
				}
				textTermsOfUse.SetActive(value == State.TermsFuse);
			}
			state_0 = value;
		}
	}

	private void Start()
	{
		State_0 = State.FAQ;
		IEnumerable<UIButton> enumerable = new UIButton[2] { faqButton, termsFuseButton }.Where((UIButton uibutton_0) => uibutton_0 != null);
		foreach (UIButton item in enumerable)
		{
			ButtonHandler component = item.GetComponent<ButtonHandler>();
			if (component != null)
			{
				component.Clicked += HandleTabPressed;
			}
		}
		if (sendFeedbackButton != null)
		{
			ButtonHandler component2 = sendFeedbackButton.GetComponent<ButtonHandler>();
			if (component2 != null)
			{
				component2.Clicked += HandleSendFeedback;
			}
		}
		if (backButton != null)
		{
			ButtonHandler component3 = backButton.GetComponent<ButtonHandler>();
			if (component3 != null)
			{
				component3.Clicked += HandleBackButton;
			}
		}
	}

	private void OnEnable()
	{
		Boolean_0 = true;
	}

	private void OnDisable()
	{
		Boolean_0 = false;
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Input.ResetInputAxes();
			BackButton();
		}
	}

	private void BackButton()
	{
		base.gameObject.SetActive(false);
		if (SettingPanel != null)
		{
			SettingPanel.SetActive(true);
		}
	}

	private void HandleBackButton(object sender, EventArgs e)
	{
		BackButton();
	}

	public static void ShowDialogWithCompletion(Action action_0)
	{
		if (action_0 != null)
		{
			action_0();
		}
	}

	private void HandleSendFeedback(object sender, EventArgs e)
	{
	}

	private void HandleTabPressed(object sender, EventArgs e)
	{
		GameObject gameObject = ((ButtonHandler)sender).gameObject;
		if (faqButton != null && gameObject == faqButton.gameObject)
		{
			State_0 = State.FAQ;
		}
		else if (termsFuseButton != null && gameObject == termsFuseButton.gameObject)
		{
			State_0 = State.TermsFuse;
		}
	}
}
