using UnityEngine;

public sealed class AddLabel : MonoBehaviour
{
	private bool bool_0;

	private bool bool_1;

	private void Start()
	{
		if (Defs.bool_5 || Defs.bool_4)
		{
			base.gameObject.SetActive(false);
		}
	}
}
