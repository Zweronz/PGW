using System;
using UnityEngine;

public abstract class UIRect : MonoBehaviour
{
	[Serializable]
	public class AnchorPoint
	{
		public Transform target;

		public float relative;

		public int absolute;

		[NonSerialized]
		public UIRect rect;

		[NonSerialized]
		public Camera targetCam;

		public AnchorPoint()
		{
		}

		public AnchorPoint(float float_0)
		{
			relative = float_0;
		}

		public void Set(float float_0, float float_1)
		{
			relative = float_0;
			absolute = Mathf.FloorToInt(float_1 + 0.5f);
		}

		public void Set(Transform transform_0, float float_0, float float_1)
		{
			target = transform_0;
			relative = float_0;
			absolute = Mathf.FloorToInt(float_1 + 0.5f);
		}

		public void SetToNearest(float float_0, float float_1, float float_2)
		{
			SetToNearest(0f, 0.5f, 1f, float_0, float_1, float_2);
		}

		public void SetToNearest(float float_0, float float_1, float float_2, float float_3, float float_4, float float_5)
		{
			float num = Mathf.Abs(float_3);
			float num2 = Mathf.Abs(float_4);
			float num3 = Mathf.Abs(float_5);
			if (num < num2 && num < num3)
			{
				Set(float_0, float_3);
			}
			else if (num2 < num && num2 < num3)
			{
				Set(float_1, float_4);
			}
			else
			{
				Set(float_2, float_5);
			}
		}

		public void SetHorizontal(Transform transform_0, float float_0)
		{
			if ((bool)rect)
			{
				Vector3[] sides = rect.GetSides(transform_0);
				float num = Mathf.Lerp(sides[0].x, sides[2].x, relative);
				absolute = Mathf.FloorToInt(float_0 - num + 0.5f);
				return;
			}
			Vector3 position = target.position;
			if (transform_0 != null)
			{
				position = transform_0.InverseTransformPoint(position);
			}
			absolute = Mathf.FloorToInt(float_0 - position.x + 0.5f);
		}

		public void SetVertical(Transform transform_0, float float_0)
		{
			if ((bool)rect)
			{
				Vector3[] sides = rect.GetSides(transform_0);
				float num = Mathf.Lerp(sides[3].y, sides[1].y, relative);
				absolute = Mathf.FloorToInt(float_0 - num + 0.5f);
				return;
			}
			Vector3 position = target.position;
			if (transform_0 != null)
			{
				position = transform_0.InverseTransformPoint(position);
			}
			absolute = Mathf.FloorToInt(float_0 - position.y + 0.5f);
		}

		public Vector3[] GetSides(Transform transform_0)
		{
			if (target != null)
			{
				if (rect != null)
				{
					return rect.GetSides(transform_0);
				}
				if (target.GetComponent<Camera>() != null)
				{
					return target.GetComponent<Camera>().GetSides(transform_0);
				}
			}
			return null;
		}
	}

	public enum AnchorUpdate
	{
		OnEnable = 0,
		OnUpdate = 1
	}

	public AnchorPoint leftAnchor = new AnchorPoint();

	public AnchorPoint rightAnchor = new AnchorPoint(1f);

	public AnchorPoint bottomAnchor = new AnchorPoint();

	public AnchorPoint topAnchor = new AnchorPoint(1f);

	public AnchorUpdate updateAnchors = AnchorUpdate.OnUpdate;

	protected GameObject gameObject_0;

	protected Transform transform_0;

	protected BetterList<UIRect> betterList_0 = new BetterList<UIRect>();

	protected bool bool_0 = true;

	protected bool bool_1;

	protected bool bool_2;

	protected bool bool_3;

	[NonSerialized]
	public float finalAlpha = 1f;

	private UIRoot uiroot_0;

	private UIRect uirect_0;

	private Camera camera_0;

	private int int_0 = -1;

	private bool bool_4;

	private bool bool_5;

	private static Vector3[] vector3_0 = new Vector3[4];

	public GameObject GameObject_0
	{
		get
		{
			if (gameObject_0 == null)
			{
				gameObject_0 = base.gameObject;
			}
			return gameObject_0;
		}
	}

	public Transform Transform_0
	{
		get
		{
			if (transform_0 == null)
			{
				transform_0 = base.transform;
			}
			return transform_0;
		}
	}

	public Camera Camera_0
	{
		get
		{
			if (!bool_4)
			{
				ResetAnchors();
			}
			return camera_0;
		}
	}

