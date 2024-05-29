using UnityEngine;

public class BonusItemDetailInfo : MonoBehaviour
{
	public UILabel title;

	public UILabel title1;

	public UILabel title2;

	public UILabel description;

	public UITexture imageHolder;

	public void SetTitle(string string_0)
	{
		title.String_0 = string_0;
		title1.String_0 = string_0;
		title2.String_0 = string_0;
	}

	public void SetDescription(string string_0)
	{
		description.String_0 = string_0;
	}

	public void SetImage(Texture2D texture2D_0)
	{
		imageHolder.Texture_0 = texture2D_0;
	}

	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}
}
