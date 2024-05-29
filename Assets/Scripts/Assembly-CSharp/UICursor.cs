using UnityEngine;

[RequireComponent(typeof(UISprite))]
public class UICursor : MonoBehaviour
{
	public static UICursor uicursor_0;

	public Camera uiCamera;

	private Transform transform_0;

	private UISprite uisprite_0;

	private UIAtlas uiatlas_0;

	private string string_0;

	private void Awake()
	{
		uicursor_0 = this;
	}

	private void OnDestroy()
	{
		uicursor_0 = null;
	}

	private void Start()
	{
		transform_0 = base.transform;
		uisprite_0 = GetComponentInChildren<UISprite>();
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (uisprite_0 != null)
		{
			uiatlas_0 = uisprite_0.UIAtlas_0;
			string_0 = uisprite_0.String_0;
			if (uisprite_0.Int32_2 < 100)
			{
				uisprite_0.Int32_2 = 100;
			}
		}
	}

	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			transform_0.position = uiCamera.ViewportToWorldPoint(mousePosition);
			if (uiCamera.orthographic)
			{
				Vector3 localPosition = transform_0.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				transform_0.localPosition = localPosition;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			transform_0.localPosition = mousePosition;
		}
	}

	public static void Clear()
	{
		if (uicursor_0 != null && uicursor_0.uisprite_0 != null)
		{
			Set(uicursor_0.uiatlas_0, uicursor_0.string_0);
		}
	}

	public static void Set(UIAtlas uiatlas_1, string string_1)
	{
		if (uicursor_0 != null && (bool)uicursor_0.uisprite_0)
		{
			uicursor_0.uisprite_0.UIAtlas_0 = uiatlas_1;
			uicursor_0.uisprite_0.String_0 = string_1;
			uicursor_0.uisprite_0.MakePixelPerfect();
			uicursor_0.Update();
		}
	}
}
