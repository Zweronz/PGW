using UnityEngine;

public class UITooltip : MonoBehaviour
{
	protected static UITooltip uitooltip_0;

	public Camera uiCamera;

	public UILabel text;

	public UISprite background;

	public float appearSpeed = 10f;

	public bool scalingTransitions = true;

	protected Transform transform_0;

	protected float float_0;

	protected float float_1;

	protected Vector3 vector3_0;

	protected Vector3 vector3_1 = Vector3.zero;

	protected UIWidget[] uiwidget_0;

	public static bool Boolean_0
	{
		get
		{
			return uitooltip_0 != null && uitooltip_0.float_0 == 1f;
		}
	}

	private void Awake()
	{
		uitooltip_0 = this;
	}

	private void OnDestroy()
	{
		uitooltip_0 = null;
	}

	protected virtual void Start()
	{
		transform_0 = base.transform;
		uiwidget_0 = GetComponentsInChildren<UIWidget>();
		vector3_0 = transform_0.localPosition;
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		SetAlpha(0f);
	}

	protected virtual void Update()
	{
		if (float_1 != float_0)
		{
			float_1 = Mathf.Lerp(float_1, float_0, RealTime.Single_1 * appearSpeed);
			if (Mathf.Abs(float_1 - float_0) < 0.001f)
			{
				float_1 = float_0;
			}
			SetAlpha(float_1 * float_1);
			if (scalingTransitions)
			{
				Vector3 vector = vector3_1 * 0.25f;
				vector.y = 0f - vector.y;
				Vector3 localScale = Vector3.one * (1.5f - float_1 * 0.5f);
				Vector3 localPosition = Vector3.Lerp(vector3_0 - vector, vector3_0, float_1);
				transform_0.localPosition = localPosition;
				transform_0.localScale = localScale;
			}
		}
	}

	protected virtual void SetAlpha(float float_2)
	{
		int i = 0;
		for (int num = uiwidget_0.Length; i < num; i++)
		{
			UIWidget uIWidget = uiwidget_0[i];
			Color color_ = uIWidget.Color_0;
			color_.a = float_2;
			uIWidget.Color_0 = color_;
		}
	}

	protected virtual void SetText(string string_0)
	{
		if (text != null && !string.IsNullOrEmpty(string_0))
		{
			float_0 = 1f;
			text.String_0 = string_0;
			vector3_0 = Input.mousePosition;
			Transform transform = text.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			vector3_1 = text.Vector2_3;
			vector3_1.x *= localScale.x;
			vector3_1.y *= localScale.y;
			if (background != null)
			{
				Vector4 vector4_ = background.Vector4_3;
				vector3_1.x += vector4_.x + vector4_.z + (localPosition.x - vector4_.x) * 2f;
				vector3_1.y += vector4_.y + vector4_.w + (0f - localPosition.y - vector4_.y) * 2f;
				background.Int32_0 = Mathf.RoundToInt(vector3_1.x);
				background.Int32_1 = Mathf.RoundToInt(vector3_1.y);
			}
			if (uiCamera != null)
			{
				vector3_0.x = Mathf.Clamp01(vector3_0.x / (float)Screen.width);
				vector3_0.y = Mathf.Clamp01(vector3_0.y / (float)Screen.height);
				float num = uiCamera.orthographicSize / transform_0.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector = new Vector2(num2 * vector3_1.x / (float)Screen.width, num2 * vector3_1.y / (float)Screen.height);
				vector3_0.x = Mathf.Min(vector3_0.x, 1f - vector.x);
				vector3_0.y = Mathf.Max(vector3_0.y, vector.y);
				transform_0.position = uiCamera.ViewportToWorldPoint(vector3_0);
				vector3_0 = transform_0.localPosition;
				vector3_0.x = Mathf.Round(vector3_0.x);
				vector3_0.y = Mathf.Round(vector3_0.y);
				transform_0.localPosition = vector3_0;
			}
			else
			{
				if (vector3_0.x + vector3_1.x > (float)Screen.width)
				{
					vector3_0.x = (float)Screen.width - vector3_1.x;
				}
				if (vector3_0.y - vector3_1.y < 0f)
				{
					vector3_0.y = vector3_1.y;
				}
				vector3_0.x -= (float)Screen.width * 0.5f;
				vector3_0.y -= (float)Screen.height * 0.5f;
			}
		}
		else
		{
			float_0 = 0f;
		}
	}

	public static void ShowText(string string_0)
	{
		if (uitooltip_0 != null)
		{
			uitooltip_0.SetText(string_0);
		}
	}
}
