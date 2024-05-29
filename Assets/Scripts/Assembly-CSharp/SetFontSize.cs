using System.Reflection;
using UnityEngine;

public class SetFontSize : MonoBehaviour
{
	private UILabel uilabel_0;

	private void Start()
	{
		uilabel_0 = GetComponent<UILabel>();
		UpdateFontSize();
	}

	[Obfuscation(Exclude = true)]
	private void UpdateFontSize()
	{
		if (uilabel_0 != null)
		{
			uilabel_0.Int32_5 = uilabel_0.Int32_1;
		}
	}

	private void OnEnable()
	{
		Invoke("UpdateFontSize", 0.05f);
	}

	private void Update()
	{
		if (uilabel_0.Int32_5 != uilabel_0.Int32_1)
		{
			uilabel_0.Int32_5 = uilabel_0.Int32_1;
		}
	}
}
