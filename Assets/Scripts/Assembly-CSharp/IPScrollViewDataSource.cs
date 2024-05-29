using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPScrollViewDataSource : MonoBehaviour
{
	public IPCycler scrollView;

	public GameObject itemPrefab;

	public UIWidget[] items;

	public int lineLength = 1;

	public UIButton backwardButton;

	public UIButton forwardButton;

	protected UIWidget[] uiwidget_0;

	protected int int_0;

	private int int_1;

	private int int_2 = -1;

	private int int_3 = -1;

	private float float_0;

	private float float_1;

	private bool bool_0;

	private float float_2;

	private float float_3;

	private bool bool_1 = true;

	private bool bool_2 = true;

	private Dictionary<int, int> dictionary_0 = new Dictionary<int, int>();

	private Dictionary<int, int> dictionary_1 = new Dictionary<int, int>();

	public virtual void Init()
	{
		scrollView.Init();
		IPCycler iPCycler = scrollView;
		iPCycler.onCyclerIndexChange = (IPCycler.CyclerIndexChangeHandler)Delegate.Combine(iPCycler.onCyclerIndexChange, new IPCycler.CyclerIndexChangeHandler(CyclerIndexChange));
		NGUITools.SetActive(itemPrefab, false);
		CreateWidgets();
		InitButtons();
	}

	private IEnumerator InitScrollPositions()
	{
		yield return null;
		Vector3 position = scrollView.UIScrollView_0.UIPanel_0.Vector3_2[0];
		Vector3 position2 = base.transform.TransformPoint(position);
		float x = UICamera.Camera_1.WorldToScreenPoint(position2).x;
		float y = UICamera.Camera_1.WorldToScreenPoint(position2).y;
		Vector3 position3 = scrollView.UIScrollView_0.UIPanel_0.Vector3_2[2];
		Vector3 position4 = base.transform.TransformPoint(position3);
		float x2 = UICamera.Camera_1.WorldToScreenPoint(position4).x;
		float y2 = UICamera.Camera_1.WorldToScreenPoint(position4).y;
		if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Horizontal)
		{
			float_0 = x;
			float_1 = x2;
		}
		else if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Vertical)
		{
			float_0 = y2;
			float_1 = y;
		}
	}

	public virtual void Setup(int int_4)
	{
		scrollView.Init();
		IPCycler iPCycler = scrollView;
		iPCycler.onCyclerIndexChange = (IPCycler.CyclerIndexChangeHandler)Delegate.Combine(iPCycler.onCyclerIndexChange, new IPCycler.CyclerIndexChangeHandler(CyclerIndexChange));
		CreateWidgets();
		SetItemsCount(int_4);
		int_1 = 0;
		UpdateWidgets();
		UpdateButtons();
	}

	public void SetPosition(int int_4)
	{
		ResetAtIndex(int_0, Mathf.Clamp(int_4, 0, int_0 - 1));
	}

	protected void InitButtons()
	{
		if (backwardButton != null)
		{
			backwardButton.list_0.Add(new EventDelegate(delegate
			{
				Backward();
			}));
		}
		if (forwardButton != null)
		{
			forwardButton.list_0.Add(new EventDelegate(delegate
			{
				Forward();
			}));
		}
	}

	protected void Forward()
	{
		Vector3 vector3_ = Vector3.zero;
		float spacing = scrollView.spacing;
		if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Horizontal)
		{
			float x = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.x;
			vector3_ = new Vector3(0f - spacing - x, 0f, 0f);
		}
		else if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Vertical)
		{
			float x = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.y;
			vector3_ = new Vector3(0f, spacing - x, 0f);
		}
		scrollView.UIScrollView_0.MoveRelative(vector3_);
		UpdateButtons();
	}

	protected void Backward()
	{
		Vector3 vector3_ = Vector3.zero;
		float spacing = scrollView.spacing;
		if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Horizontal)
		{
			float x = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.x;
			vector3_ = new Vector3(spacing - x, 0f, 0f);
		}
		else if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Vertical)
		{
			float x = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.x;
			vector3_ = new Vector3(0f, 0f - spacing - x, 0f);
		}
		scrollView.UIScrollView_0.MoveRelative(vector3_);
		UpdateButtons();
	}

	protected void ResetAtIndex(int int_4, int int_5)
	{
		SetItemsCount(int_4);
		int_1 = int_5;
		if (uiwidget_0.Length >= int_0)
		{
			int_1 = 0;
		}
		else if (int_1 + uiwidget_0.Length > int_0)
		{
			int_1 = int_0 - uiwidget_0.Length;
		}
		scrollView.UIScrollView_0.ResetPosition();
		scrollView.ResetCycler();
		UpdateWidgets();
		float spacing = scrollView.spacing;
		int num = int_5 - int_1;
		Vector3 vector3_ = Vector3.zero;
		if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Horizontal)
		{
			float x = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.x;
			float z = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.z;
			float num2 = z / spacing;
			if (num >= (int)num2)
			{
				vector3_ = new Vector3((0f - spacing) * (Mathf.Clamp((float)num - num2, 0f, float.MaxValue) + 1f) - x, 0f, 0f);
			}
		}
		else if (scrollView.UIScrollView_0.movement == UIScrollView.Movement.Vertical)
		{
			float w = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.w;
			float y = scrollView.UIScrollView_0.UIPanel_0.Vector4_1.y;
			float num3 = w / spacing;
			if (num >= (int)num3)
			{
				vector3_ = new Vector3(0f, spacing * (Mathf.Clamp((float)num - num3, 0f, float.MaxValue) + 1f) - y, 0f);
			}
		}
		scrollView.UIScrollView_0.MoveRelative(vector3_);
	}

	protected void Reset(int int_4, int int_5 = 0)
	{
		ResetAtIndex(int_4, int_5);
		float num = (float)Mathf.Max(uiwidget_0.Length, int_0);
	}

	protected void SetItemsCount(int int_4)
	{
		float f = (float)int_4 / (float)lineLength;
		int_0 = Mathf.CeilToInt(f);
	}

	private void OnDestroy()
	{
		if (scrollView != null)
		{
			IPCycler iPCycler = scrollView;
			iPCycler.onCyclerIndexChange = (IPCycler.CyclerIndexChangeHandler)Delegate.Remove(iPCycler.onCyclerIndexChange, new IPCycler.CyclerIndexChangeHandler(CyclerIndexChange));
		}
	}

	private void CyclerIndexChange(bool bool_3, int int_4)
	{
		if (int_0 == 0)
		{
			return;
		}
		if (bool_3)
		{
			int_1++;
			if (uiwidget_0.Length >= int_0)
			{
				int_1 = 0;
			}
			else if (int_1 + uiwidget_0.Length > int_0)
			{
				int_1 = int_0 - uiwidget_0.Length;
			}
		}
		else
		{
			int_1--;
			if (int_1 < 0)
			{
				int_1 = 0;
			}
		}
		CycleWidgets(bool_3, int_4);
	}

	private void Update()
	{
		if (scrollView != null && uiwidget_0 != null)
		{
			if (uiwidget_0.Length - 1 >= int_0)
			{
				scrollView.Boolean_1 = true;
			}
			else
			{
				scrollView.Boolean_1 = int_1 == int_0 - uiwidget_0.Length;
			}
			scrollView.Boolean_2 = int_1 == 0;
			if (scrollView.UIScrollView_0.restrictWithinPanel && int_1 == 0)
			{
			}
		}
	}

	private void UpdateRestrictWithinPanel()
	{
		scrollView.UIScrollView_0.restrictWithinPanel = true;
	}

	protected virtual int GetLastItemId()
	{
		return 0;
	}

	private void LateUpdate()
	{
		if (!(scrollView != null) || uiwidget_0 == null)
		{
			return;
		}
		if (scrollView.Int32_3 - 1 >= int_0)
		{
			scrollView.UIScrollView_0.restrictWithinPanel = true;
			return;
		}
		Vector3[] vector3_ = scrollView.UIScrollView_0.UIPanel_0.Vector3_3;
		Vector3 position = (vector3_[2] + vector3_[3]) * 0.5f;
		Transform transform_ = scrollView.UIScrollView_0.UIPanel_0.Transform_0;
		Transform transform = uiwidget_0[0].transform.parent;
		for (int i = 1; i < uiwidget_0.Length; i++)
		{
			Transform parent = uiwidget_0[i].transform.parent;
			if (parent.localPosition.x > transform.localPosition.x)
			{
				transform = parent;
			}
		}
		Vector3 vector = transform_.InverseTransformPoint(position);
		Vector3 vector5 = transform.localPosition - vector;
		Vector3 vector2 = UICamera.Camera_1.WorldToScreenPoint(scrollView.UIScrollView_0.transform.parent.position);
		vector2.x += (float)scrollView.UIScrollView_0.transform.parent.gameObject.GetComponent<UIWidget>().Int32_0 * 0.5f;
		Vector3 vector3 = UICamera.Camera_1.WorldToScreenPoint(transform.position);
		Vector3 vector4 = UICamera.Camera_1.WorldToScreenPoint(position);
		int itemId = transform.GetChild(0).gameObject.GetComponent<IPPickerItem>().itemId;
		bool flag = itemId == GetLastItemId();
		if (int_1 != 0 && (int_1 != int_0 - scrollView.Int32_3 || !flag || vector3.x >= vector4.x))
		{
			if (!bool_0)
			{
				scrollView.UIScrollView_0.restrictWithinPanel = false;
			}
		}
		else if (!bool_0)
		{
			bool_0 = true;
			Invoke("SpringScroll", 0.1f);
		}
	}

	private void SpringScroll()
	{
		Debug.Log(string.Format("SpringScroll Started!!!"));
		SpringPanel.Begin(scrollView.UIScrollView_0.gameObject, scrollView.UIScrollView_0.transform.localPosition, 13f).onFinished = delegate
		{
			scrollView.UIScrollView_0.restrictWithinPanel = true;
			scrollView.UIScrollView_0.DisableSpring();
			bool_0 = false;
			Debug.Log(string.Format("SpringScroll Finished!!!"));
		};
	}

	private void CycleWidgets(bool bool_3, int int_4)
	{
		int int_5 = ((!bool_3) ? int_1 : (int_1 + uiwidget_0.Length - 1));
		UpdateWidget(int_4, int_5);
	}

	protected void UpdateButtons()
	{
		if (backwardButton != null)
		{
			if (int_2 >= 0)
			{
				float_2 = ((scrollView.UIScrollView_0.movement != 0) ? UICamera.Camera_1.WorldToScreenPoint(uiwidget_0[int_2].transform.position).y : UICamera.Camera_1.WorldToScreenPoint(uiwidget_0[int_2].transform.position).x);
				backwardButton.Boolean_0 = float_2 < float_0 + scrollView.spacing * 0.1f;
				if (!backwardButton.Boolean_0 && bool_1)
				{
					bool_1 = false;
					scrollView.Scroll(-0.01f);
				}
			}
			else
			{
				backwardButton.Boolean_0 = true;
				bool_1 = true;
			}
		}
		if (!(forwardButton != null))
		{
			return;
		}
		if (int_3 >= 0)
		{
			float_3 = ((scrollView.UIScrollView_0.movement != 0) ? UICamera.Camera_1.WorldToScreenPoint(uiwidget_0[int_3].transform.position).y : UICamera.Camera_1.WorldToScreenPoint(uiwidget_0[int_3].transform.position).x);
			forwardButton.Boolean_0 = float_3 > float_1 - scrollView.spacing * 0.1f;
			if (!forwardButton.Boolean_0 && bool_2)
			{
				bool_2 = false;
				scrollView.Scroll(0.01f);
			}
		}
		else
		{
			forwardButton.Boolean_0 = true;
			bool_2 = true;
		}
	}

	public virtual void UpdateWidget(int int_4, int int_5)
	{
		dictionary_1[int_4] = int_5;
		dictionary_0[int_5] = int_4;
		int_2 = ((!dictionary_1.ContainsValue(0)) ? (-1) : dictionary_0[0]);
		int_3 = ((!dictionary_1.ContainsValue(int_0 - 1)) ? (-1) : dictionary_0[int_0 - 1]);
	}

	public void CreateWidgets()
	{
		uiwidget_0 = new UIWidget[scrollView.Int32_3];
		for (int i = 0; i < scrollView.Int32_3; i++)
		{
			Transform transform = scrollView._cycledTransforms[i];
			while (transform.childCount > 0)
			{
				Transform child = transform.GetChild(0);
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
			GameObject gameObject = NGUITools.AddChild(transform.gameObject, itemPrefab);
			NGUITools.SetActive(gameObject, true);
			uiwidget_0[i] = gameObject.GetComponentInChildren<UIWidget>();
			uiwidget_0[i].Transform_0.localPosition = Vector3.zero;
		}
	}

	public void UpdateWidgets()
	{
		for (int i = 0; i < uiwidget_0.Length; i++)
		{
			UpdateWidget(i, int_1 + i);
		}
	}
}
