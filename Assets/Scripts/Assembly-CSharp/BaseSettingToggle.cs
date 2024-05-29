using UnityEngine;

public abstract class BaseSettingToggle : MonoBehaviour
{
	public UIToggle offToggle;

	public UIToggle onToggle;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	protected bool Boolean_0
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

	private void Start()
	{
		offToggle.list_0.Add(new EventDelegate(OnValueChangeOffToggle));
		onToggle.list_0.Add(new EventDelegate(OnValueChangeOnToggle));
		Boolean_0 = isOn();
	}

	private void UpdateView()
	{
		offToggle.Boolean_0 = !Boolean_0;
		onToggle.Boolean_0 = Boolean_0;
	}

	private void OnValueChangeOffToggle()
	{
		if (!bool_0)
		{
			bool_0 = true;
			return;
		}
		Boolean_0 = false;
		onClick();
	}

	private void OnValueChangeOnToggle()
	{
		if (!bool_1)
		{
			bool_1 = true;
			return;
		}
		Boolean_0 = true;
		onClick();
	}

	protected abstract bool isOn();

	protected abstract void onClick();
}
