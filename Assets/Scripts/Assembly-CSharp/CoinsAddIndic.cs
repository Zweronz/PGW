using System.Collections;
using UnityEngine;

public class CoinsAddIndic : MonoBehaviour
{
	private const float float_0 = 255f;

	private const float float_1 = 255f;

	private const float float_2 = 0f;

	private const float float_3 = 115f;

	private const float float_4 = 0f;

	private const float float_5 = 0f;

	private const float float_6 = 255f;

	private const float float_7 = 115f;

	public AudioClip coinsAdded;

	public bool isGems;

	public bool isX3;

	private UISprite uisprite_0;

	private bool bool_0;

	private bool bool_1;

	private Color Color_0
	{
		get
		{
			return (!isGems) ? new Color(1f, 1f, 0f, 23f / 51f) : new Color(0f, 0f, 1f, 23f / 51f);
		}
	}

	private void Start()
	{
		uisprite_0 = GetComponent<UISprite>();
		bool_1 = Defs.bool_0;
	}

	private void IndicateCoinsAdd(bool bool_2)
	{
		if (bool_2 == isGems)
		{
			if (!bool_0)
			{
				StartCoroutine(blink());
			}
			StartCoroutine(PlaySound());
		}
	}

	private Color NormalColor()
	{
		return (!isX3) ? new Color(0f, 0f, 0f, 23f / 51f) : new Color(0.5019608f, 4f / 85f, 4f / 85f, 1f);
	}

	private IEnumerator blink()
	{
		yield break;
	}

	private IEnumerator PlaySound()
	{
		yield break;
	}
}
