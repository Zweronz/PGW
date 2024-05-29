public class IPDragScrollView : UIDragScrollView
{
	private void OnPress(bool bool_2)
	{
		if ((bool)scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			scrollView.Press(bool_2);
		}
	}
}
