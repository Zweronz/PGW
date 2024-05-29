using System.Collections.Generic;
using UnityEngine;

public class UIProgressBar : UIWidgetContainer
{
	public enum FillDirection
	{
		LeftToRight = 0,
		RightToLeft = 1,
		BottomToTop = 2,
		TopToBottom = 3
	}

	public delegate void OnDragFinished();

	public static UIProgressBar uiprogressBar_0;

	public OnDragFinished onDragFinished_0;

	public Transform transform_0;

	[SerializeField]
	protected UIWidget uiwidget_0;

	[SerializeField]
	protected UIWidget uiwidget_1;

	[SerializeField]
	protected float float_0 = 1f;

	[SerializeField]
	protected FillDirection fillDirection_0;

	protected Transform transform_1;

	protected bool bool_0;

	protected Camera camera_0;

	protected float float_1;

	public int int_0;

	public List<EventDelegate> list_0 = new List<EventDelegate>();

	public Transform Transform_0
	{
		get
		{
			if (transform_1 == null)
			{
				transform_1 = base.transform;
			}
			return transform_1;
		}
	}

	public Camera Camera_0
	{
		get
		{
			if (camera_0 == null)
			{
				camera_0 = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return camera_0;
		}
	}

	public UIWidget UIWidget_0
	{
		get
		{
			return uiwidget_1;
		}
		set
		{
			if (uiwidget_1 != value)
			{
				uiwidget_1 = value;
				bool_0 = true;
			}
		}
	}

	public UIWidget UIWidget_1
	{
		get
		{
			return uiwidget_0;
		}
		set
		{
			if (uiwidget_0 != value)
			{
				uiwidget_0 = value;
				bool_0 = true;
			}
		}
	}

	public FillDirection FillDirection_0
	{
		get
		{
			return fillDirection_0;
		}
		set
		{
			if (fillDirection_0 != value)
			{
				fillDirection_0 = value;
				ForceUpdate();
			}
		}
	}

	public float Single_0
	{
		get
		{
			if (int_0 > 1)
			{
				return Mathf.Round(float_0 * (float)(int_0 - 1)) / (float)(int_0 - 1);
			}
			return float_0;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (float_0 == num)
			{
				return;
			}
			float single_ = Single_0;
			float_0 = num;
			if (single_ != Single_0)
			{
				ForceUpdate();
				if (uiprogressBar_0 == null && NGUITools.GetActive(this) && EventDelegate.IsValid(list_0))
				{
					uiprogressBar_0 = this;
					EventDelegate.Execute(list_0);
					uiprogressBar_0 = null;
				}
			}
		}
	}

	public float Single_1
	{
		get
		{
			if (uiwidget_1 != null)
			{
				return uiwidget_1.Single_2;
			}
			if (uiwidget_0 != null)
			{
				return uiwidget_0.Single_2;
			}
			return 1f;
		}
		set
		{
			if (uiwidget_1 != null)
			{
				uiwidget_1.Single_2 = value;
				if (uiwidget_1.GetComponent<Collider>() != null)
				{
					uiwidget_1.GetComponent<Collider>().enabled = uiwidget_1.Single_2 > 0.001f;
				}
				else if (uiwidget_1.GetComponent<Collider2D>() != null)
				{
					uiwidget_1.GetComponent<Collider2D>().enabled = uiwidget_1.Single_2 > 0.001f;
				}
			}
			if (uiwidget_0 != null)
			{
				uiwidget_0.Single_2 = value;
				if (uiwidget_0.GetComponent<Collider>() != null)
				{
					uiwidget_0.GetComponent<Collider>().enabled = uiwidget_0.Single_2 > 0.001f;
				}
				else if (uiwidget_0.GetComponent<Collider2D>() != null)
				{
					uiwidget_0.GetComponent<Collider2D>().enabled = uiwidget_0.Single_2 > 0.001f;
				}
			}
			if (!(transform_0 != null))
			{
				return;
			}
			UIWidget component = transform_0.GetComponent<UIWidget>();
			if (component != null)
			{
				component.Single_2 = value;
				if (component.GetComponent<Collider>() != null)
				{
					component.GetComponent<Collider>().enabled = component.Single_2 > 0.001f;
				}
				else if (component.GetComponent<Collider2D>() != null)
				{
					component.GetComponent<Collider2D>().enabled = component.Single_2 > 0.001f;
				}
			}
		}
	}

	protected bool Boolean_0
	{
		get
		{
			return fillDirection_0 == FillDirection.LeftToRight || fillDirection_0 == FillDirection.RightToLeft;
		}
	}

	protected bool Boolean_1
	{
		get
		{
			return fillDirection_0 == FillDirection.RightToLeft || fillDirection_0 == FillDirection.TopToBottom;
		}
	}

	protected void Start()
	{
		Upgrade();
		if (Application.isPlaying)
		{
			if (uiwidget_0 != null)
			{
				uiwidget_0.bool_6 = true;
			}
			OnStart();
			if (uiprogressBar_0 == null && list_0 != null)
			{
				uiprogressBar_0 = this;
				EventDelegate.Execute(list_0);
				uiprogressBar_0 = null;
			}
		}
		ForceUpdate();
	}

	protected virtual void Upgrade()
	{
	}

	protected virtual void OnStart()
	{
	}

	protected void Update()
	{
		if (bool_0)
		{
			ForceUpdate();
		}
	}

	protected void OnValidate()
	{
		if (NGUITools.GetActive(this))
		{
			Upgrade();
			bool_0 = true;
			float num = Mathf.Clamp01(float_0);
			if (float_0 != num)
			{
				float_0 = num;
			}
			if (int_0 < 0)
			{
				int_0 = 0;
			}
			else if (int_0 > 20)
			{
				int_0 = 20;
			}
			ForceUpdate();
		}
		else
		{
			float num2 = Mathf.Clamp01(float_0);
			if (float_0 != num2)
			{
				float_0 = num2;
			}
			if (int_0 < 0)
			{
				int_0 = 0;
			}
			else if (int_0 > 20)
			{
				int_0 = 20;
			}
		}
	}

	protected float ScreenToValue(Vector2 vector2_0)
	{
		Transform transform = Transform_0;
		Plane plane = new Plane(transform.rotation * Vector3.back, transform.position);
		Ray ray = Camera_0.ScreenPointToRay(vector2_0);
		float enter;
		if (!plane.Raycast(ray, out enter))
		{
			return Single_0;
		}
		return LocalToValue(transform.InverseTransformPoint(ray.GetPoint(enter)));
	}

	protected virtual float LocalToValue(Vector2 vector2_0)
	{
		if (uiwidget_1 != null)
		{
			Vector3[] vector3_ = uiwidget_1.Vector3_2;
			Vector3 vector = vector3_[2] - vector3_[0];
			if (Boolean_0)
			{
				float num = (vector2_0.x - vector3_[0].x) / vector.x;
				return (!Boolean_1) ? num : (1f - num);
			}
			float num2 = (vector2_0.y - vector3_[0].y) / vector.y;
			return (!Boolean_1) ? num2 : (1f - num2);
		}
		return Single_0;
	}

	public virtual void ForceUpdate()
	{
		bool_0 = false;
		if (uiwidget_1 != null)
		{
			UIBasicSprite uIBasicSprite = uiwidget_1 as UIBasicSprite;
			if (Boolean_0)
			{
				if (uIBasicSprite != null && uIBasicSprite.Type_0 == UIBasicSprite.Type.Filled)
				{
					if (uIBasicSprite.FillDirection_0 == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.FillDirection_0 == UIBasicSprite.FillDirection.Vertical)
					{
						uIBasicSprite.FillDirection_0 = UIBasicSprite.FillDirection.Horizontal;
						uIBasicSprite.Boolean_5 = Boolean_1;
					}
					uIBasicSprite.Single_0 = Single_0;
				}
				else
				{
					uiwidget_1.Vector4_0 = ((!Boolean_1) ? new Vector4(0f, 0f, Single_0, 1f) : new Vector4(1f - Single_0, 0f, 1f, 1f));
				}
			}
			else if (uIBasicSprite != null && uIBasicSprite.Type_0 == UIBasicSprite.Type.Filled)
			{
				if (uIBasicSprite.FillDirection_0 == UIBasicSprite.FillDirection.Horizontal || uIBasicSprite.FillDirection_0 == UIBasicSprite.FillDirection.Vertical)
				{
					uIBasicSprite.FillDirection_0 = UIBasicSprite.FillDirection.Vertical;
					uIBasicSprite.Boolean_5 = Boolean_1;
				}
				uIBasicSprite.Single_0 = Single_0;
			}
			else
			{
				uiwidget_1.Vector4_0 = ((!Boolean_1) ? new Vector4(0f, 0f, 1f, Single_0) : new Vector4(0f, 1f - Single_0, 1f, 1f));
			}
		}
		if (transform_0 != null && (uiwidget_1 != null || uiwidget_0 != null))
		{
			Vector3[] array = ((!(uiwidget_1 != null)) ? uiwidget_0.Vector3_2 : uiwidget_1.Vector3_2);
			Vector4 vector = ((!(uiwidget_1 != null)) ? uiwidget_0.Vector4_3 : uiwidget_1.Vector4_3);
			array[0].x += vector.x;
			array[1].x += vector.x;
			array[2].x -= vector.z;
			array[3].x -= vector.z;
			array[0].y += vector.y;
			array[1].y -= vector.w;
			array[2].y -= vector.w;
			array[3].y += vector.y;
			Transform transform = ((!(uiwidget_1 != null)) ? uiwidget_0.Transform_0 : uiwidget_1.Transform_0);
			for (int i = 0; i < 4; i++)
			{
				array[i] = transform.TransformPoint(array[i]);
			}
			if (Boolean_0)
			{
				Vector3 from = Vector3.Lerp(array[0], array[1], 0.5f);
				Vector3 to = Vector3.Lerp(array[2], array[3], 0.5f);
				SetThumbPosition(Vector3.Lerp(from, to, (!Boolean_1) ? Single_0 : (1f - Single_0)));
			}
			else
			{
				Vector3 from2 = Vector3.Lerp(array[0], array[3], 0.5f);
				Vector3 to2 = Vector3.Lerp(array[1], array[2], 0.5f);
				SetThumbPosition(Vector3.Lerp(from2, to2, (!Boolean_1) ? Single_0 : (1f - Single_0)));
			}
		}
	}

	protected void SetThumbPosition(Vector3 vector3_0)
	{
		Transform parent = transform_0.parent;
		if (parent != null)
		{
			vector3_0 = parent.InverseTransformPoint(vector3_0);
			vector3_0.x = Mathf.Round(vector3_0.x);
			vector3_0.y = Mathf.Round(vector3_0.y);
			vector3_0.z = 0f;
			if (Vector3.Distance(transform_0.localPosition, vector3_0) > 0.001f)
			{
				transform_0.localPosition = vector3_0;
			}
		}
		else if (Vector3.Distance(transform_0.position, vector3_0) > 1E-05f)
		{
			transform_0.position = vector3_0;
		}
	}
}