	public bool Boolean_0
	{
		get
		{
			return (bool)leftAnchor.target && (bool)rightAnchor.target && (bool)topAnchor.target && (bool)bottomAnchor.target;
		}
	}

	public virtual bool Boolean_12
	{
		get
		{
			return (bool)leftAnchor.target || (bool)rightAnchor.target;
		}
	}

	public virtual bool Boolean_13
	{
		get
		{
			return (bool)bottomAnchor.target || (bool)topAnchor.target;
		}
	}

	public virtual bool Boolean_7
	{
		get
		{
			return true;
		}
	}

	public UIRect UIRect_0
	{
		get
		{
			if (!bool_2)
			{
				bool_2 = true;
				uirect_0 = NGUITools.FindInParents<UIRect>(Transform_0.parent);
			}
			return uirect_0;
		}
	}

	public UIRoot UIRoot_0
	{
		get
		{
			if (UIRect_0 != null)
			{
				return uirect_0.UIRoot_0;
			}
			if (!bool_5)
			{
				bool_5 = true;
				uiroot_0 = NGUITools.FindInParents<UIRoot>(Transform_0);
			}
			return uiroot_0;
		}
	}

	public bool Boolean_1
	{
		get
		{
			return ((bool)leftAnchor.target || (bool)rightAnchor.target || (bool)topAnchor.target || (bool)bottomAnchor.target) && Boolean_7;
		}
	}

	public abstract float Single_2 { get; set; }

	public abstract Vector3[] Vector3_2 { get; }

	public abstract Vector3[] Vector3_3 { get; }

	public abstract float CalculateFinalAlpha(int int_1);

	public virtual void Invalidate(bool bool_6)
	{
		bool_0 = true;
		if (bool_6)
		{
			for (int i = 0; i < betterList_0.size; i++)
			{
				betterList_0.buffer[i].Invalidate(true);
			}
		}
	}

	public virtual Vector3[] GetSides(Transform transform_1)
	{
		if (Camera_0 != null)
		{
			Vector3[] sides = Camera_0.GetSides(transform_1);
			UIRoot uIRoot_ = UIRoot_0;
			if (uIRoot_ != null)
			{
				float single_ = uIRoot_.Single_0;
				for (int i = 0; i < 4; i++)
				{
					sides[i] *= single_;
				}
			}
			return sides;
		}
		Vector3 position = Transform_0.position;
		for (int j = 0; j < 4; j++)
		{
			vector3_0[j] = position;
		}
		if (transform_1 != null)
		{
			for (int k = 0; k < 4; k++)
			{
				vector3_0[k] = transform_1.InverseTransformPoint(vector3_0[k]);
			}
		}
		return vector3_0;
	}

	protected Vector3 GetLocalPos(AnchorPoint anchorPoint_0, Transform transform_1)
	{
		if (!(Camera_0 == null) && !(anchorPoint_0.targetCam == null))
		{
			Vector3 vector = Vector3.zero;
			if (camera_0.rect.width != 0f)
			{
				vector = camera_0.ViewportToWorldPoint(anchorPoint_0.targetCam.WorldToViewportPoint(anchorPoint_0.target.position));
			}
			if (transform_1 != null)
			{
				vector = transform_1.InverseTransformPoint(vector);
			}
			vector.x = Mathf.Floor(vector.x + 0.5f);
			vector.y = Mathf.Floor(vector.y + 0.5f);
			return vector;
		}
		return Transform_0.localPosition;
	}

	protected virtual void OnEnable()
	{
		bool_4 = false;
		int_0 = -1;
		if (updateAnchors == AnchorUpdate.OnEnable)
		{
			bool_3 = true;
		}
		if (bool_1)
		{
			OnInit();
		}
		int_0 = -1;
	}

