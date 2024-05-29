using System.Collections.Generic;
using UnityEngine;

public class SettingsToggle : MonoBehaviour
{
	public UIToggle offToggle;

	public UIToggle onToggle;

	public EventDelegate onClick = new EventDelegate();

	private List<EventDelegate> list_0 = new List<EventDelegate>();

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	public bool Boolean_0
	{
		get
		{
			return bool_2;
		}
		set
		{
			bool_2 = value;
			UpdateView();
		}
	}

	private void Awake()
	{
		offToggle.list_0.Add(new EventDelegate(OnValueChangeOffToggle));
		onToggle.list_0.Add(new EventDelegate(OnValueChangeOnToggle));
		list_0.Add(onClick);
	}

	private void UpdateView()
	{
		offToggle.Boolean_0 = !bool_2;
		onToggle.Boolean_0 = bool_2;
	}

	private void OnValueChangeOffToggle()
	{
		if (!bool_0)
		{
			bool_0 = true;
			return;
		}
		Boolean_0 = false;
		EventDelegate.Execute(list_0);
	}

	private void OnValueChangeOnToggle()
	{
		if (!bool_1)
		{
			bool_1 = true;
			return;
		}
		Boolean_0 = true;
		EventDelegate.Execute(list_0);
	}
}
