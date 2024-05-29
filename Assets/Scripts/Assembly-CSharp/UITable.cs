using System.Collections.Generic;
using UnityEngine;

public class UITable : UIWidgetContainer
{
	public enum Direction
	{
		Down = 0,
		Up = 1
	}

	public enum Sorting
	{
		None = 0,
		Alphabetic = 1,
		Horizontal = 2,
		Vertical = 3,
		Custom = 4
	}

	public delegate void OnReposition();

	public int int_0;

	public Direction direction_0;

	public Sorting sorting_0;

	public bool bool_0 = true;

	public bool bool_1;

	public Vector2 vector2_0 = Vector2.zero;

	public OnReposition onReposition_0;

	protected UIPanel uipanel_0;

	protected bool bool_2;

	protected bool bool_3;

	protected List<Transform> list_0 = new List<Transform>();

	[SerializeField]
	private bool bool_4;

	public bool Boolean_0
	{
		set
		{
			if (value)
			{
				bool_3 = true;
				base.enabled = true;
			}
		}
	}

	public List<Transform> List_0
	{
		get
		{
			if (list_0.Count == 0)
			{
				Transform transform = base.transform;
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					if ((bool)child && (bool)child.gameObject && (!bool_0 || NGUITools.GetActive(child.gameObject)))
					{
						list_0.Add(child);
					}
				}
				if (sorting_0 != 0 || bool_4)
				{
					if (sorting_0 == Sorting.Alphabetic)
					{
						list_0.Sort(UIGrid.SortByName);
					}
					else if (sorting_0 == Sorting.Horizontal)
					{
						list_0.Sort(UIGrid.SortHorizontal);
					}
					else if (sorting_0 == Sorting.Vertical)
					{
						list_0.Sort(UIGrid.SortVertical);
					}
					else
					{
						Sort(list_0);
					}
				}
			}
			return list_0;
		}
	}

	protected virtual void Sort(List<Transform> list_1)
	{
		list_1.Sort(UIGrid.SortByName);
	}

	protected void RepositionVariableSize(List<Transform> list_1)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = ((int_0 <= 0) ? 1 : (list_1.Count / int_0 + 1));
		int num4 = ((int_0 <= 0) ? list_1.Count : int_0);
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		for (int count = list_1.Count; i < count; i++)
		{
			Transform transform = list_1[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !bool_0);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= int_0 && int_0 > 0)
			{
				num5 = 0;
				num6++;
			}
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		for (int count2 = list_1.Count; j < count2; j++)
		{
			Transform transform2 = list_1[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x += bounds2.min.x - bounds3.min.x + vector2_0.x;
			if (direction_0 == Direction.Down)
			{
				localPosition.y = 0f - num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - vector2_0.y;
			}
			else
			{
				localPosition.y = num2 + (bounds2.extents.y - bounds2.center.y);
				localPosition.y -= (bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y) * 0.5f - vector2_0.y;
			}
			num += bounds3.max.x - bounds3.min.x + vector2_0.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= int_0 && int_0 > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + vector2_0.y * 2f;
			}
		}
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !bool_2 && NGUITools.GetActive(this))
		{
			bool_3 = true;
			return;
		}
		if (!bool_2)
		{
			Init();
		}
		bool_3 = false;
		Transform transform_ = base.transform;
		list_0.Clear();
		List<Transform> list = List_0;
		if (list.Count > 0)
		{
			RepositionVariableSize(list);
		}
		if (bool_1 && uipanel_0 != null)
		{
			uipanel_0.ConstrainTargetToBounds(transform_, true);
			UIScrollView component = uipanel_0.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (onReposition_0 != null)
		{
			onReposition_0();
		}
	}

	protected virtual void Start()
	{
		Init();
		Reposition();
		base.enabled = false;
	}

	protected virtual void Init()
	{
		bool_2 = true;
		uipanel_0 = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	protected virtual void LateUpdate()
	{
		if (bool_3)
		{
			Reposition();
		}
		base.enabled = false;
	}
}
