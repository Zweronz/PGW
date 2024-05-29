using UnityEngine;

public class ShopCompareController : MonoBehaviour
{
	public ShopItemInfoPanel[] infoPanels;

	public ShopItemComparePanel[] comparePanels;

	private int int_0 = -1;

	private int int_1 = -1;

	public void ShowInfo(int int_2, int int_3)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_2);
		if (artikul != null)
		{
			HidePanels();
			if (int_3 == 0)
			{
				int_3 = ShopWindow.ShopWindow_0.GetHeadTabByArtikul(int_2);
			}
			if (int_3 != 0)
			{
				int_0 = int_3 - 1;
				NGUITools.SetActive(infoPanels[int_0].gameObject, true);
				infoPanels[int_0].SetData(artikul);
			}
		}
	}

	public void ShowCompareInfo(int int_2, int int_3, int int_4)
	{
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_2);
		ArtikulData artikul2 = ArtikulController.ArtikulController_0.GetArtikul(int_3);
		if (artikul != null && artikul2 != null)
		{
			HidePanels();
			if (int_4 == 0)
			{
				int_4 = ShopWindow.ShopWindow_0.GetHeadTabByArtikul(int_2);
			}
			if (int_4 != 0)
			{
				int_1 = int_4 - 1;
				NGUITools.SetActive(comparePanels[int_1].gameObject, true);
				comparePanels[int_1].SetData(artikul, artikul2);
			}
		}
	}

	public void HidePanels()
	{
		ShopItemInfoPanel[] array = infoPanels;
		foreach (ShopItemInfoPanel shopItemInfoPanel in array)
		{
			if (!(shopItemInfoPanel == null))
			{
				NGUITools.SetActive(shopItemInfoPanel.gameObject, false);
			}
		}
		ShopItemComparePanel[] array2 = comparePanels;
		foreach (ShopItemComparePanel shopItemComparePanel in array2)
		{
			if (!(shopItemComparePanel == null))
			{
				NGUITools.SetActive(shopItemComparePanel.gameObject, false);
			}
		}
	}
}
