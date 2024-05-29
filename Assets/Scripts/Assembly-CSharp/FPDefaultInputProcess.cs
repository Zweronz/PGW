using UnityEngine;
using engine.unity;

public sealed class FPDefaultInputProcess : FPBaseProcess
{
	public override void Update(FPProcessSharedData fpprocessSharedData_0)
	{
		if (!fpprocessSharedData_0.bool_0)
		{
			Screen.lockCursor = true;
			fpprocessSharedData_0.vector2_1.x = InputManager.GetAxis("Mouse X");
			fpprocessSharedData_0.vector2_1.y = InputManager.GetAxis("Mouse Y");
			fpprocessSharedData_0.vector2_0.x = InputManager.GetAxis("Horizontal");
			fpprocessSharedData_0.vector2_0.y = InputManager.GetAxis("Vertical");
		}
	}
}
