using UnityEngine;

public class RankTrophyView : MonoBehaviour
{
	public UISprite rankIcon;

	public UISprite shieldIcon;

	public UILabel rankValue;

	public UISprite bulletTemplate;

	public UIWidget bulletsConatiner;

	public float BulletScale = 1f;

	private int int_0;

	private int int_1 = -1;

	private bool bool_0;

	private bool bool_1 = true;

	private int int_2 = 5;

	private void Start()
	{
		Init();
		int_2 = CommonParamsSettings.Get.MaxBulletInRow;
		UsersData.Subscribe(UsersData.EventType.RANK_LEVEL_UP, OnRankChanged);
		UsersData.Subscribe(UsersData.EventType.RANK_LEVEL_DOWN, OnRankChanged);
		UsersData.Subscribe(UsersData.EventType.RANK_MEDALS_CHANGED, OnRankChanged);
		RankController.RankController_0.Subscribe(OnSeasonChanged, RankController.EventType.UPDATE_SEASON);
	}

	private void OnDestroy()
	{
		UsersData.Unsubscribe(UsersData.EventType.RANK_LEVEL_UP, OnRankChanged);
		UsersData.Unsubscribe(UsersData.EventType.RANK_LEVEL_DOWN, OnRankChanged);
		UsersData.Unsubscribe(UsersData.EventType.RANK_MEDALS_CHANGED, OnRankChanged);
		RankController.RankController_0.Unsubscribe(OnSeasonChanged, RankController.EventType.UPDATE_SEASON);
	}

	private void OnRankChanged(UsersData.EventData eventData_0)
	{
		int_1 = -1;
		int_0 = 0;
		Init();
	}

	private void OnSeasonChanged(RankController.EventData eventData_0)
	{
		int_1 = -1;
		int_0 = 0;
		Init();
	}

	private void Init()
	{
		InitRank();
		InitIcon();
		InitBullets();
		UIButton component = GetComponent<UIButton>();
		if (component != null)
		{
			component.enabled = bool_0;
		}
	}

	public void SetData(int int_3, int int_4, bool bool_2, bool bool_3)
	{
		int_0 = int_3;
		int_1 = int_4;
		bool_0 = bool_2;
		bool_1 = bool_3;
	}

	private void InitRank()
	{
		int num = ((int_0 <= 0) ? RankController.RankController_0.Int32_0 : int_0);
		rankValue.String_0 = num.ToString();
	}

	private void InitIcon()
	{
		int num = ((int_0 <= 0) ? RankController.RankController_0.Int32_0 : int_0);
		RankLevelData rankLevelData = RankController.RankController_0.GetRankLevelData(num);
		if (rankLevelData != null)
		{
			rankIcon.String_0 = rankLevelData.String_2;
			shieldIcon.String_0 = rankLevelData.String_3;
		}
	}

	private void InitBullets()
	{
		NGUITools.SetActive(bulletTemplate.gameObject, false);
		if (!bool_1)
		{
			NGUITools.SetActive(bulletsConatiner.gameObject, false);
			return;
		}
		NGUITools.SetActive(bulletsConatiner.gameObject, true);
		while (bulletsConatiner.transform.childCount > 0)
		{
			Transform child = bulletsConatiner.transform.GetChild(0);
			child.parent = null;
			Object.Destroy(child.gameObject);
		}
		int int32_ = bulletTemplate.Int32_0;
		int int32_2 = bulletTemplate.Int32_1;
		int int32_3 = bulletsConatiner.Int32_0;
		int int32_4 = bulletsConatiner.Int32_1;
		int num = ((int_1 <= -1) ? RankController.RankController_0.Int32_1 : int_1);
		int num2 = ((int_0 <= 0) ? RankController.RankController_0.Int32_0 : int_0);
		int medalsForNextRank = RankController.RankController_0.GetMedalsForNextRank(num2);
		int num3 = Mathf.CeilToInt((float)medalsForNextRank / (float)int_2);
		float num4 = 0f;
		float num5 = 0f;
		if (bulletsConatiner.Pivot_1 != UIWidget.Pivot.Top && bulletsConatiner.Pivot_1 != 0 && bulletsConatiner.Pivot_1 != UIWidget.Pivot.TopRight)
		{
			if (bulletsConatiner.Pivot_1 == UIWidget.Pivot.Bottom || bulletsConatiner.Pivot_1 == UIWidget.Pivot.BottomLeft || bulletsConatiner.Pivot_1 == UIWidget.Pivot.BottomRight)
			{
				num5 += (float)(int32_4 / 2);
			}
		}
		else
		{
			num5 -= (float)(int32_4 / 2);
		}
		int num6 = 0;
		for (int i = 0; i < num3; i++)
		{
			int num7 = Mathf.Min(medalsForNextRank - num6, int_2);
			num4 = (float)(int32_3 - int32_ * (num7 - 1)) * 0.5f;
			if (bulletsConatiner.Pivot_1 != UIWidget.Pivot.Top && bulletsConatiner.Pivot_1 != UIWidget.Pivot.Center && bulletsConatiner.Pivot_1 != UIWidget.Pivot.Bottom)
			{
				if (bulletsConatiner.Pivot_1 == UIWidget.Pivot.Right || bulletsConatiner.Pivot_1 == UIWidget.Pivot.BottomRight || bulletsConatiner.Pivot_1 == UIWidget.Pivot.TopRight)
				{
					num4 -= (float)int32_3;
				}
			}
			else
			{
				num4 -= (float)(int32_3 / 2);
			}
			for (int j = 0; j < num7; j++)
			{
				GameObject gameObject = NGUITools.AddChild(bulletsConatiner.gameObject, bulletTemplate.gameObject);
				gameObject.name = string.Format("bullet_{0}", num6);
				Vector3 localPosition = new Vector3(num4, num5);
				gameObject.transform.localPosition = localPosition;
				UISprite component = gameObject.GetComponent<UISprite>();
				component.String_0 = string.Format("bullet_{0}", (num6 >= num) ? "inactive" : "active");
				num4 += (float)int32_;
				NGUITools.SetActive(gameObject, true);
				num6++;
			}
			num5 -= (float)int32_2;
		}
		if (BulletScale != 1f)
		{
			if (num3 > 1)
			{
				bulletsConatiner.transform.localScale = new Vector3(BulletScale, BulletScale, BulletScale);
			}
			else
			{
				bulletsConatiner.transform.localScale = new Vector3(1f, 1f, 1f);
			}
		}
	}

	private void OnClick()
	{
		if (bool_0)
		{
			RankInfoWindow.Show();
		}
	}
}
