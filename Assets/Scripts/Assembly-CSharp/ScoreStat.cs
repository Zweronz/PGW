using System.Collections;
using UnityEngine;

public class ScoreStat : MonoBehaviour
{
	private IEnumerator Start()
	{
		while (LevelCompleteWindow.LevelCompleteWindow_0 == null)
		{
			yield return new WaitForEndOfFrame();
		}
		while (!(LevelCompleteWindow.LevelCompleteWindow_0.WindowShowParameters_0 is LevelCompleteWindowParams))
		{
			yield return new WaitForEndOfFrame();
		}
		GetComponent<UILabel>().String_0 = string.Empty + (LevelCompleteWindow.LevelCompleteWindow_0.WindowShowParameters_0 as LevelCompleteWindowParams).int_0;
	}
}
