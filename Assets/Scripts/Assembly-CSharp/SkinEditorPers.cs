using UnityEngine;

public class SkinEditorPers : MonoBehaviour
{
	public Transform previewPers;

	public Camera previewCamera;

	public float coeff = 0.5f;

	private GameObject gameObject_0;

	private bool bool_0;

	private bool bool_1;

	private void OnDrag(Vector2 vector2_0)
	{
		previewPers.Rotate(new Vector3(0f, (0f - vector2_0.x) * coeff, 0f));
	}

	private void OnDragStart()
	{
		bool_1 = true;
		bool_0 = false;
	}

	private void Update()
	{
	}

	private void OnPress(bool bool_2)
	{
		bool_1 = false;
		if (bool_2)
		{
			gameObject_0 = GameObjectOnTouch();
			bool_0 = true;
			TooltipController.TooltipController_0.HideAllContainers();
		}
		if (bool_2 || !bool_0 || !(gameObject_0 != null))
		{
			return;
		}
		foreach (string key in SkinEditorWindow.dictionary_1.Keys)
		{
			if (key.Equals(gameObject_0.name))
			{
				if (SkinEditController.SkinEditController_0.CanEdit())
				{
					SkinEditController.SkinEditController_0.EditPart(gameObject_0.name);
				}
				else
				{
					MessageWindow.Show(new MessageWindowParams(LocalizationStorage.Get.Term("window.skin_editor.select_skin.fail")));
				}
				break;
			}
		}
	}

	private GameObject GameObjectOnTouch()
	{
		LayerMask layerMask = ~(1 << LayerMask.NameToLayer("NGUIRoot"));
		RaycastHit hitInfo;
		if (Physics.Raycast(previewCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)), out hitInfo, 1000f, layerMask))
		{
			hitInfo.collider.gameObject.GetComponent<TooltipInfo>();
			return hitInfo.collider.gameObject;
		}
		return null;
	}

	public void Highlight(GameObject gameObject_1)
	{
		if (!(gameObject_1 == null))
		{
			MeshRenderer component = gameObject_1.GetComponent<MeshRenderer>();
			if (!(component == null))
			{
				Color color = component.materials[0].color;
				component.materials[0].color = new Color(color.r, color.g, color.b, 0.6f);
			}
		}
	}

	public void Unhighlight(GameObject gameObject_1)
	{
		if (!(gameObject_1 == null))
		{
			MeshRenderer component = gameObject_1.GetComponent<MeshRenderer>();
			if (!(component == null))
			{
				Color color = component.materials[0].color;
				component.materials[0].color = new Color(color.r, color.g, color.b, 1f);
			}
		}
	}
}
