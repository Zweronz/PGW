using UnityEngine;

public class ChooseBoxItemOnClick : MonoBehaviour
{
	public UICenterOnChild myCenterOnChaild;

	private void OnClick()
	{
		if (myCenterOnChaild != null)
		{
			myCenterOnChaild.CenterOn(base.transform);
		}
	}
}
