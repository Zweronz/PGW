using UnityEngine;

public class BattleOverRevardPositionScript : MonoBehaviour
{
	public UILabel txtReward;

	public UILabel txtMoney;

	public UILabel txtExp;

	public UILabel txtExpWord;

	public UILabel txtNoReward;

	public UISprite coin;

	public UIWidget widget;

	public UISprite leftWing;

	public UISprite rightWing;

	public UISprite plate;

	public string moneyTxt;

	public string expTxt;

	public int ShowOnUpdate;

	private int int_0;

	public void Start()
	{
		txtReward.String_0 = Localizer.Get("ui.battle_over_window.reward");
		txtExpWord.String_0 = Localizer.Get("ui.battle_over_window.exp");
		txtNoReward.String_0 = Localizer.Get("ui.battle_over_window.no_reward");
	}

	private void Update()
	{
		if (ShowOnUpdate > 0)
		{
			int_0++;
		}
		if (int_0 == ShowOnUpdate)
		{
			lastAction();
			return;
		}
		txtMoney.String_0 = moneyTxt;
		txtExp.String_0 = expTxt;
		int num = 80;
		if (!txtNoReward.gameObject.activeSelf)
		{
			if (txtReward != null)
			{
				txtReward.transform.localPosition = new Vector3(num, txtReward.transform.localPosition.y, txtReward.transform.localPosition.z);
				num += (int)((float)txtReward.Int32_0 * txtReward.transform.localScale.x);
				num += 10;
			}
			if (txtMoney != null)
			{
				txtMoney.transform.localPosition = new Vector3(num, txtMoney.transform.localPosition.y, txtMoney.transform.localPosition.z);
				num += (int)((float)txtMoney.Int32_0 * txtMoney.transform.localScale.x);
				num += 5;
			}
			if (coin != null)
			{
				coin.transform.localPosition = new Vector3(num, coin.transform.localPosition.y, coin.transform.localPosition.z);
				num += coin.Int32_0;
				num += 30;
			}
			if (txtExp != null)
			{
				txtExp.transform.localPosition = new Vector3(num, txtExp.transform.localPosition.y, txtExp.transform.localPosition.z);
				num += (int)((float)txtExp.Int32_0 * txtExp.transform.localScale.x);
				num += 5;
			}
			if (txtExpWord != null)
			{
				txtExpWord.transform.localPosition = new Vector3(num, txtExpWord.transform.localPosition.y, txtExpWord.transform.localPosition.z);
				num += (int)((float)txtExpWord.Int32_0 * txtExpWord.transform.localScale.x);
			}
		}
		else
		{
			txtNoReward.transform.localPosition = new Vector3(num, txtNoReward.transform.localPosition.y, txtNoReward.transform.localPosition.z);
			num += (int)((float)txtNoReward.Int32_0 * txtNoReward.transform.localScale.x);
		}
		num += 80;
		widget.Int32_0 = num;
		UIWidget component = widget.transform.parent.GetComponent<UIWidget>();
		widget.transform.localPosition = new Vector3(component.Int32_0 / 2 - num / 2, widget.transform.localPosition.y, widget.transform.localPosition.z);
	}

	private void lastAction()
	{
		ShowOnUpdate = 0;
		plate.transform.localScale = new Vector3(0.15f, 1f, 1f);
		plate.GetComponent<TweenScale>().enabled = true;
		leftWing.GetComponent<TweenPosition>().vector3_1 = new Vector3(leftWing.transform.localPosition.x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		float x = widget.Int32_0 / 2 - leftWing.Int32_0 / 2;
		leftWing.transform.localPosition = new Vector3(x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		leftWing.GetComponent<TweenPosition>().vector3_0 = new Vector3(leftWing.transform.localPosition.x, leftWing.transform.localPosition.y, leftWing.transform.localPosition.z);
		leftWing.GetComponent<TweenPosition>().enabled = true;
		rightWing.GetComponent<TweenPosition>().vector3_1 = new Vector3(rightWing.transform.localPosition.x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		x = widget.Int32_0 / 2 + rightWing.Int32_0 / 2;
		rightWing.transform.localPosition = new Vector3(x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		rightWing.GetComponent<TweenPosition>().vector3_0 = new Vector3(rightWing.transform.localPosition.x, rightWing.transform.localPosition.y, rightWing.transform.localPosition.z);
		rightWing.GetComponent<TweenPosition>().enabled = true;
		setNullAnchors(leftWing);
		setNullAnchors(rightWing);
		setNullAnchors(plate);
		widget.GetComponent<TweenAlpha>().enabled = true;
		txtReward.Pivot_1 = UIWidget.Pivot.Center;
		txtMoney.Pivot_1 = UIWidget.Pivot.Center;
		txtExp.Pivot_1 = UIWidget.Pivot.Center;
		txtExpWord.Pivot_1 = UIWidget.Pivot.Center;
		txtNoReward.Pivot_1 = UIWidget.Pivot.Center;
		coin.Pivot_1 = UIWidget.Pivot.Center;
		txtReward.transform.localScale = new Vector3(0f, 0f, 1f);
		txtMoney.transform.localScale = new Vector3(0f, 0f, 1f);
		txtExp.transform.localScale = new Vector3(0f, 0f, 1f);
		txtExpWord.transform.localScale = new Vector3(0f, 0f, 1f);
		txtNoReward.transform.localScale = new Vector3(0f, 0f, 1f);
		coin.transform.localScale = new Vector3(0f, 0f, 1f);
		txtReward.GetComponent<TweenScale>().enabled = true;
		txtMoney.GetComponent<TweenScale>().enabled = true;
		txtExp.GetComponent<TweenScale>().enabled = true;
		txtExpWord.GetComponent<TweenScale>().enabled = true;
		txtNoReward.GetComponent<TweenScale>().enabled = true;
		coin.GetComponent<TweenScale>().enabled = true;
		base.enabled = false;
	}

	private void setNullAnchors(UIWidget uiwidget_0)
	{
		setNullAnchor(uiwidget_0.leftAnchor);
		setNullAnchor(uiwidget_0.rightAnchor);
		setNullAnchor(uiwidget_0.topAnchor);
		setNullAnchor(uiwidget_0.bottomAnchor);
	}

	private void setNullAnchor(UIRect.AnchorPoint anchorPoint_0)
	{
		anchorPoint_0.target = null;
		anchorPoint_0.rect = null;
		anchorPoint_0.targetCam = null;
		anchorPoint_0.relative = 0f;
		anchorPoint_0.absolute = 0;
	}
}
