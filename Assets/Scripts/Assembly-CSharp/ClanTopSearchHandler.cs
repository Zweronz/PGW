using UnityEngine;

public class ClanTopSearchHandler : MonoBehaviour
{
	public UIInput input;

	public UILabel stub;

	private void Update()
	{
		NGUITools.SetActive(stub.gameObject, !input.Boolean_2 && string.IsNullOrEmpty(input.String_2));
	}
}
