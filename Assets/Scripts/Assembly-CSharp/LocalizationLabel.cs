using UnityEngine;

public class LocalizationLabel : MonoBehaviour
{
	public string localizeKey;

	private UILabel uilabel_0;

	private void Start()
	{
		uilabel_0 = base.gameObject.GetComponent<UILabel>();
		UpdateText();
		LocalizationStorage.Get.AddEventCallAfterLocalize(UpdateText);
	}

	private void Destroy()
	{
		LocalizationStorage.Get.RemoveEventCallAfterLocalize(UpdateText);
	}

	private void UpdateText()
	{
		if (uilabel_0 != null)
		{
			uilabel_0.String_0 = LocalizationStorage.Get.Term(localizeKey);
		}
	}
}
