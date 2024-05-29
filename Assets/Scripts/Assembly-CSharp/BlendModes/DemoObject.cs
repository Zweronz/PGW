using UnityEngine;

namespace BlendModes
{
	public class DemoObject : MonoBehaviour
	{
		private BlendModeEffect blendModeEffect_0;

		private void Awake()
		{
			blendModeEffect_0 = GetComponent<BlendModeEffect>();
		}

		private void Update()
		{
			if (DemoBlendModePicker.blendMode_0 != blendModeEffect_0.BlendMode_0)
			{
				blendModeEffect_0.BlendMode_0 = DemoBlendModePicker.blendMode_0;
			}
		}
	}
}
