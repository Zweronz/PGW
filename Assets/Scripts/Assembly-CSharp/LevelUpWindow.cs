using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using engine.unity;

[GameWindowParams(GameWindowType.LevelUp)]
public class LevelUpWindow : BaseGameWindow
{
	private static LevelUpWindow levelUpWindow_0;

	public LevelUpWithOffers levelUpPanel;

	public LevelUpWithOffers levelUpPanelEmpty;

	public LevelUpWithOffers levelUpPanelTier;

	public GameObject[] arrows;

	public GameObject[] shineNodes;

	private LevelUpWithOffers levelUpWithOffers_0;

	private GameObject[] gameObject_0;

	private int int_0 = -1;

	public static LevelUpWindow LevelUpWindow_0
	{
		get
		{
			return levelUpWindow_0;
		}
	}

	public static void Show(LevelUpWindowParams levelUpWindowParams_0)
	{
		if (!(levelUpWindow_0 != null))
		{
			levelUpWindow_0 = BaseWindow.Load("LevelUp") as LevelUpWindow;
			levelUpWindow_0.Parameters_0.type_0 = WindowQueue.Type.New;
			levelUpWindow_0.Parameters_0.bool_5 = false;
			levelUpWindow_0.Parameters_0.bool_0 = false;
			levelUpWindow_0.Parameters_0.bool_6 = false;
			levelUpWindow_0.InternalShow(levelUpWindowParams_0);
		}
	}

	public override void OnShow()
	{
		Init();
		base.OnShow();
	}

	public override void OnHide()
	{
		base.OnHide();
		levelUpWindow_0 = null;
	}

	private void Init()
	{
		LevelUpWindowParams levelUpWindowParams = base.WindowShowParameters_0 as LevelUpWindowParams;
		if (levelUpWindowParams == null)
		{
			return;
		}
		List<int> list = ((levelUpWindowParams.list_0 == null) ? getNewLevelArtikuls(levelUpWindowParams.int_0) : levelUpWindowParams.list_0);
		List<ArtikulData> list2 = new List<ArtikulData>();
		for (int i = 0; i < list.Count; i++)
		{
			ArtikulData artikul = ArtikulController.ArtikulController_0.GetArtikul(list[i]);
			if (artikul != null)
			{
				list2.Add(artikul);
			}
		}
		bool flag = LevelStorage.Get.IsLevelFirstOnTier(levelUpWindowParams.int_0);
		if (list2.Count > 0)
		{
			if (flag)
			{
				levelUpWithOffers_0 = levelUpPanelTier;
			}
			else
			{
				levelUpWithOffers_0 = levelUpPanel;
			}
		}
		else
		{
			levelUpWithOffers_0 = levelUpPanelEmpty;
		}
		NGUITools.SetActive(levelUpWithOffers_0.gameObject, true);
		levelUpWithOffers_0.Init(levelUpWindowParams.int_0, list2);
	}

	public void HandleContinueButton()
	{
		Hide();
	}

	public void HandleShopButtonFromTierPanel()
	{
		Hide();
		ShopWindow.Show();
	}

	private List<int> getNewLevelArtikuls(int int_1)
	{
		List<int> list = new List<int>();
		List<ShopArtikulData> list2 = ShopArtikulStorage.Get.Storage.Search(2, int_1);
		if (list2 != null && list2.Count > 0)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list2.Count; i++)
			{
				hashSet.Add(list2[i].Int32_1);
			}
			foreach (int item in hashSet)
			{
				list.Add(item);
			}
		}
		return list;
	}

	private void Start()
	{
		StartCoroutine(LoopBackgroundAnimation());
	}

	private IEnumerator LoopBackgroundAnimation()
	{
		GameObject gameObject = arrows[0];
		gameObject_0 = new GameObject[8];
		for (int i = 0; i < gameObject_0.Length; i++)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
			gameObject2.transform.parent = gameObject.transform.parent;
			gameObject_0[i] = gameObject2;
			yield return null;
		}
		for (int j = 0; j < arrows.Length; j++)
		{
			arrows[j].SetActive(false);
		}
		int_0 = -1;
		while (true)
		{
			for (int k = 0; k < shineNodes.Length; k++)
			{
				GameObject gameObject3 = shineNodes[k];
				if (gameObject3 != null && gameObject3.activeInHierarchy)
				{
					gameObject3.transform.Rotate(Vector3.forward, Time.deltaTime * 10f, Space.Self);
					if (k != int_0)
					{
						int_0 = k;
						ResetBackgroundArrows(arrows[k].transform);
					}
				}
			}
			for (int l = 0; l < gameObject_0.Length; l++)
			{
				Transform transform = gameObject_0[l].transform;
				float num = transform.localPosition.y + Time.deltaTime * 60f;
				if (num > 474f)
				{
					num -= 880f;
				}
				transform.localPosition = new Vector3(transform.localPosition.x, num, transform.localPosition.z);
			}
			yield return null;
		}
	}

	private void ResetBackgroundArrows(Transform transform_0)
	{
		for (int i = 0; i < gameObject_0.Length; i++)
		{
			Transform transform = gameObject_0[i].transform;
			transform.parent = transform_0.parent;
			transform.localScale = Vector3.one;
			transform.localPosition = new Vector3(transform_0.localPosition.x + ((i % 2 != 1) ? 0f : 90f), transform_0.localPosition.y - 110f * (float)i, transform_0.localPosition.z);
			transform.localRotation = transform_0.localRotation;
		}
	}
}
