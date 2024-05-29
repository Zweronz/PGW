using UnityEngine;

namespace Rilisoft
{
	public sealed class NewAvailableItemInShop : MonoBehaviour
	{
		public new string tag = string.Empty;

		public UITexture itemImage;

		public UILabel itemName;

		public int artikulId;

		public void OnClick()
		{
			LevelUpWindow.LevelUpWindow_0.Hide();
			ShopWindowParams shopWindowParams = new ShopWindowParams();
			shopWindowParams.int_0 = artikulId;
			ShopWindow.Show(shopWindowParams);
		}
	}
}
