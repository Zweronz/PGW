using UnityEngine;

public class OnNameClick : MonoBehaviour
{
	public BattleOverTableObject obj;

	public void OnClick()
	{
		obj.OnNickClick();
	}
}
