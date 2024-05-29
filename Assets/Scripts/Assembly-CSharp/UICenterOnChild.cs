using UnityEngine;

public class UICenterOnChild : MonoBehaviour
{
	public delegate void OnCenterCallback(GameObject gameObject_0);

	public float springStrength = 8f;

	public float nextPageThreshold;

	public SpringPanel.OnFinished onFinished;

	public OnCenterCallback onCenter;

	private UIScrollView uiscrollView_0;

	private GameObject gameObject_0;

	public GameObject GameObject_0
	{
		get
		{
			return gameObject_0;
		}
	}

	private void OnEnable()
	{
		Recenter();
	}

	private void OnDragFinished()
	{
		if (base.enabled)
		{
			Recenter();
		}
	}

	private void OnValidate()
	{
		nextPageThreshold = Mathf.Abs(nextPageThreshold);
	}

	[ContextMenu("Execute")]
	public void Recenter()
	{
		Transform transform = base.transform;
		if (transform.childCount == 0)
		{
			return;
		}
		if (uiscrollView_0 == null)
		{
			uiscrollView_0 = NGUITools.FindInParents<UIScrollView>(base.gameObject);
			if (uiscrollView_0 == null)
			{
				Debug.LogWarning(string.Concat(GetType(), " requires ", typeof(UIScrollView), " on a parent object in order to work"), this);
				base.enabled = false;
				return;
			}
			uiscrollView_0.onDragFinished = OnDragFinished;
			if (uiscrollView_0.horizontalScrollBar != null)
			{
				uiscrollView_0.horizontalScrollBar.onDragFinished_0 = OnDragFinished;
			}
			if (uiscrollView_0.verticalScrollBar != null)
			{
				uiscrollView_0.verticalScrollBar.onDragFinished_0 = OnDragFinished;
			}
		}
		if (uiscrollView_0.UIPanel_0 == null)
		{
			return;
		}
		Vector3[] vector3_ = uiscrollView_0.UIPanel_0.Vector3_3;
		Vector3 vector = (vector3_[2] + vector3_[0]) * 0.5f;
		Vector3 vector2 = vector - uiscrollView_0.Vector3_0 * (uiscrollView_0.momentumAmount * 0.1f);
		uiscrollView_0.Vector3_0 = Vector3.zero;
		float num = float.MaxValue;
		Transform transform_ = null;
		int num2 = 0;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.gameObject.activeInHierarchy)
			{
				float num3 = Vector3.SqrMagnitude(child.position - vector2);
				if (num3 < num)
				{
					num = num3;
					transform_ = child;
					num2 = i;
				}
			}
		}
		if (nextPageThreshold > 0f && UICamera.mouseOrTouch_0 != null && gameObject_0 != null && gameObject_0.transform == transform.GetChild(num2))
		{
			Vector2 vector2_ = UICamera.mouseOrTouch_0.vector2_3;
			float num4 = 0f;
			switch (uiscrollView_0.movement)
			{
			default:
				num4 = vector2_.magnitude;
				break;
			case UIScrollView.Movement.Vertical:
				num4 = vector2_.y;
				break;
			case UIScrollView.Movement.Horizontal:
				num4 = vector2_.x;
				break;
			}
			if (num4 > nextPageThreshold)
			{
				if (num2 > 0)
				{
					transform_ = transform.GetChild(num2 - 1);
				}
			}
			else if (num4 < 0f - nextPageThreshold && num2 < transform.childCount - 1)
			{
				transform_ = transform.GetChild(num2 + 1);
			}
		}
		CenterOn(transform_, vector);
	}

	private void CenterOn(Transform transform_0, Vector3 vector3_0)
	{
		if (transform_0 != null && uiscrollView_0 != null && uiscrollView_0.UIPanel_0 != null)
		{
			Transform transform_ = uiscrollView_0.UIPanel_0.Transform_0;
			gameObject_0 = transform_0.gameObject;
			Vector3 vector = transform_.InverseTransformPoint(transform_0.position);
			Vector3 vector2 = transform_.InverseTransformPoint(vector3_0);
			Vector3 vector3 = vector - vector2;
			if (!uiscrollView_0.Boolean_1)
			{
				vector3.x = 0f;
			}
			if (!uiscrollView_0.Boolean_2)
			{
				vector3.y = 0f;
			}
			vector3.z = 0f;
			SpringPanel.Begin(uiscrollView_0.UIPanel_0.GameObject_0, transform_.localPosition - vector3, springStrength).onFinished = onFinished;
		}
		else
		{
			gameObject_0 = null;
		}
		if (onCenter != null)
		{
			onCenter(gameObject_0);
		}
	}

	public void CenterOn(Transform transform_0)
	{
		if (uiscrollView_0 != null && uiscrollView_0.UIPanel_0 != null)
		{
			Vector3[] vector3_ = uiscrollView_0.UIPanel_0.Vector3_3;
			Vector3 vector3_2 = (vector3_[2] + vector3_[0]) * 0.5f;
			CenterOn(transform_0, vector3_2);
		}
	}
}
