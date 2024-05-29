using UnityEngine;
using engine.helpers;

public class PlayerTriggerController : MonoBehaviour
{
	public FirstPersonPlayerController firstPersonControl;

	private void OnTriggerEnter(Collider collider_0)
	{
		if (firstPersonControl.Boolean_0)
		{
			TriggerObject component = collider_0.gameObject.GetComponent<TriggerObject>();
			if (component != null)
			{
				Log.AddLine("PlayerTriggerController::OnTriggerEnter > collider: " + collider_0.gameObject.name);
				component.OnEnter(firstPersonControl);
			}
		}
	}

	private void OnTriggerExit(Collider collider_0)
	{
		if (firstPersonControl.Boolean_0)
		{
			TriggerObject component = collider_0.gameObject.GetComponent<TriggerObject>();
			if (component != null)
			{
				Log.AddLine("PlayerTriggerController::OnTriggerExit > collider: " + collider_0.gameObject.name);
				component.OnExit(firstPersonControl);
			}
		}
	}
}
