using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(UIScrollView))]
[RequireComponent(typeof(UIPanel))]
public class IPCycler : MonoBehaviour
{
	public enum Direction
	{
		Vertical = 0,
		Horizontal = 1
	}

	public delegate void CyclerIndexChangeHandler(bool bool_0, int int_0);

	public delegate void CyclerStoppedHandler();

	public delegate void SelectionStartedHandler();

	public delegate void CenterOnChildStartedHandler();

	public Direction direction;

	public float spacing = 30f;

	public float recenterSpeedThreshold = 0.2f;

	public float recenterSpringStrength = 8f;

	public bool restrictDragToPicker;

	public CyclerIndexChangeHandler onCyclerIndexChange;

	public CyclerStoppedHandler onCyclerStopped;

	public SelectionStartedHandler onCyclerSelectionStarted;

	public CenterOnChildStartedHandler onCenterOnChildStarted;

	public Transform[] _cycledTransforms;

	private UIScrollView uiscrollView_0;

	public IPDragScrollView ipdragScrollView_0;

	public IPUserInteraction ipuserInteraction_0;

	private UIPanel uipanel_0;

	private UICenterOnChild uicenterOnChild_0;

	private BoxCollider boxCollider_0;

	private int int_0 = -1;

	private int int_1 = -1;

	private float float_0;

	private float float_1;

	private float float_2;

	private float float_3;

	private float float_4;

	private float float_5;

	private Vector3 vector3_0;

	private bool bool_0;

	[SerializeField]
	private bool bool_1;

	[SerializeField]
	private float float_6;

	[CompilerGenerated]
	private bool bool_2;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private bool bool_3;

	[CompilerGenerated]
	private bool bool_4;

	public bool Boolean_0
	{
		[CompilerGenerated]
		get
		{
			return bool_2;
		}
		[CompilerGenerated]
		private set
		{
			bool_2 = value;
		}
	}

