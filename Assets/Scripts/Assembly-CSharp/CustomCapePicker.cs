using UnityEngine;

public sealed class CustomCapePicker : MonoBehaviour
{
	public int artikulId;

	public bool shouldLoadTexture = true;

	private void Start()
	{
		if (shouldLoadTexture)
		{
			Texture capeTexture = SkinsController.GetCapeTexture(artikulId);
			if (capeTexture != null)
			{
				SkinsController.SetTextureRecursivelyFrom(base.gameObject, capeTexture);
			}
		}
	}
}
