using UnityEngine;

public class UICenterOnClick : MonoBehaviour
{
	private void OnClick()
	{
		UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(base.gameObject);
		UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
		if (uICenterOnChild != null)
		{
			if (uICenterOnChild.enabled)
			{
				uICenterOnChild.CenterOn(base.transform);
			}
		}
		else if (uIPanel != null && uIPanel.Clipping_0 != 0)
		{
			UIScrollView component = uIPanel.GetComponent<UIScrollView>();
			Vector3 vector3_ = -uIPanel.Transform_0.InverseTransformPoint(base.transform.position);
			if (!component.Boolean_1)
			{
				vector3_.x = uIPanel.Transform_0.localPosition.x;
			}
			if (!component.Boolean_2)
			{
				vector3_.y = uIPanel.Transform_0.localPosition.y;
			}
			SpringPanel.Begin(uIPanel.GameObject_0, vector3_, 6f);
		}
	}
}
