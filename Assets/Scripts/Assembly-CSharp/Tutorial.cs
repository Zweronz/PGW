using System.Runtime.CompilerServices;
using UnityEngine;
using engine.unity;

public class Tutorial : MonoBehaviour
{
	public GameObject[] StepsUI;

	[CompilerGenerated]
	private static Tutorial tutorial_0;

	public static Tutorial Tutorial_0
	{
		[CompilerGenerated]
		get
		{
			return tutorial_0;
		}
		[CompilerGenerated]
		private set
		{
			tutorial_0 = value;
		}
	}

	public static void Show()
	{
		if (!(Tutorial_0 != null))
		{
			Tutorial_0 = ScreenController.ScreenController_0.LoadUI("Tutorial").GetComponent<Tutorial>();
			NGUITools.SetActive(Tutorial_0.gameObject, true);
		}
	}

	public static void Hide()
	{
		if (!(Tutorial_0 == null))
		{
			NGUITools.Destroy(Tutorial_0.gameObject);
			Tutorial_0 = null;
		}
	}

	private void Awake()
	{
		HideAllSteps();
	}

	public void HideAllSteps()
	{
		for (int i = 0; i < StepsUI.Length; i++)
		{
			NGUITools.SetActive(StepsUI[i], false);
		}
	}

	public void Step002()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[0], true);
	}

	public void Step002_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[17], true);
	}

	public void Step002_2()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[18], true);
	}

	public void Step002_3()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[19], true);
	}

	public void Step003()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[1], true);
	}

	public void Step004()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[2], true);
	}

	public void Step004_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[3], true);
	}

	public void Step005()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[4], true);
	}

	public void Step005_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[5], true);
	}

	public void Step005_1_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[20], true);
	}

	public void Step005_2()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[6], true);
	}

	public void Step006()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[7], true);
	}

	public void Step006_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[16], true);
	}

	public void Step007()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[8], true);
	}

	public void Step007_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[21], true);
	}

	public void Step008()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[9], true);
	}

	public void Step008_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[24], true);
	}

	public void Step008_2()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[25], true);
	}

	public void Step008_3()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[7], true);
	}

	public void Step009()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[10], true);
	}

	public void Step010()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[11], true);
	}

	public void Step011()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[12], true);
	}

	public void Step012()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[13], true);
	}

	public void Step012_1()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[14], true);
	}

	public void FinalStep()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[15], true);
	}

	public void PostFinalStep001()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[22], true);
	}

	public void PostFinalStep002()
	{
		HideAllSteps();
		NGUITools.SetActive(StepsUI[23], true);
	}
}
