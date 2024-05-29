using UnityEngine;

public class HealthUIController : MonoBehaviour
{
	public UISlider hpProgressBar;

	public UISlider arProgressBar;

	public UISprite healthIcon;

	public UISprite armorIcon;

	public UILabel healthCurrent;

	public UILabel healthMax;

	public UILabel armorCurrent;

	public UILabel armorMax;

	public UIWidget armorContainer;

	private Transform transform_0;

	private Transform transform_1;

	private void Awake()
	{
		transform_0 = armorIcon.transform;
		transform_1 = healthIcon.transform;
	}

	private void Update()
	{
		if (!(HeadUpDisplay.GetPlayerMoveC() == null))
		{
			float num = 0f;
			num = (HeadUpDisplay.GetPlayerMoveC().PlayerMechController_0.Boolean_1 ? HeadUpDisplay.GetPlayerMoveC().PlayerParametersController_0.Single_2 : HeadUpDisplay.GetPlayerMoveC().PlayerMechController_0.Single_0);
			int num2 = Mathf.CeilToInt(num);
			healthCurrent.String_0 = num2.ToString();
			float num3 = 0f;
			num3 = (HeadUpDisplay.GetPlayerMoveC().PlayerMechController_0.Boolean_1 ? HeadUpDisplay.GetPlayerMoveC().PlayerParametersController_0.Single_0 : HeadUpDisplay.GetPlayerMoveC().PlayerMechController_0.Single_1);
			num2 = Mathf.CeilToInt(num3);
			healthMax.String_0 = string.Format("/{0}", num2);
			hpProgressBar.Single_0 = num / ((!(num3 > 0f)) ? 1f : num3);
			hpProgressBar.gameObject.SetActive(num != 0f);
			hpProgressBar.GetComponent<Animation>().enabled = (double)num <= (double)num3 * 0.3;
			if (!hpProgressBar.GetComponent<Animation>().enabled)
			{
				hpProgressBar.GetComponent<AnimatedColor>().color = Color.white;
			}
			healthIcon.GetComponent<Animation>().enabled = (double)num <= (double)num3 * 0.3;
			if (!healthIcon.GetComponent<Animation>().enabled)
			{
				healthIcon.GetComponent<AnimatedColor>().color = Color.white;
			}
			transform_1.GetChild(0).gameObject.SetActive(num == num3);
			transform_1.GetChild(0).GetComponent<Animation>().enabled = num == num3;
			if (!transform_1.GetChild(0).GetComponent<Animation>().enabled)
			{
				transform_1.GetChild(0).GetComponent<AnimatedColor>().color = Color.white;
			}
			float single_ = HeadUpDisplay.GetPlayerMoveC().PlayerParametersController_0.Single_7;
			int num4 = Mathf.CeilToInt(single_);
			armorCurrent.String_0 = num4.ToString();
			float num5 = HeadUpDisplay.GetPlayerMoveC().PlayerParametersController_0.Single_5;
			num4 = Mathf.CeilToInt(num5);
			if (num4 > 0)
			{
				armorMax.String_0 = string.Format("/{0}", num4);
			}
			else
			{
				armorMax.String_0 = string.Empty;
			}
			if (num5 < single_)
			{
				num5 = single_;
			}
			arProgressBar.Single_0 = single_ / ((!(num5 > 0f)) ? 1f : num5);
			arProgressBar.gameObject.SetActive(single_ != 0f);
			transform_0.GetChild(0).gameObject.SetActive(num == num3);
			transform_0.GetChild(0).GetComponent<Animation>().enabled = single_ == num5;
			if (num5 == 0f && single_ == 0f)
			{
				armorContainer.gameObject.SetActive(false);
			}
			else
			{
				armorContainer.gameObject.SetActive(true);
			}
		}
	}
}
