using UnityEngine;

namespace I2.Loc
{
	public class LanguagePopup : MonoBehaviour
	{
		public LanguageSource Source;

		private void Start()
		{
			UIPopupList component = GetComponent<UIPopupList>();
			component.items = Source.GetLanguages();
			EventDelegate.Add(component.onChange, OnValueChange);
			component.String_0 = LocalizationManager.String_0;
		}

		public void OnValueChange()
		{
			LocalizationStore.String_44 = UIPopupList.uipopupList_0.String_0;
		}
	}
}
