using UnityEngine;

public class SpringPosition : MonoBehaviour
{
	public delegate void OnFinished();

	public static SpringPosition springPosition_0;

	public Vector3 target = Vector3.zero;

	public float strength = 10f;

	public bool worldSpace;

	public bool ignoreTimeScale;

	public bool updateScrollView;

	public OnFinished onFinished;

	[SerializeField]
	private GameObject gameObject_0;

	[SerializeField]
	public string string_0;

	private Transform transform_0;

	private float float_0;

	private UIScrollView uiscrollView_0;

	private void Start()
	{
		transform_0 = base.transform;
		if (updateScrollView)
		{
			uiscrollView_0 = NGUITools.FindInParents<UIScrollView>(base.gameObject);
		}
	}

	private void Update()
	{
		float float_ = ((!ignoreTimeScale) ? Time.deltaTime : RealTime.Single_1);
		if (worldSpace)
		{
			if (float_0 == 0f)
			{
				float_0 = (target - transform_0.position).sqrMagnitude * 0.001f;
			}
			transform_0.position = NGUIMath.SpringLerp(transform_0.position, target, strength, float_);
			if (float_0 >= (target - transform_0.position).sqrMagnitude)
			{
				transform_0.position = target;
				NotifyListeners();
				base.enabled = false;
			}
		}
		else
		{
			if (float_0 == 0f)
			{
				float_0 = (target - transform_0.localPosition).sqrMagnitude * 1E-05f;
			}
			transform_0.localPosition = NGUIMath.SpringLerp(transform_0.localPosition, target, strength, float_);
			if (float_0 >= (target - transform_0.localPosition).sqrMagnitude)
			{
				transform_0.localPosition = target;
				NotifyListeners();
				base.enabled = false;
			}
		}
		if (uiscrollView_0 != null)
		{
			uiscrollView_0.UpdateScrollbars(true);
		}
	}

	private void NotifyListeners()
	{
		springPosition_0 = this;
		if (onFinished != null)
		{
			onFinished();
		}
		if (gameObject_0 != null && !string.IsNullOrEmpty(string_0))
		{
			gameObject_0.SendMessage(string_0, this, SendMessageOptions.DontRequireReceiver);
		}
		springPosition_0 = null;
	}

	public static SpringPosition Begin(GameObject gameObject_1, Vector3 vector3_0, float float_1)
	{
		SpringPosition springPosition = gameObject_1.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = gameObject_1.AddComponent<SpringPosition>();
		}
		springPosition.target = vector3_0;
		springPosition.strength = float_1;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.float_0 = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}
}
