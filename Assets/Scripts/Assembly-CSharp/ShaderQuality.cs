using UnityEngine;

public class ShaderQuality : MonoBehaviour
{
	private int int_0 = 600;

	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (int_0 != num)
		{
			int_0 = num;
			Shader.globalMaximumLOD = int_0;
		}
	}
}
