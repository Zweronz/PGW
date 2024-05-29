using UnityEngine;

public class OnLikeClick : MonoBehaviour
{
	public BattleOverTableObject obj;

	public void OnClick()
	{
		obj.OnLikeClick();
	}
}
