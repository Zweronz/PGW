using UnityEngine;

public class UIGrid : UIWidgetContainer
{
	public enum Arrangement
	{
		Horizontal = 0,
		Vertical = 1
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

	public Arrangement arrangement_0;

	public Sorting sorting_0;

	public UIWidget.Pivot pivot_0;

	public int int_0;

	public float float_0 = 200f;

	public float float_1 = 200f;

	public bool bool_0;

	public bool bool_1 = true;

	public bool bool_2;

	public OnReposition onReposition_0;

	public BetterList<Transform>.CompareFunc compareFunc_0;

	[SerializeField]
	private bool bool_3;

	protected bool bool_4;

	protected UIPanel uipanel_0;

	protected bool bool_5;

	public bool Boolean_0
	{
		set
		{
			if (value)
			{
				bool_4 = true;
				base.enabled = true;
			}
		}
	}

	public BetterList<Transform> GetChildList()
	{
		Transform transform = base.transform;
		BetterList<Transform> betterList = new BetterList<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!bool_1 || ((bool)child && NGUITools.GetActive(child.gameObject)))
			{
				betterList.Add(child);
			}
		}
		if (sorting_0 != 0)
		{
			if (sorting_0 == Sorting.Alphabetic)
			{
				betterList.Sort(SortByName);
			}
			else if (sorting_0 == Sorting.Horizontal)
			{
				betterList.Sort(SortHorizontal);
			}
			else if (sorting_0 == Sorting.Vertical)
			{
				betterList.Sort(SortVertical);
			}
			else if (compareFunc_0 != null)
			{
				betterList.Sort(compareFunc_0);
			}
			else
			{
				Sort(betterList);
			}
		}
		return betterList;
	}

	public Transform GetChild(int int_1)
	{
		BetterList<Transform> childList = GetChildList();
		return (int_1 >= childList.size) ? null : childList[int_1];
	}

	public int GetIndex(Transform transform_0)
	{
		return GetChildList().IndexOf(transform_0);
	}

	public void AddChild(Transform transform_0)
	{
		AddChild(transform_0, true);
	}

	public void AddChild(Transform transform_0, bool bool_6)
	{
		if (transform_0 != null)
		{
			transform_0.parent = base.transform;
			ResetPosition(GetChildList());
		}
	}

	public bool RemoveChild(Transform transform_0)
	{
		BetterList<Transform> childList = GetChildList();
		if (childList.Remove(transform_0))
		{
			ResetPosition(childList);
			return true;
		}
		return false;
	}

	protected virtual void Init()
	{
		bool_5 = true;
		uipanel_0 = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	protected virtual void Start()
	{
		if (!bool_5)
		{
			Init();
		}
		bool flag = bool_0;
		bool_0 = false;
		Reposition();
		bool_0 = flag;
		base.enabled = false;
	}

	protected virtual void Update()
	{
		if (bool_4)
		{
			Reposition();
		}
		base.enabled = false;
	}

	public static int SortByName(Transform transform_0, Transform transform_1)
	{
		return string.Compare(transform_0.name, transform_1.name);
	}

	public static int SortHorizontal(Transform transform_0, Transform transform_1)
	{
		return transform_0.localPosition.x.CompareTo(transform_1.localPosition.x);
	}

	public static int SortVertical(Transform transform_0, Transform transform_1)
	{
		return transform_1.localPosition.y.CompareTo(transform_0.localPosition.y);
	}

	protected virtual void Sort(BetterList<Transform> betterList_0)
	{
	}

	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !bool_5 && NGUITools.GetActive(this))
		{
			bool_4 = true;
			return;
		}
		if (bool_3)
		{
			bool_3 = false;
			if (sorting_0 == Sorting.None)
			{
				sorting_0 = Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		if (!bool_5)
		{
			Init();
		}
		BetterList<Transform> childList = GetChildList();
		ResetPosition(childList);
		if (bool_2)
		{
			ConstrainWithinPanel();
		}
		if (onReposition_0 != null)
		{
			onReposition_0();
		}
	}

	public void ConstrainWithinPanel()
	{
		if (uipanel_0 != null)
		{
			uipanel_0.ConstrainTargetToBounds(base.transform, true);
		}
	}

	protected void ResetPosition(BetterList<Transform> betterList_0)
	{
		bool_4 = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int i = 0;
		for (int size = betterList_0.size; i < size; i++)
		{
			Transform transform2 = betterList_0[i];
			float z = transform2.localPosition.z;
			Vector3 vector = ((arrangement_0 != 0) ? new Vector3(float_0 * (float)num2, (0f - float_1) * (float)num, z) : new Vector3(float_0 * (float)num, (0f - float_1) * (float)num2, z));
			if (bool_0 && Application.isPlaying)
			{
				SpringPosition.Begin(transform2.gameObject, vector, 15f).updateScrollView = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= int_0 && int_0 > 0)
			{
				num = 0;
				num2++;
			}
		}
		if (pivot_0 == UIWidget.Pivot.TopLeft)
		{
			return;
		}
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(pivot_0);
		float num5;
		float num6;
		if (arrangement_0 == Arrangement.Horizontal)
		{
			num5 = Mathf.Lerp(0f, (float)num3 * float_0, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num4) * float_1, 0f, pivotOffset.y);
		}
		else
		{
			num5 = Mathf.Lerp(0f, (float)num4 * float_0, pivotOffset.x);
			num6 = Mathf.Lerp((float)(-num3) * float_1, 0f, pivotOffset.y);
		}
		for (int j = 0; j < transform.childCount; j++)
		{
			Transform child = transform.GetChild(j);
			SpringPosition component = child.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.target.x -= num5;
				component.target.y -= num6;
				continue;
			}
			Vector3 localPosition = child.localPosition;
			localPosition.x -= num5;
			localPosition.y -= num6;
			child.localPosition = localPosition;
		}
	}
}
