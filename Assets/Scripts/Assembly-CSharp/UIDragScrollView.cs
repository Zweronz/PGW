using UnityEngine;

public class UIDragScrollView : MonoBehaviour
{
	public UIScrollView scrollView;

	[SerializeField]
	private UIScrollView uiscrollView_0;

	private Transform transform_0;

	private UIScrollView uiscrollView_1;

	private bool bool_0;

	private bool bool_1;

	private void OnEnable()
	{
		transform_0 = base.transform;
		if (scrollView == null && uiscrollView_0 != null)
		{
			scrollView = uiscrollView_0;
			uiscrollView_0 = null;
		}
		if (bool_1 && (bool_0 || uiscrollView_1 == null))
		{
			FindScrollView();
		}
	}

	private void Start()
	{
		bool_1 = true;
		FindScrollView();
	}

	private void FindScrollView()
	{
		UIScrollView uIScrollView = NGUITools.FindInParents<UIScrollView>(transform_0);
		if (scrollView == null)
		{
			scrollView = uIScrollView;
			bool_0 = true;
		}
		else if (scrollView == uIScrollView)
		{
			bool_0 = true;
		}
		uiscrollView_1 = scrollView;
	}

	private void OnPress(bool bool_2)
	{
		if (bool_0 && uiscrollView_1 != scrollView)
		{
			uiscrollView_1 = scrollView;
			bool_0 = false;
		}
		if ((bool)scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			scrollView.Press(bool_2);
			if (!bool_2 && bool_0)
			{
				scrollView = NGUITools.FindInParents<UIScrollView>(transform_0);
				uiscrollView_1 = scrollView;
			}
		}
	}

	private void OnDrag(Vector2 vector2_0)
	{
		if ((bool)scrollView && NGUITools.GetActive(this))
		{
			scrollView.Drag();
		}
	}

	private void OnScroll(float float_0)
	{
		if ((bool)scrollView && NGUITools.GetActive(this))
		{
			scrollView.Scroll(float_0);
		}
	}
}
