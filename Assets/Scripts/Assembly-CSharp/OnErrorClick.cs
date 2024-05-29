using UnityEngine;

public class OnErrorClick : MonoBehaviour
{
	public BattleOverTableObject obj;

	public void OnClick()
	{
		obj.OnErrorClick();
	}
}
