using UnityEngine;

public class HoverChecker : MonoBehaviour
{
	public UISprite sprite;

	private float float_0;

	private void Start()
	{
		if (!(sprite == null) && sprite.gameObject.activeSelf)
		{
			sprite.Single_2 = 0f;
		}
	}

	public void Show()
	{
		if (!(sprite == null) && sprite.gameObject.activeSelf)
		{
			sprite.Single_2 = 1f;
			float_0 = 0.15f;
		}
	}

	private void Update()
	{
		float_0 -= Time.deltaTime;
		if (float_0 < 0f)
		{
			sprite.Single_2 = 0f;
		}
	}
}
