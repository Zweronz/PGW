using UnityEngine;

namespace I2.Loc
{
	public class SetLanguage : MonoBehaviour
	{
		public string _Language;

		private void OnClick()
		{
			ApplyLanguage();
		}

		public void ApplyLanguage()
		{
			if (LocalizationManager.HasLanguage(_Language))
			{
				LocalizationManager.String_0 = _Language;
			}
		}
	}
}
