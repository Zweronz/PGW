using UnityEngine;

[RequireComponent(typeof(UIProgressBar))]
public class UISliderColors : MonoBehaviour
{
	public UISprite sprite;

	public Color[] colors = new Color[3]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	private UIProgressBar uiprogressBar_0;

	private void Start()
	{
		uiprogressBar_0 = GetComponent<UIProgressBar>();
		Update();
	}

	private void Update()
	{
		if (sprite == null || colors.Length == 0)
		{
			return;
		}
		float single_ = uiprogressBar_0.Single_0;
		single_ *= (float)(colors.Length - 1);
		int num = Mathf.FloorToInt(single_);
		Color color_ = colors[0];
		if (num >= 0)
		{
			if (num + 1 >= colors.Length)
			{
				color_ = ((num >= colors.Length) ? colors[colors.Length - 1] : colors[num]);
			}
			else
			{
				float t = single_ - (float)num;
				color_ = Color.Lerp(colors[num], colors[num + 1], t);
			}
		}
		color_.a = sprite.Color_0.a;
		sprite.Color_0 = color_;
	}
}
