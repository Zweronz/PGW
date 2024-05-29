using UnityEngine;

public class OpenURLOnClick : MonoBehaviour
{
	private void OnClick()
	{
		UILabel component = GetComponent<UILabel>();
		if (component != null)
		{
			string urlAtPosition = component.GetUrlAtPosition(UICamera.vector3_0);
			if (!string.IsNullOrEmpty(urlAtPosition))
			{
				Application.OpenURL(urlAtPosition);
			}
		}
	}
}