	public int Int32_0
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		private set
		{
			int_2 = value;
		}
	}

	public int Int32_1
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		private set
		{
			int_3 = value;
		}
	}

	public int Int32_2
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		private set
		{
			int_4 = value;
		}
	}

	public int Int32_3
	{
		get
		{
			return base.transform.childCount;
		}
	}

	public int Int32_4
	{
		get
		{
			return base.transform.childCount;
		}
	}

	public bool Boolean_1
	{
		[CompilerGenerated]
		get
		{
			return bool_3;
		}
		[CompilerGenerated]
		set
		{
			bool_3 = value;
		}
	}

	public bool Boolean_2
	{
		[CompilerGenerated]
		get
		{
			return bool_4;
		}
		[CompilerGenerated]
		set
		{
			bool_4 = value;
		}
	}

	public UIScrollView UIScrollView_0
	{
		get
		{
			return uiscrollView_0;
		}
	}

	public void Init()
	{
		if (int_0 != Time.frameCount)
		{
			int_0 = Time.frameCount;
			uiscrollView_0 = base.gameObject.GetComponent(typeof(UIScrollView)) as UIScrollView;
			uipanel_0 = base.gameObject.GetComponent(typeof(UIPanel)) as UIPanel;
			NGUITools.SetActive(base.transform.parent.gameObject, true);
			if (boxCollider_0 == null)
			{
				boxCollider_0 = base.transform.parent.GetComponentInChildren(typeof(BoxCollider)) as BoxCollider;
			}
			if (boxCollider_0 != null)
			{
				ipdragScrollView_0 = boxCollider_0.gameObject.AddComponent(typeof(IPDragScrollView)) as IPDragScrollView;
				ipdragScrollView_0.scrollView = uiscrollView_0;
				ipuserInteraction_0 = boxCollider_0.gameObject.AddComponent(typeof(IPUserInteraction)) as IPUserInteraction;
				ipuserInteraction_0.cycler = this;
				ipuserInteraction_0.restrictWithinPicker = restrictDragToPicker;
			}
			else
			{
				Debug.Log("Could not get collider");
			}
			uicenterOnChild_0 = base.gameObject.AddComponent(typeof(UICenterOnChild)) as UICenterOnChild;
			uicenterOnChild_0.enabled = false;
			uicenterOnChild_0.springStrength = recenterSpringStrength;
			uicenterOnChild_0.onFinished = PickerStopped;
			ScrollWheelEvents.CheckInstance();
			uiscrollView_0.movement = ((!bool_1) ? UIScrollView.Movement.Vertical : UIScrollView.Movement.Horizontal);
			vector3_0 = uipanel_0.Transform_0.localPosition;
			float_4 = SignificantPosVector(uipanel_0.Transform_0);
			float_2 = (0f - float_6) * (float)Int32_3;
			Int32_0 = Int32_3 / 2;
			Int32_1 = 0;
			float_0 = float_4;
			float_1 = float_4 + float_6;
			float_5 = 0f;
			Int32_2 = 0;
			bool_0 = true;
		}
	}

	public void ResetCycler()
	{
		if (int_1 != Time.frameCount)
		{
			int_1 = Time.frameCount;
			uipanel_0.Transform_0.localPosition = vector3_0;
			uipanel_0.Vector2_0 = new Vector2(0f, 0f);
			float_4 = SignificantPosVector(uipanel_0.Transform_0);
			PlaceTransforms();
			Int32_1 = 0;
			float_0 = 0f;
			float_1 = float_6;
			float_5 = 0f;
		}
	}

	private void Update()
	{
		if (!bool_0)
		{
			return;
		}
		float_3 = SignificantPosVector(uipanel_0.Transform_0);
		if (!Mathf.Approximately(float_3, float_4))
		{
			Boolean_0 = true;
			float_5 = float_3 - float_4;
			if (bool_1)
			{
				float_5 = 0f - float_5;
			}
			if (float_5 > 0f)
			{
				while (TryIncrement())
				{
				}
			}
			else
			{
				while (TryDecrement())
				{
				}
			}
		}
		else if (Boolean_0)
		{
			Boolean_0 = false;
			float_5 = 0f;
		}
		float_4 = float_3;
	}

	private void PickerStopped()
	{
		if (onCyclerStopped != null)
		{
			onCyclerStopped();
		}
	}

	public void Scroll(float float_7)
	{
		uiscrollView_0.Scroll(float_7);
	}

	public int GetDeltaIndexFromScreenPos(Vector2 vector2_0)
	{
		Vector3 position = UICamera.camera_0.ScreenToWorldPoint(new Vector3(vector2_0.x, vector2_0.y, 0f));
		Vector3 vector = base.transform.parent.InverseTransformPoint(position);
		float num = ((direction != Direction.Horizontal) ? vector.y : vector.x);
		num = ((!(num >= 0f)) ? (num - spacing / 2f) : (num + spacing / 2f));
		int num2 = (int)num / (int)spacing;
		if (direction == Direction.Vertical)
		{
			num2 = -num2;
		}
		return Mathf.Clamp(num2, -Int32_3 / 2, Int32_3 / 2);
	}

	public int GetIndexFromScreenPos(Vector2 vector2_0)
	{
		int deltaIndexFromScreenPos = GetDeltaIndexFromScreenPos(vector2_0);
		int num = (Int32_0 + deltaIndexFromScreenPos) % Int32_3;
		if (num < 0)
		{
			num += Int32_3;
		}
		return num;
	}

	public void OnPress(bool bool_5)
	{
		if (bool_5)
		{
			StopAllCoroutines();
			if (onCyclerSelectionStarted != null)
			{
				onCyclerSelectionStarted();
			}
		}
	}

	public void ScrollWheelStartOrStop(bool bool_5)
	{
		if (bool_5)
		{
			StopAllCoroutines();
			if (onCyclerSelectionStarted != null)
			{
				onCyclerSelectionStarted();
			}
		}
		else
		{
			StartCoroutine(RecenterOnThreshold());
		}
	}

	private IEnumerator RecenterOnThreshold()
	{
		while (Mathf.Abs(float_5) > recenterSpeedThreshold)
		{
			yield return null;
		}
		CenterOnTransformIndex(Int32_1);
	}

	private void CenterOnTransformIndex(int int_5)
	{
		Int32_2 = int_5;
		uicenterOnChild_0.CenterOn(_cycledTransforms[int_5]);
		if (onCenterOnChildStarted != null)
		{
			onCenterOnChildStarted();
		}
	}

	private float SignificantPosVector(Transform transform_0)
	{
		return (!bool_1) ? transform_0.localPosition.y : transform_0.localPosition.x;
	}

	private void SetWidgetSignificantPos(Transform transform_0, float float_7)
	{
		if (!bool_1)
		{
			transform_0.localPosition = new Vector3(0f, float_7, transform_0.localPosition.z);
		}
		else
		{
			transform_0.localPosition = new Vector3(float_7, 0f, transform_0.localPosition.z);
		}
	}

	private bool TryIncrement()
	{
		if (Boolean_1)
		{
			return false;
		}
		if (bool_1)
		{
			if (!(float_3 < float_1))
			{
				return false;
			}
		}
		else if (!(float_3 > float_1))
		{
			return false;
		}
		float_1 += float_6;
		float_0 += float_6;
		int int_;
		Transform transform_ = FirstWidget(out int_);
		SetWidgetSignificantPos(transform_, SignificantPosVector(transform_) + float_2);
		Int32_0 = (Int32_0 + 1) % Int32_3;
		Int32_1 = (Int32_1 + 1) % Int32_3;
		if (onCyclerIndexChange != null)
		{
			onCyclerIndexChange(true, int_);
		}
		return true;
	}

	private bool TryDecrement()
	{
		if (Boolean_2)
		{
			return false;
		}
		if (bool_1)
		{
			if (!(float_3 > float_0))
			{
				return false;
			}
		}
		else if (!(float_3 < float_0))
		{
			return false;
		}
		float_1 -= float_6;
		float_0 -= float_6;
		int int_;
		Transform transform_ = LastWidget(out int_);
		SetWidgetSignificantPos(transform_, SignificantPosVector(transform_) - float_2);
		Int32_1--;
		if (Int32_1 < 0)
		{
			Int32_1 += Int32_3;
		}
		if (onCyclerIndexChange != null)
		{
			onCyclerIndexChange(false, int_);
		}
		return false;
	}

	private Transform FirstWidget(out int int_5)
	{
		int_5 = Int32_1;
		if (int_5 < 0)
		{
			int_5 += Int32_3;
		}
		return _cycledTransforms[int_5];
	}

	private Transform LastWidget(out int int_5)
	{
		int_5 = (Int32_1 + Int32_3 - 1) % Int32_3;
		return _cycledTransforms[int_5];
	}

	public void EditorInit()
	{
		DestroyChildrenOfChildren();
		if (_cycledTransforms == null || _cycledTransforms.Length != Int32_4)
		{
			_cycledTransforms = new Transform[Int32_4];
		}
		for (int i = 0; i < Int32_4; i++)
		{
			_cycledTransforms[i] = base.transform.GetChild(i);
		}
		UpdateTrueSpacing();
	}

	public void RebuildTransforms(int int_5)
	{
		int int32_ = Int32_4;
		if (int32_ != 0)
		{
			Transform[] array = new Transform[int32_];
			for (int i = 0; i < int32_; i++)
			{
				array[i] = base.transform.GetChild(i);
			}
			for (int i = 0; i < int32_; i++)
			{
				Object.DestroyImmediate(array[i].gameObject);
			}
		}
		_cycledTransforms = new Transform[int_5];
		for (int i = 0; i < int_5; i++)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.name = i.ToString();
			gameObject.layer = LayerMask.NameToLayer("NGUIRoot");
			_cycledTransforms[i] = gameObject.transform;
		}
		UpdateTrueSpacing();
	}

	public void UpdateTrueSpacing()
	{
		bool_1 = direction == Direction.Horizontal;
		float_6 = Mathf.Abs(spacing);
		if (bool_1)
		{
			float_6 = 0f - float_6;
		}
		PlaceTransforms();
	}

	public void PlaceTransforms()
	{
		UIPanel component = GetComponent<UIPanel>();
		float num = ((!bool_1) ? (component.Vector4_1.w / 2f - component.Vector2_1.y / 2f - float_6 / 2f) : ((0f - component.Vector4_1.z) / 2f + component.Vector2_1.x / 2f - float_6 / 2f));
		for (int i = 0; i < Int32_4; i++)
		{
			SetWidgetSignificantPos(_cycledTransforms[i], num);
			num -= float_6;
		}
	}

	private void DestroyChildrenOfChildren()
	{
		foreach (Transform item in base.transform)
		{
			Transform[] array = new Transform[item.childCount];
			for (int i = 0; i < item.childCount; i++)
			{
				array[i] = item.GetChild(i);
			}
			for (int j = 0; j < array.Length; j++)
			{
				Object.DestroyImmediate(array[j].gameObject);
			}
		}
	}
}
