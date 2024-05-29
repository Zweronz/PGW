using UnityEngine;

public class ShopItemSkill : MonoBehaviour
{
	public UISprite skillIcon;

	public UILabel skillTitle;

	public void Init(string string_0, string string_1)
	{
		skillTitle.String_0 = string_0;
		LabelHeightMaximizer component = skillTitle.GetComponent<LabelHeightMaximizer>();
		if (component != null)
		{
			component.Update();
		}
		skillIcon.String_0 = string_1;
	}
}
