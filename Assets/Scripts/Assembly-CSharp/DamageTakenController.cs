using UnityEngine;

public class DamageTakenController : MonoBehaviour
{
	private float float_0;

	private float float_1 = 3f;

	public UISprite mySprite;

	public void reset(float float_2)
	{
		float_0 = float_1;
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f - float_2));
	}

	private void Start()
	{
		mySprite.Color_0 = new Color(1f, 1f, 1f, 0f);
	}

	public void Remove()
	{
		float_0 = -1f;
		mySprite.Color_0 = new Color(1f, 1f, 1f, 0f);
	}

	private void Update()
	{
		if (float_0 > 0f)
		{
			mySprite.Color_0 = new Color(1f, 1f, 1f, float_0 / float_1);
			float_0 -= Time.deltaTime;
			if (float_0 < 0f)
			{
				mySprite.Color_0 = new Color(1f, 1f, 1f, 0f);
			}
		}
	}
}
