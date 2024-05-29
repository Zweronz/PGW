using UnityEngine;

public class ElixirSprite : MonoBehaviour
{
	private UISprite uisprite_0;

	private void Start()
	{
		bool flag = !Defs.bool_2;
		base.gameObject.SetActive(flag);
		if (flag)
		{
			uisprite_0 = GetComponent<UISprite>();
			uisprite_0.enabled = false;
		}
	}

	private void Update()
	{
	}
}
