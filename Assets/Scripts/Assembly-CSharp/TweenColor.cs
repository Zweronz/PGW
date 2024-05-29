using System;
using UnityEngine;

public class TweenColor : UITweener
{
	public Color color_0 = Color.white;

	public Color color_1 = Color.white;

	private bool bool_3;

	private UIWidget uiwidget_0;

	private Material material_0;

	private Light light_0;

	[Obsolete("Use 'value' instead")]
	public Color Color_0
	{
		get
		{
			return Color_1;
		}
		set
		{
			Color_1 = value;
		}
	}

	public Color Color_1
	{
		get
		{
			if (!bool_3)
			{
				Cache();
			}
			if (uiwidget_0 != null)
			{
				return uiwidget_0.Color_0;
			}
			if (light_0 != null)
			{
				return light_0.color;
			}
			if (material_0 != null)
			{
				return material_0.color;
			}
			return Color.black;
		}
		set
		{
			if (!bool_3)
			{
				Cache();
			}
			if (uiwidget_0 != null)
			{
				uiwidget_0.Color_0 = value;
			}
			if (material_0 != null)
			{
				material_0.color = value;
			}
			if (light_0 != null)
			{
				light_0.color = value;
				light_0.enabled = value.r + value.g + value.b > 0.01f;
			}
		}
	}

	private void Cache()
	{
		bool_3 = true;
		uiwidget_0 = GetComponent<UIWidget>();
		Renderer renderer = base.GetComponent<Renderer>();
		if (renderer != null)
		{
			material_0 = renderer.material;
		}
		light_0 = base.GetComponent<Light>();
		if (uiwidget_0 == null && material_0 == null && light_0 == null)
		{
			uiwidget_0 = GetComponentInChildren<UIWidget>();
		}
	}

	protected override void OnUpdate(float float_6, bool bool_4)
	{
		Color_1 = Color.Lerp(color_0, color_1, float_6);
	}

	public static TweenColor Begin(GameObject gameObject_1, float float_6, Color color_2)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(gameObject_1, float_6);
		tweenColor.color_0 = tweenColor.Color_1;
		tweenColor.color_1 = color_2;
		if (float_6 <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		color_0 = Color_1;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		color_1 = Color_1;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		Color_1 = color_0;
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		Color_1 = color_1;
	}
}
