using UnityEngine;

public sealed class ButtonClickSound : MonoBehaviour
{
	public static ButtonClickSound buttonClickSound_0;

	public AudioClip Click;

	private void Start()
	{
		buttonClickSound_0 = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void PlayClick()
	{
		if (Click != null && Defs.Boolean_0)
		{
			NGUITools.PlaySound(Click);
		}
	}
}
