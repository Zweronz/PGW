using UnityEngine;

public class PreviewSkin : MonoBehaviour
{
	public Camera previewCamera;

	private Vector2 vector2_0;

	private bool bool_0;

	private GameObject gameObject_0;

	private float float_0 = 100f;

	private float float_1 = 120f;

	private Rect rect_0;

	private Vector3 vector3_0;

	private Vector3 vector3_1;

	private void Start()
	{
		rect_0 = new Rect(float_0, float_1, (float)Screen.width - float_0 * 2f, (float)Screen.height - float_1 * 2f);
	}

	private void Update()
	{
		if (!bool_0 && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)))
		{
			vector2_0 = ((Input.touchCount <= 0) ? new Vector2(Input.mousePosition.x, Input.mousePosition.y) : Input.GetTouch(0).position);
			if (rect_0.Contains(vector2_0))
			{
				bool_0 = true;
				gameObject_0 = GameObjectOnTouch(vector2_0);
				if (gameObject_0 != null)
				{
					Highlight(gameObject_0);
				}
			}
			return;
		}
		if (bool_0)
		{
			if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Moved)
			{
				Vector2 vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				if (vector.Equals(vector2_0))
				{
					goto IL_01ec;
				}
			}
			float num = ((Input.touchCount <= 0) ? (vector2_0.x - Input.mousePosition.x) : (vector2_0.x - Input.GetTouch(0).position.x));
			if (gameObject_0 != null && Mathf.Abs(num) > 2f)
			{
				Unhighlight(gameObject_0);
				gameObject_0 = null;
			}
			else
			{
				float num2 = 0.5f;
				base.transform.Rotate(0f, num2 * num, 0f, Space.Self);
				vector2_0 = ((Input.touchCount <= 0) ? new Vector2(Input.mousePosition.x, Input.mousePosition.y) : Input.GetTouch(0).position);
			}
		}
		goto IL_01ec;
		IL_01ec:
		if ((Input.touchCount <= 0 || (Input.GetTouch(0).phase != TouchPhase.Ended && Input.GetTouch(0).phase != TouchPhase.Canceled)) && !Input.GetMouseButtonUp(0))
		{
			return;
		}
		if (gameObject_0 != null)
		{
			ButtonClickSound.buttonClickSound_0.PlayClick();
			Unhighlight(gameObject_0);
			if (SkinEditorController.skinEditorController_0 != null)
			{
				SkinEditorController.skinEditorController_0.SelectPart(gameObject_0.name);
			}
			gameObject_0 = null;
		}
		bool_0 = false;
	}

	public GameObject GameObjectOnTouch(Vector2 vector2_1)
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(previewCamera.ScreenPointToRay(new Vector3(vector2_1.x, vector2_1.y, 0f)), out hitInfo))
		{
			return hitInfo.collider.gameObject;
		}
		return null;
	}

	public void Highlight(GameObject gameObject_1)
	{
		MeshRenderer component = gameObject_1.GetComponent<MeshRenderer>();
		if (!(component == null))
		{
			Color color = component.materials[0].color;
			component.materials[0].color = new Color(color.r, color.g, color.b, 0.6f);
		}
	}

	public void Unhighlight(GameObject gameObject_1)
	{
		MeshRenderer component = gameObject_1.GetComponent<MeshRenderer>();
		if (!(component == null))
		{
			Color color = component.materials[0].color;
			component.materials[0].color = new Color(color.r, color.g, color.b, 1f);
		}
	}

	private void OnEnable()
	{
		Debug.Log("OnEnabled()");
		bool_0 = false;
		gameObject_0 = null;
	}

	private void OnDisable()
	{
		Debug.Log("OnDisabled()");
		bool_0 = false;
		gameObject_0 = null;
	}
}
