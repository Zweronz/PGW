using UnityEngine;

namespace engine.unity
{
	public class UITabState : MonoBehaviour
	{
		public UISprite back;

		public UISprite icon;

		public Color activeColorIcon = Color.white;

		public Color inactiveColorIcon = Color.white;

		public UILabel label;

		public Color activeColorLabel = Color.white;

		public Color inactiveColorLabel = Color.white;

		public void SetColor(bool bool_0, Color color_0, Color color_1)
		{
			if (back == null)
			{
				return;
			}
			back.Color_0 = ((!bool_0) ? color_1 : color_0);
			if (!(icon == null))
			{
				label.Color_0 = ((!bool_0) ? inactiveColorIcon : activeColorIcon);
				if (!(label == null))
				{
					label.Color_0 = ((!bool_0) ? inactiveColorLabel : activeColorLabel);
				}
			}
		}

		public void SetText(string string_0)
		{
			if (!(label == null))
			{
				label.String_0 = string_0;
			}
		}
	}
}
