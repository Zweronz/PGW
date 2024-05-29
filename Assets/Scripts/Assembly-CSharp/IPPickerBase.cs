using System;
using UnityEngine;

public abstract class IPPickerBase : MonoBehaviour
{
	public delegate void PickerValueUpdatedHandler();

	public string pickerName;

	public bool initInAwake = true;

	public int widgetsDepth = 3;

	public Vector3 widgetOffsetInPicker;

	public UIWidget.Pivot widgetsPivot = UIWidget.Pivot.Center;

	public Color widgetsColor = Color.white;

	public PickerValueUpdatedHandler onPickerValueUpdated;

	public IPCycler cycler;

	[SerializeField]
	protected int int_0;

	protected int int_1;

	protected int int_2;

	public int Int32_0
	{
		get
		{
			return int_2;
		}
	}

	public int Int32_1
	{
		get
		{
			return int_1;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return cycler.Boolean_0;
		}
	}

	protected virtual void Awake()
	{
		if (Application.isPlaying)
		{
			if (initInAwake)
			{
				Setup();
			}
		}
		else if (initInAwake)
		{
			Init();
		}
	}

	private void OnDestroy()
	{
		if (Application.isPlaying)
		{
			IPCycler iPCycler = cycler;
			iPCycler.onCyclerIndexChange = (IPCycler.CyclerIndexChangeHandler)Delegate.Remove(iPCycler.onCyclerIndexChange, new IPCycler.CyclerIndexChangeHandler(CyclerIndexChange));
		}
	}

	public void Setup()
	{
		cycler.Init();
		IPCycler iPCycler = cycler;
		iPCycler.onCyclerIndexChange = (IPCycler.CyclerIndexChangeHandler)Delegate.Combine(iPCycler.onCyclerIndexChange, new IPCycler.CyclerIndexChangeHandler(CyclerIndexChange));
		Init();
	}

	protected virtual void Init()
	{
		if (cycler == null)
		{
			cycler = GetComponentInChildren(typeof(IPCycler)) as IPCycler;
		}
		if (WidgetsNeedRebuild())
		{
			MakeWidgetComponents();
		}
		UpdateVirtualElementsCount();
		if (int_1 == 0)
		{
			EnableWidgets(false);
			return;
		}
		int_2 = GetInitIndex();
		UpdateCurrentValue();
		ResetWidgetsContent();
	}

	protected void ResetPickerAtIndex(int int_3)
	{
		int_2 = int_3;
		int num = int_1;
		UpdateVirtualElementsCount();
		if (num == 0)
		{
			if (int_1 > 0)
			{
				EnableWidgets(true);
			}
		}
		else if (int_1 == 0)
		{
			EnableWidgets(false);
			return;
		}
		UpdateCurrentValue();
		cycler.ResetCycler();
		ResetWidgetsContent();
	}

	public void ResetPicker()
	{
		ResetPickerAtIndex(int_2);
	}

	private void CyclerIndexChange(bool bool_0, int int_3)
	{
		if (int_1 == 0)
		{
			return;
		}
		if (bool_0)
		{
			int_2 = (int_2 + 1) % int_1;
		}
		else
		{
			int_2--;
			if (int_2 < 0)
			{
				int_2 += int_1;
			}
		}
		CycleWidgets(bool_0, int_3);
		UpdateCurrentValue();
		if (onPickerValueUpdated != null)
		{
			onPickerValueUpdated();
		}
	}

	private void CycleWidgets(bool bool_0, int int_3)
	{
		int num;
		if (bool_0)
		{
			num = (int_2 + int_0 / 2) % int_1;
		}
		else
		{
			num = (int_2 - int_0 / 2) % int_1;
			if (num < 0)
			{
				num += int_1;
			}
		}
		UpdateWidget(int_3, num);
	}

	public abstract UIWidget GetCenterWidget();

	public abstract UIWidget GetWidgetAtScreenPos(Vector2 vector2_0);

	public abstract void EnableWidgets(bool bool_0);

	public abstract void UpdateVirtualElementsCount();

	public abstract void UpdateWidget(int int_3, int int_4);

	protected abstract void MakeWidgetComponents();

	protected abstract void InitWidgets();

	protected abstract int GetInitIndex();

	protected abstract void UpdateCurrentValue();

	protected abstract bool WidgetsNeedRebuild();

	protected void RebuildWidgets()
	{
		if (cycler == null)
		{
			cycler = GetComponentInChildren(typeof(IPCycler)) as IPCycler;
		}
		MakeWidgetComponents();
		ResetWidgets();
	}

	public void ResetWidgets()
	{
		UpdateVirtualElementsCount();
		if (int_1 != 0)
		{
			int_2 = GetInitIndex();
			InitWidgets();
			ResetWidgetsContent();
		}
	}

	public void ResetWidgetsContent()
	{
		int num = int_2 - int_0 / 2;
		if (num < 0)
		{
			num += int_1;
		}
		if (num < 0)
		{
			num = 0;
		}
		for (int i = 0; i < int_0; i++)
		{
			UpdateWidget(i, num);
			num = (num + 1) % int_1;
		}
	}
}
