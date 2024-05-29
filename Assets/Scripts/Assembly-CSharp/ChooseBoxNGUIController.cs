using System.Collections;
using Rilisoft;
using UnityEngine;

public class ChooseBoxNGUIController : MonoBehaviour
{
	public MultipleToggleButton difficultyToggle;

	public UIButton backButton;

	public UIButton startButton;

	public GameObject grid;

	public Transform ScrollTransform;

	public GameObject SelectMapPanel;

	public int selectIndexMap;

	public int countMap;

	public float widthCell = 824f;

	private IEnumerator Start()
	{
		ScrollTransform.GetComponent<UIPanel>().Vector4_1 = new Vector4(0f, 0f, 760 * Screen.width / Screen.height, 736f);
		countMap = grid.transform.childCount;
		yield return null;
		Defs.int_19 = Storager.GetInt(Defs.String_12);
		if (difficultyToggle != null)
		{
			difficultyToggle.buttons[Defs.int_19].Boolean_0 = true;
			difficultyToggle.Clicked += delegate(object sender, MultipleToggleEventArgs e)
			{
				ButtonClickSound.buttonClickSound_0.PlayClick();
				Storager.SetInt(Defs.String_12, e.Int32_0);
				Defs.int_19 = e.Int32_0;
				Storager.Save();
			};
		}
	}

	private void Update()
	{
		if (SelectMapPanel.activeInHierarchy)
		{
			if (ScrollTransform.localPosition.x > 0f)
			{
				selectIndexMap = Mathf.RoundToInt((ScrollTransform.localPosition.x - (float)Mathf.FloorToInt(ScrollTransform.localPosition.x / widthCell / (float)countMap) * widthCell * (float)countMap) / widthCell);
				selectIndexMap = countMap - selectIndexMap;
			}
			else
			{
				selectIndexMap = -1 * Mathf.RoundToInt((ScrollTransform.localPosition.x - (float)Mathf.CeilToInt(ScrollTransform.localPosition.x / widthCell / (float)countMap) * widthCell * (float)countMap) / widthCell);
			}
			if (selectIndexMap == countMap)
			{
				selectIndexMap = 0;
			}
		}
	}
}
