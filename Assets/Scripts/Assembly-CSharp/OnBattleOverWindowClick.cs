using UnityEngine;

public class OnBattleOverWindowClick : MonoBehaviour
{
	public BattleOverWindow obj;

	public void OnClick()
	{
		obj.OnMissClick();
	}
}
