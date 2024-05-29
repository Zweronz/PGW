using UnityEngine;

internal sealed class AdjustSpriteToLabel : MonoBehaviour
{
	public UILabel label;

	[Range(0f, 100f)]
	public float padding = 30f;

	private UISprite uisprite_0;

	private void Start()
	{
		uisprite_0 = GetComponent<UISprite>();
		if (label == null)
		{
			label = base.transform.parent.GetComponent<UILabel>();
		}
		if (uisprite_0 == null)
		{
			Debug.LogWarning("sprite == null");
		}
		if (label == null)
		{
			Debug.LogWarning("label == null");
		}
	}

	private void Update()
	{
		if (!(uisprite_0 == null) && !(label == null))
		{
			uisprite_0.transform.localPosition = new Vector3(padding + 0.5f * (float)label.Int32_0, 0f, 0f);
		}
	}
}
