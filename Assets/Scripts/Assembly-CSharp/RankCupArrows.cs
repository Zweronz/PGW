using UnityEngine;
using engine.unity;

public class RankCupArrows : MonoBehaviour
{
	public UIButton LeftArrow;

	public UIButton RightArrow;

	public UILabel CupDescription;

	public UILabel RankPeriod;

	public UILabel RankSeason;

	private Transform transform_0;

	private Transform transform_1;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private Vector3 vector3_2 = new Vector3(0f, 100f);

	private float float_0;

	private void Start()
	{
		transform_1 = base.transform;
		vector3_0 = new Vector3((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
		float_0 = ScreenController.ScreenController_0.Single_0;
		vector3_2 *= float_0;
	}

	private void Update()
	{
		if (!(transform_0 == null))
		{
			vector3_1 = Camera.main.WorldToScreenPoint(transform_0.position);
			vector3_1 -= vector3_0;
			vector3_1 *= float_0;
			vector3_1 += vector3_2;
			transform_1.localPosition = vector3_1;
		}
	}

	public void Init(Transform transform_2)
	{
		transform_0 = transform_2;
	}

	public void OnLeftArrowClick()
	{
		RankCupController.RankCupController_0.PrevCup();
	}

	public void OnRightArrowClick()
	{
		RankCupController.RankCupController_0.NextCup();
	}

	public void SetEnableButtons(bool bool_0)
	{
		if (LeftArrow != null)
		{
			LeftArrow.Boolean_0 = bool_0;
		}
		if (RightArrow != null)
		{
			RightArrow.Boolean_0 = bool_0;
		}
	}

	public void SetDescription(int int_0 = 0)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string string_ = string.Empty;
		ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(int_0);
		if (artikul != null)
		{
			if (!string.IsNullOrEmpty(artikul.String_1))
			{
				string text3 = Localizer.Get(artikul.String_1);
				string[] array = text3.Split('|');
				text = ((array.Length <= 0) ? text : array[0]);
				text2 = ((array.Length <= 1) ? text2 : array[1]);
			}
			int placeSeasonByCup = RankController.RankController_0.GetPlaceSeasonByCup(int_0);
			if (placeSeasonByCup != 0)
			{
				string_ = string.Format(Localizer.Get("ui.cup_change_window.place_season"), placeSeasonByCup);
			}
		}
		if (CupDescription != null)
		{
			CupDescription.String_0 = text;
		}
		if (RankPeriod != null)
		{
			RankPeriod.String_0 = text2;
		}
		if (RankSeason != null)
		{
			RankSeason.String_0 = string_;
		}
	}
}
