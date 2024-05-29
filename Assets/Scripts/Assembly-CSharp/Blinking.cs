using UnityEngine;

public class Blinking : MonoBehaviour
{
	public float halfCycle = 1f;

	private UISprite uisprite_0;

	private float float_0;

	private void Start()
	{
		uisprite_0 = GetComponent<UISprite>();
	}

	private void Update()
	{
		float_0 += Time.deltaTime;
		if (uisprite_0 != null)
		{
			Color color_ = uisprite_0.Color_0;
			float num = 2f * (float_0 - Mathf.Floor(float_0 / halfCycle) * halfCycle) / halfCycle;
			if (num > 1f)
			{
				num = 2f - num;
			}
			uisprite_0.Color_0 = new Color(color_.r, color_.g, color_.b, num);
		}
	}
}
