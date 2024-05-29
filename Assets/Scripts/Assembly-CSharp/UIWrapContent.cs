using UnityEngine;

public class UIWrapContent : MonoBehaviour
{
	public delegate void OnInitializeItem(GameObject gameObject_0, int int_0, int int_1);

	public int itemSize = 100;

	public bool cullContent = true;

	public OnInitializeItem onInitializeItem;

	private Transform transform_0;

	private UIPanel uipanel_0;

	private UIScrollView uiscrollView_0;

	private bool bool_0;

	private bool bool_1 = true;

	private BetterList<Transform> betterList_0 = new BetterList<Transform>();

	protected virtual void Start()
	{
		SortBasedOnScrollMovement();
		WrapContent();
		if (uiscrollView_0 != null)
		{
			uiscrollView_0.GetComponent<UIPanel>().onClippingMoved_0 = OnMove;
			uiscrollView_0.restrictWithinPanel = false;
			if (uiscrollView_0.dragEffect == UIScrollView.DragEffect.MomentumAndSpring)
			{
				uiscrollView_0.dragEffect = UIScrollView.DragEffect.Momentum;
			}
		}
		bool_1 = false;
	}

	protected virtual void OnMove(UIPanel uipanel_1)
	{
		WrapContent();
	}

	[ContextMenu("Sort Based on Scroll Movement")]
	public void SortBasedOnScrollMovement()
	{
		if (CacheScrollView())
		{
			betterList_0.Clear();
			for (int i = 0; i < transform_0.childCount; i++)
			{
				betterList_0.Add(transform_0.GetChild(i));
			}
			if (bool_0)
			{
				betterList_0.Sort(UIGrid.SortHorizontal);
			}
			else
			{
				betterList_0.Sort(UIGrid.SortVertical);
			}
			ResetChildPositions();
		}
	}

	[ContextMenu("Sort Alphabetically")]
	public void SortAlphabetically()
	{
		if (CacheScrollView())
		{
			betterList_0.Clear();
			for (int i = 0; i < transform_0.childCount; i++)
			{
				betterList_0.Add(transform_0.GetChild(i));
			}
			betterList_0.Sort(UIGrid.SortByName);
			ResetChildPositions();
		}
	}

	protected bool CacheScrollView()
	{
		transform_0 = base.transform;
		uipanel_0 = NGUITools.FindInParents<UIPanel>(base.gameObject);
		uiscrollView_0 = uipanel_0.GetComponent<UIScrollView>();
		if (uiscrollView_0 == null)
		{
			return false;
		}
		if (uiscrollView_0.movement == UIScrollView.Movement.Horizontal)
		{
			bool_0 = true;
		}
		else
		{
			if (uiscrollView_0.movement != UIScrollView.Movement.Vertical)
			{
				return false;
			}
			bool_0 = false;
		}
		return true;
	}

	private void ResetChildPositions()
	{
		for (int i = 0; i < betterList_0.size; i++)
		{
			Transform transform = betterList_0[i];
			transform.localPosition = ((!bool_0) ? new Vector3(0f, -i * itemSize, 0f) : new Vector3(i * itemSize, 0f, 0f));
		}
	}

	public void WrapContent()
	{
		float num = (float)(itemSize * betterList_0.size) * 0.5f;
		Vector3[] vector3_ = uipanel_0.Vector3_3;
		for (int i = 0; i < 4; i++)
		{
			Vector3 position = vector3_[i];
			position = transform_0.InverseTransformPoint(position);
			vector3_[i] = position;
		}
		Vector3 vector = Vector3.Lerp(vector3_[0], vector3_[2], 0.5f);
		if (bool_0)
		{
			float num2 = vector3_[0].x - (float)itemSize;
			float num3 = vector3_[2].x + (float)itemSize;
			for (int j = 0; j < betterList_0.size; j++)
			{
				Transform transform = betterList_0[j];
				float num4 = transform.localPosition.x - vector.x;
				if (num4 < 0f - num)
				{
					transform.localPosition += new Vector3(num * 2f, 0f, 0f);
					num4 = transform.localPosition.x - vector.x;
					UpdateItem(transform, j);
				}
				else if (num4 > num)
				{
					transform.localPosition -= new Vector3(num * 2f, 0f, 0f);
					num4 = transform.localPosition.x - vector.x;
					UpdateItem(transform, j);
				}
				else if (bool_1)
				{
					UpdateItem(transform, j);
				}
				if (cullContent)
				{
					num4 += uipanel_0.Vector2_0.x - transform_0.localPosition.x;
					if (!UICamera.IsPressed(transform.gameObject))
					{
						NGUITools.SetActive(transform.gameObject, num4 > num2 && num4 < num3, false);
					}
				}
			}
			return;
		}
		float num5 = vector3_[0].y - (float)itemSize;
		float num6 = vector3_[2].y + (float)itemSize;
		for (int k = 0; k < betterList_0.size; k++)
		{
			Transform transform2 = betterList_0[k];
			float num7 = transform2.localPosition.y - vector.y;
			if (num7 < 0f - num)
			{
				transform2.localPosition += new Vector3(0f, num * 2f, 0f);
				num7 = transform2.localPosition.y - vector.y;
				UpdateItem(transform2, k);
			}
			else if (num7 > num)
			{
				transform2.localPosition -= new Vector3(0f, num * 2f, 0f);
				num7 = transform2.localPosition.y - vector.y;
				UpdateItem(transform2, k);
			}
			else if (bool_1)
			{
				UpdateItem(transform2, k);
			}
			if (cullContent)
			{
				num7 += uipanel_0.Vector2_0.y - transform_0.localPosition.y;
				if (!UICamera.IsPressed(transform2.gameObject))
				{
					NGUITools.SetActive(transform2.gameObject, num7 > num5 && num7 < num6, false);
				}
			}
		}
	}

	protected virtual void UpdateItem(Transform transform_1, int int_0)
	{
		if (onInitializeItem != null)
		{
			int int_ = ((uiscrollView_0.movement != UIScrollView.Movement.Vertical) ? Mathf.RoundToInt(transform_1.localPosition.x / (float)itemSize) : Mathf.RoundToInt(transform_1.localPosition.y / (float)itemSize));
			onInitializeItem(transform_1.gameObject, int_0, int_);
		}
	}
}
