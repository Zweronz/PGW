using UnityEngine;

public sealed class YesPresser : SkipTrainingButton
{
	public GameObject gameObject_0;

	private bool bool_0;

	protected override void OnClick()
	{
		if (!bool_0)
		{
			gameObject_0.GetComponent<UIButton>().enabled = false;
			base.enabled = false;
			GotToNextLevel.GoToNextLevel();
			bool_0 = true;
		}
	}
}
