using UnityEngine;

namespace engine.unity
{
	public class WindowFaderTouchBlocker : MonoBehaviour
	{
		private void OnClick()
		{
			BaseWindow baseWindow_ = WindowController.WindowController_0.BaseWindow_0;
			if (!(baseWindow_ != null) || baseWindow_.Parameters_0.bool_6)
			{
				WindowController.WindowController_0.HideActiveWindow();
			}
		}
	}
}
