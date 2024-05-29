using UnityEngine;

public class ConsoleNamedKeyBugFix : MonoBehaviour
{
	private void OnGUI()
	{
		string nextControlName = base.gameObject.GetHashCode().ToString();
		GUI.SetNextControlName(nextControlName);
		Rect position = new Rect(0f, 0f, 0f, 0f);
		GUI.TextField(position, string.Empty, 0);
	}
}
