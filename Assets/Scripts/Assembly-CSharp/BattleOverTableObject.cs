using System.Collections.Generic;
using UnityEngine;

public class BattleOverTableObject : MonoBehaviour
{
	public BattleOverWindow myWindow;

	public GameObject[] places;

	public UILabel placeTxt;

	public new UILabel name;

	public UILabel clanName;

	public UITexture clanTexture;

	public UILabel money;

	public UISprite coin;

	public UILabel exp;

	public UILabel score;

	public UILabel kills;

	public UILabel assists;

	public UILabel deaths;

	public UILabel headshots;

	public UILabel accuracy;

	public UILabel damage;

	public UILabel flags;

	public UISprite likeGreen;

	public UISprite likeBlue;

	public UISprite likeGray;

	public UISprite cheatSprite;

	public UISprite cheatSpriteOk;

	public UISprite[] blueLight;

	public GameObject NickButton;

	public BattleOverPlayerData data;

	private void Start()
	{
		if (data == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (data.int_0 < 4)
		{
			placeTxt.gameObject.SetActive(false);
			for (int i = 0; i < places.Length; i++)
			{
				places[i].SetActive(i + 1 == data.int_0);
			}
		}
		else
		{
			for (int j = 0; j < places.Length; j++)
			{
				places[j].SetActive(false);
			}
			placeTxt.gameObject.SetActive(true);
			placeTxt.String_0 = data.int_0.ToString();
		}
		name.String_0 = data.string_0;
		clanName.String_0 = data.string_2;
		clanTexture.Texture_0 = data.texture_0;
		NGUITools.SetActive(clanTexture.gameObject, data.texture_0 != null);
		Vector3 localPosition = clanName.Transform_0.localPosition;
		localPosition.x = ((!(data.texture_0 == null)) ? 60 : 42);
		clanName.transform.localPosition = localPosition;
		money.String_0 = data.int_10.ToString();
		exp.String_0 = data.int_11.ToString();
		score.String_0 = data.int_2.ToString();
		kills.String_0 = data.int_3.ToString();
		assists.String_0 = data.int_5.ToString();
		deaths.String_0 = data.int_6.ToString();
		headshots.String_0 = data.int_8.ToString();
		accuracy.String_0 = string.Format("{0}%", Mathf.RoundToInt(data.float_0 * 100f));
		damage.String_0 = Mathf.FloorToInt(data.float_1).ToString();
		if (flags != null)
		{
			flags.String_0 = data.int_4.ToString();
		}
		coin.transform.localPosition = new Vector3(money.transform.localPosition.x + (float)(money.Int32_0 / 2) + (float)(coin.Int32_0 / 2), coin.transform.localPosition.y, coin.transform.localPosition.z);
		bool flag = data.int_1 == UserController.UserController_0.UserData_0.user_0.int_0;
		for (int k = 0; k < blueLight.Length; k++)
		{
			blueLight[k].gameObject.SetActive(flag);
		}
		likeGreen.gameObject.SetActive(false);
		likeBlue.gameObject.SetActive(data.int_1 != UserController.UserController_0.UserData_0.user_0.int_0);
		likeGray.gameObject.SetActive(data.int_1 == UserController.UserController_0.UserData_0.user_0.int_0);
		cheatSprite.gameObject.SetActive(!flag);
		cheatSpriteOk.gameObject.SetActive(false);
		if (flag)
		{
			BoxCollider component = NickButton.GetComponent<BoxCollider>();
			if (component != null)
			{
				Object.Destroy(component);
			}
		}
	}

	public void OnNickClick()
	{
		myWindow.ShowShrinkPanel(name.gameObject, data, true);
	}

	public void OnErrorClick()
	{
		myWindow.ShowShrinkPanel(cheatSprite.gameObject, data, false);
	}

	public void OnLikeClick()
	{
		if (data.int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
		{
			return;
		}
		bool bool_ = data.bool_0;
		BattleOverWindow.BattleOverWindow_0.SetDisableAllLikes(!data.bool_0);
		data.bool_1 = false;
		data.bool_0 = !bool_;
		likeGreen.gameObject.SetActive(data.bool_0);
		likeGreen.transform.GetChild(0).GetComponent<TweenColor>().enabled = !bool_;
		if (data.bool_0)
		{
			Animation component = likeGreen.transform.GetChild(1).GetComponent<Animation>();
			component.enabled = true;
			if (component == null)
			{
				component = likeGreen.transform.GetChild(1).GetComponent<Animation>();
			}
			foreach (AnimationState item in component)
			{
				component.Play(item.clip.name);
			}
		}
		likeBlue.gameObject.SetActive(!data.bool_0 && !data.bool_1);
		likeBlue.transform.GetChild(0).GetComponent<TweenColor>().enabled = bool_;
		likeGray.gameObject.SetActive(false);
		likeGray.transform.GetChild(0).GetComponent<TweenAlpha>().PlayReverse();
	}

	private void Update()
	{
		if (data.int_1 == UserController.UserController_0.UserData_0.user_0.int_0)
		{
			return;
		}
		bool flag = false;
		foreach (KeyValuePair<int, bool> item in data.dictionary_0)
		{
			if (item.Value)
			{
				flag = true;
				break;
			}
		}
		cheatSprite.gameObject.SetActive(!flag);
		cheatSpriteOk.gameObject.SetActive(flag);
	}
}
