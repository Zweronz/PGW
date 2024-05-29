using UnityEngine;

[RequireComponent(typeof(UIPanel))]
public class SpringPanel : MonoBehaviour
{
	public delegate void OnFinished();

	public static SpringPanel springPanel_0;

	public Vector3 target = Vector3.zero;

	public float strength = 10f;

	public OnFinished onFinished;

	private UIPanel uipanel_0;

	private Transform transform_0;

	private UIScrollView uiscrollView_0;

	private void Start()
	{
		uipanel_0 = GetComponent<UIPanel>();
		uiscrollView_0 = GetComponent<UIScrollView>();
		transform_0 = base.transform;
	}

	private void Update()
	{
		AdvanceTowardsPosition();
	}

	protected virtual void AdvanceTowardsPosition()
	{
		float single_ = RealTime.Single_1;
		bool flag = false;
		Vector3 localPosition = transform_0.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(transform_0.localPosition, target, strength, single_);
		if ((vector - target).sqrMagnitude < 0.01f)
		{
			vector = target;
			base.enabled = false;
			flag = true;
		}
		transform_0.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 vector2_ = uipanel_0.Vector2_0;
		vector2_.x -= vector2.x;
		vector2_.y -= vector2.y;
		uipanel_0.Vector2_0 = vector2_;
		if (uiscrollView_0 != null)
		{
			uiscrollView_0.UpdateScrollbars(false);
		}
		if (flag && onFinished != null)
		{
			springPanel_0 = this;
			onFinished();
			springPanel_0 = null;
		}
	}

	public static SpringPanel Begin(GameObject gameObject_0, Vector3 vector3_0, float float_0)
	{
		SpringPanel springPanel = gameObject_0.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = gameObject_0.AddComponent<SpringPanel>();
		}
		springPanel.target = vector3_0;
		springPanel.strength = float_0;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}
}
