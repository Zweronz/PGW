using UnityEngine;

public class AddFrendsButtonInTableRangs : MonoBehaviour
{
	public int ID;

	private void OnPress(bool bool_0)
	{
		if (!bool_0)
		{
			base.gameObject.SetActive(false);
		}
	}
}
