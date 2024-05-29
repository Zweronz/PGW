using UnityEngine;

public class UIDragObject : MonoBehaviour
{
	public enum DragEffect
	{
		None = 0,
		Momentum = 1,
		MomentumAndSpring = 2
	}

	public Transform target;

	public Vector3 scrollMomentum = Vector3.zero;

	public bool restrictWithinPanel;

	public UIRect contentRect;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public float momentumAmount = 35f;

	[SerializeField]
	protected Vector3 vector3_0 = new Vector3(1f, 1f, 0f);

	[SerializeField]
	private float float_0;

	private Plane plane_0;

	private Vector3 vector3_1;

	private Vector3 vector3_2;

	private UIPanel uipanel_0;

	private Vector3 vector3_3 = Vector3.zero;

	private Vector3 vector3_4 = Vector3.zero;

	private Bounds bounds_0;

	private int int_0;

	private bool bool_0;

	private bool bool_1;

	public Vector3 Vector3_0
	{
		get
		{
			return vector3_0;
		}
		set
		{
			vector3_0 = value;
		}
	}

	private void OnEnable()
	{
		if (float_0 != 0f)
		{
			scrollMomentum = vector3_0 * float_0;
			float_0 = 0f;
		}
		if (contentRect == null && target != null && Application.isPlaying)
		{
			UIWidget component = target.GetComponent<UIWidget>();
			if (component != null)
			{
				contentRect = component;
			}
		}
	}

	private void OnDisable()
	{
		bool_0 = false;
	}

	private void FindPanel()
	{
		uipanel_0 = ((!(target != null)) ? null : UIPanel.Find(target.transform.parent));
		if (uipanel_0 == null)
		{
			restrictWithinPanel = false;
		}
	}

	private void UpdateBounds()
	{
		if ((bool)contentRect)
		{
			Transform transform_ = uipanel_0.Transform_0;
			Matrix4x4 worldToLocalMatrix = transform_.worldToLocalMatrix;
			Vector3[] array = contentRect.Vector3_3;
			for (int i = 0; i < 4; i++)
			{
				array[i] = worldToLocalMatrix.MultiplyPoint3x4(array[i]);
			}
			bounds_0 = new Bounds(array[0], Vector3.zero);
			for (int j = 1; j < 4; j++)
			{
				bounds_0.Encapsulate(array[j]);
			}
		}
		else
		{
			bounds_0 = NGUIMath.CalculateRelativeWidgetBounds(uipanel_0.Transform_0, target);
		}
	}

	private void OnPress(bool bool_2)
	{
		if (!base.enabled || !NGUITools.GetActive(base.gameObject) || !(target != null))
		{
			return;
		}
		if (bool_2)
		{
			if (!bool_1)
			{
				int_0 = UICamera.int_0;
				bool_1 = true;
				bool_0 = false;
				CancelMovement();
				if (restrictWithinPanel && uipanel_0 == null)
				{
					FindPanel();
				}
				if (restrictWithinPanel)
				{
					UpdateBounds();
				}
				CancelSpring();
				Transform transform = UICamera.camera_0.transform;
				plane_0 = new Plane(((!(uipanel_0 != null)) ? transform.rotation : uipanel_0.Transform_0.rotation) * Vector3.back, UICamera.vector3_0);
			}
		}
		else if (bool_1 && int_0 == UICamera.int_0)
		{
			bool_1 = false;
			if (restrictWithinPanel && dragEffect == DragEffect.MomentumAndSpring && uipanel_0.ConstrainTargetToBounds(target, ref bounds_0, false))
			{
				CancelMovement();
			}
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if (!bool_1 || int_0 != UICamera.int_0 || !base.enabled || !NGUITools.GetActive(base.gameObject) || !(target != null))
		{
			return;
		}
		UICamera.mouseOrTouch_0.clickNotification_0 = UICamera.ClickNotification.BasedOnDelta;
		Ray ray = UICamera.camera_0.ScreenPointToRay(UICamera.mouseOrTouch_0.vector2_0);
		float enter = 0f;
		if (!plane_0.Raycast(ray, out enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 vector = point - vector3_2;
		vector3_2 = point;
		if (!bool_0)
		{
			bool_0 = true;
			vector = Vector3.zero;
		}
		if (vector.x != 0f || vector.y != 0f)
		{
			vector = target.InverseTransformDirection(vector);
			vector.Scale(vector3_0);
			vector = target.TransformDirection(vector);
		}
		if (dragEffect != 0)
		{
			vector3_3 = Vector3.Lerp(vector3_3, vector3_3 + vector * (0.01f * momentumAmount), 0.67f);
		}
		Vector3 localPosition = target.localPosition;
		Move(vector);
		if (restrictWithinPanel)
		{
			bounds_0.center += target.localPosition - localPosition;
			if (dragEffect != DragEffect.MomentumAndSpring && uipanel_0.ConstrainTargetToBounds(target, ref bounds_0, true))
			{
				CancelMovement();
			}
		}
	}

	private void Move(Vector3 vector3_5)
	{
		if (uipanel_0 != null)
		{
			vector3_1 += vector3_5;
			target.position = vector3_1;
			Vector3 localPosition = target.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			target.localPosition = localPosition;
			UIScrollView component = uipanel_0.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		else
		{
			target.position += vector3_5;
		}
	}

	private void LateUpdate()
	{
		if (target == null)
		{
			return;
		}
		float single_ = RealTime.Single_1;
		vector3_3 -= vector3_4;
		vector3_4 = NGUIMath.SpringLerp(vector3_4, Vector3.zero, 20f, single_);
		if (!bool_1)
		{
			if (vector3_3.magnitude < 0.0001f)
			{
				return;
			}
			if (uipanel_0 == null)
			{
				FindPanel();
			}
			Move(NGUIMath.SpringDampen(ref vector3_3, 9f, single_));
			if (restrictWithinPanel && uipanel_0 != null)
			{
				UpdateBounds();
				if (uipanel_0.ConstrainTargetToBounds(target, ref bounds_0, dragEffect == DragEffect.None))
				{
					CancelMovement();
				}
				else
				{
					CancelSpring();
				}
			}
		}
		else
		{
			vector3_1 = ((!(target != null)) ? Vector3.zero : target.position);
		}
		NGUIMath.SpringDampen(ref vector3_3, 9f, single_);
	}

	public void CancelMovement()
	{
		vector3_1 = ((!(target != null)) ? Vector3.zero : target.position);
		vector3_3 = Vector3.zero;
		vector3_4 = Vector3.zero;
	}

	public void CancelSpring()
	{
		SpringPosition component = target.GetComponent<SpringPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	private void OnScroll(float float_1)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			vector3_4 -= scrollMomentum * (float_1 * 0.05f);
		}
	}
}
