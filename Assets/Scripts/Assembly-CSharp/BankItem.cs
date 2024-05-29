using UnityEngine;

public class BankItem : MonoBehaviour
{
	public UISprite background;

	public UILabel moneyValue;

	public UISprite moneyIcon;

	public UILabel moneyValueAdd;

	public UISprite moneyIconAdd;

	public UILabel priceValue;

	public UISprite bonusBackground;

	public UILabel bonusAmount;

	public UITexture bonusImage;

	public UISprite[] images;

	public UISprite[] stickers;

	private BankPositionData bankPositionData_0;

	public void SetData(BankPositionData bankPositionData_1)
	{
		bankPositionData_0 = bankPositionData_1;
		Init();
	}

	private void Init()
	{
		background.String_0 = string.Format("item_{0}_bg", (!bankPositionData_0.Boolean_2) ? "green" : "orange");
		moneyValue.String_0 = bankPositionData_0.Int32_1.ToString();
		moneyValueAdd.String_0 = bankPositionData_0.Int32_2.ToString();
		NGUITools.SetActive(moneyValueAdd.gameObject, bankPositionData_0.Int32_2 > 0);
		priceValue.String_0 = string.Format("${0,2}", bankPositionData_0.Single_0);
		for (int i = 0; i < images.Length; i++)
		{
			NGUITools.SetActive(images[i].gameObject, i == bankPositionData_0.Int32_6 - 1);
		}
		NGUITools.SetActive(bonusBackground.gameObject, bankPositionData_0.Int32_3 > 0);
		NGUITools.SetActive(bonusAmount.gameObject, false);
		bonusBackground.String_0 = string.Format("item_bonus_{0}", (!bankPositionData_0.Boolean_2) ? "blue" : "orange");
		NGUITools.SetActive(stickers[0].gameObject, bankPositionData_0.Boolean_1);
		NGUITools.SetActive(stickers[1].gameObject, bankPositionData_0.Boolean_0);
		NGUITools.SetActive(stickers[2].gameObject, bankPositionData_0.Boolean_2);
	}

	public void OnBuyButtonClick()
	{
		BankCheckoutWindowParams bankCheckoutWindowParams = new BankCheckoutWindowParams();
		bankCheckoutWindowParams.BankPositionData_0 = bankPositionData_0;
		BankCheckoutWindow.Show(bankCheckoutWindowParams);
	}

	private void OnClick()
	{
		OnBuyButtonClick();
	}
}
