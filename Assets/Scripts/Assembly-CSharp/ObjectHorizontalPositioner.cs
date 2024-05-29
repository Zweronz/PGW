using System.Collections.Generic;
using UnityEngine;

public class ObjectHorizontalPositioner : MonoBehaviour
{
	public GameObject[] objects;

	public int[] distance;

	public int indent;

	public bool setWidth;

	public bool leftPos;

	public bool rightPos;

	public int endActionsOnUpdate = -1;

	private UIWidget uiwidget_0;

	private List<KeyValuePair<UIWidget, int>> list_0 = new List<KeyValuePair<UIWidget, int>>();

	private int int_0;

	private void Start()
	{
		if (objects.Length != distance.Length)
		{
			base.enabled = false;
			return;
		}
		uiwidget_0 = base.gameObject.GetComponent<UIWidget>();
		if (uiwidget_0 == null)
		{
			uiwidget_0 = base.gameObject.GetComponent<UISprite>();
		}
		if (uiwidget_0 == null)
		{
			base.enabled = false;
			return;
		}
		for (int i = 0; i < objects.Length; i++)
		{
			GameObject gameObject = objects[i];
			if (!(gameObject == null))
			{
				UIWidget component = gameObject.GetComponent<UIWidget>();
				if (component == null)
				{
					component = gameObject.GetComponent<UISprite>();
				}
				if (component == null)
				{
					component = gameObject.GetComponent<UILabel>();
				}
				if (!(component == null))
				{
					list_0.Add(new KeyValuePair<UIWidget, int>(component, distance[i]));
				}
			}
		}
		if (list_0.Count != 0)
		{
		}
	}

	private void Update()
	{
		int num = indent;
		foreach (KeyValuePair<UIWidget, int> item in list_0)
		{
			num += Mathf.CeilToInt((float)item.Key.Int32_0 * item.Key.transform.localScale.x) + item.Value;
		}
		float num2 = 0f;
		if (uiwidget_0.Pivot_1 != UIWidget.Pivot.BottomLeft && uiwidget_0.Pivot_1 != UIWidget.Pivot.Left && uiwidget_0.Pivot_1 != 0)
		{
			if (uiwidget_0.Pivot_1 != UIWidget.Pivot.Top && uiwidget_0.Pivot_1 != UIWidget.Pivot.Center && uiwidget_0.Pivot_1 != UIWidget.Pivot.Bottom)
			{
				if (uiwidget_0.Pivot_1 == UIWidget.Pivot.Right || uiwidget_0.Pivot_1 == UIWidget.Pivot.BottomRight || uiwidget_0.Pivot_1 == UIWidget.Pivot.TopRight)
				{
					num2 = -uiwidget_0.Int32_0;
				}
			}
			else
			{
				num2 = (float)(-uiwidget_0.Int32_0) / 2f;
			}
		}
		else
		{
			num2 = 0f;
		}
		if (!leftPos)
		{
			num2 = ((!rightPos) ? (num2 + (float)(uiwidget_0.Int32_0 - num) / 2f) : (num2 + (float)uiwidget_0.Int32_0 - (float)num));
		}
		num2 += (float)indent;
		foreach (KeyValuePair<UIWidget, int> item2 in list_0)
		{
			float num3 = 0f;
			if (item2.Key.Pivot_1 != UIWidget.Pivot.BottomLeft && item2.Key.Pivot_1 != UIWidget.Pivot.Left && item2.Key.Pivot_1 != 0)
			{
				if (item2.Key.Pivot_1 == UIWidget.Pivot.Top || item2.Key.Pivot_1 == UIWidget.Pivot.Center || item2.Key.Pivot_1 == UIWidget.Pivot.Bottom)
				{
					num3 = num2 + (float)item2.Key.Int32_0 * item2.Key.transform.localScale.x / 2f;
				}
			}
			else
			{
				num3 = num2;
			}
			if (item2.Key.Pivot_1 == UIWidget.Pivot.Right || item2.Key.Pivot_1 == UIWidget.Pivot.BottomRight || item2.Key.Pivot_1 == UIWidget.Pivot.TopRight)
			{
				num3 = num2 + (float)item2.Key.Int32_0 * item2.Key.transform.localScale.x;
			}
			if (item2.Key.transform.localPosition.x != num3)
			{
				item2.Key.transform.localPosition = new Vector3(num3, item2.Key.transform.localPosition.y, item2.Key.transform.localPosition.z);
			}
			num2 += (float)item2.Key.Int32_0 * item2.Key.transform.localScale.x + (float)item2.Value;
		}
		if (setWidth)
		{
			uiwidget_0.Int32_0 = num;
		}
		if (endActionsOnUpdate >= 0)
		{
			if (int_0 == endActionsOnUpdate)
			{
				base.enabled = false;
			}
			int_0++;
		}
	}
}
