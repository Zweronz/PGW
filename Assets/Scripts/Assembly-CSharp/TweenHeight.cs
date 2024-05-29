using System;
using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class TweenHeight : UITweener
{
	public int int_1 = 100;

	public int int_2 = 100;

	public bool bool_3;

	private UIWidget uiwidget_0;

	private UITable uitable_0;

	public UIWidget UIWidget_0
	{
		get
		{
			if (uiwidget_0 == null)
			{
				uiwidget_0 = GetComponent<UIWidget>();
			}
			return uiwidget_0;
		}
	}

	[Obsolete("Use 'value' instead")]
	public int Int32_0
	{
		get
		{
			return Int32_1;
		}
		set
		{
			Int32_1 = value;
		}
	}

	public int Int32_1
	{
		get
		{
			return UIWidget_0.Int32_1;
		}
		set
		{
			UIWidget_0.Int32_1 = value;
		}
	}

	protected override void OnUpdate(float float_6, bool bool_4)
	{
		Int32_1 = Mathf.RoundToInt((float)int_1 * (1f - float_6) + (float)int_2 * float_6);
		if (!bool_3)
		{
			return;
		}
		if (uitable_0 == null)
		{
			uitable_0 = NGUITools.FindInParents<UITable>(base.gameObject);
			if (uitable_0 == null)
			{
				bool_3 = false;
				return;
			}
		}
		uitable_0.Boolean_0 = true;
	}

	public static TweenHeight Begin(UIWidget uiwidget_1, float float_6, int int_3)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(uiwidget_1.gameObject, float_6);
		tweenHeight.int_1 = uiwidget_1.Int32_1;
		tweenHeight.int_2 = int_3;
		if (float_6 <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		int_1 = Int32_1;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		int_2 = Int32_1;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Int32_1 = int_1;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Int32_1 = int_2;
	}
}
