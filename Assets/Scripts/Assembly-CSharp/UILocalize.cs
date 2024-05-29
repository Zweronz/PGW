using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class UILocalize : MonoBehaviour
{
	public string key;

	private bool bool_0;

	public string String_0
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			UIWidget component = GetComponent<UIWidget>();
			UILabel uILabel = component as UILabel;
			UISprite uISprite = component as UISprite;
			if (uILabel != null)
			{
				UIInput uIInput = NGUITools.FindInParents<UIInput>(uILabel.gameObject);
				if (uIInput != null && uIInput.label == uILabel)
				{
					uIInput.String_0 = value;
				}
				else
				{
					uILabel.String_0 = value;
				}
			}
			else if (uISprite != null)
			{
				uISprite.String_0 = value;
				uISprite.MakePixelPerfect();
			}
		}
	}

	private void OnEnable()
	{
		if (bool_0)
		{
			OnLocalize();
		}
	}

	private void Start()
	{
		bool_0 = true;
		OnLocalize();
	}

	private void OnLocalize()
	{
		if (string.IsNullOrEmpty(key))
		{
			UILabel component = GetComponent<UILabel>();
			if (component != null)
			{
				key = component.String_0;
			}
		}
		if (!string.IsNullOrEmpty(key))
		{
			String_0 = Localization.Get(key);
		}
	}
}
