using UnityEngine;

public class UI2DSpriteAnimation : MonoBehaviour
{
	public int framerate = 20;

	public bool ignoreTimeScale = true;

	public Sprite[] frames;

	private SpriteRenderer spriteRenderer_0;

	private UI2DSprite ui2DSprite_0;

	private int int_0;

	private float float_0;

	private void Start()
	{
		spriteRenderer_0 = GetComponent<SpriteRenderer>();
		ui2DSprite_0 = GetComponent<UI2DSprite>();
		if (framerate > 0)
		{
			float_0 = ((!ignoreTimeScale) ? Time.time : RealTime.Single_0) + 1f / (float)framerate;
		}
	}

	private void Update()
	{
		if (framerate == 0 || frames == null || frames.Length <= 0)
		{
			return;
		}
		float num = ((!ignoreTimeScale) ? Time.time : RealTime.Single_0);
		if (float_0 < num)
		{
			float_0 = num;
			int_0 = NGUIMath.RepeatIndex((framerate <= 0) ? (int_0 - 1) : (int_0 + 1), frames.Length);
			float_0 = num + Mathf.Abs(1f / (float)framerate);
			if (spriteRenderer_0 != null)
			{
				spriteRenderer_0.sprite = frames[int_0];
			}
			else if (ui2DSprite_0 != null)
			{
				ui2DSprite_0.sprite_1 = frames[int_0];
			}
		}
	}
}
