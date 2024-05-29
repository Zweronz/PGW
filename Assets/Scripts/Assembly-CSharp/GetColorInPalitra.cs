using UnityEngine;

public class GetColorInPalitra : MonoBehaviour
{
	public UITexture canvasTexture;

	private bool bool_0;

	public UISprite newColor;

	public UIButton okColorInPalitraButton;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			bool_0 = false;
		}
		if ((Input.touchCount <= 0 || Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) && !bool_0 && !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Vector2 vector2_ = ((Input.touchCount <= 0) ? new Vector2(Input.mousePosition.x, Input.mousePosition.y) : new Vector2(Input.touches[0].position.x, Input.touches[0].position.y));
		if (IsCanvasConteinPosition(vector2_))
		{
			if (Input.GetMouseButtonDown(0))
			{
				bool_0 = true;
			}
			Vector2 editPixelPos = GetEditPixelPos(vector2_);
			Color pixel = ((Texture2D)canvasTexture.Texture_0).GetPixel(Mathf.RoundToInt(editPixelPos.x), Mathf.RoundToInt(editPixelPos.y));
			newColor.Color_0 = pixel;
			okColorInPalitraButton.Color_0 = pixel;
			okColorInPalitraButton.color_1 = pixel;
			okColorInPalitraButton.color_0 = pixel;
		}
	}

	private bool IsCanvasConteinPosition(Vector2 vector2_0)
	{
		float num = (float)Screen.height / 768f;
		Vector2 vector = new Vector2(((float)Screen.width - num * (float)canvasTexture.Int32_0) * 0.5f, ((float)Screen.height + num * (float)canvasTexture.Int32_1) * 0.5f);
		Vector2 vector2 = new Vector2(((float)Screen.width + num * (float)canvasTexture.Int32_0) * 0.5f, ((float)Screen.height - num * (float)canvasTexture.Int32_1) * 0.5f);
		if (vector2_0.x > vector.x && vector2_0.x < vector2.x && vector2_0.y < vector.y && vector2_0.y > vector2.y)
		{
			return true;
		}
		return false;
	}

	private Vector2 GetEditPixelPos(Vector2 vector2_0)
	{
		float num = (float)Screen.height / 768f;
		return new Vector2(Mathf.FloorToInt((vector2_0.x - ((float)Screen.width - (float)canvasTexture.Int32_0 * num) * 0.5f) / ((float)canvasTexture.Int32_0 * num) * (float)canvasTexture.Texture_0.width), Mathf.FloorToInt((vector2_0.y - ((float)Screen.height - (float)canvasTexture.Int32_1 * num) * 0.5f) / ((float)canvasTexture.Int32_1 * num) * (float)canvasTexture.Texture_0.height));
	}
}
