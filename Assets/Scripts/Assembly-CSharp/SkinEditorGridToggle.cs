using UnityEngine;

public class SkinEditorGridToggle : MonoBehaviour
{
	public UIToggle toggle;

	private UIPlaySound uiplaySound_0;

	private void Start()
	{
		toggle.Boolean_0 = SkinEditController.SkinEditController_0.Boolean_0;
		toggle.list_0.Add(new EventDelegate(OnGridToggleChanged));
		uiplaySound_0 = GetComponent<UIPlaySound>();
	}

	public void OnGridToggleChanged()
	{
		SkinEditController.SkinEditController_0.Boolean_0 = toggle.Boolean_0;
		SkinEditController.SkinEditController_0.Dispatch(0, SkinEditController.SkinEditorEvent.GRID_CHANGED);
		if (uiplaySound_0 != null && Defs.Boolean_0)
		{
			uiplaySound_0.Play();
		}
	}
}
