using UnityEngine;
using pixelgun.tutorial;

[RequireComponent(typeof(UIWidget))]
public class WidgetRegisterInTutorial : MonoBehaviour
{
	public TUTORIAL_UI_IDS UIIDElementInTutorial;

	private void Start()
	{
		TutorialController.TutorialController_0.RegisterUIWidget(UIIDElementInTutorial, base.gameObject);
	}

	private void OnDestroy()
	{
		TutorialController.TutorialController_0.RegisterUIWidget(UIIDElementInTutorial, null);
	}
}
