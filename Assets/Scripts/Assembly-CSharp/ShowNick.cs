using UnityEngine;

public class ShowNick : MonoBehaviour
{
	public string nick;

	public bool isShowNick;

	public GUIStyle labelStyle;

	private float float_0;

	private void Start()
	{
		float_0 = (float)Screen.height / 768f;
		labelStyle.fontSize = Mathf.RoundToInt(20f * float_0);
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
	}
}
