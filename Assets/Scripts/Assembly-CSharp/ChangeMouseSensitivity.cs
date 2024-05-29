using UnityEngine;
using engine.helpers;

public class ChangeMouseSensitivity : MonoBehaviour
{
	public UILabel sensitivity;

	public UILabel sensitivityMin;

	public UILabel sensitivityMax;

	public UISlider sensitivitySlider;

	public UIInput sensitivityInput;

	public static readonly int int_0 = 1;

	public static readonly int int_1 = 100;

	private void Start()
	{
		sensitivityMin.String_0 = int_0.ToString();
		sensitivityMax.String_0 = int_1.ToString();
		float single_ = PresetGameSettings.PresetGameSettings_0.Single_0;
		float num = Mathf.Clamp(single_, int_0, int_1);
		float num2 = num - (float)int_0;
		sensitivitySlider.Single_0 = num2 / (float)(int_1 - int_0);
		sensitivity.String_0 = ((int)num).ToString();
		sensitivityInput.String_2 = ((int)num).ToString();
		sensitivityInput.onValidate = validate;
	}

	public void OnMouseSensitivityChange()
	{
		float num = sensitivitySlider.Single_0 * (float)(int_1 - int_0);
		float num2 = Mathf.Clamp(num + (float)int_0, int_0, int_1);
		PresetGameSettings.PresetGameSettings_0.Single_0 = num2;
		sensitivity.String_0 = ((int)num2).ToString();
		sensitivityInput.String_2 = ((int)num2).ToString();
		if (Application.isEditor)
		{
			Log.AddLine("SettingsWindow::OnMouseSensitivityChange > sens: " + num2);
		}
	}

	public void OnInputSubmit()
	{
		int result = 0;
		int.TryParse(sensitivityInput.String_2, out result);
		if (result > 0 && result <= 100)
		{
			PresetGameSettings.PresetGameSettings_0.Single_0 = result;
			sensitivitySlider.Single_0 = (float)result * 1f / (float)(int_1 - int_0 + 1);
		}
	}

	public void OnInputChange()
	{
		int result = 0;
		int.TryParse(sensitivityInput.String_2, out result);
		if (result > 0 && result <= 100)
		{
			PresetGameSettings.PresetGameSettings_0.Single_0 = result;
			sensitivitySlider.Single_0 = (float)result * 1f / (float)(int_1 - int_0 + 1);
		}
	}

	private char validate(string string_0, int int_2, char char_0)
	{
		string s = string.Format("{0}{1}", string_0, char_0);
		int result = 0;
		int.TryParse(s, out result);
		if (result > 0 && result <= 100)
		{
			return char_0;
		}
		return '\0';
	}
}
