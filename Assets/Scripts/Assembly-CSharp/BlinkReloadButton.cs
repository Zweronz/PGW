using UnityEngine;

public class BlinkReloadButton : MonoBehaviour
{
	public static bool bool_0;

	private bool bool_1;

	private float float_0;

	public float maxTimerBlink = 0.5f;

	public Color blinkColor = new Color(1f, 0f, 0f);

	public Color unBlinkColor = new Color(1f, 1f, 1f);

	public static bool bool_2;

	public UISprite[] blinkObjs;

	public bool isBlinkTemp;

	private void Start()
	{
		bool_0 = false;
		bool_2 = false;
	}

	private void Update()
	{
		isBlinkTemp = bool_0;
		if (bool_1 != bool_0)
		{
			float_0 = maxTimerBlink;
		}
		if (bool_0)
		{
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				float_0 = maxTimerBlink;
				bool_2 = !bool_2;
				for (int i = 0; i < blinkObjs.Length; i++)
				{
					blinkObjs[i].Color_0 = ((!bool_2) ? unBlinkColor : blinkColor);
				}
			}
		}
		if (!bool_0 && bool_2)
		{
			bool_2 = !bool_2;
			for (int j = 0; j < blinkObjs.Length; j++)
			{
				blinkObjs[j].Color_0 = ((!bool_2) ? unBlinkColor : blinkColor);
			}
		}
		bool_1 = bool_0;
	}
}