	protected virtual void OnInit()
	{
		bool_0 = true;
		bool_5 = false;
		bool_2 = false;
		if (UIRect_0 != null)
		{
			uirect_0.betterList_0.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		if ((bool)uirect_0)
		{
			uirect_0.betterList_0.Remove(this);
		}
		uirect_0 = null;
		uiroot_0 = null;
		bool_5 = false;
		bool_2 = false;
	}

	protected void Start()
	{
		bool_1 = true;
		OnInit();
		OnStart();
	}

	public void Update()
	{
		if (!bool_4)
		{
			ResetAnchors();
		}
		int frameCount = Time.frameCount;
		if (int_0 == frameCount)
		{
			return;
		}
		if (updateAnchors == AnchorUpdate.OnUpdate || bool_3)
		{
			int_0 = frameCount;
			bool_3 = false;
			bool flag = false;
			if ((bool)leftAnchor.target)
			{
				flag = true;
				if (leftAnchor.rect != null && leftAnchor.rect.int_0 != frameCount)
				{
					leftAnchor.rect.Update();
				}
			}
			if ((bool)bottomAnchor.target)
			{
				flag = true;
				if (bottomAnchor.rect != null && bottomAnchor.rect.int_0 != frameCount)
				{
					bottomAnchor.rect.Update();
				}
			}
			if ((bool)rightAnchor.target)
			{
				flag = true;
				if (rightAnchor.rect != null && rightAnchor.rect.int_0 != frameCount)
				{
					rightAnchor.rect.Update();
				}
			}
			if ((bool)topAnchor.target)
			{
				flag = true;
				if (topAnchor.rect != null && topAnchor.rect.int_0 != frameCount)
				{
					topAnchor.rect.Update();
				}
			}
			if (flag)
			{
				OnAnchor();
			}
		}
		OnUpdate();
	}

	public void UpdateAnchors()
	{
		if (Boolean_1)
		{
			OnAnchor();
		}
	}

	protected abstract void OnAnchor();

	public void SetAnchor(Transform transform_1)
	{
		leftAnchor.target = transform_1;
		rightAnchor.target = transform_1;
		topAnchor.target = transform_1;
		bottomAnchor.target = transform_1;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject gameObject_1)
	{
		Transform target = ((!(gameObject_1 != null)) ? null : gameObject_1.transform);
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		ResetAnchors();
		UpdateAnchors();
	}

	public void SetAnchor(GameObject gameObject_1, int int_1, int int_2, int int_3, int int_4)
	{
		Transform target = ((!(gameObject_1 != null)) ? null : gameObject_1.transform);
		leftAnchor.target = target;
		rightAnchor.target = target;
		topAnchor.target = target;
		bottomAnchor.target = target;
		leftAnchor.relative = 0f;
		rightAnchor.relative = 1f;
		bottomAnchor.relative = 0f;
		topAnchor.relative = 1f;
		leftAnchor.absolute = int_1;
		rightAnchor.absolute = int_3;
		bottomAnchor.absolute = int_2;
		topAnchor.absolute = int_4;
		ResetAnchors();
		UpdateAnchors();
	}

	public void ResetAnchors()
	{
		bool_4 = true;
		leftAnchor.rect = ((!leftAnchor.target) ? null : leftAnchor.target.GetComponent<UIRect>());
		bottomAnchor.rect = ((!bottomAnchor.target) ? null : bottomAnchor.target.GetComponent<UIRect>());
		rightAnchor.rect = ((!rightAnchor.target) ? null : rightAnchor.target.GetComponent<UIRect>());
		topAnchor.rect = ((!topAnchor.target) ? null : topAnchor.target.GetComponent<UIRect>());
		camera_0 = NGUITools.FindCameraForLayer(GameObject_0.layer);
		FindCameraFor(leftAnchor);
		FindCameraFor(bottomAnchor);
		FindCameraFor(rightAnchor);
		FindCameraFor(topAnchor);
		bool_3 = true;
	}

	public abstract void SetRect(float float_0, float float_1, float float_2, float float_3);

	private void FindCameraFor(AnchorPoint anchorPoint_0)
	{
		if (!(anchorPoint_0.target == null) && !(anchorPoint_0.rect != null))
		{
			anchorPoint_0.targetCam = NGUITools.FindCameraForLayer(anchorPoint_0.target.gameObject.layer);
		}
		else
		{
			anchorPoint_0.targetCam = null;
		}
	}

	public virtual void ParentHasChanged()
	{
		bool_2 = false;
		UIRect uIRect = NGUITools.FindInParents<UIRect>(Transform_0.parent);
		if (uirect_0 != uIRect)
		{
			if ((bool)uirect_0)
			{
				uirect_0.betterList_0.Remove(this);
			}
			uirect_0 = uIRect;
			if ((bool)uirect_0)
			{
				uirect_0.betterList_0.Add(this);
			}
			bool_5 = false;
		}
	}

	protected abstract void OnStart();

	protected virtual void OnUpdate()
	{
	}
}
