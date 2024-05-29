using UnityEngine;

public class RePositionPlayersButton : MonoBehaviour
{
	public Vector3 positionInCommand;

	private void Start()
	{
		if (Defs.bool_5 || Defs.bool_6)
		{
			base.transform.localPosition = positionInCommand;
		}
	}
}
